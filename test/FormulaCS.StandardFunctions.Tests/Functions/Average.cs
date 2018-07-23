using System;
using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Average : TestBase
    {
        [Fact]
        public void EvaluatesAverageFunction()
        {
            Assert.Equal(3d, Eval("=AVERAGE(1,2,3,4,5)"));
            Assert.Equal(7.5d, Eval("=AVERAGE(10,5)"));
            Assert.Equal(1d, Eval("=AVERAGE(1)"));
            Assert.Equal(86d, Eval("=AVERAGE(86)"));
            Assert.Equal(12.50d, Eval("=AVERAGE(12,13)"));
            Assert.Equal(1.25d, Eval("=AVERAGE(1, 1.5)"));
            Assert.Throws<ArgumentException>(() => Eval("=AVERAGE()"));
        }
    }
}