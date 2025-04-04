using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ForTests
{
    [TestMethod]
    public void TestFor()
    {
        var source =
            @"
            :For(A,1,5)
            :Disp A
            :End
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("1\n2\n3\n4\n5", logger.MessageOutput);
    }

    [TestMethod]
    public void TestFor_Optimized()
    {
        var source =
            @"
            :For(A,1,5
            :Disp A
            :End
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("1\n2\n3\n4\n5", logger.MessageOutput);
    }

    [TestMethod]
    public void TestFor_Step()
    {
        var source =
            @"
            :For(A,1,6,2)
            :Disp A
            :End
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("1\n3\n5", logger.MessageOutput);
    }

    [TestMethod]
    public void TestFor_NegativeStep()
    {
        var source =
            @"
            :For(A,6,1,-2)
            :Disp A
            :End
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("6\n4\n2", logger.MessageOutput);
    }

    [TestMethod]
    public void TestFor_Empty()
    {
        var source =
            @"
            :For(A,1,5)
            :End
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual(string.Empty, logger.MessageOutput);
    }
}
