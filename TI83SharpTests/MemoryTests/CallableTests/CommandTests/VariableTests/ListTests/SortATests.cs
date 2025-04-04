using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SortATests
{
    [TestMethod]
    public void TestSortA_SingleElement()
    {
        var source =
            @"
            :{42}→L₁
            :SortA(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 42 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortA_Simple()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :SortA(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 1, 3, 4, 5 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortA_MultipleLists()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :{9,2,6,5,3}→L₂
            :SortA(L₁, L₂)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 1, 3, 4, 5 }, environment.Get<TiList>("L₁"));
        Assert.AreEqual(new TiList() { 2, 5, 9, 6, 3 }, environment.Get<TiList>("L₂"));
    }

    [TestMethod]
    public void TestSortA_Floats()
    {
        var source =
            @"
            :{3.1,1.2,4.3,1.4,5.5}→L₁
            :SortA(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1.2f, 1.4f, 3.1f, 4.3f, 5.5f }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestSortA_MultipleListsWithFloats()
    {
        var source =
            @"
            :{3.1,1.2,4.3,1.4,5.5}→L₁
            :{9.9,2.2,6.6,5.5,3.3}→L₂
            :SortA(L₁, L₂)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1.2f, 1.4f, 3.1f, 4.3f, 5.5f }, environment.Get<TiList>("L₁"));
        Assert.AreEqual(new TiList() { 2.2f, 5.5f, 9.9f, 6.6f, 3.3f }, environment.Get<TiList>("L₂"));
    }

    [TestMethod]
    public void TestSortA_ErrDataType()
    {
        var source = ":SortA(5)";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }

    [TestMethod]
    public void TestSortA_ErrDimMismatch()
    {
        var source =
            @"
            :{3,1,4,1,5}→L₁
            :{9,2,6}→L₂
            :SortA(L₁, L₂)
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DIM MISMATCH");
    }
}
