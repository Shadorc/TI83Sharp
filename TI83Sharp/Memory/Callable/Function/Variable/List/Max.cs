
namespace TI83Sharp;

public class Max : MinMaxFunction
{
    public Max() : base("max(")
    {
    }

    protected override float ApplyFunction(TiNumber a, TiNumber b)
    {
        return MathF.Max(a, b);
    }
}
