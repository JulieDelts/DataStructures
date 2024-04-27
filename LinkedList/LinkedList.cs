using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    internal class LinkedList<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public int Length { get; private set; } = 0;
        public Node<T>? Root { get; internal set; }
        public Node<T>? Tail { get; internal set; }

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
            Root = null;
            Tail = null;
        }
        public LinkedList(T value)
        {
            Length = 1;
            Root = new Node<T>(value);
            Tail = Root;
        }
        public LinkedList(T[] values)
        {
            Length = values.Length;
            if (values.Length != 0)
            {
                Root = new Node<T>(values[0]);
                Tail = Root;
                for (int i = 1; i < values.Length; i++)
                {
                    Tail.Next = new Node<T>(values[i]);
                    Tail = Tail.Next;
                }
            }
            else
            {
                Length = 0;
                Root = null;
                Tail = null;
            }
        }

        public int GetFirstIndexByValue(T value)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            Node<T> current = Root;
            int neededIndex = -1;
            for (int i = 0; i < Length && current.Next is not null; i++)
            {
                if (current.Value.CompareTo(value) == 0)
                {
                    neededIndex = i;
                    break;
                }
                current = current.Next;

            }
            return neededIndex;
        }
        public T GetMaxValue()
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            Node<T> current = Root;
            T maxValue = current.Value;
            while (current.Next is not null)
            {
                if (current.Value.CompareTo(maxValue) == 1)
                {
                    maxValue = current.Value;

                }
                current = current.Next;
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            Node<T> current = Root;
            T minValue = current.Value;
            while (current.Next is not null)
            {
                if (current.Value.CompareTo(minValue) == -1)
                {
                    minValue = current.Value;
                }

                current = current.Next;
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            if (Length == 0)
            {
                Length++;
                Root = new Node<T>(value);
                Tail = Root;
            }
            else
            {
                Length++;
                Tail.Next = new Node<T>(value);
                Tail = Tail.Next;
            }
        }
        public void Add(LinkedList<T> list)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (list.Root is null || list.Tail is null) throw new Exception("The LinkedListToInsert is empty");

            if (Length == 0)
            {
                Root = list.Root;
                Tail = list.Tail;
            }
            else
            {
                Tail.Next = list.Root;
                Tail = list.Tail;
            }
            Length += list.Length;
        }
        public void InsertAtBeginning(T value)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            if (Length == 0)
            {
                Length++;
                Root = new Node<T>(value);
                Tail = Root;
            }
            else
            {
                Length++;
                Node<T> tmp = new Node<T>(value);
                tmp.Next = Root;
                Root = tmp;
            }
        }
        public void InsertAtBeginning(LinkedList<T> list)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (list.Root is null || list.Tail is null) throw new Exception("The LinkedListToInsert is empty");

            if (Length == 0)
            {
                Root = list.Root;
                Tail = list.Tail;
            }
            else
            {
                list.Tail.Next = Root;
                Root = list.Root;
            }
            Length += list.Length;
        }
        public void InsertByIndex(int index, T value)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (list.Root is null || list.Tail is null) throw new Exception("The LinkedListToInsert is empty");
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            if (index == 0)
            {
                InsertAtBeginning(list);
            }
            else if (index == (Length - 1))
            {
                Add(list);
            }
            else
            {
                Node<T> current = GetNodeByIndex(index - 1);
                if (current.Next is not null)
                {
                    Node<T> tmp = current.Next;
                    current.Next = list.Root;
                    list.Tail.Next = tmp;
                    Length += list.Length;
                }
                else throw new Exception();
            }

        }
        public void Clear()
        {
            Root = null;
            Tail = null;
            Length = 0;
        }
        public void RemoveFirstElement()
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            if (Length == 1)
            {
                Clear();
            }
            else
            {
                Root = Root.Next;
                Length--;
            }
        }
        public void RemoveLastElement()
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (Length == 1)
            {
                Clear();
            }
            else
            {
                Node<T> current = GetNodeByIndex(Length - 2);
                current.Next = null;
                Tail = current;
                Length--;
            }
        }
        public void RemoveByIndex(int index)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
                if (current.Next is not null)
                {
                    current.Next = current.Next.Next;
                    Length--;
                }
                else throw new Exception();
            }
        }
        public void RemoveNumberOfFirstElements(int number)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
                Tail = current;
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            int index = GetFirstIndexByValue(value);
            RemoveByIndex(index);
        }
        public void RemoveAllElementsByValue(T value)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (Length == 0) return;
            else
            {
                Node<T> current = Root;
                while (current.Next is not null)
                {
                    Node<T> tmp = current.Next;
                    current.Next = tmp.Next;
                    tmp.Next = Root;
                    Root = tmp;
                }
                Tail = current;
            }
        }
        public void MergeSort(bool desc = false)
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            if (Length == 0) return;
            else
            {
                Node<T> current = Root;
                Root = MergeSortUtil(current, desc);
                if (Root is not null)
                {
                    Node<T> newTail = Root;
                    while (newTail.Next is not null)
                    {
                        newTail = newTail.Next;
                    }
                    Tail = newTail;
                }
                else throw new Exception();
            }

        }

        public override string ToString()
        {
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");

            if (Length != 0)
            {
                Node<T> current = Root;
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            LinkedList<T>? list = obj as LinkedList<T>;
            if (list is null || list.Root is null || list.Tail is null) return false;
            if (Length != list.Length) return false;
            if (Length == 1 && list.Length == 1)
            {
                if (this[0].Equals(list[0])) return true;
                else return false;
            }
            Node<T>? currentThis = Root;
            Node<T>? currentList = list.Root;
            while (currentThis.Next is not null && currentList.Next is not null)
            {
                if (!currentThis.Value.Equals(currentList.Value))
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
            if (Root is null || Tail is null) throw new Exception("The LinkedList is empty");
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            Node<T> current = Root;
            for (int i = 1; i <= index && current.Next is not null; i++)
            {
                current = current.Next;
            }
            return current;
        }
        private Node<T>? MergeSortUtil(Node<T>? root, bool desc)
        {
            if (root is null || root.Next is null) return root;
            Node<T> middle = GetMiddleNode(root);
            if (middle.Next is null) throw new Exception();
            Node<T> nextToMiddle = middle.Next;
            middle.Next = null;
            Node<T>? left = MergeSortUtil(root, desc);
            Node<T>? right = MergeSortUtil(nextToMiddle, desc);
            if (left is null || right is null) throw new Exception();
            Node<T> sortedList;
            sortedList = Merge(left, right, desc);
            return sortedList;
        }
        private Node<T> GetMiddleNode(Node<T> root)
        {
            Node<T>? fastptr = root.Next;
            Node<T>? slowptr = root;

            while (fastptr is not null)
            {
                fastptr = fastptr.Next;
                if (fastptr is not null && slowptr.Next is not null)
                {
                    slowptr = slowptr.Next;
                    fastptr = fastptr.Next;
                }
            }
            return slowptr;
        }
        private Node<T> Merge(Node<T>? a, Node<T>? b, bool desc)
        {
            if (a is null && b is not null)
                return b;
            else if (b is null && a is not null)
                return a;
            else if (a is not null && b is not null)
            {
                Node<T> result;
                if (desc == false)
                {
                    if (a.Value.CompareTo(b.Value) == -1 || a.Value.CompareTo(b.Value) == 0)
                    {
                        result = a;

                        result.Next = Merge(a?.Next, b, desc);
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
            else throw new Exception();

        }


    }
}
