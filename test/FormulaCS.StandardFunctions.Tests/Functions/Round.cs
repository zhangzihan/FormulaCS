using System;
using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Round : TestBase
    {
        [Fact]
        public void EvaluatesRoundFunction()
        {
            // Examples from https://support.office.com/en-us/article/ROUND-function-c018c5d8-40fb-4053-90b1-b3e7f61a213c
            Assert.Equal(2.2, Eval("=ROUND(2.15, 1)"));
            Assert.Equal(2.1, Eval("=ROUND(2.149, 1)"));
            Assert.Equal(-1.48, Eval("=ROUND(-1.475, 2)"));
            Assert.Equal(20d, Eval("=ROUND(21.5, -1)"));
            Assert.Equal(1000d, Eval("=ROUND(626.3,-3)"));
            Assert.Equal(0d, Eval("=ROUND(1.98,-1)"));
            Assert.Equal(-100d, Eval("=ROUND(-50.55,-2)"));
            Assert.Equal(1.2d, Eval("=ROUND(1.20,2)"));
            Assert.Throws<ArgumentException>(() => Eval("=ROUND()"));
        }
    }
}