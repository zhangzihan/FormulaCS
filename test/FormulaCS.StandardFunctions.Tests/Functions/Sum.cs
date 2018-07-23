using System;
using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Sum : TestBase
    {
        [Fact]
        public void EvaluatesSumFunction()
        {
            // Examples from https://support.office.com/en-us/article/SUM-function-043E1C7D-7726-4E80-8F32-07B23E057F89
            Assert.Equal(15d, Eval("=SUM(1,2,3,4,5)"));
            Assert.Equal(15d, Eval("=SUM(10,5)"));
            Assert.Equal(1d, Eval("=SUM(1)"));
            Assert.Throws<ArgumentException>(() => Eval("=SUM()"));
        }

        [Fact]
        public void EvaluatesSumProductFunction()
        {
            Assert.Equal(2.11d, Math.Round(double.Parse(Eval("=SUM(4*3,2*2,3*1)/SUM(4+2+3)").ToString()),2));
        }
    }
}