namespace TI83Sharp;

public class Expression : Stmt
{
    public readonly Expr Expr;

    public Expression(Expr expr)
    {
        Expr = expr;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitExpression(this);
    }
}
