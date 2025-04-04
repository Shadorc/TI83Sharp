using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class DimTests
{
    [TestMethod]
    public void TestDim_List()
    {
        var source =
            @"
            :{1,2,3}→L₁
            :dim(L₁)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)3, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestDim_ListAssignAdd()
    {
        var source =
            @"
            :{1,2→L₁
            :4→dim(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 2, 0, 0 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestDim_ListAssignRemove()
    {
        var source =
            @"
            :{1,2→L₁
            :1→dim(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestDim_ListAssignCreate()
    {
        var source =
            @"
            :3→dim(L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 0, 0, 0 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestDim_AugmentOptimized()
    {
        var source =
            @"
            :{1,2,3}→L₁ 
            :4→X
            :X→L₁(1+dim(L₁
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 2, 3, 4 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestDim_ListAssignCreate_ErrInvalidDim()
    {
        var source =
            @"
            :1500→dim(L₁)
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestDim_Matrix()
    {
        var source =
            @"
            :[[1][2][3]]→[A]
            :dim([A])→L₁
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 3, 1 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestDim_MatrixAssignAdd()
    {
        var source =
            @"
            :[[1][2][3]]→[A]
            :{3,2→dim([A])
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
            new List<TiNumber> { 1, 0 },
            new List<TiNumber> { 2, 0 },
            new List<TiNumber> { 3, 0 }
        }), environment.Get<TiMatrix>("[A]"));
    }

    [TestMethod]
    public void TestDim_MatrixAssignRemove()
    {
        var source =
            @"
            :[[1][2][3]]→[A]
            :{2,1→dim([A])
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
            new List<TiNumber> { 1 },
            new List<TiNumber> { 2 }
        }), environment.Get<TiMatrix>("[A]"));
    }

    [TestMethod]
    public void TestDim_MatrixAssignCreate()
    {
        var source =
            @"
            :{2,1→dim([A])
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
            new List<TiNumber> { 0 },
            new List<TiNumber> { 0 }
        }), environment.Get<TiMatrix>("[A]"));
    }

    [TestMethod]
    public void TestDim_MatrixAssignCreate_ErrInvalidDim()
    {
        var source =
            @"
            :{1500,1→dim([A])
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestDim_ErrDataType()
    {
        var source =
            @"
            :1→A
            :dim(A)→B
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }
}
