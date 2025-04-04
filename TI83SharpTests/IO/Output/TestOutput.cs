using System.Text;
using TI83Sharp;

namespace TI83SharpTests;

public class TestOutput : IOutput
{
    public string MessageOutput => NormalizeLogs(MessageOutputBuilder);
    public string ErrorOutput => NormalizeLogs(ErrorOutputBuilder);

    private readonly StringBuilder MessageOutputBuilder = new StringBuilder();
    private readonly StringBuilder ErrorOutputBuilder = new StringBuilder();

    public void Message(string message, LogAlignement alignement /* ignored */)
    {
        MessageOutputBuilder.AppendLine(message);
    }

    public void Message(string message, int x /* ignored */, int y /* ignored */)
    {
        MessageOutputBuilder.AppendLine(message);
    }

    public void Error(string message)
    {
        ErrorOutputBuilder.AppendLine(message);
    }

    public void Error(Exception exception)
    {
        ErrorOutputBuilder.AppendLine(exception.Message);
    }

    public void Clear()
    {
        MessageOutputBuilder.Clear();
        ErrorOutputBuilder.Clear();
    }

    private static string NormalizeLogs(StringBuilder logs)
    {
        return logs.ToString().Replace("\r\n", "\n").Trim();
    }
}
