
namespace TI83Sharp;

public class Sin : MathFunction
{
    public Sin() : base("sin(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Sin(number);
    }
}
