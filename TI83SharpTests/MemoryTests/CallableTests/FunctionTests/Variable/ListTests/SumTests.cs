using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SumTests
{
    [TestMethod]
    public void TestSum_Simple()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁)→A
            ";
        Interpret(source, out _, out var environment);

        Assert.AreEqual((TiNumber)15, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_SingleElement()
    {
        var source =
            @"
            :{42}→L₁
            :sum(L₁)→A
            ";
        Interpret(source, out _, out var environment);

        Assert.AreEqual((TiNumber)42, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_NegativeNumbers()
    {
        var source =
            @"
            :{-1,-2,-3,-4,-5}→L₁
            :sum(L₁)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(-(TiNumber)15, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_Floats()
    {
        var source =
            @"
            :{1.1,2.2,3.3,4.4,5.5}→L₁
            :sum(L₁)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)16.5f, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_WithStartAndEnd()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁, 2, 4)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)9, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_WithStartOnly()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁, 3)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)12, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSum_InvalidStart()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁, 0)→A
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestSum_InvalidEnd()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁, 1, 6)→A
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestSum_StartGreaterThanEnd()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :sum(L₁, 4, 2)→A
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestSum_NonListArgument()
    {
        var source = ":sum(5)→A";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }
}
