using System.Runtime.InteropServices;

namespace TI83Sharp;

public class ConsoleOutput : IOutput
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();

    public void Message(string value, LogAlignement alignement = LogAlignement.NewLine | LogAlignement.Left)
    {
        if (alignement.HasFlag(LogAlignement.NewLine))
        {
            Console.WriteLine(value);
        }
        else
        {
            Console.Write(value);
        }
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
        Console.Error.WriteLine(exception);

    }

    public void Clear()
    {
        Console.Clear();
    }

    public static void ShowErrorBeforeExit(string message)
    {
        Console.Error.WriteLine(message);
        System.Environment.Exit(-1);
    }
}
