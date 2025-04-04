using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

public partial class OperationTests
{
    [TestClass]
    public class NumberOperationTests
    {
        [TestMethod]
        public void TestNumber_Add()
        {
            var source =
                @"
                :.2→A
                :.5→B
                :A+B→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber).7f, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestNumber_Sub()
        {
            var source =
                @"
                :.2→A
                :.5→B
                :A-B→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)(-.3f), environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestNumber_Mult()
        {
            var source =
                 @"
                :.2→A
                :.5→B
                :A*B→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber).1f, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestNumber_Div()
        {
            var source =
                 @"
                :.2→A
                :.5→B
                :A/B→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber).4f, environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestNumber_Div_ErrDivideByZero()
        {
            var source = ":2/0";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DIVIDE BY 0");
        }

        [TestMethod]
        public void TestNumber_Pow()
        {
            var source =
                 @"
                :.2→A
                :.5→B
                :A^B→C
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual((TiNumber)MathF.Pow(.2f, .5f), environment.Get<TiNumber>("C"));
        }

        [TestMethod]
        public void TestNumber_Pow_ErrDomain()
        {
            var source = ":0^0";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");

            source = ":0^(-1)";
            AssertInterpretThrows<RuntimeError>(source, "ERR:DOMAIN");
        }
    }
}
