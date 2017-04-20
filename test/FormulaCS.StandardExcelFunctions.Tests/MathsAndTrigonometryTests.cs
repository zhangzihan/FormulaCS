using System;
using FormulaCS.Evaluator;
using FormulaCS.StandardExcelFunctions.Utils;
using Xunit;
using Xunit.Abstractions;

namespace FormulaCS.StandardExcelFunctions.Tests
{
    public class MathsAndTrigonometryTests
    {
        private readonly ITestOutputHelper output;
        private readonly FormulaEvaluator evaluator = new FormulaEvaluator();

        public MathsAndTrigonometryTests(ITestOutputHelper output)
        {
            this.output = output;
            evaluator.AddStandardFunctions();
        }

        private object Eval(string formula)
        {
            return evaluator.Evaluate(formula);
        }

        [Fact]
        public void EvaluatesLnFunction()
        {
            // Examples from https://support.office.com/en-us/article/LN-function-81fe1ed7-dac9-4acd-ba1d-07a142c6118f

            // =LN(86)
            output.WriteLine("=LN(86)\n{0}", DoubleConverter.ToExactString((double)Eval("=LN(86)")));
            Assert.Equal(4.4543472962535073378376182517968118190765380859375, Eval("=LN(86)"));
            Assert.Equal(4.45434729625351, Math.Round((double)Eval("=LN(86)"), 14)); // Excel result

            // =LN(2.7182818)
            output.WriteLine("=LN(2.7182818)\n{0}", DoubleConverter.ToExactString((double)Eval("=LN(2.7182818)")));
            Assert.Equal(0.99999998953050239780537822298356331884860992431640625, Eval("=LN(2.7182818)"));
            Assert.Equal(0.999999989530502, Math.Round((double)Eval("=LN(2.7182818)"), 15)); // Excel result

            // =LN(EXP(3))
            // TODO: Implement EXP function then enable this test.
            // TODO: Assert.Equal(3, Eval("=LN(EXP(3))"));
        }

        [Fact]
        public void EvaluatesLogFunction()
        {
            // Examples from https://support.office.com/en-us/article/LOG-function-4e82f196-1ca9-4747-8fb0-6c4a3abb3280

            // =LOG(10)
            Assert.Equal(1.0, Eval("=LOG(10)"));

            // =LOG(8, 2)
            Assert.Equal(3.0, Eval("=LOG(8, 2)"));

            // =LOG(86, 2.7182818)
            output.WriteLine("=LOG(86, 2.7182818)\n{0}", DoubleConverter.ToExactString((double)Eval("=LOG(86, 2.7182818)")));
            Assert.Equal(4.45434734288828604320542581262998282909393310546875, Eval("=LOG(86, 2.7182818)"));
            Assert.Equal(4.45434734288829, Math.Round((double)Eval("=LOG(86, 2.7182818)"), 14)); // Excel result
        }

        [Fact]
        public void EvaluatesRoundUpFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDUP-function-f8bc9b23-e795-47db-8703-db171d0c42a7
            Assert.Equal(4.0, Eval("=ROUNDUP(3.2,0)"));
            Assert.Equal(77.0, Eval("=ROUNDUP(76.9,0)"));
            Assert.Equal(3.142, Eval("=ROUNDUP(3.14159,3)"));
            Assert.Equal(-3.2, Eval("=ROUNDUP(-3.14159,1)"));
            Assert.Equal(31500.0, Eval("=ROUNDUP(31415.92654,-2)"));
        }

        [Fact]
        public void EvaluatesRoundDownFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDDOWN-function-2ec94c73-241f-4b01-8c6f-17e6d7968f53
            Assert.Equal(3.0, Eval("=ROUNDDOWN(3.2,0)"));
            Assert.Equal(76.0, Eval("=ROUNDDOWN(76.9,0)"));
            Assert.Equal(3.141, Eval("=ROUNDDOWN(3.14159,3)"));
            Assert.Equal(-3.1, Eval("=ROUNDDOWN(-3.14159,1)"));
            Assert.Equal(31400.0, Eval("=ROUNDDOWN(31415.92654,-2)"));
        }
    }
}