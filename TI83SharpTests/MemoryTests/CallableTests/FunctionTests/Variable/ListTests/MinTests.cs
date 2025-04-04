using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class MinTests
{
    [TestMethod]
    public void TestMin_List()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :min(L₁)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber).1f, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestMin_NumberNumber()
    {
        var source =
            @"
            :.2→A
            :.5→B
            :min(A,B)→C
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber).2f, environment.Get<TiNumber>("C"));
    }

    [TestMethod]
    public void TestMin_NumberList()
    {
        var source =
            @"
            :.2→A
            :{.5,.3,.1}→L₂
            :min(A,L₂)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .2f, .2f, .1f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMin_ListNumber()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :.2→B
            :min(L₁,B)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .2f, .2f, .1f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMin_ListList()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :{.2,.4,.6}→L₂
            :min(L₁,L₂)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .1f, .2f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMin_ErrDimMismatch()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :{.2,.4,.6,.8}→L₂
            :min(L₁,L₂)→L₃
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DIM MISMATCH");
    }

    [TestMethod]
    public void TestMin_ErrInvalidDim()
    {
        var source =
            @"
            :{}→A
            :min(A)→B
            ";
        Assert.ThrowsException<SyntaxError>(() => Interpret(source, out _, out _));
    }
}
