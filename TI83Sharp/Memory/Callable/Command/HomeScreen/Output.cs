
namespace TI83Sharp;

public class Output : Command
{
    public Output() : base("Output(", 3)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        var arg2 = interpreter.Evaluate(arguments[1]);
        var arg3 = interpreter.Evaluate(arguments[2]);

        if (arg1 is not TiNumber row || arg2 is not TiNumber column)
        {
            throw RuntimeError.DataType;
        }

        interpreter.Output.Message(arg3.ToString()!, column, row);
    }
}
