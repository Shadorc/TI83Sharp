namespace TI83Sharp;

public class Noop : Stmt
{
    public Noop()
    {
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitNoop(this);
    }
}
