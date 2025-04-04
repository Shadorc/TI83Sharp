namespace TI83Sharp;

public class While : Stmt
{
    public readonly Expr Condition;
    public readonly Stmt Body;

    public While(Expr condition, Stmt body)
    {
        Condition = condition;
        Body = body;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitWhile(this);
    }
}
