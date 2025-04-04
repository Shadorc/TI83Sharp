namespace TI83Sharp;

// A function has a return value
public abstract class Function : Callable
{
    protected Function(string name) : base(name)
    {
    }

    protected Function(string name, int arity) : base(name, arity)
    {
    }

    protected Function(string name, int arityMin, int arityMax) : base(name, arityMin, arityMax)
    {
    }

    public abstract object Call(Interpreter interpreter, List<Expr> arguments);
}
