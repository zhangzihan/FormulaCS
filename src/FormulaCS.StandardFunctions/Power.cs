using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Power
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: POWER(number, power)
            // https://support.office.com/en-gb/article/POWER-function-d3f2908b-56f4-4c3f-895a-07fb519c362a

            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    $"POWER function takes 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = args.Parameters[1].Evaluate();
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
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

            args.Result = Math.Pow((double)val1, (double)val2);
        }        
    }
}