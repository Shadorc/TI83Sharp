using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class SubTests
{
    [TestMethod]
    public void TestSub_String()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,3,3)→Str2
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual("llo", environment.Get<string>("Str2"));
    }

    [TestMethod]
    public void TestSub_StringConstant()
    {
        var source = @"
            :sub(""Hello"",3,3)→Str2
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual("llo", environment.Get<string>("Str2"));
    }

    [TestMethod]
    public void TestSub_String_IndexOf()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,2,1)→Str2
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual("e", environment.Get<string>("Str2"));
    }

    [TestMethod]
    public void TestSub_String_ErrNegativeIndex()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,-2,2)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestSub_String_ErrNegativeLength()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,2,-2)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestSub_String_ErrZeroIndex()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,0,1)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
    }

    [TestMethod]
    public void TestSub_String_ErrInvalidDimStart()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,6,1)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestSub_String_ErrInvalidDimLength()
    {
        var source = @"
            :""Hello""→Str1
            :sub(Str1,2,5)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
    }

    [TestMethod]
    public void TestSub_String_ErrUndefined()
    {
        var source = @"
            :sub(Str1,2,5)→Str2
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:UNDEFINED");
    }

    [TestMethod]
    public void TestSub_NumberConstant()
    {
        var source = @"
            :sub(550)→A
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5.5f, environment.Get<TiNumber>("A"));
    }

    [TestMethod]
    public void TestSub_Number()
    {
        var source = @"
            :125→A
            :sub(A)→B
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)1.25f, environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestSub_ErrArgument()
    {
        var source = @"
            :sub(550,5)→A
            ";
        AssertInterpretThrows<RuntimeError>(source, "ERR:ARGUMENT");
    }
}
