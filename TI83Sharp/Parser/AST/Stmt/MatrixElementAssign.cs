namespace TI83Sharp;

public class MatrixElementAssign : Stmt
{
    public readonly Expr Value;
    public readonly Token MatrixName;
    public readonly Expr Row;
    public readonly Expr Column;

    public MatrixElementAssign(Expr value, Token matrixName, Expr row, Expr column)
    {
        Value = value;
        MatrixName = matrixName;
        Row = row;
        Column = column;
    }

    public override void Accept(IStmtVisitor visitor)
    {
        visitor.VisitMatrixElementAssign(this);
    }
}
