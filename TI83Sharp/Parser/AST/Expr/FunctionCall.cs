namespace TI83Sharp;

public class FunctionCall : Expr
{
    public readonly Expr Callee;
    public readonly Token Variable;
    public readonly List<Expr> Arguments;

    public FunctionCall(Expr callee, Token variable, List<Expr> arguments)
    {
        Callee = callee;
        Variable = variable;
        Arguments = arguments;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitFunctionCall(this);
    }
}
