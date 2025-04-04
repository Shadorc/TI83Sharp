
namespace TI83Sharp;

public class Asin : MathFunction
{
    public Asin() : base("sinֿ¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Asin(number);
    }
}
