using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

public partial class OperationTests
{
    [TestClass]
    public class StringOperationTests
    {
        [TestMethod]
        public void TestStr_Concat()
        {
            var source =
                @"
                :""A""→Str0
                :""B""→Str1
                :Str0+Str1+""C""→Str2
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual("ABC", environment.Get<string>("Str2"));
        }

        [TestMethod]
        public void TestStr_ConcatOneLine()
        {
            var source =
                @"
                :""A""+"" ""+""B""→Str2
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual("A B", environment.Get<string>("Str2"));
        }

        [TestMethod]
        public void TestStr_ConcatOptimized()
        {
            var source =
                @"
                :""A""+""B→Str2
                ";
            Interpret(source, out _, out var environment);
            Assert.AreEqual("AB", environment.Get<string>("Str2"));
        }
    }
}
