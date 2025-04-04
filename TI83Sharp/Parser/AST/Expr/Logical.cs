namespace TI83Sharp;

public class Logical : Expr
{
    public readonly Expr Left;
    public readonly Token Op;
    public readonly Expr Right;

    public Logical(Expr left, Token op, Expr right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitLogical(this);
    }
}
