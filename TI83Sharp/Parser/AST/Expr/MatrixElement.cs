namespace TI83Sharp;

public class MatrixElement : Expr
{
    public readonly Token MatrixName;
    public readonly Expr Row;
    public readonly Expr Column;

    public MatrixElement(Token matrixName, Expr row, Expr column)
    {
        MatrixName = matrixName;
        Row = row;
        Column = column;
    }

    public override T Accept<T>(IExprVisitor<T> visitor)
    {
        return visitor.VisitMatrixElement(this);
    }
}