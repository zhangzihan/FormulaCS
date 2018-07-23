using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class If : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: IF(logical_test, value_if_true, [value_if_false])
            // https://support.office.com/en-gb/article/IF-function-69aed7c9-4e8a-4755-a9bc-aa8bbff73be2

            if (args.Parameters.Length != 2 && args.Parameters.Length != 3)
            {
                throw new ArgumentException(
                    "IF function takes 2 or 3 arguments",
                    nameof(args));
            }

            var arg = args.Parameters[0].Evaluate();
            if (arg is ErrorValue)
            {
                args.Result = arg;
                return;
            }

            if (args.Parameters.Length == 3)
            {
                args.Result = Convert.ToBoolean(arg)
                    ? args.Parameters[1].Evaluate()
                    : args.Parameters[2].Evaluate();
            }
            else
            {
                args.Result = Convert.ToBoolean(arg)
                    ? args.Parameters[1].Evaluate()
                    : false;
            }
        }        
    }
}