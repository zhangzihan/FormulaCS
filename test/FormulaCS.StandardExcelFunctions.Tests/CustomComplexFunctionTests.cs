using System;
using FormulaCS.Common;
using FormulaCS.Evaluator;
using FormulaCS.StandardExcelFunctions.Utils;
using Xunit;
using Xunit.Abstractions;

namespace FormulaCS.StandardExcelFunctions.Tests
{
    public class CustomComplexFunctionTests
    {
        private readonly ITestOutputHelper output;
        private readonly FormulaEvaluator evaluator = new FormulaEvaluator();

        public CustomComplexFunctionTests(ITestOutputHelper output)
        {
            this.output = output;
            evaluator.AddStandardFunctions();
        }

        private object Eval(string formula)
        {
            return evaluator.Evaluate(formula);
        }

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
        public void EvaluatesAverageFunction()
        {
            Assert.Equal(3d, Eval("=AVERAGE(1,2,3,4,5)"));
            Assert.Equal(7.5d, Eval("=AVERAGE(10,5)"));
            Assert.Equal(1d, Eval("=AVERAGE(1)"));
            Assert.Throws<ArgumentException>(() => Eval("=AVERAGE()"));
        }

        [Fact]
        public void EvaluatesRoundFunction()
        {
            Assert.Equal(1.2d, Eval("=ROUND(1.20,2)"));
            Assert.Throws<ArgumentException>(() => Eval("=ROUND()"));
        }

        [Fact]
        public void EvaluatesSumProductFunction()
        {
            Assert.Equal(2.11d, Math.Round(double.Parse(Eval("=SUM(4*3,2*2,3*1)/SUM(4+2+3)").ToString()),2));
        }

        [Fact]
        public void EvaluatesCustomFunction1()
        {
            Assert.Equal(7d, Math.Round(double.Parse(Eval("=(2*SUM(2,4)+AVERAGE(1,2,3))/2").ToString()), 2));
        }
    }
}