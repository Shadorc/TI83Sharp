namespace TI83Sharp;

public class If : Stmt
{
    public readonly Expr Condition;
    public readonly Stmt ThenBranch;
    public readonly Stmt? ElseBranch;

    public If(Expr condition, Stmt thenBranch, Stmt? elseBranch)
    {
        Condition = condition;
        ThenBranch = thenBranch;
        ElseBranch = elseBranch;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitIf(this);
    }
}
