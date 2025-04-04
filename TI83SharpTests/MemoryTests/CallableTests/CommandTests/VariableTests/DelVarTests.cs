using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class DelVarTests
{
    [TestMethod]
    public void TestDelVar()
    {
        var source =
            @"
            :10→A
            :DelVar A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestDelVar_Chainable()
    {
        var source =
            @"
            :10→A
            :20→B
            :DelVar ADelVar B";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("A"));
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestDelVar_MultipleLines()
    {
        var source =
            @"
            :10→A
            :20→B
            :DelVar A
            :DelVar B";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("A"));
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestDelVar_NonVariableArgument()
    {
        var source = ":DelVar 10";
        AssertInterpretThrows<RuntimeError>(source, "ERR:SYNTAX");
    }
}
