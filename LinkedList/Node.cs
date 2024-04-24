using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    internal class Node<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public T Value { get; set; }
        public Node<T>? Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}