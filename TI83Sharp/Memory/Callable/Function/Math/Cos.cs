
namespace TI83Sharp;

public class Cos : MathFunction
{
    public Cos() : base("cos(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Cos(number);
    }
}
