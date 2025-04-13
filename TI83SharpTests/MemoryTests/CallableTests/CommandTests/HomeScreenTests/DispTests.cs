using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class DispTests
{
    [TestMethod]
    public void TestDisp_NoArg()
    {
        var source = ":Disp";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual(string.Empty, homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_Const()
    {
        var source = ":Disp 5";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("5", homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_Str()
    {
        var source = ":Disp \"Hello World :";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("Hello World :", homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_Strs()
    {
        var source = ":Disp \"Hello World\", \"Foo\"";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("Hello World\nFoo", homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_Var()
    {
        var source =
            @"
            :123→A
            :Disp A
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("123", homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_Consts()
    {
        var source = @":Disp 6,5";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("6\n5", homeScreen.ToString());
    }

    [TestMethod]
    public void TestDisp_NotChainable()
    {
        var source = @":Disp 6Disp 5";
        Assert.ThrowsException<SyntaxError>(() => Interpret(source, out _, out _));
    }
}
