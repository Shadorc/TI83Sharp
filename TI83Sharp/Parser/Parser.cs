using System.Diagnostics.CodeAnalysis;

namespace TI83Sharp;

public class Parser
{
    private readonly List<Token> _tokens;

    private int _currentIdx;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public List<Stmt> Parse()
    {
        var statements = new List<Stmt>();

        while (!IsAtEnd())
        {
            statements.Add(Statement());
        }

        return statements;
    }

    private SyntaxError Error(string message)
    {
        return new SyntaxError(message, Peek());
    }

    private Block Block(params TokenType[] tokenTypes)
    {
        var statements = new List<Stmt>();

        while (!IsAtEnd())
        {
            bool endTokenFound = false;
            foreach (var tokenType in tokenTypes)
            {
                if (Check(TokenType.Colon) && CheckNext(tokenType))
                {
                    endTokenFound = true;
                    break;
                }
            }

            if (endTokenFound)
            {
                break;
            }

            statements.Add(Statement());
        }

        return new Block(statements);
    }

    private Stmt Statement()
    {
        Consume(TokenType.Colon);

        if (Check(TokenType.Colon) || Check(TokenType.Eof))
        {
            return new Noop();
        }
        else if (TryConsume(TokenType.Lbl))
        {
            var name = Consume(TokenType.LblName);
            return new Lbl(name.Lexeme);
        }
        else if (TryConsume(TokenType.Goto))
        {
            var name = Consume(TokenType.LblName);
            return new Goto(name.Lexeme);
        }
        else if (TryConsume(TokenType.If))
        {
            return IfStatement();
        }
        else if (TryConsume(TokenType.While))
        {
            return WhileStatement();
        }
        else if (TryConsume(TokenType.Repeat))
        {
            return RepeatStatement();
        }
        else if (TryConsume(TokenType.For))
        {
            return ForStatement();
        }
        else if (Check(TokenType.CommandId))
        {
            return CommandStatement();
        }
        else
        {
            return ExpressionStatement();
        }
    }

    private If IfStatement()
    {
        var condition = Expression();

        Stmt thenBranch;
        Stmt? elseBranch = null;
        if (Check(TokenType.Colon) && CheckNext(TokenType.Then))
        {
            Consume(TokenType.Colon);
            Consume(TokenType.Then);

            thenBranch = Block(TokenType.Else, TokenType.End);
            Consume(TokenType.Colon);

            if (TryConsume(TokenType.Else))
            {
                elseBranch = Block(TokenType.End);
                Consume(TokenType.Colon);
            }

            Consume(TokenType.End);
        }
        else
        {
            thenBranch = Statement();
        }

        return new If(condition, thenBranch, elseBranch);
    }

    private While WhileStatement()
    {
        var condition = Expression();

        var body = Block(TokenType.End);
        Consume(TokenType.Colon);
        Consume(TokenType.End);

        return new While(condition, body);
    }

    private Repeat RepeatStatement()
    {
        var condition = Expression();

        var body = Block(TokenType.End);
        Consume(TokenType.Colon);
        Consume(TokenType.End);

        return new Repeat(condition, body);
    }

    private For ForStatement()
    {
        Consume(TokenType.LeftParentheses);
        if (!TryConsume(out var nameToken, TokenType.ConstId))
        {
            nameToken = Consume(TokenType.NumberId);
        }

        var variable = new Variable(nameToken);
        Consume(TokenType.Comma);
        var start = Expression();
        Consume(TokenType.Comma);
        var end = Expression();
        Expr? increment = null;
        if (TryConsume(TokenType.Comma))
        {
            increment = Expression();
        }

        if (!IsClosingOptional())
        {
            Consume(TokenType.RightParentheses);
        }

        var body = Block(TokenType.End);
        Consume(TokenType.Colon);
        Consume(TokenType.End);

        return new For(variable, start, end, increment, body);
    }

    private CommandCall CommandStatement()
    {
        var name = Consume(TokenType.CommandId);
        var command = new Variable(name);

        var arguments = new List<Expr>();
        while (!IsAtEnd() && !Check(TokenType.Colon))
        {
            arguments.Add(Expression());
            if (!TryConsume(TokenType.Comma))
            {
                break;
            }
        }

        if (name.Lexeme.EndsWith('(') && !IsClosingOptional())
        {
            Consume(TokenType.RightParentheses);
        }

        return new CommandCall(command, name, arguments);
    }

    private Stmt ExpressionStatement()
    {
        var expr = Expression();

        if (TryConsume(TokenType.Store))
        {
            if (Check(TokenType.FunctionId))
            {
                if (FunctionCall() is not FunctionCall functionCall)
                {
                    throw Error("Expected function");
                }

                return new FunctionAssign(expr, functionCall);
            }
            else
            {
                var nameToken = Consume(TokenType.ConstId, TokenType.NumberId, TokenType.StringId, TokenType.ListId, TokenType.MatrixId);
                if (nameToken.Type == TokenType.ListId && TryConsume(TokenType.LeftParentheses))
                {
                    var index = Expression();
                    if (!IsClosingOptional())
                    {
                        Consume(TokenType.RightParentheses);
                    }

                    return new ListElementAssign(expr, nameToken, index);
                }
                else if (nameToken.Type == TokenType.MatrixId && TryConsume(TokenType.LeftParentheses))
                {
                    var row = Expression();
                    Consume(TokenType.Comma);
                    var column = Expression();
                    if (!IsClosingOptional())
                    {
                        Consume(TokenType.RightParentheses);
                    }

                    return new MatrixElementAssign(expr, nameToken, row, column);
                }
                else
                {
                    return new Assign(expr, nameToken);
                }
            }
        }

        return new Expression(expr);
    }

    private Expr Expression()
    {
        return Or();
    }

    private Expr Or()
    {
        var expr = And();

        while (TryConsume(out var op, TokenType.Or, TokenType.Xor))
        {
            var right = And();
            expr = new Logical(expr, op, right);
        }

        return expr;
    }

    private Expr And()
    {
        var expr = Equality();

        while (TryConsume(out var op, TokenType.And))
        {
            var right = Equality();
            expr = new Logical(expr, op, right);
        }

        return expr;
    }

    private Expr Equality()
    {
        var expr = Comparison();

        while (TryConsume(out var op, TokenType.NotEqual, TokenType.Equal))
        {
            var right = Comparison();
            expr = new Binary(expr, op, right);
        }

        return expr;
    }

    private Expr Comparison()
    {
        var expr = Term();

        while (TryConsume(out var op, TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual))
        {
            var right = Term();
            expr = new Binary(expr, op, right);
        }

        return expr;
    }

    private Expr Term()
    {
        var expr = Factor();

        while (TryConsume(out var op, TokenType.Minus, TokenType.Plus))
        {
            var right = Factor();
            expr = new Binary(expr, op, right);
        }

        return expr;
    }

    private Expr Factor()
    {
        var expr = Unary();

        while (true)
        {
            if (TryConsume(out var op, TokenType.Div, TokenType.Mult))
            {
                var right = Unary();
                expr = new Binary(expr, op, right);
            }
            else if (IsImplicitMult(Peek(-1), Peek()))
            {
                var right = Unary();
                expr = new Binary(expr, new Token(TokenType.Mult, "*"), right);
            }
            else
            {
                break;
            }
        }

        return expr;
    }

    private static bool IsImplicitMult(Token previousToken, Token currentToken)
    {
        // Example conditions for implicit multiplication:
        // - A number followed by a variable or a left parenthesis
        // - A right parenthesis followed by a number, variable, or a left parenthesis
        // - A variable followed by a left parenthesis
        return (previousToken.Type == TokenType.Number && (currentToken.Type == TokenType.NumberId || currentToken.Type == TokenType.LeftParentheses)) ||
            (previousToken.Type == TokenType.RightParentheses && (currentToken.Type == TokenType.Number || currentToken.Type == TokenType.NumberId || currentToken.Type == TokenType.LeftParentheses)) ||
            (previousToken.Type == TokenType.NumberId && currentToken.Type == TokenType.LeftParentheses);
    }

    private Expr Unary()
    {
        if (TryConsume(out var op, TokenType.Minus))
        {
            var right = Unary();
            return new Unary(op, right);
        }

        return Pow();
    }

    private Expr Pow()
    {
        var expr = FunctionCall();

        while (TryConsume(out var op, TokenType.Pow, TokenType.Square, TokenType.Cube))
        {
            Expr right;
            if (op.Type == TokenType.Square)
            {
                right = new Literal((TiNumber)2);
            }
            else if (op.Type == TokenType.Cube)
            {
                right = new Literal((TiNumber)3);
            }
            else
            {
                right = Pow();
            }
            expr = new Binary(expr, op, right);
        }

        return expr;
    }

    private Expr FunctionCall()
    {
        var expr = Primary();

        if (expr is Variable variable && variable.Name.Type == TokenType.FunctionId)
        {
            var arguments = new List<Expr>();

            if (Environment.GetCallableByName(variable.Name.Lexeme).ArityMax > 0)
            {
                do
                {
                    arguments.Add(Expression());
                } while (TryConsume(TokenType.Comma));

                if (!IsClosingOptional())
                {
                    Consume(TokenType.RightParentheses);
                }
            }

            expr = new FunctionCall(expr, variable.Name, arguments);
        }

        return expr;
    }

    private Expr Primary()
    {
        if (TryConsume(out var nameToken, TokenType.String))
        {
            var stringValue = nameToken.Literal!;
            return new Literal(stringValue);
        }
        else if (TryConsume(out nameToken, TokenType.Number))
        {
            var numberValue = (TiNumber)(float)nameToken.Literal!;
            return new Literal(numberValue);
        }
        else if (TryConsume(out nameToken, TokenType.ConstId))
        {
            var constValue = Environment.Consts[nameToken.Lexeme];
            return new Literal(constValue);
        }
        else if (TryConsume(out nameToken, TokenType.NumberId, TokenType.StringId, TokenType.CommandId, TokenType.FunctionId))
        {
            return new Variable(nameToken);
        }
        else if (TryConsume(out nameToken, TokenType.MatrixId))
        {
            if (TryConsume(TokenType.LeftParentheses))
            {
                var row = Expression();
                Consume(TokenType.Comma);
                var column = Expression();
                if (!IsClosingOptional())
                {
                    Consume(TokenType.RightParentheses);
                }

                return new MatrixElement(nameToken, row, column);
            }
            else
            {
                return new Variable(nameToken);
            }
        }
        else if (TryConsume(out nameToken, TokenType.ListId))
        {
            if (TryConsume(TokenType.LeftParentheses))
            {
                var index = Expression();
                if (!IsClosingOptional())
                {
                    Consume(TokenType.RightParentheses);
                }

                return new ListElement(nameToken, index);
            }
            else
            {
                return new Variable(nameToken);
            }
        }
        else if (TryConsume(TokenType.LeftParentheses))
        {
            var expr = Expression();
            if (!IsClosingOptional())
            {
                Consume(TokenType.RightParentheses);
            }
            return new Grouping(expr);
        }
        else if (TryConsume(TokenType.LeftCurlyBracket))
        {
            return LiteralList();
        }
        else if (TryConsume(TokenType.LeftSquareBracket))
        {
            return LiteralMatrix();
        }

        throw Error("Unknown primary");
    }

    private LiteralList LiteralList()
    {
        if (Check(TokenType.RightCurlyBracket))
        {
            // Empty list not allowed
            throw Error("List cannot be empty");
        }

        var items = new List<Expr>();
        do
        {
            items.Add(Expression());
        } while (TryConsume(TokenType.Comma));

        if (!IsClosingOptional())
        {
            Consume(TokenType.RightCurlyBracket);
        }

        return new LiteralList(items);
    }

    private LiteralMatrix LiteralMatrix()
    {
        if (Check(TokenType.RightSquareBracket))
        {
            // Empty matrix not allowed
            throw Error("Matrix cannot be empty");
        }

        Consume(TokenType.LeftSquareBracket);
        var rows = new List<List<Expr>>();
        do
        {
            var row = new List<Expr>();
            do
            {
                row.Add(Expression());
            } while (TryConsume(TokenType.Comma));

            rows.Add(row);

            if (IsClosingOptional())
            {
                break;
            }

            Consume(TokenType.RightSquareBracket);

        } while (TryConsume(TokenType.LeftSquareBracket));

        if (!IsClosingOptional())
        {
            Consume(TokenType.RightSquareBracket);
        }

        return new LiteralMatrix(rows);
    }

    private bool TryConsume(params TokenType[] tokenTypes)
    {
        return TryConsume(out _, tokenTypes);
    }

    private bool TryConsume([MaybeNullWhen(false)] out Token token, params TokenType[] tokenTypes)
    {
        foreach (var tokenType in tokenTypes)
        {
            if (Check(tokenType))
            {
                token = Advance();
                return true;
            }
        }

        token = null;
        return false;
    }

    private Token Consume(params TokenType[] tokenTypes)
    {
        foreach (var tokenType in tokenTypes)
        {
            if (Check(tokenType))
            {
                return Advance();
            }
        }

        throw Error($"Expected '{string.Join<TokenType>(",", tokenTypes)}'");
    }

    private Token Advance()
    {
        var previous = Peek();
        if (!IsAtEnd())
        {
            ++_currentIdx;
        }

        return previous;
    }

    private bool Check(TokenType tokenType)
    {
        return Peek().Type == tokenType;
    }

    private bool CheckNext(TokenType tokenType)
    {
        return Peek(1).Type == tokenType;
    }

    private Token Peek(int offset = 0)
    {
        return _tokens[_currentIdx + offset];
    }

    private bool IsClosingOptional()
    {
        return Check(TokenType.Store) || Check(TokenType.Colon) || IsAtEnd();
    }

    private bool IsAtEnd()
    {
        return Check(TokenType.Eof);
    }
}