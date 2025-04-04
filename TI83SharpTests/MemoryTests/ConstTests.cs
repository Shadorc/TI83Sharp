using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ConstTests
{
    [TestMethod]
    public void TestPi()
    {
        var source = ":π→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)3.1415926535898f, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestE()
    {
        var source = ":e→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)2.718281828459f, environment.Get<TiNumber>("A"));
    }
}
