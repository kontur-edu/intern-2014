using System;
using System.Collections.Concurrent;

namespace Sample5
{
    public class WrapperStack<TValue> : IStack<TValue>
    {
        private readonly ConcurrentStack<TValue> stack = new ConcurrentStack<TValue>();

        public void Push(TValue value)
        {
            stack.Push(value);
        }

        public TValue Pop()
        {
            TValue result;
            if (!stack.TryPop(out result))
                throw new Exception("Empty stack!");

            return result;
        }

        public TValue Peek()
        {
            TValue result;
            if (!stack.TryPeek(out result))
                throw new Exception("Empty stack!");

            return result;
        }
    }
}