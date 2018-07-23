using System;
using FormulaCS.Common;

namespace FormulaCS.StandardExtraFunctions
{
    public class Weighted
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 1 && args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    $"WEIGHTED function takes 1 or 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }
            var arg2 = new object();

            if (args.Parameters.Length == 2)
            {
                arg2 = args.Parameters[1].Evaluate();
                if (arg2 is ErrorValue)
                {
                    args.Result = arg2;
                    return;
                }
            }
            else
            {
                arg2 = 1;
            }

            var val1 = Conversion.ToDoubleOrErrorValue(arg1);
            if (val1 is ErrorValue)
            {
                args.Result = val1;
                return;
            }

            var val2 = Conversion.ToDoubleOrErrorValue(arg2);
            if (val2 is ErrorValue)
            {
                args.Result = val2;
                return;
            }

            args.Result = (double)val1 * (double)val2;
        }        
    }
}