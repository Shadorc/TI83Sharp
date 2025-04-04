using System.Diagnostics.CodeAnalysis;
using TI83Sharp;

namespace TI83SharpTests;

public class TestsHelper
{
    public static void Interpret(string source, out TestOutput output, [MaybeNullWhen(false)] out TI83Sharp.Environment environment)
    {
        output = new TestOutput();
        var input = new TiScreenInput();

        var scanner = new Scanner(output, source);

        var tokens = new List<Token>();
        scanner.ScanTokens(tokens);

        var parser = new Parser(tokens);
        var tree = parser.Parse();

        var interpreter = new Interpreter(output, input);
        interpreter.Interpret(tree);

        if (!string.IsNullOrEmpty(output.ErrorOutput))
        {
            Console.Error.WriteLine(output.ErrorOutput);
        }

        environment = interpreter.Environment;
    }

    public static void AssertInterpretThrows<T>(string source, string errMessage) where T : Exception
    {
        var err = Assert.ThrowsException<T>(() => Interpret(source, out _, out _));
        Assert.AreEqual(errMessage, err.Message);
    }
}