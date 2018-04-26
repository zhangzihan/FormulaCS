using System;
using FormulaCS.Evaluator;
using Xunit;
using Xunit.Abstractions;

namespace FormulaCS.StandardExcelFunctions.Tests
{
    public class StatisticalTests
    {
        private readonly ITestOutputHelper output;
        private readonly FormulaEvaluator evaluator = new FormulaEvaluator();

        public StatisticalTests(ITestOutputHelper output)
        {
            this.output = output;
            evaluator.AddStandardFunctions();
        }

        private object Eval(string formula)
        {
            return evaluator.Evaluate(formula);
        }

        [Fact]
        public void EvaluatesAverageFunction()
        {
            Assert.Equal(86d, Eval("=AVERAGE(86)"));
            Assert.Equal(12.50d, Eval("=AVERAGE(12,13)"));
            Assert.Equal(1.25d, Eval("=AVERAGE(1, 1.5)"));
            Assert.Throws<ArgumentException>(() => Eval("=AVERAGE()"));
        }
    }
}