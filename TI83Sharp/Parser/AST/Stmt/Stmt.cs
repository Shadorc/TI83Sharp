namespace TI83Sharp;

public abstract class Stmt
{
    public readonly string? Label;

    public Stmt()
    {
        Label = null;
    }

    public Stmt(string label)
    {
        Label = label;
    }

    public abstract void Accept(IStmtVisitor visitor);
}
