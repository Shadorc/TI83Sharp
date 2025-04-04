namespace TI83Sharp;

public class Not : Function
{
    public Not() : base("not(", 1)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var isTrue = LogicHelper.IsTrue(interpreter.Evaluate(arguments[0]));
        return LogicHelper.BoolToNumber(!isTrue);
    }
}
