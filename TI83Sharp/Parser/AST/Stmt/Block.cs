namespace TI83Sharp;

public class Block : Stmt
{
    public readonly List<Stmt> Statements;

    public Block(List<Stmt> statements)
    {
        Statements = statements;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitBlock(this);
    }
}
