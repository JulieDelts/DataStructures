using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    internal class LinkedList<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public int Length { get; private set; }
        private Node<T>? _root;
        private Node<T>? _tail;

        public T this[int index]
        {
            get
            {
                Node<T> current = GetNodeByIndex(index);
                return current.Value;
            }
            set
            {
                Node<T> current = GetNodeByIndex(index);
                current.Value = value;
            }
        }

        public LinkedList()
        {
            Length = 0;
            _root = null;
            _tail = null;
        }
        public LinkedList(T value)
        {
            Length = 1;
            _root = new Node<T>(value);
            _tail = _root;
        }
        public LinkedList(T[] values)
        {
            if (values is null) throw new Exception("The LinkedList is null");
            Length = values.Length;
            if (values.Length != 0)
            {
                _root = new Node<T>(values[0]);
                _tail = _root;
                for (int i = 1; i < values.Length; i++)
                {
                    _tail.Next = new Node<T>(values[i]);
                    _tail = _tail.Next;
                }
            }
            else
            {
                Length = 0;
                _root = null;
                _tail = null;
            }
        }

        public int GetFirstIndexByValue(T value)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            Node<T> current = _root!;
            int neededIndex = -1;
            for (int i = 0; i < Length; i++)
            {
                if (current!.Value.CompareTo(value) == 0)
                {
                    neededIndex = i;
                    break;
                }

                current = current.Next!;
            }
            return neededIndex;
        }
        public T GetMaxValue()
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            Node<T> current = _root!;
            T maxValue = current.Value;
            for (int i = 0; i < Length; i++)
            {
                if (current!.Value.CompareTo(maxValue) == 1)
                {
                    maxValue = current.Value;

                }

                current = current.Next!;
            }
            return maxValue;

        }
        public int GetIndexOfMaxValue()
        {
            T maxValue = GetMaxValue();
            int indexOfMaxValue = GetFirstIndexByValue(maxValue);
            return indexOfMaxValue;
        }
        public T GetMinValue()
        {
            if (this is null || this.Length == 0) throw new Exception("The LinkedList is null or empty");

            Node<T> current = _root!;
            T minValue = current.Value;
            for (int i = 0; i < Length; i++)
            {
                if (current!.Value.CompareTo(minValue) == -1)
                {
                    minValue = current.Value;
                }

                current = current.Next!;
            }
            return minValue;
        }
        public int GetIndexOfMinValue()
        {
            T minValue = GetMinValue();
            int indexOfMinValue = GetFirstIndexByValue(minValue);
            return indexOfMinValue;
        }
        public void Add(T value)
        {
            if (this is null) throw new Exception("The LinkedList is null");

            if (Length == 0)
            {
                Length++;
                _root = new Node<T>(value);
                _tail = _root;
            }
            else
            {
                Length++;
                _tail!.Next = new Node<T>(value);
                _tail = _tail.Next;
            }
        }
        public void Add(LinkedList<T> list)
        {
            if (this is null || list is null || list.Length == 0) throw new Exception("The LinkedList is null or empty");

            if (Length == 0)
            {
                _root = list._root;
                _tail = list._tail;
            }
            else
            {
                _tail!.Next = list._root;
                _tail = list._tail;
            }
            Length += list.Length;
        }
        public void InsertAtBeginning(T value)
        {
            if (this is null) throw new Exception("The LinkedList is null");

            if (Length == 0)
            {
                Length++;
                _root = new Node<T>(value);
                _tail = _root;
            }
            else
            {
                Length++;
                Node<T> tmp = new Node<T>(value);
                tmp.Next = _root;
                _root = tmp;
            }
        }
        public void InsertAtBeginning(LinkedList<T> list)
        {
            if (this is null || list is null || list.Length == 0) throw new Exception("The LinkedList is null or empty");

            if (Length == 0)
            {
                _root = list._root;
                _tail = list._tail;
            }
            else
            {
                list._tail!.Next = _root;
                _root = list._root;
            }
            Length += list.Length;
        }
        public void InsertByIndex(int index, T value)
        {
            if (this is null) throw new Exception("The LinkedList is null");
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                InsertAtBeginning(value);
            }
            else if (index == (Length - 1))
            {
                Add(value);
            }
            else
            {
                Node<T> current = GetNodeByIndex(index - 1);
                Node<T> tmp = new Node<T>(value);
                tmp.Next = current.Next;
                current.Next = tmp;
                Length++;
            }
        }
        public void InsertByIndex(int index, LinkedList<T> list)
        {
            if (this is null || list is null || list.Length == 0) throw new Exception("The LinkedList is null or empty");
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                InsertAtBeginning(list);
            }
            else if (index == (Length - 1))
            {
                InsertAtBeginning(list);
            }
            else
            {
                Node<T> current = GetNodeByIndex(index - 1);
                Node<T> tmp = current.Next!;
                current.Next = list._root;
                list._tail!.Next = tmp;
                Length += list.Length;
            }

        }
        public void Clear()
        {
            _root = null;
            _tail = null;
            Length = 0;
        }
        public void RemoveFirstElement()
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");

            if (Length == 1)
            {
                Clear();
            }
            else
            {
                _root = _root!.Next;
                Length--;
            }
        }
        public void RemoveLastElement()
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            if (Length == 1)
            {
                Clear();
            }
            else
            {
                Node<T> current = GetNodeByIndex(Length - 2);
                current.Next = null;
                _tail = current;
                Length--;
            }
        }
        public void RemoveByIndex(int index)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                RemoveFirstElement();
            }
            else if (index == Length - 1)
            {
                RemoveLastElement();
            }
            else
            {
                Node<T> current = GetNodeByIndex(index - 1);
                current.Next = current.Next!.Next;
                Length--;
            }
        }
        public void RemoveNumberOfFirstElements(int number)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            if (Length < number) throw new IndexOutOfRangeException();
            else if (Length == number)
            {
                Clear();
            }
            else
            {
                for (int i = 0; i < number; i++)
                {
                    RemoveFirstElement();
                }
            }
        }
        public void RemoveNumberOfLastElements(int number)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            if (Length < number) throw new IndexOutOfRangeException();
            else if (Length == number)
            {
                Clear();
            }
            else
            {
                for (int i = 0; i < number; i++)
                {
                    RemoveLastElement();
                }
            }
        }
        public void RemoveNumberOfElementsByIndex(int index, int number)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            if (index < 0 || index >= Length || Length < number) throw new IndexOutOfRangeException();
            else if (Length == number)
            {
                Clear();
            }
            else if (index == 0)
            {
                RemoveNumberOfFirstElements(number);
            }
            else if (index + number == Length)
            {
                Node<T> current = GetNodeByIndex(index - 1);
                current.Next = null;
                _tail = current;
                Length -= number;
            }
            else
            {
                Node<T> firstPart = GetNodeByIndex(index - 1);
                Node<T> secondPart = GetNodeByIndex(index + number - 1);
                firstPart.Next = secondPart.Next;
                Length -= number;
            }
        }
        public void RemoveFirstElementByValue(T value)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            int index = GetFirstIndexByValue(value);
            RemoveByIndex(index);
        }
        public void RemoveAllElementsByValue(T value)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is null or empty");
            bool check = true;
            while (check == true)
            {
                int index = GetFirstIndexByValue(value);
                if (index != -1)
                {
                    RemoveByIndex(index);
                }
                else
                {
                    check = false;
                }
            }
        }
        public void Reverse()
        {
            if (this is null) throw new Exception("The LinkedList is null");
            if (Length == 0) return;
            else
            {
                Node<T> current = _root!;
                while (current.Next is not null)
                {
                    Node<T> tmp = current.Next;
                    current.Next = tmp.Next;
                    tmp.Next = _root;
                    _root = tmp;
                }
                _tail = current;
            }
        }
        public void MergeSort(bool desc = false)
        {
            if (this is null) throw new Exception("The LinkedList is null");

            if (Length == 0) return;
            else
            {
                Node<T> current = _root!;
                _root = MergeSortUtil(current, desc);
                Node<T> newTail = _root;
                while (newTail.Next is not null) newTail = newTail.Next;
                _tail = newTail;
            }
        }

        public override string ToString()
        {
            if (Length != 0)
            {
                Node<T> current = _root!;
                StringBuilder s = new StringBuilder(current.Value + " ");
                while (current.Next is not null)
                {
                    current = current.Next;
                    s.Append(current.Value + " ");

                }
                return s.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public override bool Equals(object? obj)
        {
            LinkedList<T>? list = obj as LinkedList<T>;
            if (this is null || list is null) return false;
            if (Length != list.Length) return false;
            if (Length == 1 && list.Length == 1)
            {
                if (this[0].Equals(list[0])) return true;
                else return false;
            }
            Node<T>? currentThis = _root!;
            Node<T>? currentList = list._root!;
            while (currentThis.Next is not null)
            {
                if (!currentThis.Value.Equals(currentList!.Value))
                {
                    return false;
                }
                currentList = currentList.Next;
                currentThis = currentThis.Next;
            }
            return true;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private Node<T> GetNodeByIndex(int index)
        {
            if (this is null || Length == 0) throw new Exception("The LinkedList is empty");
            if (index < 0 || index > Length) throw new IndexOutOfRangeException();

            Node<T> current = _root!;
            for (int i = 1; i <= index; i++)
            {
                current = current.Next!;
            }
            return current;
        }
        private Node<T> MergeSortUtil(Node<T> root, bool desc)
        {
            if (root is null || root.Next is null)
            {
                return root;
            }
            Node<T> middle = GetMiddleNode(root);
            Node<T> nextToMiddle = middle.Next!;
            middle.Next = null;
            Node<T> left = MergeSortUtil(root, desc);
            Node<T> right = MergeSortUtil(nextToMiddle, desc);
            Node<T> sortedlist = Merge(left, right, desc);
            return sortedlist;
        }
        private Node<T> GetMiddleNode(Node<T> root)
        {
            if (root is null) return root;
            Node<T> fastptr = root.Next;
            Node<T> slowptr = root;

            while (fastptr is not null)
            {
                fastptr = fastptr.Next;
                if (fastptr is not null)
                {
                    slowptr = slowptr.Next;
                    fastptr = fastptr.Next;
                }
            }
            return slowptr;
        }
        private Node<T> Merge(Node<T> a, Node<T> b, bool desc)
        {
            Node<T> result;
            if (a is null)
                return b;
            if (b is null)
                return a;
            if (desc == false)
            {
                if (a.Value.CompareTo(b.Value) == -1 || a.Value.CompareTo(b.Value) == 0)
                {
                    result = a;

                    result.Next = Merge(a.Next, b, desc);
                }
                else
                {
                    result = b;
                    result.Next = Merge(a, b.Next, desc);
                }
            }
            else
            {
                if (a.Value.CompareTo(b.Value) == 1)
                {
                    result = a;

                    result.Next = Merge(a.Next, b, desc);
                }
                else
                {
                    result = b;
                    result.Next = Merge(a, b.Next, desc);
                }
            }

            return result;
        }
    }
}

