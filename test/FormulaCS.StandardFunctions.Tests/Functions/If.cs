using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class If : TestBase
    {
        [Fact]
        public void EvaluatesIfFunction()
        {
            // Examples from https://support.office.com/en-gb/article/IF-function-69aed7c9-4e8a-4755-a9bc-aa8bbff73be2

            // C2="Yes"

            // =IF(C2="Yes",1,2)
            Assert.Equal(1d, Eval("=IF(\"Yes\"=\"Yes\",1,2)"));

            // B2=$800.00
            // C2=$921.58

            // =IF(C2>B2,"Over Budget","Within Budget")
            Assert.Equal("Over Budget", Eval("=IF(921.58>800.00,\"Over Budget\",\"Within Budget\")"));

            // =IF(C2>B2,C2-B2,0)
            // NOTE: Test fails to compare double result here, but compares as decimals.
            // TODO: Should be able to compare an exact value here. May need to resolve error in calculation.
            var result = Eval("=IF(921.58>800.00,921.58-800.00,0)");
            Assert.NotEqual(121.58, result);
            Assert.NotEqual(121.58, (double)result);
            Assert.IsType<double>(result);
            Assert.IsType<double>(121.58);
            Assert.Equal(new decimal(121.58), new decimal((double)result));
        }
    }
}