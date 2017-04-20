using FormulaCS.Evaluator;
using Xunit;

namespace FormulaCS.StandardExcelFunctions.Tests
{
    public class LogicalTests
    {
        private readonly FormulaEvaluator evaluator = new FormulaEvaluator();

        public LogicalTests()
        {
            evaluator.AddStandardFunctions();
        }

        private object Eval(string formula)
        {
            return evaluator.Evaluate(formula);
        }

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