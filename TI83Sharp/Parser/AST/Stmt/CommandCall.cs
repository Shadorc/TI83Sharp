namespace TI83Sharp;

public class CommandCall : Stmt
{
    public readonly Expr Callee;
    public readonly Token Token;
    public readonly List<Expr> Arguments;

    public CommandCall(Expr callee, Token token, List<Expr> arguments)
    {
        Callee = callee;
        Token = token;
        Arguments = arguments;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitCommandCall(this);
    }
}
