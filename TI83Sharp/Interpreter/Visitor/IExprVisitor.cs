namespace TI83Sharp;

public interface IExprVisitor<T>
{
    T VisitBinary(Binary binary);
    T VisitFunctionCall(FunctionCall functionCall);
    T VisitGrouping(Grouping grouping);
    T VisitListElement(ListElement listElement);
    T VisitLiteral(Literal literal);
    T VisitLiteralList(LiteralList literalList);
    T VisitLiteralMatrix(LiteralMatrix literalMatrix);
    T VisitLogical(Logical logical);
    T VisitMatrixElement(MatrixElement matrixElement);
    T VisitUnary(Unary unary);
    T VisitVariable(Variable variable);
}
