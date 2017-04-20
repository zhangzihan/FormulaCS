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

        private double EvalG15(string formula)
        {
            return Convert.ToDouble(((double)Eval(formula)).ToString("G15"));
        }

        [Fact]
        public void EvaluatesLnFunction()
        {
            // Examples from https://support.office.com/en-us/article/LN-function-81fe1ed7-dac9-4acd-ba1d-07a142c6118f

            // =LN(86)
            output.WriteLine("=LN(86)\n{0}", DoubleConverter.ToExactString((double)Eval("=LN(86)")));
            Assert.Equal(4.4543472962535073378376182517968118190765380859375, Eval("=LN(86)"));
            Assert.Equal(4.45434729625351, EvalG15("=LN(86)")); // Excel result

            // =LN(2.7182818)
            output.WriteLine("=LN(2.7182818)\n{0}", DoubleConverter.ToExactString((double)Eval("=LN(2.7182818)")));
            Assert.Equal(0.99999998953050239780537822298356331884860992431640625, Eval("=LN(2.7182818)"));
            Assert.Equal(0.999999989530502, EvalG15("=LN(2.7182818)")); // Excel result

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
            Assert.Equal(4.45434734288829, EvalG15("=LOG(86, 2.7182818)")); // Excel result
        }

        [Fact]
        public void EvaluatesPowerFunction()
        {
            // Examples from https://support.office.com/en-us/article/POWER-function-d3f2908b-56f4-4c3f-895a-07fb519c362a

            // =POWER(5,2)
            Assert.Equal(25.0, Eval("=POWER(5,2)"));

            // =POWER(98.6,3.2)
            output.WriteLine("=POWER(98.6,3.2)\n{0}", DoubleConverter.ToExactString((double)Eval("=POWER(98.6,3.2)")));
            Assert.Equal(2401077.222069577313959598541259765625, Eval("=POWER(98.6,3.2)"));
            Assert.Equal(2401077.22206958, EvalG15("=POWER(98.6,3.2)")); // Excel result

            // =POWER(4,5/4)
            output.WriteLine("=POWER(4,5/4)\n{0}", DoubleConverter.ToExactString((double)Eval("=POWER(4,5/4)")));
            Assert.Equal(5.65685424949238058189848743495531380176544189453125, Eval("=POWER(4,5/4)"));
            Assert.Equal(5.65685424949238, EvalG15("=POWER(4,5/4)")); // Excel result
        }

        [Fact]
        public void EvaluatesPiFunction()
        {
            // Examples from https://support.office.com/en-us/article/PI-function-264199d0-a3ba-46b8-975a-c4a04608989b

            // =PI()
            output.WriteLine("=PI()\n{0}", DoubleConverter.ToExactString((double)Eval("=PI()")));
            Assert.Equal(3.141592653589793115997963468544185161590576171875, Eval("=PI()"));
            Assert.Equal(3.14159265358979, EvalG15("=PI()")); // Excel result

            // =PI()/2
            output.WriteLine("=PI()/2\n{0}", DoubleConverter.ToExactString((double)Eval("=PI()/2")));
            Assert.Equal(1.5707963267948965579989817342720925807952880859375, Eval("=PI()/2"));
            Assert.Equal(1.5707963267949, EvalG15("=PI()/2")); // Excel result

            // A3: 3

            // =PI()*(A3^2)
            output.WriteLine("=PI()*(3^2)\n{0}", DoubleConverter.ToExactString((double)Eval("=PI()*(3^2)")));
            Assert.Equal(28.274333882308138043981671216897666454315185546875, Eval("=PI()*(3^2)"));
            Assert.Equal(28.2743338823081, EvalG15("=PI()*(3^2)")); // Excel result
        }

        [Fact]
        public void EvaluatesRadiansFunction()
        {
            // Example from https://support.office.com/en-us/article/RADIANS-function-ac409508-3d48-45f5-ac02-1497c92de5bf

            // =RADIANS(270)
            Assert.Equal(4.71238898038469, EvalG15("=RADIANS(270)"));
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