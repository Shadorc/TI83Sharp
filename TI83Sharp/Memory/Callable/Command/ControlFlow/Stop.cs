
namespace TI83Sharp;

public class Stop : Command
{
    public Stop() : base("Stop")
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        throw new StopException();
    }
}
