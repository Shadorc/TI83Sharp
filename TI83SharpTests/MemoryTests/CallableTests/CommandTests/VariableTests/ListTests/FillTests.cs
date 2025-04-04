using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class FillTests
{
    [TestMethod]
    public void TestFill_List()
    {
        var source =
            @"
            :{1,2}→L₁
            :Fill(0,L₁)
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 0, 0 }, environment.Get<TiList>("L₁"));
    }

    [TestMethod]
    public void TestFill_Matrix()
    {
        var source =
            @"
            :[[1,1][5,6]]→[A]
            :Fill(0,[A])
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> { new List<TiNumber> { 0, 0 }, new List<TiNumber> { 0, 0 } }), environment.Get<TiMatrix>("[A]"));
    }

    [TestMethod]
    public void TestFill_ErrDataType()
    {
        var source =
            @"
            :Fill(0,5)
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");

        source =
            @"
            :Fill(0,{1,2})
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }
}
