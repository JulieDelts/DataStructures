using System;
using System.Collections.Generic;
using System.Text;

namespace SinglyLinkedList
{
    public class SinglyLinkedNode<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public T Value { get; set; }

        internal SinglyLinkedNode<T>? Next { get; set; }

        public SinglyLinkedNode(T value)
        {
            Value = value;
            Next = null;
        }
    }
}