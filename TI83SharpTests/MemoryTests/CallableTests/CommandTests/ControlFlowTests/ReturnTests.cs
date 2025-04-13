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
        Interpret(source, out var homeScreen, out _);
        Assert.IsTrue(homeScreen.ToString().Length == 0);

        source =
            @"
            :Disp 1
            :Return
            ";
        Interpret(source, out homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());
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
        Interpret(source, out var homeScreen, out _);
        Assert.IsTrue(homeScreen.ToString().Length == 0);
    }
}
