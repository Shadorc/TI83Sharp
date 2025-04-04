using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ReturnTests
{
    [TestMethod]
    public void TestReturn()
    {
        var source =
            @"
            :Return
            :Disp 1
            ";
        Interpret(source, out var output, out _);
        Assert.IsTrue(output.MessageOutput.Length == 0);

        source =
            @"
            :Disp 1
            :Return
            ";
        Interpret(source, out output, out _);
        Assert.AreEqual("1", output.MessageOutput);
    }

    [TestMethod]
    public void TestReturn_Loop()
    {
        var source =
            @"
            :While 1
            :Return
            :End
            :Disp 1
            ";
        Interpret(source, out var output, out _);
        Assert.IsTrue(output.MessageOutput.Length == 0);
    }
}
