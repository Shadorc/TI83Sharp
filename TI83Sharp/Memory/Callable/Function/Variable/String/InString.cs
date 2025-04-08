
namespace TI83Sharp;

public class InString : Function
{
    public InString() : base("inString(", 2, 3)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        if (arg1 is not string haystack)
        {
            throw RuntimeError.DataType;
        }

        var arg2 = interpreter.Evaluate(arguments[1]);
        if (arg2 is not string needle)
        {
            throw RuntimeError.DataType;
        }

        var start = (TiNumber)1;
        if (arguments.Count == 3)
        {
            var arg3 = interpreter.Evaluate(arguments[2]);
            if (arg3 is not TiNumber startArg)
            {
                throw RuntimeError.DataType;
            }

            if (!startArg.IsInt() || startArg < 1)
            {
                throw RuntimeError.Domain;
            }

            start = startArg;
        }

        return (TiNumber)(haystack.IndexOf(needle, start - 1) + 1);
    }
}
