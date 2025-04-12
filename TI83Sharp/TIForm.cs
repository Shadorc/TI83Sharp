using System.ComponentModel;
using System.Drawing.Text;
using System.Numerics;
using System.Runtime.InteropServices;

namespace TI83Sharp;

public partial class TIForm : Form
{
    public const string WINDOW_TITLE = "TI83Sharp";

    private const float ORIGINAL_FONT_SIZE = 17f;

    private static readonly Vector2 s_originalScreenPos = new Vector2(65, 99);
    private static readonly Vector2 s_originalScreenSize = new Vector2(315, 195);
    private static readonly Brush s_blackBrush = Pens.Black.Brush;
    private static readonly PrivateFontCollection s_fontCollection = new PrivateFontCollection();

    private readonly Brush _backBrush;
    private readonly float _aspectRatio;
    private readonly int _originalWidth;
    private readonly Image _ti83Image;
    private readonly BufferedGraphicsContext _bufferedGraphicsContext;
    private readonly System.Windows.Forms.Timer _refreshTimer;

    private Font _font;
    private TiHomeScreen? _screen;
    private float _resizeRatio = 1f;
    private BufferedGraphics _bufferedGraphics;

    public TIForm()
    {
        InitializeComponent();

        Text = WINDOW_TITLE;
        Size = new Size(500, 1000);
        MinimumSize = Size / 2;

        _backBrush = new SolidBrush(BackColor);
        _aspectRatio = (float)Width / Height;
        _originalWidth = Width;

        var resources = new ComponentResourceManager(typeof(TIForm));
        _ti83Image = LoadImage(resources, "ti83+");
        _font = LoadFont(resources, "ti-83-plus-large", ORIGINAL_FONT_SIZE);

        Resize += new EventHandler(OnResize);
        Paint += new PaintEventHandler(OnPaint);

        // Enable double buffering to reduce flickering
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);

        _bufferedGraphicsContext = BufferedGraphicsManager.Current;
        _bufferedGraphicsContext.MaximumBuffer = new Size(Width + 1, Height + 1);
        _bufferedGraphics = _bufferedGraphicsContext.Allocate(CreateGraphics(), ClientRectangle);

        var g = _bufferedGraphics.Graphics;
        PaintCalculator(g);

        // Initialize and start the refresh timer
        _refreshTimer = new System.Windows.Forms.Timer
        {
            // TODO: Synchronizes interpreter refresh to this value
            Interval = 16 // 16 milliseconds ~ 60 FPS
        };
        _refreshTimer.Tick += (sender, e) => Invalidate();
        _refreshTimer.Start();
    }

    ~TIForm()
    {
        s_blackBrush.Dispose();
        _ti83Image.Dispose();
        _font.Dispose();
        _bufferedGraphics?.Dispose();
        _refreshTimer.Dispose();
    }

    public void OnScreenChange(object? sender, ScreenChangedEventArgs e)
    {
        _screen = e.Screen;
    }

    private void OnResize(object? sender, EventArgs e)
    {
        // Conserve original aspect ratio
        int newWidth = (int)(Height * _aspectRatio);
        int newHeight = (int)(Width / _aspectRatio);
        if (newWidth != Width)
        {
            Width = newWidth;
        }
        else
        {
            Height = newHeight;
        }

        _resizeRatio = (float)Width / _originalWidth;
        ResizeFont();

        _bufferedGraphicsContext.MaximumBuffer = new Size(Width + 1, Height + 1);
        _bufferedGraphics?.Dispose();
        _bufferedGraphics = _bufferedGraphicsContext.Allocate(CreateGraphics(), ClientRectangle);

        var g = _bufferedGraphics.Graphics;
        PaintCalculator(g);

        Invalidate();
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        var g = _bufferedGraphics.Graphics;
        PaintScreen(g);

        _bufferedGraphics.Render(e.Graphics);
    }

    private void PaintCalculator(Graphics g)
    {
        g.Clear(BackColor);

        var calcRect = GetCalculatorRect(_ti83Image);
        g.DrawImage(_ti83Image, calcRect.X, calcRect.Y, calcRect.Width, calcRect.Height);
    }

    private void PaintScreen(Graphics g)
    {
        if (_screen == null)
        {
            return;
        }

        var tiRect = GetCalculatorRect(_ti83Image);
        var screenRect = GetScreenRect(new Vector2(tiRect.X, tiRect.Y));

        g.FillRectangle(_backBrush, screenRect);

        for (int x = 0; x < TiHomeScreen.WIDTH; x++)
        {
            for (int y = 0; y < TiHomeScreen.HEIGHT; y++)
            {
                var c = _screen[x + 1, y + 1];
                var charX = screenRect.X + screenRect.Width / TiHomeScreen.WIDTH * x;
                var charY = screenRect.Y + screenRect.Height / TiHomeScreen.HEIGHT * y;
                g.DrawString(c.ToString(), _font, s_blackBrush, charX, charY);
            }
        }
    }

    private Rectangle GetCalculatorRect(Image image)
    {
        var screenRectangle = RectangleToScreen(ClientRectangle);
        int topOffset = screenRectangle.Top - Top;
        int sideOffset = screenRectangle.Left - Left - (screenRectangle.Right - Right);

        float imageAspectRatio = (float)image.Width / image.Height;

        int imgHeight = Height - topOffset;
        int imgWidth = (int)(imgHeight * imageAspectRatio);

        // Centered horizontally
        int x = (Width - imgWidth - sideOffset) / 2;
        return new Rectangle(x, 0, imgWidth, imgHeight);
    }

    private Rectangle GetScreenRect(Vector2 calculatorPos)
    {
        var screenPos = s_originalScreenPos * _resizeRatio + calculatorPos;
        var screenSize = s_originalScreenSize * _resizeRatio;
        return new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)screenSize.X, (int)screenSize.Y);
    }

    private void ResizeFont()
    {
        float fontSize = ORIGINAL_FONT_SIZE * _resizeRatio;
        if (fontSize != _font.Size)
        {
            _font = new Font(_font.FontFamily, fontSize);
        }
    }

    private static Image LoadImage(ComponentResourceManager res, string name)
    {
        if (res.GetObject(name) is not byte[] buffer)
        {
            throw new ArgumentException($"Image '{name}' not found");
        }

        using var stream = new MemoryStream(buffer);
        return Image.FromStream(stream);
    }

    private static Font LoadFont(ComponentResourceManager res, string name, float emSize)
    {
        if (res.GetObject(name) is not byte[] buffer)
        {
            throw new ArgumentException($"Font '{name}' not found");
        }

        using var stream = new MemoryStream(buffer);
        var fontData = new byte[stream.Length];
        stream.Read(fontData, 0, (int)stream.Length);
        var fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
        Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
        s_fontCollection.AddMemoryFont(fontPtr, fontData.Length);
        Marshal.FreeCoTaskMem(fontPtr);
        return new Font(s_fontCollection.Families[0], emSize);
    }

    public static void ShowErrorBeforeExit(string message)
    {
        MessageBox.Show(message, $"{WINDOW_TITLE}: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        System.Environment.Exit(-1);
    }
}
