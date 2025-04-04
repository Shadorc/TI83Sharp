using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class GotoTests
{
    [TestMethod]
    public void TestGoto()
    {
        var source =
            @"
            :Goto 22
            :Disp 1
            :Lbl 1
            :Disp 2
            :Goto AB
            :Lbl 22
            :Disp 3 
            :Goto 1
            :Lbl AB
            ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("3\n2", logger.MessageOutput);
    }

    [TestMethod]

    public void TestGoto_NoLbl()
    {
        var source =
            @"
            :Goto 0
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:LABEL");
    }
}
