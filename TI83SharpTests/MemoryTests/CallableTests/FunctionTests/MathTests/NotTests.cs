using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class NotTests
{
    [TestMethod]
    public void TestNot_True()
    {
        var source = ":Disp not(0)";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());

        source = ":Disp not(0";
        Interpret(source, out homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());
    }

    [TestMethod]
    public void TestNot_False()
    {
        var source = ":Disp not(1)";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("0", homeScreen.ToString());

        source = ":Disp not(1";
        Interpret(source, out homeScreen, out _);
        Assert.AreEqual("0", homeScreen.ToString());
    }

    [TestMethod]
    public void TestNot_EmptyVar()
    {
        var source = ":Disp not(A)";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());

        source = ":Disp not(A";
        Interpret(source, out homeScreen, out _);
        Assert.AreEqual("1", homeScreen.ToString());
    }

    [TestMethod]
    public void TestNot_Var()
    {
        var source =
            @"
            :50→A
            :Disp not(A)
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("0", homeScreen.ToString());

        source =
            @"
            :50→A
            :Disp not(A
            ";
        Interpret(source, out homeScreen, out _);
        Assert.AreEqual("0", homeScreen.ToString());
    }
}
