using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ExprTests
{
    [TestMethod]
    public void TestExpr_ImplicitMultiplication()
    {
        var source = ":2(5-3)";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("4", logger.MessageOutput);
    }

    [TestMethod]
    public void TestExpr_ConditionOptimized()
    {
        /*
         * :If A=B
         * :C+2→C
         */
        var source =
            @"
            :1→A
            :1→B
            :3→C
            :C+2(A=B→C
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("C"));
    }

    [TestMethod]
    public void TestExpr_Noop()
    {
        var source =
            @"
            :
            ";
        Interpret(source, out _, out var environment);
    }
}
