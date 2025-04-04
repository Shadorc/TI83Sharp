namespace TI83Sharp;

public abstract class Expr
{
    public abstract T Accept<T>(IExprVisitor<T> visitor);
}
