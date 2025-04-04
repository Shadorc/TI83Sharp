namespace TI83Sharp;

public class ClrList : Command
{
    public ClrList() : base("ClrList", 1, int.MaxValue)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        foreach (var arg in arguments)
        {
            if (arg is not Variable list || list.Name.Type != TokenType.ListId)
            {
                throw RuntimeError.Argument;
            }

            var listName = list.Name.Lexeme;
            interpreter.Environment.ClearList(listName);
        }
    }
}
