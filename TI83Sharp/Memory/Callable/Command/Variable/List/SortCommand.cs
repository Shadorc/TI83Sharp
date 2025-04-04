namespace TI83Sharp;

public abstract class SortCommand : Command
{
    public SortCommand(string name) : base(name, 1, int.MaxValue)
    {

    }

    protected abstract Comparison<int> Compare(TiList list);

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        var arg1 = arguments[0];
        if (arg1 is not Variable variable1 || variable1.Name.Type != TokenType.ListId)
        {
            throw RuntimeError.DataType;
        }

        var variable1Name = variable1.Name.Lexeme;
        var list1 = interpreter.Environment.Get<TiList>(variable1Name);

        // Create index mapping
        var indices = Enumerable.Range(0, list1.Count).ToList();
        indices.Sort(Compare(list1));

        ApplyPermutation(list1, indices);
        interpreter.Environment.Set(variable1Name, list1);

        for (int i = 1; i < arguments.Count; ++i)
        {
            var argI = arguments[i];
            if (argI is not Variable variableI || variableI.Name.Type != TokenType.ListId)
            {
                throw RuntimeError.DataType;
            }

            var variableIName = variableI.Name.Lexeme;
            var listI = interpreter.Environment.Get<TiList>(variableIName);
            if (listI.Count != list1.Count)
            {
                throw RuntimeError.DimMismatch;
            }

            ApplyPermutation(listI, indices);
            interpreter.Environment.Set(variableIName, listI);
        }
    }

    private static void ApplyPermutation(TiList list, List<int> indices)
    {
        var sorted = indices.Select(i => list[i]).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = sorted[i];
        }
    }
}
