
namespace TI83Sharp;

public class Dim : Assignable
{
    public Dim() : base("dim(", 1)
    {
    }

    public override void Assign(Interpreter interpreter, List<Expr> arguments, object value)
    {
        var arg = arguments[0];
        if (arg is not Variable variable)
        {
            throw RuntimeError.DataType;
        }

        var env = interpreter.Environment;
        if (variable.Name.Type == TokenType.ListId)
        {
            if (value is not TiNumber size)
            {
                throw RuntimeError.DataType;
            }

            if (!size.IsInt())
            {
                throw RuntimeError.Domain;
            }

            if (size > 999)
            {
                throw RuntimeError.InvalidDim;
            }

            var variableName = variable.Name.Lexeme;
            var list = env.Exists(variableName) ? env.Get<TiList>(variableName) : new TiList(size);
            if (list.Count != size)
            {
                if (list.Count > size)
                {
                    list.RemoveRange(size, list.Count - size);
                }
                else
                {
                    list.AddRange(Enumerable.Repeat((TiNumber)0, size - list.Count));
                }
            }
            env.Set(variableName, list);
        }
        else if (variable.Name.Type == TokenType.MatrixId)
        {
            if (value is not TiList size)
            {
                throw RuntimeError.DataType;
            }

            var rows = size[0];
            var cols = size[1];
            if (!rows.IsInt() || !cols.IsInt())
            {
                throw RuntimeError.Domain;
            }

            if (rows > 99 || cols > 99)
            {
                throw RuntimeError.InvalidDim;
            }

            var variableName = variable.Name.Lexeme;
            var matrix = env.Exists(variableName) ? env.Get<TiMatrix>(variableName) : new TiMatrix(rows, cols);
            if (matrix.Rows != rows || matrix.Cols != cols)
            {
                var rowDiff = Math.Abs(rows - matrix.Rows);
                if (matrix.Rows < rows)
                {
                    matrix.AddRow(rowDiff);
                }
                else if (matrix.Rows > rows)
                {
                    matrix.RemoveRow(rowDiff);
                }

                var colDiff = Math.Abs(cols - matrix.Cols);
                if (matrix.Cols < cols)
                {
                    matrix.AddColumn(colDiff);
                }
                else if (matrix.Cols > cols)
                {
                    matrix.RemoveColumn(colDiff);
                }
            }

            env.Set(variableName, matrix);
        }
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is TiList list)
        {
            return (TiNumber)list.Count;
        }
        else if (arg is TiMatrix matrix)
        {
            return new TiList() { matrix.Rows, matrix.Cols };
        }

        throw RuntimeError.DataType;
    }
}
