namespace TI83Sharp;

public class RandInt : Function
{
    public RandInt() : base("randInt(", 1, 3)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        var arg2 = interpreter.Evaluate(arguments[1]);
        if (arg1 is not TiNumber num1 || arg2 is not TiNumber num2)
        {
            throw RuntimeError.DataType;
        }

        if (!num1.IsInt() || !num2.IsInt())
        {
            throw RuntimeError.Domain;
        }

        var min = num1;
        var max = num2;

        if (arguments.Count == 2)
        {
            return RandRange(interpreter.Random, min, max);
        }
        else
        {
            var arg3 = interpreter.Evaluate(arguments[2]);
            if (arg3 is not TiNumber arg3Num)
            {
                throw RuntimeError.DataType;
            }

            if (!arg3Num.IsInt())
            {
                throw RuntimeError.Domain;
            }

            var n = arg3Num;
            var runtimeList = new TiList(n);
            for (int i = 0; i < n; i++)
            {
                runtimeList.Add(RandRange(interpreter.Random, min, max));
            }
            return runtimeList;
        }
    }

    private static TiNumber RandRange(Random rand, TiNumber min, TiNumber max)
    {
        return rand.Next(min, max + 1);
    }
}
