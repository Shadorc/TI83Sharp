using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class MaxTests
{
    [TestMethod]
    public void TestMax_List()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :max(L₁)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber).5f, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestMax_NumberNumber()
    {
        var source =
            @"
            :.2→A
            :.5→B
            :max(A,B)→C
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber).5f, environment.Get<TiNumber>("C"));
    }

    [TestMethod]
    public void TestMax_NumberList()
    {
        var source =
            @"
            :.2→A
            :{.5,.3,.1}→L₂
            :max(A,L₂)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .5f, .3f, .2f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMax_ListNumber()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :.2→A
            :max(L₁,A)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .5f, .3f, .2f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMax_ListList()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁ 
            :{.2,.4,.6}→L₂
            :max(L₁,L₂)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { .5f, .6f }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestMax_ErrDimMismatch()
    {
        var source =
            @"
            :{.5,.3,.1}→L₁
            :{.2,.4,.6,.8}→L₂
            :max(L₁,L₂)→L₃
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DIM MISMATCH");
    }

    [TestMethod]
    public void TestMax_ErrInvalidDim()
    {
        var source =
            @"
            :{}→L₁
            :max(A)→L₂
            ";
        Assert.ThrowsException<SyntaxError>(() => Interpret(source, out _, out _));
    }
}
