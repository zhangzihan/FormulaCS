using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Power : TestBase
    {
        [Fact]
        public void EvaluatesPowerFunction()
        {
            // Examples from https://support.office.com/en-us/article/POWER-function-d3f2908b-56f4-4c3f-895a-07fb519c362a
            Assert.Equal(25d, Eval("=POWER(5,2)"));
            Assert.Equal(2401077.22206958, EvalG15("=POWER(98.6,3.2)"));
            Assert.Equal(5.65685424949238, EvalG15("=POWER(4,5/4)"));
        }
    }
}