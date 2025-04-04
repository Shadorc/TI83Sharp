
namespace TI83Sharp;

public class Asinh : MathFunction
{
    public Asinh() : base("sinhֿ¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Asinh(number);
    }
}
