using System;
using FormulaCS.Common;
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
            var result = Eval(formula);
            output.WriteLine("{0}\n{1}", formula, DoubleConverter.ToExactString((double)result));
            return Convert.ToDouble(((double)result).ToString("G15"));
        }

        [Fact]
        public void EvaluatesLnFunction()
        {
            // Examples from https://support.office.com/en-us/article/LN-function-81fe1ed7-dac9-4acd-ba1d-07a142c6118f
            Assert.Equal(4.45434729625351, EvalG15("=LN(86)"));
            Assert.Equal(0.999999989530502, EvalG15("=LN(2.7182818)"));
            // TODO: Assert.Equal(3, Eval("=LN(EXP(3))"));
        }

        [Fact]
        public void EvaluatesLogFunction()
        {
            // Examples from https://support.office.com/en-us/article/LOG-function-4e82f196-1ca9-4747-8fb0-6c4a3abb3280
            Assert.Equal(1d, Eval("=LOG(10)"));
            Assert.Equal(3d, Eval("=LOG(8, 2)"));
            Assert.Equal(4.45434734288829, EvalG15("=LOG(86, 2.7182818)"));
        }

        [Fact]
        public void EvaluatesPowerFunction()
        {
            // Examples from https://support.office.com/en-us/article/POWER-function-d3f2908b-56f4-4c3f-895a-07fb519c362a
            Assert.Equal(25d, Eval("=POWER(5,2)"));
            Assert.Equal(2401077.22206958, EvalG15("=POWER(98.6,3.2)"));
            Assert.Equal(5.65685424949238, EvalG15("=POWER(4,5/4)"));
        }

        [Fact]
        public void EvaluatesPiFunction()
        {
            // Examples from https://support.office.com/en-us/article/PI-function-264199d0-a3ba-46b8-975a-c4a04608989b
            // A3=3
            Assert.Equal(3.14159265358979, EvalG15("=PI()"));
            Assert.Equal(1.5707963267949, EvalG15("=PI()/2"));
            Assert.Equal(28.2743338823081, EvalG15("=PI()*(3^2)")); // =PI()*(A3^2)
        }

        [Fact]
        public void EvaluatesRadiansFunction()
        {
            // Example from https://support.office.com/en-us/article/RADIANS-function-ac409508-3d48-45f5-ac02-1497c92de5bf
            Assert.Equal(4.71238898038469, EvalG15("=RADIANS(270)"));
        }

        [Fact]
        public void EvaluatesRoundFunction()
        {
            // Examples from https://support.office.com/en-us/article/ROUND-function-c018c5d8-40fb-4053-90b1-b3e7f61a213c
            Assert.Equal(2.2, Eval("=ROUND(2.15, 1)"));
            Assert.Equal(2.1, Eval("=ROUND(2.149, 1)"));
            Assert.Equal(-1.48, Eval("=ROUND(-1.475, 2)"));
            Assert.Equal(20d, Eval("=ROUND(21.5, -1)"));
            Assert.Equal(1000d, Eval("=ROUND(626.3,-3)"));
            Assert.Equal(0d, Eval("=ROUND(1.98,-1)"));
            Assert.Equal(-100d, Eval("=ROUND(-50.55,-2)"));
        }

        [Fact]
        public void EvaluatesRoundUpFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDUP-function-f8bc9b23-e795-47db-8703-db171d0c42a7
            Assert.Equal(4d, Eval("=ROUNDUP(3.2,0)"));
            Assert.Equal(77d, Eval("=ROUNDUP(76.9,0)"));
            Assert.Equal(3.142, Eval("=ROUNDUP(3.14159,3)"));
            Assert.Equal(-3.2, Eval("=ROUNDUP(-3.14159,1)"));
            Assert.Equal(31500d, Eval("=ROUNDUP(31415.92654,-2)"));
        }

        [Fact]
        public void EvaluatesRoundDownFunction()
        {
            // Examples from https://support.office.com/en-gb/article/ROUNDDOWN-function-2ec94c73-241f-4b01-8c6f-17e6d7968f53
            Assert.Equal(3d, Eval("=ROUNDDOWN(3.2,0)"));
            Assert.Equal(76d, Eval("=ROUNDDOWN(76.9,0)"));
            Assert.Equal(3.141, Eval("=ROUNDDOWN(3.14159,3)"));
            Assert.Equal(-3.1, Eval("=ROUNDDOWN(-3.14159,1)"));
            Assert.Equal(31400d, Eval("=ROUNDDOWN(31415.92654,-2)"));
        }

        [Fact]
        public void EvaluatesSqrtFunction()
        {
            // Examples from https://support.office.com/en-us/article/SQRT-function-654975c2-05c4-4831-9a24-2c65e4040fdf
            // A2=-16
            Assert.Equal(4d, Eval("=SQRT(16)"));
            Assert.Equal(ErrorValue.Num, Eval("=SQRT(-16)"));
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

        [Theory]
        [InlineData("=WEIGHTED(3, 5)", "15")]
        [InlineData("=WEIGHTED(8, 3)", "24")]
        public void ShouldBeAbleToApplyWeighting(string formula, string expected)
        {
            // assert
            Assert.Equal(Convert.ToDouble(expected), Eval(formula));
        }
    }
}