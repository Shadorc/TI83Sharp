namespace TI83Sharp;

public class Assign : Stmt
{
    public readonly Expr Value;
    public readonly Token Name;

    public Assign(Expr value, Token name)
    {
        Value = value;
        Name = name;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitAssign(this);
    }
}
