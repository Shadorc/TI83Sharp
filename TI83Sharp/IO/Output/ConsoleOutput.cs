using System.Runtime.InteropServices;

namespace TI83Sharp;

public class ConsoleOutput
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();

    public ConsoleOutput()
    {
        AllocConsole();
    }

    public void OnScreenChange(object? sender, ScreenChangedEventArgs e)
    {
        Console.Clear();

        var screen = e.Screen;
        for (int y = 1; y < TiHomeScreen.HEIGHT + 1; y++)
        {
            for (int x = 1; x < TiHomeScreen.WIDTH + 1; x++)
            {
                Console.Write(screen[x, y]);
            }
            Console.WriteLine();
        }
    }

    public static void ShowErrorBeforeExit(string message)
    {
        Console.Error.WriteLine(message);
        System.Environment.Exit(-1);
    }
}
