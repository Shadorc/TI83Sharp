
using System.Windows.Forms;

namespace TI83Sharp;

public class ConsoleInput : IInput
{
    private static readonly Dictionary<ConsoleKey, int> s_consoleKeysToCode = new Dictionary<ConsoleKey, int>()
    {
        { ConsoleKey.LeftArrow, 24 },
        { ConsoleKey.UpArrow, 25 },
        { ConsoleKey.RightArrow, 26 },
        { ConsoleKey.DownArrow, 34 },
        { ConsoleKey.A, 41 },
        { ConsoleKey.B, 42 },
        { ConsoleKey.C, 43 },
        { ConsoleKey.D, 51 },
        { ConsoleKey.E, 52 },
        { ConsoleKey.F, 53 },
        { ConsoleKey.G, 54 },
        { ConsoleKey.H, 55 },
        { ConsoleKey.I, 61 },
        { ConsoleKey.J, 62 },
        { ConsoleKey.K, 63 },
        { ConsoleKey.L, 64 },
        { ConsoleKey.M, 65 },
        { ConsoleKey.N, 71 },
        { ConsoleKey.O, 72 },
        { ConsoleKey.P, 73 },
        { ConsoleKey.Q, 74 },
        { ConsoleKey.R, 75 },
        { ConsoleKey.S, 81 },
        { ConsoleKey.T, 82 },
        { ConsoleKey.U, 83 },
        { ConsoleKey.V, 84 },
        { ConsoleKey.W, 85 },
        { ConsoleKey.X, 91 },
        { ConsoleKey.Y, 92 },
        { ConsoleKey.Z, 93 },
        { ConsoleKey.NumPad9, 74 },
        { ConsoleKey.NumPad8, 73 },
        { ConsoleKey.NumPad7, 72 },
        { ConsoleKey.NumPad6, 84 },
        { ConsoleKey.NumPad4, 82 },
        { ConsoleKey.NumPad3, 94 },
        { ConsoleKey.NumPad5, 83 },
        { ConsoleKey.NumPad2, 93 },
        { ConsoleKey.NumPad1, 92 },
        { ConsoleKey.NumPad0, 102 },
        { ConsoleKey.Subtract, 104 },
        { ConsoleKey.Enter, 105 },
    };

    private static readonly Dictionary<ConsoleKey, char> s_consoleKeysToChar = new Dictionary<ConsoleKey, char>()
    {
        { ConsoleKey.A, 'A' },
        { ConsoleKey.B, 'B' },
        { ConsoleKey.C, 'C' },
        { ConsoleKey.D, 'D' },
        { ConsoleKey.E, 'E' },
        { ConsoleKey.F, 'F' },
        { ConsoleKey.G, 'G' },
        { ConsoleKey.H, 'H' },
        { ConsoleKey.I, 'I' },
        { ConsoleKey.J, 'J' },
        { ConsoleKey.K, 'K' },
        { ConsoleKey.L, 'L' },
        { ConsoleKey.M, 'M' },
        { ConsoleKey.N, 'N' },
        { ConsoleKey.O, 'O' },
        { ConsoleKey.P, 'P' },
        { ConsoleKey.Q, 'Q' },
        { ConsoleKey.R, 'R' },
        { ConsoleKey.S, 'S' },
        { ConsoleKey.T, 'T' },
        { ConsoleKey.U, 'U' },
        { ConsoleKey.V, 'V' },
        { ConsoleKey.W, 'W' },
        { ConsoleKey.X, 'X' },
        { ConsoleKey.Y, 'Y' },
        { ConsoleKey.Z, 'Z' },
        { ConsoleKey.D0, '0' },
        { ConsoleKey.D1, '1' },
        { ConsoleKey.D2, '2' },
        { ConsoleKey.D3, '3' },
        { ConsoleKey.D4, '4' },
        { ConsoleKey.D5, '5' },
        { ConsoleKey.D6, '6' },
        { ConsoleKey.D7, '7' },
        { ConsoleKey.D8, '8' },
        { ConsoleKey.D9, '9' },
        { ConsoleKey.NumPad0, '0' },
        { ConsoleKey.NumPad1, '1' },
        { ConsoleKey.NumPad2, '2' },
        { ConsoleKey.NumPad3, '3' },
        { ConsoleKey.NumPad4, '4' },
        { ConsoleKey.NumPad5, '5' },
        { ConsoleKey.NumPad6, '6' },
        { ConsoleKey.NumPad7, '7' },
        { ConsoleKey.NumPad8, '8' },
        { ConsoleKey.NumPad9, '9' },
        { ConsoleKey.Enter, '\n' },
    };

    private ConsoleKey _lastKeyPressed;
    private bool _isReading;

    public async void ReadInputAsync()
    {
        await Task.Run(() =>
        {
            while (true)
            {
                _lastKeyPressed = Console.ReadKey(true).Key;
                _isReading = false;
            }
        });
    }

    public int GetKey()
    {
        if (_lastKeyPressed == default)
        {
            return 0;
        }

        var key = _lastKeyPressed;
        _lastKeyPressed = default;

        var keyCode = s_consoleKeysToCode[key];
        return keyCode;
    }

    public char WaitChar()
    {
        _isReading = true;

        while (_isReading)
        {
            Thread.Sleep(50);
        }

        var key = _lastKeyPressed;
        _lastKeyPressed = default;
        return s_consoleKeysToChar[key];
    }
}
