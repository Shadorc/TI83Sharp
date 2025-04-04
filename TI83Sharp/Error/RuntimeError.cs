using System.Text;

namespace TI83Sharp;

public class RuntimeError : Error
{
    public static readonly RuntimeError DimMismatch = new RuntimeError("ERR:DIM MISMATCH");
    public static readonly RuntimeError DivideByZero = new RuntimeError("ERR:DIVIDE BY 0");
    public static readonly RuntimeError InvalidDim = new RuntimeError("ERR:INVALID DIM");
    public static readonly RuntimeError DataType = new RuntimeError("ERR:DATA TYPE");
    public static readonly RuntimeError Domain = new RuntimeError("ERR:DOMAIN");
    public static readonly RuntimeError Syntax = new RuntimeError("ERR:SYNTAX");
    public static readonly RuntimeError Argument = new RuntimeError("ERR:ARGUMENT");
    public static readonly RuntimeError Undefined = new RuntimeError("ERR:UNDEFINED");
    public static readonly RuntimeError Label = new RuntimeError("ERR:LABEL");

    public RuntimeError(string message) : this(null, message)
    {
    }

    public RuntimeError(Token? token, string message) : base(FormatMessage(token, message))
    {
    }

    private static string FormatMessage(Token? token, string message)
    {
        var str = new StringBuilder(message);
        if (token != null)
        {
            str.Append($"\n[line {token.Line}]");
        }
        return str.ToString();
    }
}
