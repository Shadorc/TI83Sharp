namespace TI83Sharp;

public class LiteralMatrix : Expr
{
    public readonly List<List<Expr>> Items;

    public LiteralMatrix(List<List<Expr>> items)
    {
        Items = items;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitLiteralMatrix(this);
    }
}
