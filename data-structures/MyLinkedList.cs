

namespace data_structures
{
#nullable disable
    internal class MyLinkedList<T>
    {
        internal int count;
        internal MyLinkedListNode<T> head;
        public MyLinkedList() { }
        public int Count
        {
            get { return count; }
        }
        public MyLinkedListNode<T> First
        {
            get
            {
                return head;
            }
        }
        public MyLinkedListNode<T> Last
        {
            get
            {
                if (head == null) return null;
                MyLinkedListNode<T> current = head;
                while(current.next != null)
                {
                    current = current.next;
                }
                return current;
            }
        }
        public void AddFirst(T value)
        {
            
            AddFirst(CreateNode(value));
        }
        public void AddFirst(MyLinkedListNode<T> node)
        {
            if (head == null)
            {
                head = node;
            } else
            {
                node.next = head;
                head = node;
                
            }
            count++;
        }
        public void AddLast(T value)
        {
            AddLast(CreateNode(value));
        }
        public void AddLast(MyLinkedListNode<T> node)
        {
            if (head == null)
            {
                head = node;
            }
            else
            {
                Last.next = node;
            }
            count++;
        }
        public void RemoveFirst()
        {
            if (head == null) return;
            head = head.next;
            count--;
        }
        public void RemoveLast()
        {
            if (head == null || head.list != this) return;
            if (head.next != null)
            {
                MyLinkedListNode<T> current = head;
                while (current.next.next != null)
                {
                    current = current.next;
                }
                current.next = null;
                count--;
            }
            else RemoveFirst();
        }
        internal MyLinkedListNode<T> CreateNode(T value)
        {
            MyLinkedListNode<T> newNode = new MyLinkedListNode<T>(value);
            newNode.list = this;
            return newNode;
        }
    }
#nullable enable
}
