namespace TI83Sharp;

public class SyntaxError : Error
{
    public SyntaxError(string message, int line) : base($"{message}\n[line {line}]")
    {
    }

    public SyntaxError(Token token) : this("ERR:SYNTAX", token.Line)
    {
    }
}
