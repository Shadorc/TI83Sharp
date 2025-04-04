
namespace TI83Sharp;

public class Return : Command
{
    public Return() : base("Return")
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        throw new ReturnException();
    }
}
