namespace TI83Sharp;

public class For : Stmt
{
    public readonly Variable Variable;
    public readonly Expr Start;
    public readonly Expr End;
    public readonly Expr? Increment;
    public readonly Stmt Body;

    public For(Variable variable, Expr start, Expr end, Expr? increment, Stmt body)
    {
        Variable = variable;
        Start = start;
        End = end;
        Increment = increment;
        Body = body;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitFor(this);
    }
}
