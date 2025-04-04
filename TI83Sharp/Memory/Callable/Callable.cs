namespace TI83Sharp;

public abstract class Callable
{
    public readonly string Name;
    public readonly int ArityMin;
    public readonly int ArityMax;

    public Callable(string name) : this(name, 0)
    {
    }

    public Callable(string name, int arity) : this(name, arity, arity)
    {
    }

    public Callable(string name, int arityMin, int arityMax)
    {
        Name = name;
        ArityMin = arityMin;
        ArityMax = arityMax;
    }

    public override string ToString()
    {
        return Name;
    }
}
