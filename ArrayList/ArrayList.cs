namespace ArrayList
{
    public class ArrayList<T> where T : notnull, IComparable<T>, IEquatable<T>
    {
        public int Length { get; private set; }

        private T[] _array;

        public ArrayList()
        {
            Length = 0;
            _array = new T[10];
        }

        public ArrayList(T value)
        {
            Length = 1;
            _array = new T[10];
            _array[0] = value;
        }

        public ArrayList(T[] value)
        {
            if (value.Length == 0)
            {
                throw new ArgumentException("The array is empty.");
            }

            Length = value.Length;
            _array = value;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("The index is out of range");
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException("The index is out of range");
                }

                _array[index] = value;
            }
        }

        public void Add(T value)
        {
            if (Length == _array.Length)
            {
                Upsize();
            }

            _array[Length] = value;
            Length++;
        }

        public void Add(ArrayList<T> arrayListToAdd)
        {
            if (arrayListToAdd.Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            while (arrayListToAdd.Length >= (_array.Length - Length))
            {
                Upsize();
            }

            for (int i = 0; i < arrayListToAdd.Length; i++)
            {
                _array[Length] = arrayListToAdd[i];
                Length++;
            }
        }

        public void InsertAt(int index, T value)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length == 0)
            {
                Add(value);
            }
            else
            {
                if (Length == _array.Length)
                {
                    Upsize();
                }

                _array = ShiftToRight(index);
                _array[index] = value;
                Length++;
            }
        }

        public void InsertAt(int index, ArrayList<T> arrayListToInsert)
        {
            if (arrayListToInsert.Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            if (Length == 0)
            {
                Add(arrayListToInsert);
            }
            else
            {
                while (arrayListToInsert.Length >= (_array.Length - Length))
                {
                    Upsize();
                }

                Length += arrayListToInsert.Length;
                T[] tempArray = new T[_array.Length];

                for (int i = 0; i < index; i++)
                {
                    tempArray[i] = _array[i];
                }

                for (int i = 0; i < arrayListToInsert.Length; i++)
                {
                    tempArray[i + index] = arrayListToInsert[i];
                }

                for (int i = 0; i < (Length - arrayListToInsert.Length - index); i++)
                {
                    tempArray[arrayListToInsert.Length + index + i] = _array[index + i];
                }

                _array = tempArray;
            }
        }

        public void RemoveAt(int index)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            _array = ShiftToLeft(index);
            Length--;

            if (Length == _array.Length / 2)
            {
                Downsize();
            }
        }

        public void RemoveAt(int index, int number)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("The index is out of range");
            }

            _array = ShiftToLeft(index, number);
            Length -= number;

            if (Length == (_array.Length / 2))
            {
                Downsize();
            }
        }

        public void RemoveByValue(T value)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            int index = GetIndexByValue(value);

            if (index == -1)
            {
                throw new ArgumentException("There is no such value in the ArrayList");
            }
            else
            {
                _array = ShiftToLeft(index);
                Length--;

                if (Length == (_array.Length / 2))
                {
                    Downsize();
                }
            }
        }

        public void RemoveAllByValue(T value)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            bool check = true;

            while (check == true)
            {
                int index = GetIndexByValue(value);

                if (index != -1)
                {
                    _array = ShiftToLeft(index);
                    Length--;
                }
                else
                {
                    check = false;
                }
            }

            if (Length == (_array.Length / 2))
            {
                Downsize();
            }
        }

        public T GetMinValue()
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            T min = _array[0];

            for (int i = 0; i < Length; i++)
            {
                if (min.CompareTo(_array[i]) > 0)
                {
                    min = _array[i];
                }
            }

            return min;
        }

        public int GetIndexOfMinValue()
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            T min = _array[0];
            int indexOfMin = 0;

            for (int i = 0; i < Length; i++)
            {
                if (min.CompareTo(_array[i]) > 0)
                {
                    min = _array[i];
                    indexOfMin = i;
                }
            }

            return indexOfMin;
        }

        public T GetMaxValue()
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            T max = _array[0];

            for (int i = 0; i < Length; i++)
            {
                if (max.CompareTo(_array[i]) < 0)
                {
                    max = _array[i];
                }
            }

            return max;
        }

        public int GetIndexOfMaxValue()
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            T max = _array[0];
            int indexOfMax = 0;

            for (int i = 0; i < Length; i++)
            {
                if (max.CompareTo(_array[i]) < 0)
                {
                    max = _array[i];
                    indexOfMax = i;
                }
            }

            return indexOfMax;
        }

        public int GetIndexByValue(T value)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            int neededIndex = -1;

            for (int i = 0; i < Length; i++)
            {
                if (_array[i].CompareTo(value) == 0)
                {
                    neededIndex = i;
                    break;
                }
            }

            return neededIndex;
        }

        public void QuickSort(bool desc = false)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            QuickSortUtil(0, Length - 1, desc);
        }

        public void Reverse()
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }

            T temp;
            int start = 0;
            int end = Length - 1;

            while (start < end)
            {
                temp = this[start];
                this[start] = this[end];
                this[end] = temp;
                start++;
                end--;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            ArrayList<T>? arrayList = obj as ArrayList<T>;

            if (arrayList is null)
            {
                return false;
            }

            if (Length != arrayList.Length)
            {
                return false;
            }

            for (int i = 0; i < Length; i++)
            {
                if (!_array[i].Equals(arrayList._array[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string stringArray = string.Empty;

            for (int i = 0; i < Length; i++)
            {
                stringArray += _array[i] + " ";
            }

            return stringArray;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void QuickSortUtil(int leftPart, int rightPart, bool desc)
        {
            if (leftPart < rightPart)
            {
                int partition = Partition(leftPart, rightPart, desc);
                QuickSortUtil(leftPart, partition - 1, desc);
                QuickSortUtil(partition + 1, rightPart, desc);
            }
        }

        private int Partition(int leftPart, int rightPart, bool desc)
        {
            T pivot = _array[rightPart];
            int pointer = leftPart - 1;

            if (desc == false)
            {
                for (int i = leftPart; i < rightPart; i++)
                {
                    if (_array[i].CompareTo(pivot) == -1 || _array[i].CompareTo(pivot) == 0)
                    {
                        pointer++;
                        T tmp = _array[pointer];
                        _array[pointer] = _array[i];
                        _array[i] = tmp;
                    }
                }
            }
            else
            {
                for (int i = leftPart; i < rightPart; i++)
                {
                    if (_array[i].CompareTo(pivot) == 1)
                    {
                        pointer++;
                        T tmp = _array[pointer];
                        _array[pointer] = _array[i];
                        _array[i] = tmp;
                    }
                }
            }

            T temp = _array[pointer + 1];
            _array[pointer + 1] = _array[rightPart];
            _array[rightPart] = temp;

            return pointer + 1;
        }

        private void Upsize()
        {
            int newLength = (int)(_array.Length * 1.33d + 1);
            T[] tmpArray = new T[newLength];

            for (int i = 0; i < _array.Length; i++)
            {
                tmpArray[i] = _array[i];
            }

            _array = tmpArray;
        }

        private void Downsize()
        {
            int newLength = (int)(_array.Length * 0.67d + 1);
            T[] tmpArray = new T[newLength];

            for (int i = 0; i < tmpArray.Length; i++)
            {
                tmpArray[i] = _array[i];
            }

            _array = tmpArray;
        }

        private T[] ShiftToRight(int index)
        {
            T[] tempArray = new T[_array.Length];

            for (int i = 0; i < index; i++)
            {
                tempArray[i] = _array[i];
            }

            for (int i = index; i < Length; i++)
            {
                tempArray[i + 1] = _array[i];

            }

            _array = tempArray;

            return _array;
        }

        private T[] ShiftToLeft(int index)
        {
            T[] tempArray = new T[_array.Length];

            for (int i = 0; i < index; i++)
            {
                tempArray[i] = _array[i];
            }

            for (int i = index; i < Length - 1; i++)
            {
                tempArray[i] = _array[i + 1];
            }

            _array = tempArray;

            return _array;
        }

        private T[] ShiftToLeft(int index, int numOfElements)
        {
            for (int i = index; i < (index + numOfElements); i++)
            {
                _array = ShiftToLeft(index);
            }

            return _array;
        }

    }
}
