using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    internal class ArrayList<T> where T : notnull, IComparable<T>, IEquatable<T>
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
            if (value.Length == 0 || value == null)
            {
                throw new ArgumentException();
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
                    throw new IndexOutOfRangeException();
                }
                return _array[index];
            }
            set
            {
                if (index < 0 || index > Length)
                {
                    throw new IndexOutOfRangeException();
                }
                _array[index] = value;
            }
        }

        public void Add(T value)
        {
            if (Length == _array.Length)
            {
                UpSize();
            }
            _array[Length] = value;
            Length++;
        }
        public void AddArrayList(ArrayList<T> arrayListToAdd)
        {
            if (arrayListToAdd == null || arrayListToAdd.Length == 0)
            {
                throw new ArgumentException();
            }
            while (arrayListToAdd.Length >= (_array.Length - Length))
            {
                UpSize();
            }
            for (int i = 0; i < arrayListToAdd.Length; i++)
            {
                _array[Length] = arrayListToAdd[i];
                Length++;
            }
        }
        public void InsertElementAtBeginning(T value)
        {
            if (Length == 0)
            {
                Add(value);
            }
            else
            {
                if (Length == _array.Length)
                {
                    UpSize();
                }
                _array = ShiftToRight(0);
                _array[0] = value;
                Length++;
            }
        }
        public void InsertElementByIndex(int index, T value)
        {
            if (Length == 0)
            {
                Add(value);
            }
            else if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                if (Length == _array.Length)
                {
                    UpSize();
                }
                _array = ShiftToRight(index);
                _array[index] = value;
                Length++;
            }

        }
        public void InsertArrayListAtBeginning(ArrayList<T> arrayListToInsert)
        {
            if (arrayListToInsert == null || arrayListToInsert.Length == 0)
            {
                throw new ArgumentException();
            }
            if (Length == 0)
            {
                AddArrayList(arrayListToInsert);
            }
            else
            {
                while (arrayListToInsert.Length >= (_array.Length - Length))
                {
                    UpSize();
                }
                Length += arrayListToInsert.Length;
                T[] tempArray = new T[_array.Length];
                for (int i = 0; i < arrayListToInsert.Length; i++)
                {
                    tempArray[i] = arrayListToInsert[i];
                }
                for (int i = 0; i < (Length - arrayListToInsert.Length); i++)
                {
                    tempArray[arrayListToInsert.Length + i] = _array[i];
                }

                _array = tempArray;
            }

        }
        public void InsertArrayListByIndex(int index, ArrayList<T> arrayListToInsert)
        {
            if (arrayListToInsert == null || arrayListToInsert.Length == 0)
            {
                throw new ArgumentException();
            }
            if (Length == 0)
            {
                AddArrayList(arrayListToInsert);
            }
            else if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                while (arrayListToInsert.Length >= (_array.Length - Length))
                {
                    UpSize();
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
        public void RemoveElementByIndex(int index)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            else if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                _array = ShiftToLeft(index);
                Length--;
            }
            if (Length == (int)(_array.Length / 2))
            {
                DownSize();
            }
        }
        public void RemoveElementByValue(T value)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            int index = GetFirstIndexByValue(value);
            if (index == -1)
            {
                throw new ArgumentException();
            }
            else
            {
                _array = ShiftToLeft(index);
                Length--;
                if (Length == (int)(_array.Length / 2))
                {
                    DownSize();
                }
            }
        }
        public void RemoveAllElementsByValue(T value)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            bool check = true;
            while (check == true)
            {
                int index = GetFirstIndexByValue(value);
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
            if (Length == (int)(_array.Length / 2))
            {
                DownSize();
            }
        }
        public void RemoveNumberOfLastElements(int number)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            else if (number > Length)
            {
                Length = 0;
            }
            else
            {
                Length -= number;
            }
        }
        public void RemoveNumberOfFirstElements(int number)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            else if (number > Length)
            {
                Length = 0;
            }
            else
            {
                T[] tmpArray = new T[_array.Length];

                for (int i = 0; i < _array.Length - number; i++)
                {
                    tmpArray[i] = _array[i + number];
                }
                _array = tmpArray;
                Length -= number;
            }
        }
        public void RemoveNumberOfElementsByIndex(int index, int number)
        {
            if (Length == 0)
            {
                throw new ArgumentException("The ArrayList is empty");
            }
            else if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                _array = ShiftToLeft(index, number);
                Length -= number;
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
        public int GetFirstIndexByValue(T value)
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
            QuickSortUtil(0, this.Length - 1, desc);
        }
        public void Reverse()
        {
            T temp;
            int start = 0;
            int end = this.Length - 1;
            while (start < end)
            {
                temp = this[start];
                this[start] = this[end];
                this[end] = temp;
                start++;
                end--;
            }

        }
        public bool Equals(ArrayList<T> obj)
        {
            ArrayList<T> arrayList = obj;

            if (this.Length != arrayList.Length)
            {
                return false;
            }
            for (int i = 0; i < Length; i++)
            {
                if (!this._array[i].Equals(arrayList._array[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Length; i++)
            {
                s += _array[i] + " ";
            }
            return s;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void QuickSortUtil(int l, int r, bool desc)
        {
            if (l < r)
            {
                int partition = Partition(l, r, desc);
                QuickSortUtil(l, partition - 1, desc);
                QuickSortUtil(partition + 1, r, desc);
            }
        }
        private int Partition(int l, int r, bool desc)
        {
            T pivot = this[r];
            int i = l - 1;
            if (desc == false)
            {
                for (int j = l; j < r; j++)
                {
                    if (this[j].CompareTo(pivot) == -1 || this[j].CompareTo(pivot) == 0)
                    {
                        i++;
                        T tmp = this[i];
                        this[i] = this[j];
                        this[j] = tmp;
                    }
                }
            }
            else
            {
                for (int j = l; j < r; j++)
                {
                    if (this[j].CompareTo(pivot) == 1)
                    {
                        i++;
                        T tmp = this[i];
                        this[i] = this[j];
                        this[j] = tmp;
                    }
                }
            }
            T temp = this[i + 1];
            this[i + 1] = this[r];
            this[r] = temp;
            return i + 1;
        }
        private void UpSize()
        {
            int newLength = (int)(_array.Length * 1.33d + 1);
            T[] tmpArray = new T[newLength];
            for (int i = 0; i < _array.Length; i++)
            {
                tmpArray[i] = _array[i];
            }
            _array = tmpArray;
        }
        private void DownSize()
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
            if (Length == (int)(_array.Length / 2))
            {
                DownSize();
            }
            return _array;
        }


    }
}
