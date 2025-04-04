using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SortDTests
{
    [TestMethod]
    public void TestSortD_SingleElement()
    {
        var source =
            @"
            :{42}→L₁
            :SortD(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 42 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortD_Simple()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :SortD(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 5, 4, 3, 1, 1 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortD_MultipleLists()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :{9,2,6,5,3}→L₂
            :SortD(L₁, L₂)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 5, 4, 3, 1, 1 }, environment.Get<TiList>("L₁"));
        Assert.AreEqual(new TiList() { 3, 6, 9, 2, 5 }, environment.Get<TiList>("L₂"));
    }

    [TestMethod]
    public void TestSortD_Floats()
    {
        var source =
            @"
            :{3.1,1.2,4.3,1.4,5.5}→L₁
            :SortD(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 5.5f, 4.3f, 3.1f, 1.4f, 1.2f }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortD_MultipleListsWithFloats()
    {
        var source =
            @"
            :{3.1,1.2,4.3,1.4,5.5}→L₁
            :{9.9,2.2,6.6,5.5,3.3}→L₂
            :SortD(L₁, L₂)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 5.5f, 4.3f, 3.1f, 1.4f, 1.2f }, environment.Get<TiList>("L₁"));
        Assert.AreEqual(new TiList() { 3.3f, 6.6f, 9.9f, 5.5f, 2.2f }, environment.Get<TiList>("L₂"));
    }

    [TestMethod]
    public void TestSortD_ErrDataType()
    {
        var source = ":SortD(5)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }

    [TestMethod]
    public void TestSortD_ErrDimMismatch()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :{9,2,6}→L₂
            :SortD(L₁, L₂)
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DIM MISMATCH");
    }
}
