
namespace TI83Sharp;

public class Tan : MathFunction
{
    public Tan() : base("tan(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Tan(number);
    }
}
