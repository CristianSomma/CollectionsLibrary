namespace CollectionsLibrary.Collections
{
    internal class Node<T>
    {
        private T _item;
        private Node<T>? _next;

#pragma warning disable CS8618
        public Node(T item, Node<T>? next = null)
        {
            Item = item;
            Next = next;
        }
#pragma warning restore CS8618

        public T Item
        {
            get => _item;
            set => _item = value;
        }

        public Node<T>? Next
        {
            get => _next;
            set => _next = value;
        }
    }
}
