using System;
using Xunit;

namespace FormulaCS.StandardExtraFunctions.Tests.Functions
{
    public class Weighted : TestBase
    {
        [Theory]
        [InlineData("=WEIGHTED(3, 5)", "15")]
        [InlineData("=WEIGHTED(8, 3)", "24")]
        public void ShouldBeAbleToApplyWeighting(string formula, string expected)
        {
            // assert
            Assert.Equal(Convert.ToDouble(expected), Eval(formula));
        }
    }
}