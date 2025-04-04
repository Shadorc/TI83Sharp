
namespace TI83Sharp;

public class TiScreenOutput : IOutput
{
    private readonly TiHomeScreen _screen;

    public TiScreenOutput(TiHomeScreen screen)
    {
        _screen = screen;
    }

    public void Message(string value, LogAlignement alignement = LogAlignement.Left | LogAlignement.NewLine)
    {
        _screen.Disp(value, alignement);
    }

    public void Message(string value, int x, int y)
    {
        _screen.Output(value, x, y);
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
        _screen.Clear();
    }
}
