using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

public partial class OperationTests
{
    [TestClass]
    public class MatrixOperationTests
    {
        [TestMethod]
        public void TestMatrix_Add()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[[1,2][3,4]]→[B]
                :[A]+[B]→[C]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[C]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)2, result[0][0]);
            Assert.AreEqual((TiNumber)4, result[0][1]);
            Assert.AreEqual((TiNumber)6, result[1][0]);
            Assert.AreEqual((TiNumber)8, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_Subtract()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[[1,2][3,4]]→[B]
                :[A]-[B]→[C]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[C]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)0, result[0][0]);
            Assert.AreEqual((TiNumber)0, result[0][1]);
            Assert.AreEqual((TiNumber)0, result[1][0]);
            Assert.AreEqual((TiNumber)0, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_Multiply()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[[1,2][3,4]]→[B]
                :[A]*[B]→[C]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[C]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)7, result[0][0]);
            Assert.AreEqual((TiNumber)10, result[0][1]);
            Assert.AreEqual((TiNumber)15, result[1][0]);
            Assert.AreEqual((TiNumber)22, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_MultiplyNumberRight()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[A]*2→[B]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[B]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)2, result[0][0]);
            Assert.AreEqual((TiNumber)4, result[0][1]);
            Assert.AreEqual((TiNumber)6, result[1][0]);
            Assert.AreEqual((TiNumber)8, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_MultiplyNumberLeft()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :2*[A]→[B]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[B]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)2, result[0][0]);
            Assert.AreEqual((TiNumber)4, result[0][1]);
            Assert.AreEqual((TiNumber)6, result[1][0]);
            Assert.AreEqual((TiNumber)8, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_Pow()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[A]^2→[C]
                ";
            Interpret(source, out _, out var environment);
            var result = environment.Get<TiMatrix>("[C]");
            Assert.AreEqual(2, result.Rows);
            Assert.AreEqual(2, result.Cols);
            Assert.AreEqual((TiNumber)7, result[0][0]);
            Assert.AreEqual((TiNumber)10, result[0][1]);
            Assert.AreEqual((TiNumber)15, result[1][0]);
            Assert.AreEqual((TiNumber)22, result[1][1]);
        }

        [TestMethod]
        public void TestMatrix_Pow_ErrInvalidDim()
        {
            var source =
                @"
                :[[1,2,3][4,5,6]]→[A]
                :[A]^2→[C]
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
        }

        [TestMethod]
        public void TestMatrix_Equals()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[[1,2][3,4]]→[B]
                :[A]=[B]→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)1, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestMatrix_NotEquals()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[[1,2][3,4]]→[B]
                :[A]≠[B]→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)0, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestMatrix_GetElement()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[A](1,1)→A
                :[A](1,2)→B
                :[A](2,1)→C
                :[A](2,2)→D
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)1, environment.Get<TiNumber>("A"));
            Assert.AreEqual((TiNumber)2, environment.Get<TiNumber>("B"));
            Assert.AreEqual((TiNumber)3, environment.Get<TiNumber>("C"));
            Assert.AreEqual((TiNumber)4, environment.Get<TiNumber>("D"));
        }

        [TestMethod]
        public void TestList_GetElement_ErrInvalidDim()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :[A](3,1)→A
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
        }

        [TestMethod]
        public void TestList_SetElement()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :6→[A](2,1)
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiMatrix(new List<List<TiNumber>>() { new List<TiNumber>() { 1, 2 }, new List<TiNumber>() { 6, 4 } }), environment.Get<TiMatrix>("[A]"));
        }

        [TestMethod]
        public void TestList_SetElement_ErrInvalidDim()
        {
            var source =
                @"
                :[[1,2][3,4]]→[A]
                :6→[A](3,1)
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
        }

    }
}
