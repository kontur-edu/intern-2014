using System;
using System.Threading;

namespace Sample5
{
    public class LockFreeStack<TValue> : IStack<TValue>
    {
        private class Node
        {
            public TValue value;
            public Node next;
        }

        private Node head;

        public void Push(TValue value)
        {
            var newNode = new Node {value = value};

            Node currentHead;
            do
            {
                currentHead = head;
                newNode.next = currentHead;
            } while (Interlocked.CompareExchange(ref head, newNode, currentHead) != currentHead);
        }

        public TValue Pop()
        {
            Node currentHead, result;
            do
            {
                currentHead = head;
                result = currentHead;
            } while (currentHead != null
                && Interlocked.CompareExchange(ref head, head.next, currentHead) != currentHead);
            if (result == null)
            {
                throw new InvalidOperationException("try to pop empty stack!");
            }
            return result.value;
        }

        public TValue Peek()
        {
            Node currentHead;
            do
            {
                currentHead = head;
            } while (currentHead != head);
            return currentHead.value;
        }
    }
}