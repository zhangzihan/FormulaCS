using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Log : TestBase
    {
        [Fact]
        public void EvaluatesLogFunction()
        {
            // Examples from https://support.office.com/en-us/article/LOG-function-4e82f196-1ca9-4747-8fb0-6c4a3abb3280
            Assert.Equal(1d, Eval("=LOG(10)"));
            Assert.Equal(3d, Eval("=LOG(8, 2)"));
            Assert.Equal(4.45434734288829, EvalG15("=LOG(86, 2.7182818)"));
        }
    }
}