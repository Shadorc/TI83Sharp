using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ClrListTests
{
    [TestMethod]
    public void TestClrList()
    {
        var source =
            @"
            :{1,2}→L₁
            :ClrList L₁
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("L₁").Count);
    }

    [TestMethod]
    public void TestClrList_Multiple()
    {
        var source =
            @"
            :{1,2}→L₁
            :{5}→L₂
            :ClrList L₁,L₂
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("L₁").Count);
        Assert.AreEqual(0, environment.Get<TiList>("L₂").Count);
    }

    [TestMethod]
    public void TestClrList_Named()
    {
        var source =
            @"
            :{1,2}→∟LIST
            :ClrList ∟LIST
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual(0, environment.Get<TiList>("∟LIST").Count);
    }

    [TestMethod]
    public void TestClrList_ErrSyntax()
    {
        var source =
            @"
            :{1,2}→∟LIST
            :ClrList LIST
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:SYNTAX");
    }
}
