namespace DummyCheck
{
    public class DummyElement : IComparable<DummyElement>, IEquatable<DummyElement>
    {
        public int Value { get; private set; }

        public DummyElement(int value)
        {
            Value = value;
        }

        public int CompareTo(DummyElement? other)
        {
            if (other is null)
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        public bool Equals(DummyElement? other)
        {
            if (other is null)
            {
                return false;
            }    

            return Value == other.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            DummyElement? other = obj as DummyElement;

            if (other is null)
            {
                return false;
            }

            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}