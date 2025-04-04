
namespace TI83Sharp;

public class Acosh : MathFunction
{
    public Acosh() : base("coshֿ¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Acosh(number);
    }
}
