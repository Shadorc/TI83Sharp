namespace TI83Sharp;

public class Variable : Expr
{
    public readonly Token Name;

    public Variable(Token name)
    {
        Name = name;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitVariable(this);
    }
}
