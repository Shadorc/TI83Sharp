namespace TI83Sharp;

public class DeltaList : Function
{
    public DeltaList() : base("ΔList(", 1)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg = interpreter.Evaluate(arguments[0]);
        if (arg is TiList list)
        {
            var result = new TiList(list.Count - 1);
            for (int i = 1; i < list.Count; i++)
            {
                result.Add(list[i] - list[i - 1]);
            }
            return result;
        }

        throw RuntimeError.DataType;
    }
}
