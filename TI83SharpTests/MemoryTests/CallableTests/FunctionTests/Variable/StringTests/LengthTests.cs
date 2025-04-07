using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class LengthTests
{
    [TestMethod]
    public void TestLength()
    {
        var source = @"
            :""Hello""→Str1
            :length(Str1)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestLength_EmptyString()
    {
        var source = @"
            :""→Str1
            :length(Str1)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestLength_NonString()
    {
        var source = @"
            :1→A
            :length(A)→B
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE");
    }

    [TestMethod]
    public void TestLength_Token()
    {
        var source = @"
            :""sin(""→Str1
            :length(Str1)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)1, environment.Get<TiNumber>("B"));
    }
}
