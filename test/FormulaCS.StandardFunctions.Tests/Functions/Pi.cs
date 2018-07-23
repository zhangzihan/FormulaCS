using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Pi : TestBase
    {
        [Fact]
        public void EvaluatesPiFunction()
        {
            // Examples from https://support.office.com/en-us/article/PI-function-264199d0-a3ba-46b8-975a-c4a04608989b
            // A3=3
            Assert.Equal(3.14159265358979, EvalG15("=PI()"));
            Assert.Equal(1.5707963267949, EvalG15("=PI()/2"));
            Assert.Equal(28.2743338823081, EvalG15("=PI()*(3^2)")); // =PI()*(A3^2)
        }
    }
}