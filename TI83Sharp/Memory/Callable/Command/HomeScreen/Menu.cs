
namespace TI83Sharp;

public class Menu : Command
{
    private readonly struct Choice
    {
        internal readonly int Id;
        internal readonly string Option;
        internal readonly string Label;

        public Choice(int id, string option, string label)
        {
            Id = id;
            Option = option;
            Label = label;
        }
    }

    public Menu() : base("Menu(", 3, 15)
    {
    }

    public override void Call(Interpreter interpreter, List<Expr> arguments)
    {
        if ((arguments.Count - 1) % 2 != 0)
        {
            throw RuntimeError.Argument;
        }

        var arg1 = interpreter.Evaluate(arguments[0]);
        if (arg1 is not string titleStr)
        {
            throw RuntimeError.DataType;
        }

        var choices = new List<Choice>();
        for (int i = 1; i < arguments.Count; i += 2)
        {
            var optionArg = interpreter.Evaluate(arguments[i]);
            if (optionArg is not string option)
            {
                throw RuntimeError.DataType;
            }

            var labelArg = interpreter.Evaluate(arguments[i + 1]);
            choices.Add(new Choice((i + 1) / 2, option, labelArg.ToString()!));
        }

        interpreter.HomeScreen.Clear();
        interpreter.HomeScreen.Disp(titleStr);

        foreach (var choice in choices)
        {
            interpreter.HomeScreen.Disp($"{choice.Id}:{choice.Option}");
        }

        char input;
        do
        {
            input = interpreter.Input.WaitChar();
        } while (!char.IsBetween(input, '1', '7') || input - '1' >= choices.Count);

        throw new GotoException(choices[input - '1'].Label);
    }
}
