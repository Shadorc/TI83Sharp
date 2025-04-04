using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ClrAllListsTests
{
    [TestMethod]
    public void TestClrAllLists()
    {
        var source =
            @"
            :{1,2}→L₁
            :ClrAllLists
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("L₁").Count);
    }

    [TestMethod]
    public void TestClrAllLists_Multiple()
    {
        var source =
            @"
            :{1,2}→L₁
            :{5}→L₂
            :ClrAllLists
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("L₁").Count);
        Assert.AreEqual(0, environment.Get<TiList>("L₂").Count);
    }

    [TestMethod]
    public void TestClrAllLists_Named()
    {
        var source =
            @"
            :{1,2}→∟LIST
            :ClrAllLists
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("∟LIST").Count);
    }
}
