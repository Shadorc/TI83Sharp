namespace TI83Sharp;

public class Sum : Function
{
    public Sum() : base("sum(", 1, 3)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        if (arg1 is not TiList list)
        {
            throw RuntimeError.DataType;
        }

        var start = (TiNumber)1;
        if (arguments.Count > 1)
        {
            var arg2 = interpreter.Evaluate(arguments[1]);
            if (arg2 is not TiNumber number || !number.IsInt() || number < 1)
            {
                throw RuntimeError.Domain;
            }
            start = number;
        }

        var end = (TiNumber)list.Count;
        if (arguments.Count > 2)
        {
            var arg3 = interpreter.Evaluate(arguments[2]);
            if (arg3 is not TiNumber number || !number.IsInt() || number < 1)
            {
                throw RuntimeError.Domain;
            }
            end = number;
        }

        if (start >= list.Count + 1 || end >= list.Count + 1 || start > end)
        {
            throw RuntimeError.InvalidDim;
        }

        var sum = (TiNumber)0;
        for (var i = start - 1; i < end; i++)
        {
            sum += list[i];
        }

        return sum;
    }
}
