namespace TI83Sharp;

// An assignable is a function to which a value can be assigned
public abstract class Assignable : Function
{
    public Assignable(string name) : base(name)
    {
    }

    public Assignable(string name, int arity) : base(name, arity)
    {
    }

    public Assignable(string name, int arityMin, int arityMax) : base(name, arityMin, arityMax)
    {
    }

    public abstract void Assign(Interpreter interpreter, List<Expr> arguments, object value);
}
