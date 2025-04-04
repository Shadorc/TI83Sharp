namespace TI83Sharp;

public interface IOutput
{
    public void Message(string value, LogAlignement alignement = LogAlignement.Left | LogAlignement.NewLine);

    public void Message(string value, int x, int y);

    public void Error(string message);

    public void Error(Exception exception);

    public void Clear();
}