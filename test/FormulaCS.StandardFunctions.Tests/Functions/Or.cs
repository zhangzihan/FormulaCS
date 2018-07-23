using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class Or : TestBase
    {
        [Fact]
        public void EvaluatesOrFunction()
        {
            // Examples from https://support.office.com/en-gb/article/OR-function-7d17ad14-8700-4281-b308-00b131e22af0

            Assert.Equal(true, Eval("=OR(TRUE,TRUE)"));
            Assert.Equal(true, Eval("=OR(TRUE,FALSE)"));
            Assert.Equal(true, Eval("=OR(1=1,2=2,3=3)"));
            Assert.Equal(false, Eval("=OR(1=2,2=3,3=4)"));

            // A2=50
            // A3=100

            // =OR(A2>1,A2<100)
            Assert.Equal(true, Eval("=OR(50>1,50<100)"));

            // =IF(OR(A2>1,A2<100),A3,"The value is out of range")
            Assert.Equal(100d, Eval("=IF(OR(50>1,50<100),100,\"The value is out of range\")"));

            // =IF(OR(A2<0,A2>50),A2,"The value is out of range")
            Assert.Equal("The value is out of range", Eval("=IF(OR(50<0,50>50),50,\"The value is out of range\")"));
        }
    }
}