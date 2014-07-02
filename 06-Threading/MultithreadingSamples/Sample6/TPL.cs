using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sample6
{
    public class TPL
    {
        [Test]
        public void TestStatuses()
        {
            var task = new Task(() => Console.WriteLine("Task1 running..."));
            Console.WriteLine("Task1 status: {0}", task.Status);
            task.Start();
            Console.WriteLine("Task1 status: {0}", task.Status);
            task.Wait();
            Console.WriteLine("Task1 status: {0}", task.Status);
        }

        [Test]
        public void TestSynchronousRun()
        {
            var task = new Task(() => Console.WriteLine("Task1 running..."));
            Console.WriteLine("Task1 status: {0}", task.Status);
            task.RunSynchronously();
            Console.WriteLine("Task1 status: {0}", task.Status);
        }

        [Test]
        public void TestWaitAllTasks()
        {
            var firstTask = new Task(() =>
            {
                Console.WriteLine("Task 0 starting...");
                Thread.SpinWait(10000000);
                Console.WriteLine("Task 0 finishing...");
            });
            var secondTask = new Task(() =>
            {
                Console.WriteLine("Task 1 starting...");
                Thread.SpinWait(1000000000);
                Console.WriteLine("Task 1 finishing...");
            });

            firstTask.Start();
            secondTask.Start();
            var finishedTaskId = Task.WaitAny(firstTask, secondTask);
            Console.WriteLine("Task {0} finished", finishedTaskId);

            Task.WaitAll(firstTask, secondTask);
            Console.WriteLine("All tasks finished");
        }


        [Test]
        public void TestParent()
        {
            var parent = Task.Factory.StartNew(() =>
                                               {
                                                   Console.WriteLine("Outer task executing.");

                                                   Task.Factory.StartNew(() =>
                                                                         {
                                                                             Console.WriteLine("Nested task starting.");
                                                                             Thread.SpinWait(500000);
                                                                             Console.WriteLine("Nested task completing.");
                                                                         }, TaskCreationOptions.AttachedToParent);
                                               });

            parent.Wait();
            Console.WriteLine("Outer has completed.");
        }

        [Test]
        public void TestContinueWith()
        {
            var tasksChain = Task.Run(() => Console.WriteLine("Asking Deep Thought..."))
                                 .ContinueWith(previousTask => Thread.SpinWait(500000000))
                                 .ContinueWith(previousTask => Console.WriteLine("Processing is done!"))
                                 .ContinueWith(previousTask => 42)
                                 .ContinueWith(
                                     previousTask =>
                                     Console.WriteLine(
                                         "Answer to the Ultimate Question of Life, the Universe, and Everything is {0}",
                                         previousTask.Result));

            tasksChain.Wait();
        }

        [Test]
        public void TestExceptionSimple()
        {
            var task = Task.Factory.StartNew(() =>
            {
                throw new Exception("I failed, sorry!");
            });

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine(e);
                }
            }
        }

        [Test]
        public void TestSimpleCancel()
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var task = Task.Factory.StartNew(() =>
                                             {
                                                 token.ThrowIfCancellationRequested();

                                                 while (true)
                                                 {
                                                     if (token.IsCancellationRequested)
                                                     {
                                                         Console.WriteLine("Some clean up!");
                                                         token.ThrowIfCancellationRequested();
                                                     }
                                                 }
                                             }, tokenSource.Token);

            Thread.Sleep(100);
            tokenSource.Cancel();

            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(v.Message);
            }
            Console.WriteLine(task.Status);
        }
    }
}
