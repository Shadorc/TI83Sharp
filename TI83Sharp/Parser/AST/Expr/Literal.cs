namespace TI83Sharp;

public class Literal : Expr
{
    public readonly object Value;

    public Literal(object value)
    {
        Value = value;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitLiteral(this);
    }
}
