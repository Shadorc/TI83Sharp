using TI83Sharp;

namespace TI83SharpTests;

[TestClass]
public class MathFunctionTests
{
    private class TestMathFunction : MathFunction
    {
        public TestMathFunction() : base("test") { }

        protected override float ApplyFunction(TiNumber number)
        {
            return number + 1; // Simple implementation for testing
        }
    }

    [TestMethod]
    public void Call_WithNumber_ReturnsExpectedResult()
    {
        var function = new TestMathFunction();
        var interpreter = new Interpreter(null, null);
        var arguments = new List<Expr> { new Literal((TiNumber)1) };

        var result = function.Call(interpreter, arguments);

        Assert.IsInstanceOfType<TiNumber>(result);
        Assert.AreEqual((TiNumber)2, result);
    }

    [TestMethod]
    public void Call_WithList_ReturnsExpectedResult()
    {
        var function = new TestMathFunction();
        var interpreter = new Interpreter(null, null);
        var list = new TiList { (TiNumber)1, (TiNumber)2 };
        var arguments = new List<Expr> { new Literal(list) };

        var result = function.Call(interpreter, arguments);

        Assert.IsInstanceOfType<TiList>(result);
        var resultList = (TiList)result;
        Assert.AreEqual((TiNumber)2, resultList[0]);
        Assert.AreEqual((TiNumber)3, resultList[1]);
    }

    [TestMethod]
    public void Call_WithInvalidArgument_ThrowsRuntimeError()
    {
        var function = new TestMathFunction();
        var interpreter = new Interpreter(null, null);
        var arguments = new List<Expr> { new Literal("invalid") };

        Assert.ThrowsException<RuntimeError>(() => function.Call(interpreter, arguments));
    }
}
