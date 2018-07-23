using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class IfError : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: IFERROR(value, value_if_error)
            // https://support.office.com/en-gb/article/IFERROR-function-c526fd07-caeb-47b8-8bb6-63f3e417f611

            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    "IFERROR function takes exactly 2 arguments",
                    nameof(args));
            }

            try
            {
                var arg = args.Parameters[0].Evaluate();
                if (arg is ErrorValue)
                {
                    args.Result = args.Parameters[1].Evaluate();
                    return;
                }

                args.Result = arg;
            }
            catch (Exception)
            {
                args.Result = args.Parameters[1].Evaluate();
            }
        }        
    }
}