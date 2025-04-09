namespace TI83Sharp;

public class Interpreter : IExprVisitor<object>, IStmtVisitor
{
    public readonly IOutput Output;
    public readonly IInput Input;
    public readonly Environment Environment;

    internal Random Random;

    public Interpreter(IOutput output, IInput input)
    {
        Output = output;
        Input = input;
        Environment = new Environment();

        Random = new Random();
    }

    public void Interpret(List<Stmt> statements)
    {
        string? label = null;
        do
        {
            try
            {
                var startIdx = GetStartIdx(statements, label);
                for (int idx = startIdx; idx < statements.Count; idx++)
                {
                    Execute(statements[idx]);
                }
                label = null;
            }
            catch (GotoException gotoException)
            {
                label = gotoException.Label;
            }
            catch (StopException)
            {
                break;
            }
            catch (ReturnException)
            {
                break;
            }
        } while (label != null);
    }

    internal void SetRandomSeed(int seed)
    {
        Random = new Random(seed);
    }

    private static int GetStartIdx(List<Stmt> statements, string? label = null)
    {
        if (label == null)
        {
            return 0;
        }

        for (int i = 0; i < statements.Count; i++)
        {
            if (statements[i].Label == label)
            {
                return i;
            }
        }

        throw RuntimeError.Label;
    }

    #region Expr
    public object VisitVariable(Variable variable)
    {
        var name = variable.Name.Lexeme;
        return Environment.Get<object>(name);
    }

    public object VisitLiteral(Literal literal)
    {
        return literal.Value;
    }

    public object VisitGrouping(Grouping grouping)
    {
        return Evaluate(grouping.Expr);
    }

    public object VisitUnary(Unary unary)
    {
        var right = Evaluate(unary.Right);
        if (unary.Op.Type == TokenType.Minus)
        {
            if (right is TiNumber rightNumber)
            {
                return -rightNumber;
            }
        }

        throw RuntimeError.DataType;
    }

    public object VisitBinary(Binary binary)
    {
        var left = Evaluate(binary.Left);
        var right = Evaluate(binary.Right);

        if (binary.Op.Type == TokenType.Plus)
        {
            if (left is string leftStr && right is string rightStr)
            {
                return leftStr + rightStr;
            }
            else if (left is TiNumber leftNumber)
            {
                if (right is TiNumber rightNumber)
                {
                    return leftNumber + rightNumber;
                }
                else if (right is TiList rightList)
                {
                    return leftNumber + rightList;
                }
            }
            else if (left is TiList leftList)
            {
                if (right is TiList rightList)
                {
                    return leftList + rightList;
                }
                else if (right is TiNumber rightNumber)
                {
                    return leftList + rightNumber;
                }
            }
            else if (left is TiMatrix leftMatrix && right is TiMatrix rightMatrix)
            {
                return leftMatrix + rightMatrix;
            }

            throw new RuntimeError(binary.Op, RuntimeError.DataType.Message);
        }
        else if (binary.Op.Type == TokenType.Minus)
        {
            if (left is TiNumber leftNumber)
            {
                if (right is TiNumber rightNumber)
                {
                    return leftNumber - rightNumber;
                }
                else if (right is TiList rightList)
                {
                    return leftNumber - rightList;
                }
            }
            else if (left is TiList leftList)
            {
                if (right is TiList rightList)
                {
                    return leftList - rightList;
                }
                else if (right is TiNumber rightNumber)
                {
                    return leftList - rightNumber;
                }
            }
            else if (left is TiMatrix leftMatrix && right is TiMatrix rightMatrix)
            {
                return leftMatrix - rightMatrix;
            }

            throw new RuntimeError(binary.Op, RuntimeError.DataType.Message);
        }
        else if (binary.Op.Type == TokenType.Mult)
        {
            if (left is TiNumber leftNumber && right is TiNumber rightNumber)
            {
                return leftNumber * rightNumber;
            }
            else if (left is TiList leftList && right is TiList rightList)
            {
                return leftList * rightList;
            }
            else if (left is TiMatrix leftMatrix && right is TiMatrix rightMatrix)
            {
                return leftMatrix * rightMatrix;
            }

            throw new RuntimeError(binary.Op, RuntimeError.DataType.Message);
        }
        else if (binary.Op.Type == TokenType.Div)
        {
            if (left is TiNumber leftNumber && right is TiNumber rightNumber)
            {
                return leftNumber / rightNumber;
            }
            else if (left is TiList leftList && right is TiList rightList)
            {
                return leftList / rightList;
            }

            throw new RuntimeError(binary.Op, RuntimeError.DataType.Message);
        }
        else if (binary.Op.Type == TokenType.Pow || binary.Op.Type == TokenType.Square || binary.Op.Type == TokenType.Cube)
        {
            if (right is TiNumber rightNumber)
            {
                if (left is TiNumber leftNumber)
                {
                    return leftNumber ^ rightNumber;
                }
                else if (left is TiList leftList)
                {
                    return leftList ^ rightNumber;
                }
                else if (left is TiMatrix leftMatrix)
                {
                    return leftMatrix ^ rightNumber;
                }
            }

            throw new RuntimeError(binary.Op, RuntimeError.DataType.Message);
        }
        else if (binary.Op.Type == TokenType.Greater)
        {
            return LogicHelper.BoolToNumber((TiNumber)left > (TiNumber)right);
        }
        else if (binary.Op.Type == TokenType.GreaterEqual)
        {
            return LogicHelper.BoolToNumber((TiNumber)left >= (TiNumber)right);
        }
        else if (binary.Op.Type == TokenType.Less)
        {
            return LogicHelper.BoolToNumber((TiNumber)left < (TiNumber)right);
        }
        else if (binary.Op.Type == TokenType.LessEqual)
        {
            return LogicHelper.BoolToNumber((TiNumber)left <= (TiNumber)right);
        }
        else if (binary.Op.Type == TokenType.NotEqual)
        {
            if (left is TiList leftList && right is TiList rightList)
            {
                return leftList.TiNotEquals(rightList);
            }
            return LogicHelper.BoolToNumber(!Equals(left, right));
        }
        else if (binary.Op.Type == TokenType.Equal)
        {
            if (left is TiList leftList && right is TiList rightList)
            {
                return leftList.TiEquals(rightList);
            }
            return LogicHelper.BoolToNumber(Equals(left, right));
        }

        throw RuntimeError.Syntax;
    }

    public object VisitLogical(Logical logical)
    {
        var left = Evaluate(logical.Left);

        if (logical.Op.Type == TokenType.Or)
        {
            if (LogicHelper.IsTrue(left))
            {
                return left;
            }
        }
        else if (logical.Op.Type == TokenType.And)
        {
            if (!LogicHelper.IsTrue(left))
            {
                return left;
            }
        }
        else if (logical.Op.Type == TokenType.Xor)
        {
            var right = Evaluate(logical.Right);
            return LogicHelper.BoolToNumber(LogicHelper.IsTrue(left) ^ LogicHelper.IsTrue(right));
        }

        return Evaluate(logical.Right);
    }

    public object VisitFunctionCall(FunctionCall functionCall)
    {
        var function = (Function)Evaluate(functionCall.Callee);

        var arguments = functionCall.Arguments;
        if (arguments.Count < function.ArityMin || function.ArityMax < arguments.Count)
        {
            throw new RuntimeError(functionCall.Variable, RuntimeError.Argument.Message);
        }

        return function.Call(this, arguments);
    }

    public object VisitLiteralList(LiteralList literalList)
    {
        var items = literalList.Items;
        var runtimeList = new TiList(items.Count);
        foreach (var item in items)
        {
            runtimeList.Add((TiNumber)Evaluate(item));
        }
        return runtimeList;
    }

    public object VisitLiteralMatrix(LiteralMatrix literalMatrix)
    {
        var items = literalMatrix.Items;
        var runtimeMatrix = new TiMatrix(items.Count, items[0].Count);
        for (var i = 0; i < items.Count; i++)
        {
            for (var j = 0; j < items[i].Count; j++)
            {
                runtimeMatrix[i][j] = (TiNumber)Evaluate(items[i][j]);
            }
        }
        return runtimeMatrix;
    }

    public object VisitListElement(ListElement listElement)
    {
        var list = Environment.Get<TiList>(listElement.ListName.Lexeme);
        var index = Evaluate(listElement.Index);
        if (index is not TiNumber indexNumber || indexNumber < 1 || indexNumber > list.Count)
        {
            throw RuntimeError.InvalidDim;
        }
        return list[indexNumber - 1];
    }

    public object VisitMatrixElement(MatrixElement matrixElement)
    {
        var matrix = Environment.Get<TiMatrix>(matrixElement.MatrixName.Lexeme);
        var row = Evaluate(matrixElement.Row);
        if (row is not TiNumber rowNumber || rowNumber < 1 || rowNumber > matrix.Rows)
        {
            throw RuntimeError.InvalidDim;
        }
        var column = Evaluate(matrixElement.Column);
        if (column is not TiNumber columnNumber || columnNumber < 1 || columnNumber > matrix.Cols)
        {
            throw RuntimeError.InvalidDim;
        }

        return matrix[rowNumber - 1][columnNumber - 1];
    }

    public object Evaluate(Expr node)
    {
        return node.Accept(this);
    }

    #endregion

    #region Stmt
    public void VisitNoop(Noop noop)
    {
        // Do nothing
    }

    public void VisitBlock(Block block)
    {
        foreach (var statement in block.Statements)
        {
            Execute(statement);
        }
    }

    public void VisitAssign(Assign assignStmt)
    {
        var value = Evaluate(assignStmt.Value);
        var varToken = assignStmt.Name;

        bool isValid = varToken.Type == TokenType.NumberId && value is TiNumber
                    || varToken.Type == TokenType.StringId && value is string
                    || varToken.Type == TokenType.ListId && value is TiList
                    || varToken.Type == TokenType.MatrixId && value is TiMatrix;
        if (!isValid)
        {
            throw new RuntimeError(varToken, RuntimeError.DataType.Message);
        }

        Environment.Set(varToken.Lexeme, value);
        Environment.Set(Environment.ANS_VALUE, value);
    }

    public void VisitListElementAssign(ListElementAssign listElementAssign)
    {
        var value = Evaluate(listElementAssign.Value);
        if (value is not TiNumber valueNumber)
        {
            throw RuntimeError.DataType;
        }

        var list = Environment.Get<TiList>(listElementAssign.ListName.Lexeme);
        var index = Evaluate(listElementAssign.Index);
        if (index is not TiNumber indexNumber || indexNumber < 1 || indexNumber > list.Count + 1)
        {
            throw RuntimeError.InvalidDim;
        }

        if (indexNumber > list.Count)
        {
            list.Add(valueNumber);
        }
        else
        {
            list[indexNumber - 1] = valueNumber;
        }
    }

    public void VisitMatrixElementAssign(MatrixElementAssign matrixElementAssign)
    {
        var value = Evaluate(matrixElementAssign.Value);
        if (value is not TiNumber valueNumber)
        {
            throw RuntimeError.DataType;
        }

        var matrix = Environment.Get<TiMatrix>(matrixElementAssign.MatrixName.Lexeme);
        var row = Evaluate(matrixElementAssign.Row);
        if (row is not TiNumber rowNumber || rowNumber < 1 || rowNumber > matrix.Rows)
        {
            throw RuntimeError.InvalidDim;
        }
        var column = Evaluate(matrixElementAssign.Column);
        if (column is not TiNumber columnNumber || columnNumber < 1 || columnNumber > matrix.Cols)
        {
            throw RuntimeError.InvalidDim;
        }

        matrix[rowNumber - 1][columnNumber - 1] = valueNumber;
    }

    public void VisitFunctionAssign(FunctionAssign functionAssign)
    {
        var value = Evaluate(functionAssign.Value);
        var functionCall = functionAssign.FunctionCall;

        var function = (Assignable)Evaluate(functionCall.Callee);

        var arguments = functionCall.Arguments;
        if (arguments.Count < function.ArityMin || function.ArityMax < arguments.Count)
        {
            throw new RuntimeError(functionCall.Variable, RuntimeError.Argument.Message);
        }

        function.Assign(this, arguments, value);
    }

    public void VisitExpression(Expression expressionStmt)
    {
        var value = Evaluate(expressionStmt.Expr);
        Environment.Set(Environment.ANS_VALUE, value);
        Output.Message(value.ToString()!);
    }

    public void VisitIf(If ifStmt)
    {
        if (LogicHelper.IsTrue(Evaluate(ifStmt.Condition)))
        {
            Execute(ifStmt.ThenBranch);
        }
        else if (ifStmt.ElseBranch != null)
        {
            Execute(ifStmt.ElseBranch);
        }
    }

    public void VisitWhile(While whileStmt)
    {
        var body = whileStmt.Body;
        while (LogicHelper.IsTrue(Evaluate(whileStmt.Condition)))
        {
            Execute(body);
        }
    }

    public void VisitRepeat(Repeat repeatStmt)
    {
        var body = repeatStmt.Body;
        do
        {
            Execute(body);
        } while (!LogicHelper.IsTrue(Evaluate(repeatStmt.Condition)));
    }

    public void VisitFor(For forStmt)
    {
        var varName = forStmt.Variable.Name.Lexeme;
        var start = (TiNumber)Evaluate(forStmt.Start);
        var end = (TiNumber)Evaluate(forStmt.End);
        var step = forStmt.Increment == null ? (TiNumber)1 : (TiNumber)Evaluate(forStmt.Increment);
        var body = forStmt.Body;

        Environment.Set(varName, start);

        bool isIncrementing = step > (TiNumber)0;
        while (isIncrementing ? Environment.Get<TiNumber>(varName) <= end : Environment.Get<TiNumber>(varName) >= end)
        {
            Execute(body);
            Environment.Set(varName, Environment.Get<TiNumber>(varName) + step);
        }
    }

    public void VisitCommandCall(CommandCall commandCallStmt)
    {
        var command = (Command)Evaluate(commandCallStmt.Callee);

        var arguments = commandCallStmt.Arguments;
        if (arguments.Count < command.ArityMin || command.ArityMax < arguments.Count)
        {
            throw new RuntimeError(commandCallStmt.Token, RuntimeError.Domain.Message);
        }

        command.Call(this, arguments);
    }

    public void VisitLbl(Lbl lblStmt)
    {
        // Do nothing
    }

    public void VisitGoto(Goto gotoStmt)
    {
        throw new GotoException(gotoStmt.GotoLabel);
    }

    private void Execute(Stmt statement)
    {
        statement.Accept(this);
    }
    #endregion
}
