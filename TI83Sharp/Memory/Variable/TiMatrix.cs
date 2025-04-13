using System.Text;

namespace TI83Sharp;

public class TiMatrix : IEquatable<TiMatrix>
{
    private readonly List<List<TiNumber>> _matrix = new List<List<TiNumber>>();
    private int _rows;
    private int _cols;

    public int Rows => _rows;
    public int Cols => _cols;

    public List<TiNumber> this[int index]
    {
        get => _matrix[index];
        set => _matrix[index] = value;
    }

    public TiNumber this[int row, int col]
    {
        get => _matrix[row][col];
        set => _matrix[row][col] = value;
    }

    public TiMatrix(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;

        for (var i = 0; i < rows; i++)
        {
            var row = new List<TiNumber>();
            for (var j = 0; j < cols; j++)
            {
                row.Add(new TiNumber(0));
            }
            _matrix.Add(row);
        }
    }

    public TiMatrix(List<List<TiNumber>> matrix)
    {
        _rows = matrix.Count;
        _cols = matrix[0].Count;
        _matrix = matrix;
    }

    public void AddRow(int count)
    {
        var row = new List<TiNumber>(Cols);
        for (int col = 0; col < Cols; ++col)
        {
            row.Add(0);
        }

        for (int i = 0; i < count; ++i)
        {
            _matrix.Add(new List<TiNumber>(row));
            ++_rows;
        }
    }

    public void AddColumn(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            for (int row = 0; row < Rows; ++row)
            {
                _matrix[row].Add(0);
            }

            ++_cols;
        }
    }

    public void RemoveRow(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            _matrix.RemoveAt(Rows - 1);
            --_rows;
        }
    }

    public void RemoveColumn(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            for (int row = 0; row < Rows; ++row)
            {
                _matrix[row].RemoveAt(Cols - 1);
            }

            --_cols;
        }
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as TiMatrix);
    }

    public bool Equals(TiMatrix? other)
    {
        if (other == null || Rows != other.Rows || Cols != other.Cols)
        {
            return false;
        }

        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Cols; j++)
            {
                if (!this[i][j].Equals(other[i][j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return _matrix.GetHashCode();
    }

    public override string ToString()
    {
        var str = new StringBuilder("[");
        for (var i = 0; i < Rows; i++)
        {
            str.Append('[');
            for (var j = 0; j < Cols; j++)
            {
                str.Append(this[i][j].ToString());
            }
            str.Append(']');
        }
        str.Append(']');
        return str.ToString();
    }

    private static TiMatrix PerformOperation(TiMatrix left, TiMatrix right, Func<TiNumber, TiNumber, TiNumber> operation)
    {
        if (left.Rows != right.Rows || left.Cols != right.Cols)
        {
            throw RuntimeError.InvalidDim;
        }

        var result = new TiMatrix(left.Rows, left.Cols);
        for (var i = 0; i < left.Rows; i++)
        {
            for (var j = 0; j < left.Cols; j++)
            {
                result[i][j] = operation(left[i][j], right[i][j]);
            }
        }
        return result;
    }

    private static TiMatrix PerformOperation(TiMatrix matrix, Func<TiNumber, TiNumber> operation)
    {
        var result = new TiMatrix(matrix.Rows, matrix.Cols);
        for (var i = 0; i < matrix.Rows; i++)
        {
            for (var j = 0; j < matrix.Cols; j++)
            {
                result[i][j] = operation(matrix[i][j]);
            }
        }
        return result;
    }

    public static TiMatrix operator +(TiMatrix left, TiMatrix right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum + rightNum);
    }

    public static TiMatrix operator -(TiMatrix left, TiMatrix right)
    {
        return PerformOperation(left, right, (leftNum, rightNum) => leftNum - rightNum);
    }

    public static TiMatrix operator *(TiMatrix left, TiMatrix right)
    {
        if (left.Cols != right.Rows)
        {
            throw RuntimeError.InvalidDim;
        }

        var result = new TiMatrix(left.Rows, right.Cols);
        for (var i = 0; i < left.Rows; i++)
        {
            for (var j = 0; j < right.Cols; j++)
            {
                for (var k = 0; k < left.Cols; k++)
                {
                    result[i][j] += left[i][k] * right[k][j];
                }
            }
        }
        return result;
    }

    public static TiMatrix operator *(TiMatrix left, TiNumber right)
    {
        return PerformOperation(left, (leftNum) => leftNum * right);
    }

    public static TiMatrix operator *(TiNumber left, TiMatrix right)
    {
        return PerformOperation(right, (rightNum) => left * rightNum);
    }

    public static TiMatrix operator ^(TiMatrix matrix, int power)
    {
        if (matrix.Rows != matrix.Cols)
        {
            throw RuntimeError.InvalidDim;
        }

        if (power == 1)
        {
            return matrix;
        }

        return matrix * (matrix ^ (power - 1));
    }
}
