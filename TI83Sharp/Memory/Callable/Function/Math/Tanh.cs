
namespace TI83Sharp;

public class Tanh : MathFunction
{
    public Tanh() : base("tanh(")
    {
    }

    protected override float ApplyFunction(TiNumber number)
    {
        return MathF.Tanh(number);
    }
}
