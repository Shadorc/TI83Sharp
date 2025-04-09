using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SquareTests
{
    [TestMethod]
    public void TestSquare()
    {
        var source =
            @"
            :5.32²→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)28.302402f, environment.Get<TiNumber>("A"));

        source =
            @"
            :4²→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)16, environment.Get<TiNumber>("A"));

        source =
            @"
            :5→A
            :A²→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)25, environment.Get<TiNumber>("A"));
    }
}
