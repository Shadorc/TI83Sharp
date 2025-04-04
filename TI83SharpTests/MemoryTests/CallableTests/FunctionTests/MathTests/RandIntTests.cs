using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class RandIntTests
{
    [TestMethod]
    public void TestRandInt()
    {
        var source = ":randInt(1, 10)→A";
        Interpret(source, out _, out var environment);
        var a = environment.Get<TiNumber>("A");
        Assert.IsTrue(a >= (TiNumber)1 && a <= (TiNumber)10);
    }

    [TestMethod]
    public void TestRandInt_List()
    {
        var source = ":randInt(1, 10, 5)→L₁";
        Interpret(source, out _, out var environment);
        var list = environment.Get<TiList>("L₁");
        Assert.AreEqual(5, list.Count);
        foreach (var item in list)
        {
            var a = item;
            Assert.IsTrue(a >= (TiNumber)1 && a <= (TiNumber)10);
        }
    }

    [TestMethod]
    public void TestRandInt_ErrDomain()
    {
        var source = ":randInt(1, 10.5)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");

        source = ":randInt(1.5, 10)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");

        source = ":randInt(1, 10, 5.5)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestRandInt_ErrDataType()
    {
        var source = ":randInt(1, \"10\")";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");

        source = ":randInt(\"1\", 10)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");

        source = ":randInt(1, 10, \"5\")";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }
}
