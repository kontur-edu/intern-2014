using System.Collections.Generic;

namespace Sample5
{
    public class LockWrapperStack<TValue> : IStack<TValue>
    {
        private readonly Stack<TValue> stack = new Stack<TValue>(); 

        public void Push(TValue value)
        {
            lock (stack)
                stack.Push(value);
        }

        public TValue Peek()
        {
            lock (stack)
                return stack.Peek();
        }

        public TValue Pop()
        {
            lock (stack)
                return stack.Pop();
        }
    }
}