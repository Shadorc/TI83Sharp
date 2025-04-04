namespace TI83Sharp;

public class IPart : MathFunction
{
    public IPart() : base ("iPart(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return number.ToInt();
    }
}
