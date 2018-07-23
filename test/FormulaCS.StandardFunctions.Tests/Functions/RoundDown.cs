using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class RoundDown : TestBase
    {
        [Fact]
        public void EvaluatesRoundDownFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDDOWN-function-2ec94c73-241f-4b01-8c6f-17e6d7968f53
            Assert.Equal(3d, Eval("=ROUNDDOWN(3.2,0)"));
            Assert.Equal(76d, Eval("=ROUNDDOWN(76.9,0)"));
            Assert.Equal(3.141, Eval("=ROUNDDOWN(3.14159,3)"));
            Assert.Equal(-3.1, Eval("=ROUNDDOWN(-3.14159,1)"));
            Assert.Equal(31400d, Eval("=ROUNDDOWN(31415.92654,-2)"));
        }
    }
}