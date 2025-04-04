using System.Text;

namespace TI83Sharp;

public class TiList : List<TiNumber>, IEquatable<TiList>
{
    public const int MAX_CAPACITY = 999;

    public TiList() : base()
    {
    }

    public TiList(int capacity) : base(capacity)
    {
    }

    public new void Add(TiNumber item)
    {
        base.Add(item);

        if (1 > Count || Count > MAX_CAPACITY)
        {
            throw RuntimeError.InvalidDim;
        }
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TiList);
    }

    public bool Equals(TiList? other)
    {
        if (other == null || Count != other.Count)
        {
            return false;
        }

        for (int i = 0; i < Count; i++)
        {
            if (!this[i].Equals(other[i]))
            {
                return false;
            }
        }

        return true;
    }

    public TiList TiEquals(TiList other)
    {
        if (Count != other.Count)
        {
            throw RuntimeError.DimMismatch;
        }

        var result = new TiList(Count);
        for (int i = 0; i < Count; i++)
        {
            result.Add(this[i].Equals(other[i]) ? 1 : 0);
        }

        return result;
    }

    public TiList TiNotEquals(TiList other)
    {
        if (Count != other.Count)
        {
            throw RuntimeError.DimMismatch;
        }

        var result = new TiList(Count);
        for (int i = 0; i < Count; i++)
        {
            result.Add(this[i].Equals(other[i]) ? 0 : 1);
        }

        return result;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        var str = new StringBuilder("{");
        for (int i = 0; i < Count; i++)
        {
            str.Append(this[i].ToString());
            if (i < Count - 1)
            {
                str.Append(',');
            }
        }
        str.Append('}');
        return str.ToString();
    }

    private static TiList PerformOperation(TiList left, TiList right, Func<TiNumber, TiNumber, TiNumber> operation)
    {
        if (left.Count != right.Count)
        {
            throw RuntimeError.DimMismatch;
        }

        var result = new TiList(left.Count);
        for (int i = 0; i < left.Count; ++i)
        {
            result.Add(operation(left[i], right[i]));
        }
        return result;
    }

    private static TiList PerformOperation(TiList left, Func<TiNumber, TiNumber> operation)
    {
        var result = new TiList(left.Count);
        for (int i = 0; i < left.Count; ++i)
        {
            result.Add(operation(left[i]));
        }
        return result;
    }

    public static TiList operator +(TiList left, TiList right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum + rightNum);
    }

    public static TiList operator +(TiList left, TiNumber right)
    {
        return PerformOperation(left, (leftNum) => leftNum + right);
    }

    public static TiList operator +(TiNumber left, TiList right)
    {
        return right + left;
    }

    public static TiList operator -(TiList left, TiList right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum - rightNum);
    }

    public static TiList operator -(TiList left, TiNumber right)
    {
        return PerformOperation(left, (leftNum) => leftNum - right);
    }

    public static TiList operator -(TiNumber left, TiList right)
    {
        return PerformOperation(right, (rightNum) => left - rightNum);
    }

    public static TiList operator *(TiList left, TiList right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum * rightNum);
    }

    public static TiList operator /(TiList left, TiList right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum / rightNum);
    }
}
