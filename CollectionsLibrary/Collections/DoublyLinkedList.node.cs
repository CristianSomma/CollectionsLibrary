using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Collections
{
    public partial class DoublyLinkedList<T>
    {
        private class Node
        {
            private Node? _prev, _next;
            private T _item;

            public Node(T item, Node? previous = null, Node? next = null)
            {
                Item = item;
                Prev = previous;
                Next = next;
            }

            public T Item
            {
                get => _item;
                set => _item = value;
            }

            public Node? Prev
            {
                get => _prev;
                set => _prev = value;
            }

            public Node? Next
            {
                get => _next;
                set => _next = value;
            }
        }
    }
}
