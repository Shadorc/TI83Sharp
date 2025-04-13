using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class RepeatTests
{
    [TestMethod]
    public void TestRepeat()
    {
        var source =
            @"
            :1→A
            :Repeat A>5
            :Disp A
            :A+1→A
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1\n2\n3\n4\n5", homeScreen.ToString());
    }

    [TestMethod]
    public void TestRepeat_Empty()
    {
        var source =
            @"
            :Repeat 1
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual(string.Empty, homeScreen.ToString());
    }

    [TestMethod]
    public void TestRepeat_False()
    {
        var source =
            @"
            :Repeat 1
            :Disp 1
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());
    }
}
