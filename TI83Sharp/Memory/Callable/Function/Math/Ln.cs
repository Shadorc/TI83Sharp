namespace TI83Sharp;

public class Ln : MathFunction
{
    public Ln() : base("ln(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Log(number);
    }
}
