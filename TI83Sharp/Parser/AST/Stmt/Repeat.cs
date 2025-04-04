namespace TI83Sharp;

public class Repeat : Stmt
{
    public readonly Expr Condition;
    public readonly Stmt Body;

    public Repeat(Expr condition, Stmt body)
    {
        Condition = condition;
        Body = body;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitRepeat(this);
    }
}
