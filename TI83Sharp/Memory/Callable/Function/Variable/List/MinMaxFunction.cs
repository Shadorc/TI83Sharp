using TI83Sharp;

public abstract class MinMaxFunction : Function
{
    protected MinMaxFunction(string name) : base(name, 1, 2)
    {
    }

    protected abstract float ApplyFunction(TiNumber a, TiNumber b);

    public override object Call(Interpreter interpreter, List<Expr> arguments)
    {
        if (arguments.Count == 1)
        {
            var arg = interpreter.Evaluate(arguments[0]);
            if (arg is not TiList list)
            {
                throw RuntimeError.DataType;
            }

            if (list.Count == 0)
            {
                throw RuntimeError.InvalidDim;
            }

            return list.Aggregate((a, b) => ApplyFunction(a, b));
        }

        else if (arguments.Count == 2)
        {
            var arg1 = interpreter.Evaluate(arguments[0]);
            var arg2 = interpreter.Evaluate(arguments[1]);

            if (arg1 is TiNumber num1)
            {
                // Number, Number
                if (arg2 is TiNumber num2)
                {
                    return (TiNumber)ApplyFunction(num1, num2);
                }

                // Number, List
                else if (arg2 is TiList list2)
                {
                    if (list2.Count == 0)
                    {
                        throw RuntimeError.InvalidDim;
                    }

                    var result = new TiList();
                    foreach (var item in list2)
                    {
                        result.Add(ApplyFunction(num1, item));
                    }
                    return result;
                }
            }
            else if (arg1 is TiList list1)
            {
                if (list1.Count == 0)
                {
                    throw RuntimeError.InvalidDim;
                }

                // List, Number
                if (arg2 is TiNumber num2)
                {
                    var result = new TiList();
                    foreach (var item in list1)
                    {
                        result.Add(ApplyFunction(item, num2));
                    }
                    return result;
                }
                // List, List
                else if (arg2 is TiList list2)
                {
                    if (list2.Count == 0)
                    {
                        throw RuntimeError.InvalidDim;
                    }

                    if (list1.Count != list2.Count)
                    {
                        throw RuntimeError.DimMismatch;
                    }

                    return new TiList() { list1.Aggregate((a, b) => ApplyFunction(a, b)), list2.Aggregate((a, b) => ApplyFunction(a, b)) };
                }
            }
        }

        throw RuntimeError.DataType;
    }
}