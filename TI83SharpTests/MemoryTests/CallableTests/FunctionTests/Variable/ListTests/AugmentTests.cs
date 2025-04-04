using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class AugmentTests
{
    [TestMethod]
    public void TestAugment_List()
    {
        var source =
            @"
            :{1,2,3}→L₁
            :{4,5,6}→L₂
            :augment(L₁,L₂)→L₃
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 2, 3, 4, 5, 6 }, environment.Get<TiList>("L₃"));
    }

    [TestMethod]
    public void TestAugment_Matrix()
    {
        var source =
            @"
            :[[1,2][3,4]]→[A]
            :[[5,6][7,8]]→[B]
            :augment([A],[B])→[C]
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
            new List<TiNumber> { 1, 2, 5, 6 },
            new List<TiNumber> { 3, 4, 7, 8 }
        }), environment.Get<TiMatrix>("[C]"));
    }

    [TestMethod]
    public void TestAugment_ErrDataType()
    {
        var source =
            @"
            :{4,5,6}→L₂
            :augment(5,L₂)→L₃
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }

    [TestMethod]
    public void TestAugment_ErrDimMismatch()
    {
        var source =
            @"
            :[[1,2][3,4]]→[A]
            :[[1,2,3]]→[B]
            :augment([A],[B])→[C]
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DIM MISMATCH");
    }
}
