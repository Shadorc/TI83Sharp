using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SeqTests
{
    [TestMethod]
    public void TestSeq()
    {
        var source = ":seq(I,I,3,7→L₁";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(new TiList() { 3, 4, 5, 6, 7 }, environment.Get<TiList>("L₁"));

        source =
            @"
            :2→A            
            :seq(AX^2,X,1,7→L₁
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual(new TiList() { 2, 8, 18, 32, 50, 72, 98 }, environment.Get<TiList>("L₁"));

        source = ":seq(I,I,7,1,-1)→L₁";
        Interpret(source, out _, out environment);
        Assert.AreEqual(new TiList() { 7, 6, 5, 4, 3, 2, 1 }, environment.Get<TiList>("L₁"));
    }
}
