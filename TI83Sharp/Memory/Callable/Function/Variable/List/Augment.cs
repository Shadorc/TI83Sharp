namespace TI83Sharp;

public class Augment : Function
{
    public Augment() : base("augment(", 2)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        var arg2 = interpreter.Evaluate(arguments[1]);
        if (arg1 is TiList list1 && arg2 is TiList list2)
        {
            if (list1.Count == 0 || list2.Count == 0 || list1.Count + list2.Count > 999)
            {
                throw RuntimeError.InvalidDim;
            }

            var result = new TiList();
            result.AddRange(list1);
            result.AddRange(list2);
            return result;
        }
        else if (arg1 is TiMatrix matrix1 && arg2 is TiMatrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows)
            {
                throw RuntimeError.DimMismatch;
            }

            if (matrix1.Cols + matrix2.Cols > 99)
            {
                throw RuntimeError.InvalidDim;
            }

            var result = new TiMatrix(matrix1.Rows, matrix1.Cols + matrix2.Cols);
            for (var i = 0; i < matrix1.Rows; i++)
            {
                for (var j = 0; j < matrix1.Cols; j++)
                {
                    result[i][j] = matrix1[i][j];
                }
                for (var j = 0; j < matrix2.Cols; j++)
                {
                    result[i][j + matrix1.Cols] = matrix2[i][j];
                }
            }
            return result;
        }
        else
        {
            throw RuntimeError.DataType;
        }
    }
}
