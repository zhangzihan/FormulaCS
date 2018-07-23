using FormulaCS.Common;

namespace FormulaCS.Evaluator
{
    public class FunctionArgs : IFunctionArgs
    {
        private object _result;

        public IExpression[] Parameters { get; set; } = new IExpression[0];

        public bool HasResult { get; private set; }

        public object Result
        {
            get => _result;
            set
            {
                _result = value;
                HasResult = true;
            }
        }
    }
}