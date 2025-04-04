namespace TI83Sharp;

public class ListElementAssign : Stmt
{
    public readonly Expr Value;
    public readonly Token ListName;
    public readonly Expr Index;

    public ListElementAssign(Expr value, Token listName, Expr index)
    {
        Value = value;
        ListName = listName;
        Index = index;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitListElementAssign(this);
    }
}
