using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class InStringTests
{
    [TestMethod]
    public void TestInString()
    {
        var source = @"
            :inString(""TI-BASIC"",""BASIC→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)4, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestInString_NotFound()
    {
        var source = @"
            :inString(""TI-BASIC"",""ELEPHANT→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestInString_Start()
    {
        var source = @"
            :inString(""TI-BASIC"",""I"",2→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)2, environment.Get<TiNumber>("A"));

        source = @"
            :inString(""TI-BASIC"",""I"",3→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)7, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestInString_ErrDomain()
    {
        var source = @"
            :inString(""TI-BASIC"",""BASIC"",1.2→A
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }
}