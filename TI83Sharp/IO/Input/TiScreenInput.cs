namespace TI83Sharp;

public class TiScreenInput : IInput
{
    private Keys _lastKeyPressed;
    private Keys _lastWaitingKeyPressed;
    private bool _isReading;

    private static readonly Dictionary<Keys, int> s_keyCodeMap = new Dictionary<Keys, int>()
    {
        { Keys.Left, 24 },
        { Keys.Up, 25 },
        { Keys.Right, 26 },
        { Keys.Down, 34 },
        { Keys.A, 41 },
        { Keys.B, 42 },
        { Keys.C, 43 },
        { Keys.D, 51 },
        { Keys.E, 52 },
        { Keys.F, 53 },
        { Keys.G, 54 },
        { Keys.H, 55 },
        { Keys.I, 61 },
        { Keys.J, 62 },
        { Keys.K, 63 },
        { Keys.L, 64 },
        { Keys.M, 65 },
        { Keys.N, 71 },
        { Keys.O, 72 },
        { Keys.P, 73 },
        { Keys.Q, 74 },
        { Keys.R, 75 },
        { Keys.S, 81 },
        { Keys.T, 82 },
        { Keys.U, 83 },
        { Keys.V, 84 },
        { Keys.W, 85 },
        { Keys.X, 91 },
        { Keys.Y, 92 },
        { Keys.Z, 93 },
        { Keys.NumPad9, 74 },
        { Keys.NumPad8, 73 },
        { Keys.NumPad7, 72 },
        { Keys.NumPad6, 84 },
        { Keys.NumPad4, 82 },
        { Keys.NumPad3, 94 },
        { Keys.NumPad5, 83 },
        { Keys.NumPad2, 93 },
        { Keys.NumPad1, 92 },
        { Keys.NumPad0, 102 },
        { Keys.Subtract, 104 },
        { Keys.Enter, 105 },
    };

    private static readonly Dictionary<Keys, char> s_keyCharMap = new Dictionary<Keys, char>()
    {
        { Keys.A, 'A' },
        { Keys.B, 'B' },
        { Keys.C, 'C' },
        { Keys.D, 'D' },
        { Keys.E, 'E' },
        { Keys.F, 'F' },
        { Keys.G, 'G' },
        { Keys.H, 'H' },
        { Keys.I, 'I' },
        { Keys.J, 'J' },
        { Keys.K, 'K' },
        { Keys.L, 'L' },
        { Keys.M, 'M' },
        { Keys.N, 'N' },
        { Keys.O, 'O' },
        { Keys.P, 'P' },
        { Keys.Q, 'Q' },
        { Keys.R, 'R' },
        { Keys.S, 'S' },
        { Keys.T, 'T' },
        { Keys.U, 'U' },
        { Keys.V, 'V' },
        { Keys.W, 'W' },
        { Keys.X, 'X' },
        { Keys.Y, 'Y' },
        { Keys.Z, 'Z' },
        { Keys.D0, '0' },
        { Keys.D1, '1' },
        { Keys.D2, '2' },
        { Keys.D3, '3' },
        { Keys.D4, '4' },
        { Keys.D5, '5' },
        { Keys.D6, '6' },
        { Keys.D7, '7' },
        { Keys.D8, '8' },
        { Keys.D9, '9' },
        { Keys.NumPad0, '0' },
        { Keys.NumPad1, '1' },
        { Keys.NumPad2, '2' },
        { Keys.NumPad3, '3' },
        { Keys.NumPad4, '4' },
        { Keys.NumPad5, '5' },
        { Keys.NumPad6, '6' },
        { Keys.NumPad7, '7' },
        { Keys.NumPad8, '8' },
        { Keys.NumPad9, '9' },
        { Keys.Enter, '\n' },
    };

    public int GetKey()
    {
        if (_lastKeyPressed == default)
        {
            return 0;
        }

        var keyCode = s_keyCodeMap[_lastKeyPressed];
        _lastKeyPressed = default;
        return keyCode;
    }

    public void OnKeyPressed(Keys keys)
    {
        if (_isReading)
        {
            _lastWaitingKeyPressed = keys;
            _isReading = false;
        }
        else
        {
            _lastKeyPressed = keys;
        }
    }

    public char WaitChar()
    {
        _isReading = true;

        while (_isReading)
        {
            Thread.Sleep(50);
        }

        return s_keyCharMap[_lastWaitingKeyPressed];
    }
}
