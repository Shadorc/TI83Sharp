using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class DeltaListTests
{
    [TestMethod]
    public void TestDeltaList()
    {
        var source =
            @"
            :{1,2,3,4,5}→L₁
            :ΔList(L₁)→L₂
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 1, 1, 1, 1 }, environment.Get<TiList>("L₂"));
    }
}
