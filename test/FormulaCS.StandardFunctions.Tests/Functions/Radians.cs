using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Radians : TestBase
    {
        [Fact]
        public void EvaluatesRadiansFunction()
        {
            // Example from https://support.office.com/en-us/article/RADIANS-function-ac409508-3d48-45f5-ac02-1497c92de5bf
            Assert.Equal(4.71238898038469, EvalG15("=RADIANS(270)"));
        }
    }
}