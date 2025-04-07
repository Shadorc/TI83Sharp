
namespace TI83Sharp;

public class Length : Function
{
    public Length() : base("length(", 1)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is not string str)
        {
            throw RuntimeError.DataType;
        }

        // TODO: The length is measured in the number of tokens, and not the number of letters in the string

        return (TiNumber)str.Length;
    }
}
