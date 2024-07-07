using System.Text;

namespace LinkedList
{
    public class SinglyLinkedList<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public int Length { get; private set; }

        public SinglyLinkedNode<T>? Root { get; internal set; }

        public SinglyLinkedNode<T>? Tail { get; internal set; }

        public T this[int index]
        {
            get
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index);
                return current.Value;
            }
            set
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index);
                current.Value = value;
            }
        }

        public SinglyLinkedList(T value)
        {
            Length = 1;
            Root = new SinglyLinkedNode<T>(value);
            Tail = Root;
        }

        public SinglyLinkedList(T[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("The array is empty");
            }

            Length = values.Length;

            Root = new SinglyLinkedNode<T>(values[0]);
            Tail = Root;

            for (int i = 1; i < values.Length; i++)
            {
                Tail.Next = new SinglyLinkedNode<T>(values[i]);
                Tail = Tail.Next;
            }
        }

        public int GetIndexByValue(T value)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            SinglyLinkedNode<T> current = Root;
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
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            SinglyLinkedNode<T> current = Root;
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
            int indexOfMaxValue = GetIndexByValue(maxValue);
            return indexOfMaxValue;
        }

        public T GetMinValue()
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            SinglyLinkedNode<T> current = Root;
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
            int indexOfMinValue = GetIndexByValue(minValue);
            return indexOfMinValue;
        }

        public void Add(T value)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Length == 0)
            {
                Length++;
                Root = new SinglyLinkedNode<T>(value);
                Tail = Root;
            }
            else
            {
                Length++;
                Tail.Next = new SinglyLinkedNode<T>(value);
                Tail = Tail.Next;
            }
        }

        public void Add(SinglyLinkedList<T> list)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (list.Root is null)
            {
                throw new ArgumentException("The SinglyLinkedListToInsert is empty");
            }

            if (list.Tail is null)
            {
                throw new ArgumentException("The SinglyLinkedListToInsert is empty");
            }

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

        public void InsertAt(int index, T value)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length == 0)
            {
                Length++;
                Root = new SinglyLinkedNode<T>(value);
                Tail = Root;
            }
            else if (index == 0)
            {
                Length++;
                SinglyLinkedNode<T> tmp = new SinglyLinkedNode<T>(value);
                tmp.Next = Root;
                Root = tmp;
            }
            else if (index == (Length - 1))
            {
                Add(value);
            }
            else
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index - 1);
                SinglyLinkedNode<T> tmp = new SinglyLinkedNode<T>(value);
                tmp.Next = current.Next;
                current.Next = tmp;
                Length++;
            }
        }

        public void InsertAt(int index, SinglyLinkedList<T> list)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (list.Root is null)
            {
                throw new ArgumentException("The SinglyLinkedListToInsert is empty");
            }

            if (list.Tail is null)
            {
                throw new ArgumentException("The SinglyLinkedListToInsert is empty");
            }

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length == 0)
            {
                Root = list.Root;
                Tail = list.Tail;
                Length += list.Length;
            }
            else if (index == 0)
            {
                list.Tail.Next = Root;
                Root = list.Root;
                Length += list.Length;
            }
            else if (index == (Length - 1))
            {
                Add(list);
            }
            else
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index - 1);

                if (current.Next is not null)
                {
                    SinglyLinkedNode<T> tmp = current.Next;
                    current.Next = list.Root;
                    list.Tail.Next = tmp;
                    Length += list.Length;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void Clear()
        {
            Root = null;
            Tail = null;
            Length = 0;
        }

        public void RemoveAt(int index)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length == 1)
            {
                Clear();
            }
            else if (index == 0)
            {
                Root = Root.Next;
                Length--;
            }
            else if (index == Length - 1)
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(Length - 2);
                current.Next = null;
                Tail = current;
                Length--;
            }
            else
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index - 1);
                if (current.Next is not null)
                {
                    current.Next = current.Next.Next;
                    Length--;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void RemoveAt(int index, int number)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length < number)
            {
                throw new ArgumentException("The SinglyLinkedList is too small");
            }

            if (Length == number)
            {
                Clear();
            }
            else if (index == 0)
            {
                for (int i = 0; i < number && Root.Next is not null; i++)
                {
                    Root = Root.Next;
                    Length--;
                }
            }
            else if (index + number == Length)
            {
                SinglyLinkedNode<T> current = GetNodeByIndex(index - 1);
                current.Next = null;
                Tail = current;
                Length -= number;
            }
            else
            {
                SinglyLinkedNode<T> firstPart = GetNodeByIndex(index - 1);
                SinglyLinkedNode<T> secondPart = GetNodeByIndex(index + number - 1);
                firstPart.Next = secondPart.Next;
                Length -= number;
            }
        }

        public void RemoveByValue(T value)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            int index = GetIndexByValue(value);
            RemoveAt(index);
        }

        public void RemoveAllByValue(T value)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            bool check = true;

            while (check == true)
            {
                int index = GetIndexByValue(value);

                if (index != -1)
                {
                    RemoveAt(index);
                }
                else
                {
                    check = false;
                }
            }
        }

        public void Reverse()
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Length == 0)
            {
                return;
            }
            else
            {
                SinglyLinkedNode<T> current = Root;

                while (current.Next is not null)
                {
                    SinglyLinkedNode<T> tmp = current.Next;
                    current.Next = tmp.Next;
                    tmp.Next = Root;
                    Root = tmp;
                }

                Tail = current;
            }
        }

        public void MergeSort(bool desc = false)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Length == 0)
            {
                return;
            }
            else
            {
                SinglyLinkedNode<T> current = Root;
                Root = MergeSortUtil(current, desc);

                if (Root is not null)
                {
                    SinglyLinkedNode<T> newTail = Root;

                    while (newTail.Next is not null)
                    {
                        newTail = newTail.Next;
                    }

                    Tail = newTail;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public override string ToString()
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Length != 0)
            {
                SinglyLinkedNode<T> current = Root;
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
            if (Root is null || Tail is null)
            {
                return false;
            }
   
            SinglyLinkedList<T>? list = obj as SinglyLinkedList<T>;

            if (list is null || list.Root is null || list.Tail is null)
            {
                return false;
            }

            if (Length != list.Length)
            {
                return false;
            }

            if (Length == 1 && list.Length == 1)
            {
                if (this[0].Equals(list[0]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
  
            SinglyLinkedNode<T>? currentThis = Root;
            SinglyLinkedNode<T>? currentList = list.Root;

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

        private SinglyLinkedNode<T> GetNodeByIndex(int index)
        {
            if (Root is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (Tail is null)
            {
                throw new Exception("The SinglyLinkedList is empty");
            }

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            SinglyLinkedNode<T> current = Root;

            for (int i = 1; i <= index && current.Next is not null; i++)
            {
                current = current.Next;
            }

            return current;
        }

        private SinglyLinkedNode<T>? MergeSortUtil(SinglyLinkedNode<T>? root, bool desc)
        {
            if (root is null || root.Next is null)
            {
                return root; 
            }

            SinglyLinkedNode<T> middle = GetMiddleNode(root);

            if (middle.Next is null)
            {
                throw new Exception();
            }

            SinglyLinkedNode<T> nextToMiddle = middle.Next;
            middle.Next = null;

            SinglyLinkedNode<T>? left = MergeSortUtil(root, desc);
            SinglyLinkedNode<T>? right = MergeSortUtil(nextToMiddle, desc);

            if (left is null || right is null)
            { 
                throw new Exception();
            }

            SinglyLinkedNode<T> sortedList;
            sortedList = Merge(left, right, desc);

            return sortedList;
        }

        private SinglyLinkedNode<T> GetMiddleNode(SinglyLinkedNode<T> root)
        {
            SinglyLinkedNode<T>? fastPointer = root.Next;
            SinglyLinkedNode<T>? slowPointer = root;

            while (fastPointer is not null)
            {
                fastPointer = fastPointer.Next;

                if (fastPointer is not null && slowPointer.Next is not null)
                {
                    slowPointer = slowPointer.Next;
                    fastPointer = fastPointer.Next;
                }
            }

            return slowPointer;
        }

        private SinglyLinkedNode<T> Merge(SinglyLinkedNode<T>? firstPart, SinglyLinkedNode<T>? secondPart, bool desc)
        {
            if (firstPart is null && secondPart is not null)
            {
                return secondPart;
            }
            else if (secondPart is null && firstPart is not null)
            {
                return firstPart;
            }
            else if (firstPart is not null && secondPart is not null)
            {
                SinglyLinkedNode<T> result;

                if (desc == false)
                {
                    if (firstPart.Value.CompareTo(secondPart.Value) == -1 || firstPart.Value.CompareTo(secondPart.Value) == 0)
                    {
                        result = firstPart;
                        result.Next = Merge(firstPart.Next, secondPart, desc);
                    }
                    else
                    {
                        result = secondPart;
                        result.Next = Merge(firstPart, secondPart.Next, desc);
                    }
                }
                else
                {
                    if (firstPart.Value.CompareTo(secondPart.Value) == 1)
                    {
                        result = firstPart;

                        result.Next = Merge(firstPart.Next, secondPart, desc);
                    }
                    else
                    {
                        result = secondPart;
                        result.Next = Merge(firstPart, secondPart.Next, desc);
                    }

                }
                return result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
