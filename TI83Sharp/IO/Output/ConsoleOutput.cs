using System.Runtime.InteropServices;
using Timer = System.Threading.Timer;

namespace TI83Sharp;

public class ConsoleOutput
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();

    private readonly Timer _refreshTimer;

    private bool _needsRefresh;
    private TiHomeScreen? _screen;

    public ConsoleOutput()
    {
        AllocConsole();

        _refreshTimer = new Timer(
                new TimerCallback(_ => OutputScreen()),
                null,
                0,
                500 // 500ms = 2Hz
            );
    }

    ~ConsoleOutput()
    {
        _refreshTimer.Dispose();
    }

    public void OnScreenChange(object? sender, ScreenChangedEventArgs e)
    {
        _screen = e.Screen;
        _needsRefresh = true;
    }

    private void OutputScreen()
    {
        if (_screen == null || !_needsRefresh)
        {
            return;
        }

        Console.Clear();

        for (int y = 1; y < TiHomeScreen.HEIGHT + 1; y++)
        {
            for (int x = 1; x < TiHomeScreen.WIDTH + 1; x++)
            {
                Console.Write(_screen[x, y]);
            }
            Console.WriteLine();
        }

        _needsRefresh = false;
    }

    public static void ShowErrorBeforeExit(string message)
    {
        Console.Error.WriteLine(message);
        System.Environment.Exit(-1);
    }
}
