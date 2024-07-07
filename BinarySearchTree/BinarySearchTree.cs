using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BinarySearchTree
{
    public class BinarySearchTree<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public int NodeCount { get; private set; }

        private BinarySearchTreeNode<T>? _root;

        public enum Traversal 
        {
            Preorder,
            Inorder,
            Postorder
        }

        public BinarySearchTree(T value)
        {
            BinarySearchTreeNode<T> node = new BinarySearchTreeNode<T>(value);
            _root = node;
            NodeCount++;
        }

        public void Add(T value) 
        {
            if (_root is null)
            {
                _root = new BinarySearchTreeNode<T>(value);
            }
            else
            {
                AddUtil(_root, value);
            }
        }

        public void Remove(T value)
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            RemoveUtil(_root, value);
        }

        public bool Contains(T value)
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            BinarySearchTreeNode<T>? current = _root;

            while (current is not null)
            {
                if (value.CompareTo(current.Value) == 0)
                {
                    return true;
                }
                else if (value.CompareTo(current.Value) == -1)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return false;
        }

        public T GetMinValue(BinarySearchTreeNode<T> root)
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            T min = root.Value;
            BinarySearchTreeNode<T> current = root;

            while (current.Left is not null)
            {
                min = current.Left.Value;
                current = current.Left;
            }

            return min;
        }

        public T GetMaxValue(BinarySearchTreeNode<T> root)
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            T max = root.Value;
            BinarySearchTreeNode<T> current = root;

            while (current.Right is not null)
            {
                max = current.Right.Value;
                current = current.Right;
            }

            return max;
        }

        public void Clear()
        {
            _root = null;
        }

        public List<T> ToList(Traversal order)
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            List<T> result = new List<T>();

            switch (order)
            {
                case Traversal.Preorder:
                    PreorderTraversalUtil(_root, result);
                    break;
                case Traversal.Inorder: 
                    InorderTraversalUtil(_root, result);
                    break;
                case Traversal.Postorder:
                    PostorderTraversalUtil(_root, result);
                    break;
            }
   
            return result;
        }

        public override string ToString()
        {
            if (_root is null)
            {
                throw new Exception("The BinarySearchTree is empty");
            }

            string result = string.Empty;
            List<T> listUtil = ToList(Traversal.Inorder);

            foreach (T element in listUtil)
            {
                result += element.ToString() + " ";
            }

            return result;
        }

        //Equals

        private void PreorderTraversalUtil(BinarySearchTreeNode<T>? node, List<T> result)
        {
            if (node != null)
            {
                result.Add(node.Value);
                PreorderTraversalUtil(node.Left, result);
                PreorderTraversalUtil(node.Right, result);
            }
        }

        private void InorderTraversalUtil(BinarySearchTreeNode<T>? node, List<T> result)
        {
            if (node != null)
            {
                InorderTraversalUtil(node.Left, result);
                result.Add(node.Value);
                InorderTraversalUtil(node.Right, result);
            }
        }

        private void PostorderTraversalUtil(BinarySearchTreeNode<T>? node, List<T> result)
        {
            if (node != null)
            {
                PostorderTraversalUtil(node.Left, result);
                PostorderTraversalUtil(node.Right, result);
                result.Add(node.Value);
            }
        }

        private BinarySearchTreeNode<T> AddUtil(BinarySearchTreeNode<T>? currentRoot, T value)
        {
            if (currentRoot == null)
            {
                NodeCount++;
                currentRoot = new BinarySearchTreeNode<T>(value);
            }
            else if (value.CompareTo(currentRoot.Value) == -1)
            {
                currentRoot.Left = AddUtil(currentRoot.Left, value);
            }
            else
            {
                currentRoot.Right = AddUtil(currentRoot.Right, value);
            }

            return currentRoot;
        }

        private BinarySearchTreeNode<T>? RemoveUtil(BinarySearchTreeNode<T>? currentRoot, T value)
        {
            if (currentRoot == null)
            {
                return currentRoot;
            }

            if (currentRoot.Value.CompareTo(value) == 1)
            {
                currentRoot.Left = RemoveUtil(currentRoot.Left, value);
            }
            else if (currentRoot.Value.CompareTo(value) == -1)
            {
                currentRoot.Right = RemoveUtil(currentRoot.Right, value);
            }
            else
            {
                if (currentRoot.Left == null && currentRoot.Right == null)
                { 
                    currentRoot = null;
                }
                else if (currentRoot.Left == null || currentRoot.Right == null)
                {
                    if (currentRoot.Left == null)
                    {
                        BinarySearchTreeNode<T> temp = currentRoot.Right!;
                        currentRoot = null;
                        return temp;
                    }
                    else if (currentRoot.Right == null)
                    {
                        BinarySearchTreeNode<T> temp = currentRoot.Left!;
                        currentRoot = null;
                        return temp;
                    }
                }
                else
                {
                    currentRoot.Value = GetMinValue(currentRoot.Right);
                    currentRoot.Right = RemoveUtil(currentRoot.Right, currentRoot.Value);
                }
            }

            return currentRoot;
        }
    }
}
