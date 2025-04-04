namespace TI83Sharp;

public class Logarithm : MathFunction
{
    public Logarithm() : base("log(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Log10(number);
    }
}
