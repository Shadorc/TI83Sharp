namespace TI83Sharp;

public class ClrAllLists : Command
{
    public ClrAllLists() : base("ClrAllLists")
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        interpreter.Environment.ClearAllLists();
    }
}
