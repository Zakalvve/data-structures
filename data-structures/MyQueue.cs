using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures
{
    internal class MyQueue<T>
    {
        internal MyLinkedList<T> queue = new MyLinkedList<T>();

        public void Enqueue(T value)
        {
            queue.AddLast(value);
        }

        public T Dequeue()
        {
            T removedValue = queue.First.Value;
            queue.RemoveFirst();
            return removedValue;
        }
    }
}
