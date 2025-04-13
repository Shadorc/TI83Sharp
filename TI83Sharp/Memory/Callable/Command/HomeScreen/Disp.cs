namespace TI83Sharp;

public class Disp : Command
{
    public Disp() : base("Disp", 0, int.MaxValue)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        foreach (var argument in arguments)
        {
            var evaluatedArg = interpreter.Evaluate(argument);
            var alignement = evaluatedArg is string ? MessageAlignement.Left : MessageAlignement.Right;
            interpreter.HomeScreen.Disp(evaluatedArg.ToString()!, alignement | MessageAlignement.NewLine);
        }
    }
}
