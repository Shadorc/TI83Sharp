namespace TI83Sharp;

public class Int : MathFunction
{
    public Int() : base("int(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Floor(number);
    }
}
