using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class WhileTests
{
    [TestMethod]
    public void TestWhile()
    {
        var source =
            @"
            :1→A
            :While A<5
            :Disp A
            :A+1→A
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1\n2\n3\n4", homeScreen.ToString());
    }

    [TestMethod]
    public void TestWhile_Nested()
    {
        var source =
            @"
            :1→A
            :While A<4
            :1→B
            :While B<2
            :Disp B
            :B+1→B
            :End
            :Disp A
            :A+1→A
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual("1\n1\n1\n2\n1\n3", homeScreen.ToString());
    }

    [TestMethod]
    public void TestWhile_Empty()
    {
        var source =
            @"
            :While 0
            :End
            ";
        Interpret(source, out var homeScreen, out _);
        Assert.AreEqual(string.Empty, homeScreen.ToString());
    }
}
