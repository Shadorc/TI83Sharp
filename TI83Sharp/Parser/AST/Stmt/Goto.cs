namespace TI83Sharp;

public class Goto : Stmt
{
    public readonly string GotoLabel;

    public Goto(string label)
    {
        GotoLabel = label;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitGoto(this);
    }
}
