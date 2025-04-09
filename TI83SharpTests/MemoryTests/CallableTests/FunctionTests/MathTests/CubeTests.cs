using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class CubeTests
{
    [TestMethod]
    public void TestCube()
    {
        var source =
            @"
            :5.32³→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)150.56879f, environment.Get<TiNumber>("A"));

        source =
            @"
            :4³→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)64, environment.Get<TiNumber>("A"));

        source =
            @"
            :5→A
            :A³→A
            ";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)125, environment.Get<TiNumber>("A"));
    }
}
