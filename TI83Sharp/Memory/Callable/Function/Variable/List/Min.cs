
namespace TI83Sharp;

public class Min : MinMaxFunction
{
    public Min() : base("min(")
    {
    }

    protected override float ApplyFunction(TiNumber a, TiNumber b)
    {
        return MathF.Min(a, b);
    }
}
