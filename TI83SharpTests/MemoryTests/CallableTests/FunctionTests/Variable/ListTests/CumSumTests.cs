using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class CumSumTests
{
    [TestMethod]
    public void TestCumSum_List()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :cumSum(L₁)→L₂
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 3, 6, 10, 15 }, environment.Get<TiList>("L₂"));
    }

    [TestMethod]
    public void TestCumSum_Matrix()
    {
        var source =
            @"
            :[[1,2,3][4,5,6][7,8,9]]→[A]
            :cumSum([A])→[B]
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
            new List<TiNumber> { 1, 2, 3 },
            new List<TiNumber> { 5, 7, 9 },
            new List<TiNumber> { 12, 15, 18 }
        }), environment.Get<TiMatrix>("[B]"));
    }
}
