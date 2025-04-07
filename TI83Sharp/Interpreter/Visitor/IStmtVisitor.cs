namespace TI83Sharp;

public interface IStmtVisitor
{
    void VisitAssign(Assign assignStmt);
    void VisitListElementAssign(ListElementAssign assignListElement);
    void VisitBlock(Block block);
    void VisitCommandCall(CommandCall commandCall);
    void VisitExpression(Expression expressionStmt);
    void VisitFor(For forStmt);
    void VisitFunctionAssign(FunctionAssign functionAssign);
    void VisitGoto(Goto @goto);
    void VisitIf(If ifStmt);
    void VisitLbl(Lbl lbl);
    void VisitRepeat(Repeat repeatStmt);
    void VisitWhile(While whileStmt);
    void VisitMatrixElementAssign(MatrixElementAssign matrixElementAssign);
    void VisitNoop(Noop noop);
}
