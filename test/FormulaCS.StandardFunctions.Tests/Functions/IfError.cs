using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class IfError : TestBase
    {
        [Fact]
        public void EvaluatesIfErrorFunction()
        {
            // Examples from https://support.office.com/en-gb/article/IFERROR-function-c526fd07-caeb-47b8-8bb6-63f3e417f611

            // A2=210
            // A3=55
            // A4=
            // B2=35
            // B3=0
            // B4=23

            // =IFERROR(A2/B2, "Error in calculation")
            Assert.Equal(6d, Eval("=IFERROR(210/35, \"Error in calculation\")"));

            // =IFERROR(A3/B3, "Error in calculation")
            Assert.Equal("Error in calculation", Eval("=IFERROR(55/0, \"Error in calculation\")"));

            // =IFERROR(A4/B4, "Error in calculation")
            // NOTE: Treating blank value as zero here. Grammar doesn't support missing values.
            Assert.Equal(0d, Eval("=IFERROR(0/23, \"Error in calculation\")"));
        }
    }
}