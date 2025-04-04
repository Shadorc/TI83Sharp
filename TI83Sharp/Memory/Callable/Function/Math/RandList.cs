
namespace TI83Sharp;

public class RandList : Function
{
    public RandList() : base("rand(", 1)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is not TiNumber number)
        {
            throw RuntimeError.DataType;
        }

        if (!number.IsInt() || number < 1 || number > 999)
        {
            throw RuntimeError.Domain;
        }

        var result = new TiList(number);
        for (int i = 0; i < number; i++)
        {
            result.Add(interpreter.Random.NextSingle());
        }

        return result;
    }
}
