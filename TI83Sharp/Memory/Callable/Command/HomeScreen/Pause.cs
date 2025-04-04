
namespace TI83Sharp;

public class Pause : Command
{
    public Pause() : base("Pause", 0, 1)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        if (arguments.Count == 1)
        {
            var arg = arguments[0];
            var evaluatedArg = interpreter.Evaluate(arg);
            interpreter.Environment.Set(Environment.ANS_VALUE, evaluatedArg);

            var alignement = evaluatedArg is string ? LogAlignement.Left : LogAlignement.Right;
            interpreter.Output.Message(evaluatedArg.ToString()!, alignement);
        }

        char c;
        do
        {
            c = interpreter.Input.WaitChar();
        } while (c != '\n');
    }
}
