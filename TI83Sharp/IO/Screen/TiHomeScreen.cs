using System.Text;

namespace TI83Sharp;

public class TiHomeScreen
{
    public const int WIDTH = 16;
    public const int HEIGHT = 8;

    public Action<TiHomeScreen>? Change;

    private readonly char[,] _screen;
    private int _cursorX; // [1, WIDTH]
    private int _cursorY; // [1, HEIGHT]

    private int CursorX
    {
        get => _cursorX;
        set
        {
            _cursorX = value;
            if (_cursorX > WIDTH)
            {
                _cursorX = 1;
                ++CursorY;
            }
        }
    }

    private int CursorY
    {
        get => _cursorY;
        set
        {
            _cursorY = value;
            if (_cursorY > HEIGHT)
            {
                // Scroll
                for (int y = 1; y < HEIGHT; y++)
                {
                    for (int x = 1; x < WIDTH + 1; x++)
                    {
                        this[x, y] = this[x, y + 1];
                    }
                }
                // Clear last line
                for (int x = 1; x < WIDTH + 1; x++)
                {
                    this[x, HEIGHT] = default;
                }
                _cursorX = 1;
                _cursorY = HEIGHT;

                Change?.Invoke(this);
            }
        }
    }

    public char this[int x, int y]
    {
        get => _screen[x - 1, y - 1];
        set => _screen[x - 1, y - 1] = value;
    }

    public TiHomeScreen()
    {
        _screen = new char[WIDTH, HEIGHT];
        CursorX = 1;
        CursorY = 1;
    }

    public void Output(string str, int x, int y)
    {
        if (x < 1 || x > WIDTH || y < 1 || y > HEIGHT)
        {
            throw RuntimeError.Domain;
        }

        foreach (var c in str)
        {
            this[x, y] = c;
            ++x;
            if (x > WIDTH)
            {
                ++y;
                x = 1;
            }
            if (y > HEIGHT)
            {
                // If the text goes past the last column of the last row, the remainder will be truncated.
                // Output( will never cause the screen to scroll.
                break;
            }
        }

        Change?.Invoke(this);
    }

    public void Disp(string str, MessageAlignement alignement = MessageAlignement.Left | MessageAlignement.NewLine)
    {
        if (str.Length > WIDTH)
        {
            str = str[..(WIDTH - 3)] + "...";
        }

        if (str.Length == 0)
        {
            str = " "; // Consider empty string as whitespace to add new line with Disp("")
        }

        if (CursorX != 1 && alignement.HasFlag(MessageAlignement.NewLine))
        {
            ++CursorY;
            CursorX = 1;
        }

        if (alignement.HasFlag(MessageAlignement.Right))
        {
            CursorX = WIDTH - str.Length + 1;
        }

        for (int i = 0; i < str.Length; i++)
        {
            this[CursorX, CursorY] = str[i];
            ++CursorX;
        }

        Change?.Invoke(this);
    }

    public void Clear()
    {
        CursorX = 1;
        CursorY = 1;

        for (int x = 1; x < WIDTH + 1; x++)
        {
            for (int y = 1; y < HEIGHT + 1; y++)
            {
                this[x, y] = default;
            }
        }

        Change?.Invoke(this);
    }

    public override string ToString()
    {
        var screenStr = new StringBuilder();
        var lineStr = new StringBuilder();
        for (int y = 1; y < HEIGHT + 1; y++)
        {
            lineStr.Clear();
            for (int x = 1; x < WIDTH + 1; x++)
            {
                char c = this[x, y];
                lineStr.Append(c == '\0' ? ' ' : c);
            }

            var line = lineStr.ToString().Trim();
            if (line.Length > 0)
            {
                screenStr.AppendLine(line);
            }
        }

        return screenStr.ToString().Trim().Replace("\r\n", "\n");
    }
}
