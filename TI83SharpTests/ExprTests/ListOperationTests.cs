using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

public partial class OperationTests
{
    [TestClass]
    public class ListOperationTests
    {
        [TestMethod]
        public void TestList_AddList()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{2,5,3}→L₂
                :L₁+L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 3, 7, 6 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_AddNumberRight()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁+4→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 5, 6, 7 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_AddNumberLeft()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :4+L₁→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 5, 6, 7 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_SubtractList()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{2,5,3}→L₂
                :L₁-L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { -1, -3, 0 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_SubtractNumberRight()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁-4→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { -3, -2, -1 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_SubtractNumberLeft()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :4-L₁→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 3, 2, 1 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_DivideList()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{2,5,3}→L₂
                :L₁/L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 0.5f, 0.4f, 1f }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_DivideNumberRight()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁/2→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 0.5f, 1, 1.5f }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_DivideNumberLeft()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :2/L₁→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 2, 1, 2f / 3f }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_DivideList_ErrDivideByZero()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{0,5,3}→L₂
                :L₁/L₂→L₃
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DIVIDE BY 0");
        }

        [TestMethod]
        public void TestList_MultiplyList()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{2,5,3}→L₂
                :L₁*L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 2, 10, 9 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_MultiplyNumberRight()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁*4→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 4, 8, 12 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_MultiplyNumberLeft()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :4*L₁→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 4, 8, 12 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_PowNumber()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁^4→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 16, 81 }, environment.Get<TiList>("L₃"));
        }


        [TestMethod]
        public void TestList_Equals()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{1,2,3}→L₂
                :L₁=L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 1, 1 }, environment.Get<TiList>("L₃"));

            source =
                @"
                :{1,2,3}→L₁
                :{1,5,3}→L₂
                :L₁=L₂→L₃
                ";
            Interpret(source, out _, out environment);
            Assert.AreEqual(new TiList() { 1, 0, 1 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_NotEquals()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :{1,2,3}→L₂
                :L₁≠L₂→L₃
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 0, 0, 0 }, environment.Get<TiList>("L₃"));

            source =
                @"
                :{1,2,3}→L₁
                :{1,5,3}→L₂
                :L₁≠L₂→L₃
                ";
            Interpret(source, out _, out environment);
            Assert.AreEqual(new TiList() { 0, 1, 0 }, environment.Get<TiList>("L₃"));
        }

        [TestMethod]
        public void TestList_GetElement()
        {
            var source =
                @"
                :{1,2,5}→L₁
                :L₁(1)→A
                :L₁(2)→B
                :L₁(3)→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)1, environment.Get<TiNumber>("A"));
            Assert.AreEqual((TiNumber)2, environment.Get<TiNumber>("B"));
            Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestList_GetElement_ErrInvalidDim()
        {
            var source =
                @"
                :{1,2,3}→L₁
                :L₁(0)→A
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
        }

        [TestMethod]
        public void TestList_SetElement()
        {
            var source =
                @"
                :{1,2,5}→L₁
                :6→L₁(2)
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 6, 5 }, environment.Get<TiList>("L₁"));
        }

        [TestMethod]
        public void TestList_SetElementIncreaseDim()
        {
            var source =
                @"
                :{1,2,5}→L₁
                :6→L₁(4)
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual(new TiList() { 1, 2, 5, 6 }, environment.Get<TiList>("L₁"));
        }

        [TestMethod]
        public void TestList_SetElement_ErrInvalidDim()
        {
            var source =
                @"
                :{1,2,5}→L₁
                :6→L₁(5)
                ";
            AssertInterpretThrows<RuntimeError>(source, "ERR:INVALID DIM");
        }
    }
}
