namespace TI83Sharp;

public class Round : Function
{
    public Round() : base("round(", 1, 2)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        var arg2 = arguments.Count > 1 ? interpreter.Evaluate(arguments[1]) : (TiNumber)9;
        if (arg2 is not TiNumber decimals)
        {
            throw RuntimeError.DataType;
        }

        if (decimals < 0 || decimals > 9)
        {
            throw RuntimeError.Domain;
        }

        if (arg1 is TiNumber number)
        {
            return (TiNumber)ApplyFunction(number, decimals);
        }
        else if (arg1 is TiList list)
        {
            var results = new TiList(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                results.Add(ApplyFunction(list[i], decimals));
            }
            return results;
        }

        throw RuntimeError.DataType;
    }

    private static float ApplyFunction(TiNumber number, TiNumber decimals)
    {
        return (float)Math.Round((float)number, decimals);
    }
}
