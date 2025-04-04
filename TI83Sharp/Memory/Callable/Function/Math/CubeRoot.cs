namespace TI83Sharp;

public class CubeRoot : MathFunction
{
    private static readonly TiNumber ONE_THIRD = new TiNumber(1f / 3f);

    public CubeRoot() : base("³√(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Pow(number, ONE_THIRD);
    }
}
