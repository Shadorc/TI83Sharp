
namespace TI83Sharp;

public abstract class MathFunction : Function
{
    public MathFunction(string name) : base(name, 1)
    {
    }

    protected abstract float ApplyFunction(TiNumber number);

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is TiNumber number)
        {
            return (TiNumber)ApplyFunction(number);
        }
        else if (arg is TiList list)
        {
            var result = new TiList(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(ApplyFunction(list[i]));
            }
            return result;
        }

        throw RuntimeError.DataType;
    }
}
