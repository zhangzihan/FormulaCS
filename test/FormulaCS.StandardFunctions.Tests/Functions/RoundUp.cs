using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class RoundUp : TestBase
    {
        [Fact]
        public void EvaluatesRoundUpFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDUP-function-f8bc9b23-e795-47db-8703-db171d0c42a7
            Assert.Equal(4d, Eval("=ROUNDUP(3.2,0)"));
            Assert.Equal(77d, Eval("=ROUNDUP(76.9,0)"));
            Assert.Equal(3.142, Eval("=ROUNDUP(3.14159,3)"));
            Assert.Equal(-3.2, Eval("=ROUNDUP(-3.14159,1)"));
            Assert.Equal(31500d, Eval("=ROUNDUP(31415.92654,-2)"));
        }
    }
}