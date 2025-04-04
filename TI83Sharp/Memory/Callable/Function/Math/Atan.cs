
namespace TI83Sharp;

public class Atan : MathFunction
{
    public Atan() : base("tan¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Atan(number);
    }
}
