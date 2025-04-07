using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class AssignTests
{
    [TestClass]
    public class NumberAssignTests
    {
        [TestMethod]
        public void TestNumberAssign_Integer()
        {
            var source = ":10→A";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)10, environment.Get<TiNumber>("A"));
        }

        [TestMethod]
        public void TestAssign_Real()
        {
            var source = ":10.2→A";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)10.2f, environment.Get<TiNumber>("A"));
        }

        [TestMethod]
        public void TestAssign_PartialReal()
        {
            var source = ":.2→A";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber).2f, environment.Get<TiNumber>("A"));
        }

        [TestMethod]
        public void TestAssign_Function()
        {
            var source = ":cos(90)→A";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)MathF.Cos(90f), environment.Get<TiNumber>("A"));
        }

        [TestMethod]
        public void TestAssign_FunctionOptimized()
        {
            var source = ":cos(90→A";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)MathF.Cos(90f), environment.Get<TiNumber>("A"));
        }

        [TestMethod]
        public void TestAssign_ErrString()
        {
            var source = ":\"Test\"→A";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestAssign_ErrList()
        {
            var source = ":{1,2,3}→A";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestAssign_ErrMatrix()
        {
            var source = ":[[1][2][3]]→A";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }
    }

    [TestClass]
    public class StringAssignTests
    {
        [TestMethod]
        public void TestStr_Assign()
        {
            var source = ":\"Hello World\"→Str0";
            Interpret(source, out _, out var environment);
            Assert.AreEqual("Hello World", environment.Get<string>("Str0"));
        }

        [TestMethod]
        public void TestStr_AssignOptimized()
        {
            var source = ":\"Hello World→Str0";
            Interpret(source, out _, out var environment);
            Assert.AreEqual("Hello World", environment.Get<string>("Str0"));
        }

        [TestMethod]
        public void TesstAssign_ErrNumber()
        {
            var source = ":10→Str0";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestAssign_ErrList()
        {
            var source = ":{1,2,3}→Str0";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestAssign_ErrMatrix()
        {
            var source = ":[[1][2][3]]→Str0";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }
    }

    [TestClass]
    public class ListAssignTests
    {
        [TestMethod]
        public void TestList_Assign()
        {
            var source = ":{1,2,3}→L₁";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 2, 3 }, environment.Get<TiList>("L₁"));
        }

        [TestMethod]
        public void TestList_AssignOptimized()
        {
            var source = ":{1,2,3→L₁";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 2, 3 }, environment.Get<TiList>("L₁"));
        }

        [TestMethod]
        public void TestList_AssignNamed()
        {
            var source = ":{1,2,3}→∟ABC";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 2, 3 }, environment.Get<TiList>("∟ABC"));
        }

        [TestMethod]
        public void TestList_ErrNameTooLong()
        {
            var source = ":{1,2,3}→∟ABCDEF";
            AssertInterpretThrows<SyntaxError>(source, "ERR:SYNTAX\nList name '∟ABCDEF' must be between one and five characters.\n[line 1]");
        }

        [TestMethod]
        public void TestList_ErrNameTooShort()
        {
            var source = ":{1,2,3}→∟";
            AssertInterpretThrows<SyntaxError>(source, "ERR:SYNTAX\nList name '∟' must be between one and five characters.\n[line 1]");
        }

        [TestMethod]
        public void TestList_ErrNameStartsWithNumber()
        {
            var source = ":{1,2,3}→∟123";
            AssertInterpretThrows<SyntaxError>(source, "ERR:SYNTAX\nList name '∟123' must start with a letter or theta.\n[line 1]");
        }

        [TestMethod]
        public void TestList_ErrNumber()
        {
            var source = ":10→L₁";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestList_ErrString()
        {
            var source = ":\"Test\"→L₁";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestList_ErrMatrix()
        {
            var source = ":[[1][2][3]]→L₁";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }
    }

    [TestClass]
    public class MatrixAssignTests
    {
        [TestMethod]
        public void TestMatrix_Assign()
        {
            var source = ":[[1,2][3,4]]→[A]";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
                new List<TiNumber> { 1, 2 },
                new List<TiNumber>{ 3, 4 }
            }), environment.Get<TiMatrix>("[A]"));
        }

        [TestMethod]
        public void TestMatrix_AssignOptimized()
        {
            var source = ":[[1,2][3,4→[A]";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiMatrix(new List<List<TiNumber>> {
                new List<TiNumber> { 1, 2 },
                new List<TiNumber>{ 3, 4 }
            }), environment.Get<TiMatrix>("[A]"));
        }

        [TestMethod]
        public void TestMatrix_ErrName()
        {
            var source = ":[[1,2][3,4]]→[K]";
            Assert.ThrowsException<SyntaxError>(() => Interpret(source, out _, out _));
        }

        [TestMethod]
        public void TestMatrix_ErrNumber()
        {
            var source = ":10→[A]";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestMatrix_ErrString()
        {
            var source = ":\"Test\"→[A]";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }

        [TestMethod]
        public void TestMatrix_ErrList()
        {
            var source = ":{1,2,3}→[A]";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DATA TYPE\n[line 1]");
        }
    }
}
