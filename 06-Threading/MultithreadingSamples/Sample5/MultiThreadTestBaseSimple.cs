using System;
using System.Threading;

namespace Sample5
{
    public class MultiThreadTestBaseSimple
    {
        public void Process(Action<int> action, int tasksCount)
        {
            using (var ev = new CountdownEvent(tasksCount))
            {
                for (int i = 0; i < tasksCount; i++)
                    ThreadPool.QueueUserWorkItem(state =>
                                                 {
                                                     action(i);
                                                     ev.Signal();
                                                 });

                ev.Wait();
            }
        }
    }
}