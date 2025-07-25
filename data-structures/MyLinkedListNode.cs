namespace data_structures
{
#nullable disable
    public sealed class MyLinkedListNode<T>
    {
        public T Value { get; set; }
        public MyLinkedListNode<T> Next { get { return next; }}
        internal MyLinkedListNode<T> next;
        internal MyLinkedList<T> list;
        public MyLinkedListNode(T value)
        {
            Value = value;

        }
        internal void Clear()
        {
            next = null;
        }
    }
#nullable enable
}
