using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class StopTests
{
    [TestMethod]
    public void TestStop()
    {
        var source =
            @"
            :Stop
            :Disp 1
            ";
        Interpret(source, out var output, out _);
        Assert.IsTrue(output.MessageOutput.Length == 0);

        source =
            @"
            :Disp 1
            :Stop
            ";
        Interpret(source, out output, out _);
        Assert.AreEqual("1", output.MessageOutput);
    }

    [TestMethod]
    public void TestStop_Loop()
    {
        var source =
            @"
            :While 1
            :Stop
            :End
            :Disp 1
            ";
        Interpret(source, out var output, out _);
        Assert.IsTrue(output.MessageOutput.Length == 0);
    }
}
