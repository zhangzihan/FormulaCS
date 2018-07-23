using FormulaCS.Common;
using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Sqrt : TestBase
    {
        [Fact]
        public void EvaluatesSqrtFunction()
        {
            // Examples from https://support.office.com/en-us/article/SQRT-function-654975c2-05c4-4831-9a24-2c65e4040fdf
            // A2=-16
            Assert.Equal(4d, Eval("=SQRT(16)"));
            Assert.Equal(ErrorValue.Num, Eval("=SQRT(-16)"));
        }
    }
}