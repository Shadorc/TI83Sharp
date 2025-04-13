
namespace TI83Sharp;

public class ClrHome : Command
{
    public ClrHome() : base("ClrHome")
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        interpreter.HomeScreen.Clear();
    }
}
