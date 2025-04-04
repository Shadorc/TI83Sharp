namespace TI83Sharp;

public class GotoException : Exception
{
    public readonly string Label;

    public GotoException(string label)
    {
        Label = label;
    }
}
