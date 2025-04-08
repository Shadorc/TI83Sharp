namespace TI83Sharp;

public class Sub : Function
{
    public Sub() : base("sub(", 1, 3)
    {
    }

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = arguments[0];
        if (arg1 is Variable variable && variable.Name.Type == TokenType.StringId)
        {
            var name = variable.Name.Lexeme;
            if (!interpreter.Environment.Exists(name))
            {
                throw RuntimeError.Undefined;
            }

            return ComputeString(interpreter, arguments, interpreter.Environment.Get<string>(name));
        }

        var evaluatedArg = interpreter.Evaluate(arg1);

        if (evaluatedArg is string str)
        {
            return ComputeString(interpreter, arguments, str);
        }

        if (arguments.Count != 1)
        {
            throw RuntimeError.Argument;
        }

        if (evaluatedArg is TiNumber number)
        {
            return ComputeNumber(number);
        }
        else if (evaluatedArg is TiList list)
        {
            var result = new TiList(list.Count);
            foreach (var item in list)
            {
                result.Add(ComputeNumber(item));
            }
            return result;
        }

        throw RuntimeError.DataType;
    }

    private static string ComputeString(Interpreter interpreter, List<Expr> arguments, string str)
    {
        if (arguments.Count != 3)
        {
            throw RuntimeError.Argument;
        }

        var arg2 = interpreter.Evaluate(arguments[1]);
        var arg3 = interpreter.Evaluate(arguments[2]);

        if (arg2 is not TiNumber start || !start.IsInt() || start < 1
            || arg3 is not TiNumber length || !length.IsInt() || length < 1)
        {
            throw RuntimeError.Domain;
        }

        if (start + length > str.Length + 1)
        {
            throw RuntimeError.InvalidDim;
        }

        return str.Substring(start - 1, length);
    }

    private static TiNumber ComputeNumber(TiNumber number)
    {
        return number / (TiNumber)100;
    }
}
