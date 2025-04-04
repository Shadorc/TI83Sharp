namespace TI83Sharp;

public class LiteralList : Expr
{
    public readonly List<Expr> Items;

    public LiteralList(List<Expr> items)
    {
        Items = items;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitLiteralList(this);
    }
}
