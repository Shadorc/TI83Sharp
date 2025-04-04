namespace TI83Sharp;

// A command has no return value
public abstract class Command : Callable
{
    protected Command(string name) : base(name)
    {
    }

    protected Command(string name, int arity) : base(name, arity)
    {
    }

    protected Command(string name, int arityMin, int arityMax) : base(name, arityMin, arityMax)
    {
    }

    public abstract void Call(Interpreter interpreter, List<Expr> arguments);
}
