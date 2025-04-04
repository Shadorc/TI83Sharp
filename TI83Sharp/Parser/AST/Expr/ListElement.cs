namespace TI83Sharp;

public class ListElement : Expr
{
    public readonly Token ListName;
    public readonly Expr Index;

    public ListElement(Token listName, Expr index)
    {
        ListName = listName;
        Index = index;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitListElement(this);
    }
}