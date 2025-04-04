namespace TI83Sharp;

public class ScreenChangedEventArgs : EventArgs
{
    public readonly TiHomeScreen Screen;

    public ScreenChangedEventArgs(TiHomeScreen screen)
    {
        Screen = screen;
    }
}
