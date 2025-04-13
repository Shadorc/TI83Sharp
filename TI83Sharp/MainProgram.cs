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

        [Option(Required = false, HelpText = "Execute in the console.")]
        public bool NoGUI { get; set; }
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
        ShowErrorBeforeExit(false, helpText);
    }

    private static void RunOptions(Options opts)
    {
        if (!string.IsNullOrWhiteSpace(opts.ScriptText))
        {
            Execute(opts.NoGUI, opts.ScriptText);
        }
        else if (!string.IsNullOrWhiteSpace(opts.ScriptFile))
        {
            string scriptFile = opts.ScriptFile;
            if (!File.Exists(scriptFile))
            {
                ShowErrorBeforeExit(opts.NoGUI, $"File '{scriptFile}' does not exist");
                return;
            }

            if (Path.GetExtension(scriptFile) != ".bas")
            {
                ShowErrorBeforeExit(opts.NoGUI, $"File '{scriptFile}' format is not supported, expected .bas extension");
                return;
            }

            Execute(opts.NoGUI, File.ReadAllText(scriptFile));
        }
        else
        {
            ShowErrorBeforeExit(opts.NoGUI, $"Either '-{nameof(Options.ScriptText).ToLower()}' or '-{nameof(Options.ScriptFile).ToLower()}' must be specified and non empty");
        }
    }

    private static void ShowErrorBeforeExit(bool noGUI, string message)
    {
        if (noGUI)
        {
            ConsoleOutput.ShowErrorBeforeExit(message);
        }
        else
        {
            TIForm.ShowErrorBeforeExit(message);
        }
    }

    private async static void Execute(bool noGUI, string content)
    {
        if (noGUI)
        {
            var consoleOutput = new ConsoleOutput();

            var screen = new TiHomeScreen();
            screen.Change += consoleOutput.OnScreenChange;

            var input = new ConsoleInput();
            input.ReadInputAsync();

            Interpret(content, screen, input);
        }
        else
        {
            var tiForm = new TIForm();

            var screen = new TiHomeScreen();
            screen.Change += tiForm.OnScreenChange;

            var input = new TiScreenInput();
            tiForm.KeyDown += (sender, e) => input.OnKeyPressed(e.KeyCode);

            var interpreterTask = Task.Run(() => Interpret(content, screen, input));

            Application.Run(tiForm);

            await interpreterTask;
        }
    }

    private static void Interpret(string content, TiHomeScreen homeScreen, IInput input)
    {
        try
        {
            var scanner = new Scanner(content);

            var tokens = new List<Token>();
            scanner.ScanTokens(tokens);

            var parser = new Parser(tokens);
            var statements = parser.Parse();

            var interpreter = new Interpreter(homeScreen, input);
            interpreter.Interpret(statements);
        }
        catch (Exception err)
        {
            if (err is SyntaxError || err is RuntimeError)
            {
                foreach (var line in err.Message.Split('\n'))
                {
                    homeScreen.Disp(line);
                }
            }

            throw;
        }
    }
}