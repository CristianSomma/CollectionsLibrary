using System.Collections;

namespace CollectionsLibrary.Collections
{
    public partial class BinaryTree<T> : IEnumerable<T>
    {
        private Node? _root;

        public BinaryTree()
        {
            _root = null;
        }

        public BinaryTree(T rootItem)
        {
            _root = new Node(rootItem);
        }

        public BinaryTree(Node root)
        {
            _root = root;
        }

        public bool IsEmpty()
        {
            return _root is null;
        }

        public Node? GetRoot()
        {
            return _root;
        }

        public int Grade(Node node)
        {
            int count = 0;

            if (node.LeftChild is not null)
                count++;

            if (node.RightChild is not null)
                count++;

            return count;
        }

        public Node AddSubtree(BinaryTree<T> subtree, Node rootParent)
        {
            if (subtree._root is null)
                throw new InvalidOperationException("The subtree is empty.");

            if (rootParent.LeftChild is null)
                rootParent.LeftChild = subtree._root;
            else if (rootParent.RightChild is null)
                rootParent.RightChild = subtree._root;
            else
                throw new InvalidOperationException("The node already has two children.");

            return subtree._root;
        }

        public BinaryTree<T> RemoveSubtreeOf(Node node)
        {
            Node parent = GetParent(node)
                ?? throw new InvalidOperationException("The node has no parent.");

            if (parent.LeftChild == node)
                parent.LeftChild = null;
            else
                parent.RightChild = null;

            return new BinaryTree<T>(node);
        }

        public IEnumerable<Node> GetChildren(Node parent)
        {
            if (parent.LeftChild is not null)
                yield return parent.LeftChild;

            if (parent.RightChild is not null)
                yield return parent.RightChild;
        }

        public int CountNodes() { return CountNodes(_root); }

        private int CountNodes(Node? currentNode)
        {
            if (currentNode is null)
                return 0;

            return 1 + CountNodes(currentNode.LeftChild) + CountNodes(currentNode.RightChild);
        }

        public Node? GetParent(Node node) { return FindParent(_root, node); }

        private Node? FindParent(Node? currentNode, Node target)
        {
            if (currentNode is null)
                return null;

            if (currentNode.LeftChild == target || currentNode.RightChild == target)
                return currentNode;

            return FindParent(currentNode.LeftChild, target)
                ?? FindParent(currentNode.RightChild, target);
        }

        public void InsertLast(T item)
        {
            Node newNode = new Node(item);

            if (IsEmpty())
            {
                _root = newNode;
                return;
            }

            Queue<Node> nodesToVisit = new Queue<Node>();
            nodesToVisit.Enqueue(_root!);

            while (nodesToVisit.GetSize() > 0)
            {
                Node currentNode = nodesToVisit.Dequeue();

                if (currentNode.LeftChild is null)
                {
                    currentNode.LeftChild = newNode;
                    return;
                }
                nodesToVisit.Enqueue(currentNode.LeftChild);

                if (currentNode.RightChild is null)
                {
                    currentNode.RightChild = newNode;
                    return;
                }
                nodesToVisit.Enqueue(currentNode.RightChild);
            }
        }

        public T RemoveLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty tree.");

            if (_root!.LeftChild is null && _root.RightChild is null)
            {
                T rootItem = _root.Item;
                _root = null;
                return rootItem;
            }

            Node lastNodeParent = _root;
            Node lastNode = _root;

            Queue<Node> nodesToVisit = new Queue<Node>();
            nodesToVisit.Enqueue(_root);

            while (nodesToVisit.GetSize() > 0)
            {
                Node currentNode = nodesToVisit.Dequeue();

                if (currentNode.LeftChild is not null)
                {
                    lastNodeParent = currentNode;
                    lastNode = currentNode.LeftChild;
                    nodesToVisit.Enqueue(lastNode);
                }

                if (currentNode.RightChild is not null)
                {
                    lastNodeParent = currentNode;
                    lastNode = currentNode.RightChild;
                    nodesToVisit.Enqueue(lastNode);
                }
            }

            T lastNodeItem = lastNode.Item;

            if (lastNodeParent.RightChild == lastNode)
                lastNodeParent.RightChild = null;
            else
                lastNodeParent.LeftChild = null;

            return lastNodeItem;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty())
                yield break;

            Queue<Node> nodesToVisit = new Queue<Node>();
            nodesToVisit.Enqueue(_root!);

            while (nodesToVisit.GetSize() > 0)
            {
                Node currentNode = nodesToVisit.Dequeue();
                yield return currentNode.Item;

                if (currentNode.LeftChild is not null)
                    nodesToVisit.Enqueue(currentNode.LeftChild);

                if (currentNode.RightChild is not null)
                    nodesToVisit.Enqueue(currentNode.RightChild);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}