using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Ln : TestBase
    {
        [Fact]
        public void EvaluatesLnFunction()
        {
            // Examples from https://support.office.com/en-us/article/LN-function-81fe1ed7-dac9-4acd-ba1d-07a142c6118f
            Assert.Equal(4.45434729625351, EvalG15("=LN(86)"));
            Assert.Equal(0.999999989530502, EvalG15("=LN(2.7182818)"));
            // TODO: Assert.Equal(3, Eval("=LN(EXP(3))"));
        }
    }
}