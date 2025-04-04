namespace TI83Sharp;

public class Binary : Expr
{
    public readonly Expr Left;
    public readonly Token Op;
    public readonly Expr Right;

    public Binary(Expr left, Token op, Expr right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitBinary(this);
    }
}
