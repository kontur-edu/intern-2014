using System;
using NUnit.Framework;

namespace Sample5
{
    public class StackTest : MultiThreadTestBaseSimple
    {
        [Test]
        public void TestPushAndPop()
        {
            const int tasksNumber = 16;
            var actionFactory = new Func<IStack<int>, Action<int>>(stack => threadNumber =>
                                                                            {
                                                                                var acc = 0;

                                                                                for (int i = 0; i < 1000000; i++)
                                                                                {
                                                                                    stack.Push(threadNumber);
                                                                                    acc += stack.Pop();
                                                                                }
                                                                            });

            Process(actionFactory(new WrapperStack<int>()), tasksNumber);
            Process(actionFactory(new LockFreeStack<int>()), tasksNumber);
            Process(actionFactory(new LockStack<int>()), tasksNumber);
            Process(actionFactory(new LockWrapperStack<int>()), tasksNumber);            
        }
    }
}