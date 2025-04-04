namespace TI83Sharp;

public class Seq : Function
{
    public Seq() : base("seq(", 4, 5)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var formula = arguments[0];
        var arg2 = arguments[1];
        var arg3 = interpreter.Evaluate(arguments[2]);
        var arg4 = interpreter.Evaluate(arguments[3]);
        var arg5 = arguments.Count > 4 ? interpreter.Evaluate(arguments[4]) : (TiNumber)1;

        if (arg2 is not Variable variable || variable.Name.Type != TokenType.NumberId
            || arg3 is not TiNumber start
            || arg4 is not TiNumber end
            || arg5 is not TiNumber step)
        {
            throw RuntimeError.DataType;
        }

        var env = interpreter.Environment;

        var varName = variable.Name.Lexeme;
        env.Set(varName, start);

        var results = new TiList();
        bool isIncrementing = step > (TiNumber)0;
        while (isIncrementing ? env.Get<TiNumber>(varName) <= end : env.Get<TiNumber>(varName) >= end)
        {
            if (interpreter.Evaluate(formula) is not TiNumber number)
            {
                throw RuntimeError.Argument;
            }

            results.Add(number);
            env.Set(varName, env.Get<TiNumber>(varName) + step);
        }
        return results;
    }
}
