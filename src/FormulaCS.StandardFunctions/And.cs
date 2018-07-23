using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class And : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: AND(logical1, [logical2], ...)
            // https://support.office.com/en-gb/article/AND-function-5f19b2e8-e1df-4408-897a-ce285a19e9d9

            if (args.Parameters.Length < 2)
            {
                throw new ArgumentException(
                    $"AND function requires at least 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var result = true;

            foreach (var fx in args.Parameters)
            {
                var arg = fx.Evaluate();

                if (arg is ErrorValue)
                {
                    args.Result = arg;
                    return;
                }

                if (!(arg is bool))
                {
                    args.Result = ErrorValue.Num;
                    return;
                }

                if ((bool)arg)
                {
                    continue;
                }

                result = false;
                break;
            }

            args.Result = result;
        }
    }
}