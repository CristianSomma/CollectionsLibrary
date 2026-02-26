namespace CollectionsLibrary.Collections
{
    public class Node<T>
    {
        private T _item;
        private Node<T>? _next;

        public Node(T item, Node<T>? next = null)
        {
            Item = item;
            Next = next;
        }

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
