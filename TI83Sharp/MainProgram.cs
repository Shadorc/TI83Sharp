using CommandLine;
using CommandLine.Text;

namespace TI83Sharp;

public static class MainProgram
{
    private class Options
    {
        [Option(Required = false, HelpText = "Script file to be processed.")]
        public string? ScriptFile { get; set; }

        [Option(Required = false, HelpText = "Script text to be processed.")]
        public string? ScriptText { get; set; }
    }

    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        var parseResult = CommandLine.Parser.Default.ParseArguments<Options>(args);
        parseResult
            .WithParsed(RunOptions)
            .WithNotParsed(_ => DisplayHelp(parseResult));
    }

    private static void DisplayHelp<T>(ParserResult<T> result)
    {
        var helpText = HelpText.AutoBuild(result, h =>
        {
            h.AdditionalNewLineAfterOption = false;
            h.Heading = TIForm.WINDOW_TITLE;
            h.Copyright = string.Empty;
            h.AutoVersion = false;
            return h;
        });
        TIForm.ShowErrorDialog(helpText, -1);
    }

    private static void RunOptions(Options opts)
    {
        if (!string.IsNullOrWhiteSpace(opts.ScriptText))
        {
            Execute(opts.ScriptText);
        }
        else if (!string.IsNullOrWhiteSpace(opts.ScriptFile))
        {
            string scriptFile = opts.ScriptFile;
            if (!File.Exists(scriptFile))
            {
                TIForm.ShowErrorDialog($"File '{scriptFile}' does not exist", -1);
                return;
            }

            if (Path.GetExtension(scriptFile) != ".bas")
            {
                TIForm.ShowErrorDialog($"File '{scriptFile}' format is not supported, expected .bas extension", -1);
                return;
            }

            Execute(File.ReadAllText(scriptFile));
        }
        else
        {
            TIForm.ShowErrorDialog($"Either '-{nameof(Options.ScriptText).ToLower()}' or '-{nameof(Options.ScriptFile).ToLower()}' must be specified and non empty", -1);
        }
    }

    private static void Execute(string content)
    {
        Application.Run(new TIForm(content));
    }
}