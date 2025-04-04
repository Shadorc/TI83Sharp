
namespace TI83Sharp;

public class Rand : Assignable
{
    public Rand() : base("rand")
    {
    }

    public override void Assign(Interpreter interpreter, List<Expr> arguments, object value)
    {
        if (value is not TiNumber number)
        {
            throw RuntimeError.DataType;
        }

        interpreter.SetRandomSeed((int)MathF.Abs(number));
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        return (TiNumber)interpreter.Random.NextSingle();
    }
}
