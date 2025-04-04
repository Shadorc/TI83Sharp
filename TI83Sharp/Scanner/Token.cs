using System.Text;

namespace TI83Sharp;

public class Token
{
    public TokenType Type { get; private set; }

    public string Lexeme { get; private set; }

    public readonly object? Literal;

    public int Line { get; private set; }

    public Token(TokenType type, string lexeme, object? literal = null, int line = -1)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;
    }

    public override string ToString()
    {
        var str = new StringBuilder($"Token({Type}, {Lexeme}");
        if (Literal != null)
        {
            str.Append($", {Literal}");
        }
        if (Line != -1)
        {
            str.Append($", line={Line}");
        }
        str.Append(')');

        return str.ToString();
    }
}
