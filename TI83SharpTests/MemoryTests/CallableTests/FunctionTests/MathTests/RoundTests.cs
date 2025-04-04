using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class RoundTests
{
    [TestMethod]
    public void TestRound()
    {
        var source =
            @"
            :round(5.45,0)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("A"));

        source =
            @"
            :round(5.65,0)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)6, environment.Get<TiNumber>("A"));

        source =
            @"
            :round(-5.65,0)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)(-6), environment.Get<TiNumber>("A"));

        source =
            @"
            :round(π,4)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)3.1416f, environment.Get<TiNumber>("A"));

        source =
            @"
            :round(π)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)3.141592653f, environment.Get<TiNumber>("A"));
    }
}
