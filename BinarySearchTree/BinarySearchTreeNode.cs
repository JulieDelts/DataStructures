using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BinarySearchTreeNode<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public T Value { get; internal set; }

        internal BinarySearchTreeNode<T>? Left { get; set; }

        internal BinarySearchTreeNode<T>? Right { get; set; }

        public BinarySearchTreeNode(T value)
        {
            Value = value;
        }
    }
}
