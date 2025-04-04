using System.Diagnostics;
using System.Globalization;

namespace TI83Sharp;

public readonly struct TiNumber : IComparable<TiNumber>, IEquatable<TiNumber>
{
    private static readonly NumberFormatInfo _nfi = new NumberFormatInfo
    {
        NumberDecimalSeparator = "."
    };

    private readonly float _value;

    public TiNumber(float value)
    {
        _value = value;
    }

    public TiNumber(int value)
    {
        _value = value;
    }

    public TiNumber ToInt()
    {
        return new TiNumber((int)_value);
    }

    public readonly bool IsInt()
    {
        return (int)_value == _value;
    }

    public readonly int CompareTo(TiNumber obj)
    {
        return _value.CompareTo(obj._value);
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is float floatObj)
        {
            return floatObj == _value;
        }
        if (obj is TiNumber numberObj)
        {
            return numberObj._value == _value;
        }
        return false;
    }

    public bool Equals(TiNumber other)
    {
        return other._value == _value;
    }

    public override readonly int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override readonly string ToString()
    {
        return _value.ToString(_nfi);
    }

    public static bool operator ==(TiNumber a, TiNumber b) => a.Equals(b);
    public static bool operator !=(TiNumber a, TiNumber b) => !a.Equals(b);
    public static bool operator <(TiNumber a, TiNumber b) => a.CompareTo(b) < 0;
    public static bool operator >(TiNumber a, TiNumber b) => a.CompareTo(b) > 0;
    public static bool operator <=(TiNumber a, TiNumber b) => a.CompareTo(b) <= 0;
    public static bool operator >=(TiNumber a, TiNumber b) => a.CompareTo(b) >= 0;

    public static TiNumber operator +(TiNumber left, TiNumber right) => new TiNumber(left._value + right._value);

    public static TiNumber operator -(TiNumber left, TiNumber right) => new TiNumber(left._value - right._value);

    public static TiNumber operator *(TiNumber left, TiNumber right) => new TiNumber(left._value * right._value);

    public static TiNumber operator /(TiNumber left, TiNumber right)
    {
        if (right == (TiNumber)0)
        {
            throw RuntimeError.DivideByZero;
        }

        return new TiNumber(left._value / right._value);
    }

    public static TiNumber operator ^(TiNumber left, TiNumber right)
    {
        if (left == (TiNumber)0 && (right == (TiNumber)0 || right < (TiNumber)0))
        {
            throw RuntimeError.Domain;
        }

        return (TiNumber)MathF.Pow(left, right);
    }

    public static TiNumber operator -(TiNumber num) => new TiNumber(-num._value);


    public static implicit operator TiNumber(float value) => new TiNumber(value);

    public static implicit operator TiNumber(int value) => new TiNumber(value);

    public static implicit operator float(TiNumber num) => num._value;

    public static implicit operator int(TiNumber num)
    {
        Debug.Assert(num.IsInt(), "Number is not an int");
        return (int)num._value;
    }
}
