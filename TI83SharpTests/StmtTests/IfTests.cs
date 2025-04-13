using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class IfTests
{
    [TestMethod]
    public void TestIf_True()
    {
        var source =
            @"
            :1→A
            :If A=1
            :Disp 1
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());
    }

    [TestMethod]
    public void TestIf_False()
    {
        var source =
            @"
            :1→A
            :If A=2
            :Disp 1
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual(string.Empty, homeScreen.ToString());
    }

    [TestMethod]
    public void TestIf_Else()
    {
        var source =
            @"
            :1→A
            :If A=2
            :Then
            :Disp 1
            :Else
            :Disp 2
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("2", homeScreen.ToString());
    }
}
