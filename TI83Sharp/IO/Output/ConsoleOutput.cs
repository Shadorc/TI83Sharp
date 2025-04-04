
namespace TI83Sharp;

public class ConsoleOutput : IOutput
{
    public void Message(string value, LogAlignement alignement /* ignored */)
    {
        Console.WriteLine(value);
    }

    public void Message(string value, int x /* ignored */, int y /* ignored */)
    {
        Console.WriteLine(value);
    }

    public void Error(string message)
    {
        Console.Error.WriteLine(message);
    }

    public void Error(Exception exception)
    {
        Error($"{exception.Message}\n{exception.StackTrace}");
    }

    public void Clear()
    {
        Console.Clear();
    }
}
