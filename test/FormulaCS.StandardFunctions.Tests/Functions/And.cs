using Xunit;

namespace FormulaCS.StandardFunctions.Tests.Functions
{
    public class And : TestBase
    {
        [Fact]
        public void EvaluatesAndFunction()
        {
            // Examples from https://support.office.com/en-us/article/AND-function-5f19b2e8-e1df-4408-897a-ce285a19e9d9
            
            // A2=50
            // A3=100

            // =AND(A2>1,A2<100)
            Assert.Equal(true, Eval("=AND(50>1,50<100)"));

            // =IF(AND(A2<A3,A2<100),A2,"The value is out of range")
            Assert.Equal(50d, Eval("=IF(AND(50<100,50<100),50,\"The value is out of range\")"));

            // =IF(AND(A3>1,A3<100),A3,"The value is out of range")
            Assert.Equal("The value is out of range", Eval("=IF(AND(100>1,100<100),100,\"The value is out of range\")"));
        }
    }
}