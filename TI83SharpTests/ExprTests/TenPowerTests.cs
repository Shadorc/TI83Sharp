using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class TenPowerTests
{
    [TestMethod]
    public void TestTenPower_PositiveExponent()
    {
        var source = ":5ᴇ3→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5000, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestTenPower_NegativeExponent()
    {
        var source = ":5ᴇ-5→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0.00005, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestTenPower_ImplicitExponent()
    {
        var source = ":ᴇ-3→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0.001, environment.Get<TiNumber>("A"));
    }
}
