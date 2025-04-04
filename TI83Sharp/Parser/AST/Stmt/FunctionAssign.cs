namespace TI83Sharp;

public class FunctionAssign : Stmt
{
    public readonly Expr Value;
    public readonly FunctionCall FunctionCall;

    public FunctionAssign(Expr value, FunctionCall functionCall)
    {
        Value = value;
        FunctionCall = functionCall;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitFunctionAssign(this);
    }
}
