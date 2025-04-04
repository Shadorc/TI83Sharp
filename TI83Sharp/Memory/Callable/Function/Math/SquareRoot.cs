namespace TI83Sharp;

public class SquareRoot : MathFunction
{
    public SquareRoot() : base("√(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Sqrt(number);
    }
}
