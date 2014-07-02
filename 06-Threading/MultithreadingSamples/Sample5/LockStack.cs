using System;

namespace Sample5
{
    public class LockStack<TValue> : IStack<TValue>
    {
        private class Node
        {
            public TValue value;
            public Node next;
        }

        private readonly object lockObj = new object();
        private Node head;

        public void Push(TValue value)
        {
            var newNode = new Node {value = value};

            lock (lockObj)
            {
                newNode.next = head;
                head = newNode;
            }
        }

        public TValue Pop()
        {
            Node top;
            lock (lockObj)
            {
                top = head;
                if (top == null)
                    throw new Exception("Empty stack!");
                head = head.next;
            }
            
            return top.value;
        }

        public TValue Peek()
        {
            var top = head;
            if (top == null)
                throw new Exception("Empty stack!");
            return top.value;
        }
    }
}