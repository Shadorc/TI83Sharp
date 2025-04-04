
namespace TI83Sharp;

public class DelVar : Command
{
    public DelVar() : base("DelVar", 1)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = arguments[0];
        if (arg is not Variable variable)
        {
            throw RuntimeError.Syntax;
        }

        var variableName = variable.Name.Lexeme;
        if (variable.Name.Type == TokenType.NumberId)
        {
            interpreter.Environment.Set(variableName, (TiNumber)0);
        }
        else
        {
            interpreter.Environment.Delete(variableName);
        }
    }
}
