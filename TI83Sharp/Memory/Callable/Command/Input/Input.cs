
using System.Text;

namespace TI83Sharp;

public class Input : Command
{
    public Input() : base("Input", 1, 2)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        if (arguments.Count == 1)
        {
            HandleInput(interpreter, "?", arguments[0]);
        }
        else
        {
            var arg1 = interpreter.Evaluate(arguments[0]);
            if (arg1 is not string text)
            {
                throw RuntimeError.DataType;
            }

            HandleInput(interpreter, text, arguments[1]);
        }
    }

    private static void HandleInput(Interpreter interpreter, string prompt, Expr argument)
    {
        if (argument is not Variable variable)
        {
            throw RuntimeError.DataType;
        }

        interpreter.Output.Message(prompt);

        var input = new StringBuilder();
        while (true)
        {
            var c = interpreter.Input.WaitChar();
            if (c == '\n')
            {
                if (input.Length == 0)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            input.Append(c);
            interpreter.Output.Message(c.ToString(), LogAlignement.Left);
        }

        if (!float.TryParse(input.ToString(), out var number))
        {
            throw RuntimeError.Syntax;
        }

        interpreter.Environment.Set(variable.Name.Lexeme, (TiNumber)number);
    }
}
