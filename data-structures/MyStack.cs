using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_structures
{
    internal class MyStack<T>
    {
        internal MyLinkedList<T> stack = new MyLinkedList<T>();

        public void Push(T value)
        {
            stack.AddFirst(value);
        }

        public T Pop()
        {
            T removedValue = stack.First.Value;
            stack.RemoveFirst();
            return removedValue;
        }
    }
}
