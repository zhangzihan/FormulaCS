using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Sqrt
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: SQRT(number)
            // https://support.office.com/en-gb/article/SQRT-function-654975c2-05c4-4831-9a24-2c65e4040fdf

            if (args.Parameters.Length != 1)
            {
                throw new ArgumentException(
                    $"SQRT function takes only 1 argument, got {args.Parameters.Length}");
            }

            var arg = args.Parameters[0].Evaluate();
            if (arg is ErrorValue)
            {
                args.Result = arg;
                return;
            }

            var val = Conversion.ToDoubleOrErrorValue(arg);
            if (val is ErrorValue)
            {
                args.Result = val;
                return;
            }

            args.Result = Conversion.ErrorValueOnInvalidDouble(Math.Sqrt((double)val));
        }        
    }
}