using System;
using FormulaCS.Evaluator;

namespace FormulaCS.StandardFunctions.Tests
{
    public class TestBase
    {
        private readonly FormulaEvaluator _evaluator = new FormulaEvaluator();

        protected TestBase()
        {
            _evaluator.AddStandardFunctions();
        }
        
        protected object Eval(string formula)
        {
            return _evaluator.Evaluate(formula);
        }
        
        protected double EvalG15(string formula)
        {
            var result = Eval(formula);
            return Convert.ToDouble(((double)result).ToString("G15"));
        }
    }
}