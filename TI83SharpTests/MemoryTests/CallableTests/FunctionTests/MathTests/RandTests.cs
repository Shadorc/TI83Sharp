using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class RandTests
{
    [TestMethod]
    public void TestRand()
    {
        var source = ":rand→A";
        Interpret(source, out _, out var environment);
        var a = environment.Get<TiNumber>("A");
        Assert.IsTrue(a >= (TiNumber)0 && a < (TiNumber)1);
    }

    [TestMethod]
    public void TestRand_List()
    {
        var source = ":rand(5)→L₁";
        Interpret(source, out _, out var environment);
        var list = environment.Get<TiList>("L₁");
        Assert.AreEqual(5, list.Count);
        foreach (var item in list)
        {
            var a = item;
            Assert.IsTrue(a >= (TiNumber)0 && a < (TiNumber)1);
        }
    }

    [TestMethod]
    public void TestRand_ErrDomain()
    {
        var source = ":rand(1.5)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestRand_ErrDataType()
    {
        var source = ":rand(\"1\")";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }

    [TestMethod]
    public void TestRand_Seed()
    {
        var source =
            @"
            :0→rand
            :rand→A
            ";
        Interpret(source, out _, out var environment);
        var a = environment.Get<TiNumber>("A");
        Assert.AreEqual((TiNumber)0.72624326f, a);
    }
}
