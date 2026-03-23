using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary.Collections
{
    public partial class BinaryTree<T>
    {
        public class Node
        {
            private T _item;
            private Node? _leftChild, _rightChild;

            internal Node(T item, Node? leftChild = null, Node? rightChild = null)
            {
                _item = item;
                _leftChild = leftChild;
                _rightChild = rightChild;
            }

            public T Item
            {
                get => _item;
                set => _item = value;
            }

            public Node? LeftChild
            {
                get => _leftChild;
                set => _leftChild = value;
            }

            public Node? RightChild
            {
                get => _rightChild;
                set => _rightChild = value;
            }
        }
    }
}