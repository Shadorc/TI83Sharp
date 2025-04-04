using CommandLine;

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

    private readonly static ConsoleOutput s_defaultLogger = new ConsoleOutput();

    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        CommandLine.Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    private static void HandleParseError(IEnumerable<CommandLine.Error> errors)
    {
        foreach (var error in errors)
        {
            s_defaultLogger.Error(error.ToString()!);
        }
        System.Environment.Exit(-1);
    }

    private static void RunOptions(Options opts)
    {
        if (opts.ScriptText != null)
        {
            Execute(opts.ScriptText);
        }
        else if (opts.ScriptFile != null)
        {
            string scriptFile = opts.ScriptFile;
            if (!File.Exists(scriptFile))
            {
                s_defaultLogger.Error($"'{scriptFile}' does not exist");
                System.Environment.Exit(-1);
                return;
            }

            if (Path.GetExtension(scriptFile) != ".bas")
            {
                s_defaultLogger.Error($"'{scriptFile}' format is not supported, expected .bas extension");
                System.Environment.Exit(-1);
                return;
            }

            Execute(File.ReadAllText(scriptFile));
        }
        else
        {
            s_defaultLogger.Error($"'{nameof(Options.ScriptText)}' or '{nameof(Options.ScriptFile)}' args must be specified");
            System.Environment.Exit(-1);
        }
    }

    private static void Execute(string content)
    {
        Application.Run(new TIForm(content));
    }
}