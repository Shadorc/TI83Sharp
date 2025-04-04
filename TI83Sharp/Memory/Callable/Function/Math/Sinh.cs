
namespace TI83Sharp;

public class Sinh : MathFunction
{
    public Sinh() : base("sinh(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Sinh(number);
    }
}
