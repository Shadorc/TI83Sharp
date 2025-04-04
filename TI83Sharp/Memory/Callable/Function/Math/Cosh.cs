
namespace TI83Sharp;

public class Cosh : MathFunction
{
    public Cosh() : base("cosh(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Cosh(number);
    }
}
