
namespace TI83Sharp;

public class Acos : MathFunction
{
    public Acos() : base("cosֿ¹(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Acos(number);
    }
}
