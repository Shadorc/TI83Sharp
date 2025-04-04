namespace TI83Sharp;

public class Grouping : Expr
{
    public readonly Expr Expr;

    public Grouping(Expr expr)
    {
        Expr = expr;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitGrouping(this);
    }
}
