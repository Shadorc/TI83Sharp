namespace TI83Sharp;

public class Lbl : Stmt
{
    public Lbl(string label) : base(label)
    {
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitLbl(this);
    }
}
