namespace TI83Sharp;

public class SyntaxError : Error
{
    public SyntaxError(string description, int line) : base($"ERR:SYNTAX\n{description}\n[line {line}]")
    {
    }

    public SyntaxError(string description, Token token) : this(description, token.Line)
    {
    }
}
