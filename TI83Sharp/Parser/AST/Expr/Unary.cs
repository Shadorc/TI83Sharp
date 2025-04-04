namespace TI83Sharp;

public class Unary : Expr
{
    public readonly Token Op;
    public readonly Expr Right;

    public Unary(Token op, Expr right)
    {
        Op = op;
        Right = right;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitUnary(this);
    }
}
