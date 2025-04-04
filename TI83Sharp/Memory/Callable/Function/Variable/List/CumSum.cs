namespace TI83Sharp;

public class CumSum : Function
{
    public CumSum() : base("cumSum(", 1)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is TiList list)
        {
            var result = new TiList(list.Count);
            var sum = (TiNumber)0;
            foreach (var item in list)
            {
                sum += item;
                result.Add(sum);
            }
            return result;
        }
        else if (arg is TiMatrix matrix)
        {
            var result = new TiMatrix(matrix.Rows, matrix.Cols);
            for (var col = 0; col < matrix.Cols; col++)
            {
                var sum = (TiNumber)0;
                for (var row = 0; row < matrix.Rows; row++)
                {
                    sum += matrix[row, col];
                    result[row, col] = sum;
                }
            }
            return result;
        }

        throw RuntimeError.DataType;
    }
}
