
using System.Drawing.Drawing2D;

namespace TI83Sharp;

public class Fill : Command
{
    public Fill() : base("Fill(", 2)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = interpreter.Evaluate(arguments[0]);
        if (arg1 is not TiNumber value)
        {
            throw RuntimeError.DataType;
        }

        var arg2 = arguments[1];
        if (arg2 is not Variable variable)
        {
            throw RuntimeError.DataType;
        }

        if (variable.Name.Type == TokenType.ListId)
        {
            var list = interpreter.Environment.Get<TiList>(variable.Name.Lexeme);
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = value;
            }
        }
        else if (variable.Name.Type == TokenType.MatrixId)
        {
            var matrix = interpreter.Environment.Get<TiMatrix>(variable.Name.Lexeme);
            for (int row = 0; row < matrix.Rows; row++)
            {
                for (int col = 0; col < matrix.Cols; col++)
                {
                    matrix[row][col] = value;
                }
            }
        }
        else
        {
            throw RuntimeError.DataType;
        }
    }
}
