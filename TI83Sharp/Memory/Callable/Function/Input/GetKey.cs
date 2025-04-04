namespace TI83Sharp;

public class GetKey : Function
{
    public GetKey() : base("getKey")
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        return (TiNumber)interpreter.Input.GetKey();
    }
}
