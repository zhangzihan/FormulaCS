using FormulaCS.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace FormulaCS.Evaluator.Tests
{
    public class FormulaEvaluatorRangeTests
    {
        private FormulaEvaluator _calculationEngine;
        private Dictionary<string, object> _values;

        public FormulaEvaluatorRangeTests()
        {
            Setup();
        }

        private void Setup()
        {
            _calculationEngine = new FormulaEvaluator();
            _calculationEngine.AddStandardFunctions();
            _calculationEngine.AddFunction("AddThem", AddThemFunction);

            _values = new Dictionary<string, object>
            {
                { "ID5", 3 },
                { "ID6", 5 },
                { "ID7", 8 }
            };
        }

        private static void AddThemFunction(IFunctionArgs args, IExcelCaller caller)
        {
            var arg1 = Conversion.ToDoubleOrErrorValue(args.Parameters[0].Evaluate());
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = Conversion.ToDoubleOrErrorValue(args.Parameters[1].Evaluate());
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
            }

            args.Result = (double)arg1 + (double)arg2;
        }


        [Theory]
        [InlineData("=SUM(1)", "1")]
        [InlineData("=SUM(1,2, 3)", "6")]

        [InlineData("=AVERAGE(1,2,2,3,4)", "2.4")]
        [InlineData("=AVERAGE(SUM(2,2),SUM(6,4))", "7")]

        [InlineData("=MIN(3,2,4,10,17)", "2")]
        [InlineData("=MAX(3,2,4,10,17)", "17")]

        [InlineData("=(4*2 + 3*5 + 2*1)/(2 + 5 + 1)", "3.125")]
        public void ShouldPassStandardFormula(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(Convert.ToDouble(expected), _calculationEngine.Evaluate(formula));
        }

        [Theory]
        [InlineData("=SUM([ID5])", "3")]
        [InlineData("=SUM([ID5], [ID6], [ID7])", "16")]

        [InlineData("=AVERAGE([ID6], [ID7])", "6.5")]
        [InlineData("=AVERAGE(SUM([ID5],[ID6]), [ID7])", "8")]

        [InlineData("=MIN([ID5], [ID6], [ID7])", "3")]
        [InlineData("=MAX([ID5], [ID6], [ID7])", "8")]
        public void ShouldTakeValuesFromColumnPositions(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(Convert.ToDouble(expected), _calculationEngine.Evaluate(formula));
        }

        [Theory]
        [InlineData("=SUM([ID5:ID6])", "8")]
        [InlineData("=SUM([ID6:ID7])", "13")]

        [InlineData("=AVERAGE([ID5:ID6],[ID7])", "5.33333333333333")]
        [InlineData("=AVERAGE([ID6:ID7],[ID5])", "5.33333333333333")]

        [InlineData("=MIN([ID5:ID6],[ID7])", "3")]
        [InlineData("=MIN([ID6:ID7],[ID5])", "3")]

        [InlineData("=MAX([ID5:ID6],[ID7])", "8")]
        [InlineData("=MAX([ID6:ID7],[ID5])", "8")]
        public void ShouldTakeValuesFromARangeOfAllocations(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(Math.Round(Convert.ToDouble(expected), 4), Math.Round((double)_calculationEngine.Evaluate(formula), 4));
        }

        [Theory]
        [InlineData("=SUM([ID5:],[ID7])", "24")]
        [InlineData("=SUM([ID6:],[ID7],[ID5])", "24")]
        [InlineData("=SUM([ID7:],[ID5])", "11")]

        [InlineData("=AVERAGE([ID5:],[ID7])", "6")]
        [InlineData("=AVERAGE([ID6:],[ID5])", "5.33")]
        [InlineData("=AVERAGE([ID7:],[ID6])", "6.5")]

        [InlineData("=MIN([ID5:],[ID7])", "3")]
        [InlineData("=MIN([ID6:],[ID5])", "3")]
        [InlineData("=MIN([ID7:],[ID6])", "5")]

        [InlineData("=MAX([ID5:],[ID7])", "8")]
        [InlineData("=MAX([ID6:],[ID5])", "8")]
        [InlineData("=MAX([ID7:],[ID6])", "8")]
        public void ShouldTakeValuesFromAnAllocationFromAStartingPoint(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(Math.Round(Convert.ToDouble(expected), 2), Math.Round((double)_calculationEngine.Evaluate(formula), 2));
        }

        [Theory]
        [InlineData("=SUM([ID5],[ID5],[ID5])", "9")]
        [InlineData("=COUNT([ID5],[ID6],[ID6],[ID6],[ID7])", "5")]
        public void ShouldBeAbleToReferenceColumnsMoreThanOnce(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(Convert.ToDouble(expected), _calculationEngine.Evaluate(formula));
        }

        [Theory]
        [InlineData("=MEDIAN([ID5], [ID6], [ID7])", "5")]
        [InlineData("=MEDIAN([ID5], [ID5], [ID6], [ID7])", "4")]

        [InlineData("=RANGE([ID5],[ID7])", "5")]
        [InlineData("=RANGE([ID6],[ID7])", "3")]

        [InlineData("=ROUND([ID6]/[ID7])", "1")]
        [InlineData("=ROUND([ID6]/[ID7], 1)", "0.6")]
        [InlineData("=ROUND([ID6]/[ID7], 2)", "0.62")]
        [InlineData("=ROUND([ID6]/[ID7], 3)", "0.625")]
        public void ShouldBeAbleToPerformAllOtherRelevantFunctions(string formula, string expected)
        {
            Setup();
            _calculationEngine.Variables = _values;
            Assert.Equal(Convert.ToDouble(expected), (double)_calculationEngine.Evaluate(formula));
        }

        [Theory]
        [InlineData("=STDEV([ID5], [ID5], [ID6], [ID6])", "1.15470053837925")]
        public void ShouldBeAbleToPerformStdevFunction(string formula, string expected)
        {
            Setup();
            _calculationEngine.Variables = _values;
            Assert.Equal(Math.Round(Convert.ToDouble(expected), 4), Math.Round((double)_calculationEngine.Evaluate(formula), 4));
        }

        [Theory]
        [InlineData("=IF([ID5]=3, \"correct\", \"incorrect\")", "correct")]
        [InlineData("=IF([ID7]=3, \"correct\", \"incorrect\")", "incorrect")]
        public void ShouldBeAbleToLogicFunctionsWithStringResults(string formula, string expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(expected, _calculationEngine.Evaluate(formula));
        }

        [Theory]
        [InlineData("=IF([ID5]=3, true, false)", true)]
        [InlineData("=IF([ID7]=3, true, false)", false)]
        public void ShouldBeAbleToLogicFunctionsWithBooleanResults(string formula, bool expected)
        {
            _calculationEngine.Variables = _values;
            Assert.Equal(expected, _calculationEngine.Evaluate(formula));
        }
    }
}