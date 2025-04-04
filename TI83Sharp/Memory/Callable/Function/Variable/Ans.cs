
namespace TI83Sharp;

public class Ans : Function
{
    public Ans() : base ("Ans")
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        return interpreter.Environment.Get<object>(Environment.ANS_VALUE);
    }
}
