
namespace TI83Sharp;

public class Atanh : MathFunction
{
    public Atanh() : base("tanhֿ¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Atanh(number);
    }
}
