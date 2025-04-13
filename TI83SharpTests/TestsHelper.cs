using System.Diagnostics.CodeAnalysis;
using TI83Sharp;

namespace TI83SharpTests;

public class TestsHelper
{
    public static void Interpret(string source, out TiHomeScreen homeScreen, [MaybeNullWhen(false)] out TI83Sharp.Environment environment)
    {
        homeScreen = new TiHomeScreen();
        var input = new TiScreenInput();

        var scanner = new Scanner(source);

        var tokens = new List<Token>();
        scanner.ScanTokens(tokens);

        var parser = new Parser(tokens);
        var tree = parser.Parse();

        var interpreter = new Interpreter(homeScreen, input);
        interpreter.Interpret(tree);

        environment = interpreter.Environment;
    }

    public static void AssertInterpretThrows<T>(string source, string errMessage) where T : Exception
    {
        var err = Assert.ThrowsException<T>(() => Interpret(source, out _, out _));
        Assert.AreEqual(errMessage, err.Message);
    }
}