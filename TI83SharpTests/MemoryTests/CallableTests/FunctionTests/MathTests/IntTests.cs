using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class IntTests
{
    [TestMethod]
    public void TestInt()
    {
        var source =
            @"
            :int(5.32)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("A"));

        source =
            @"
            :int(4/5)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("A"));

        source =
            @"
            :int(-5.32)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)(-6), environment.Get<TiNumber>("A"));

        source =
            @"
            :int(-4/5)→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)(-1), environment.Get<TiNumber>("A"));
    }
}
