using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Ln
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: LN(number)
            // https://support.office.com/en-gb/article/LN-function-81fe1ed7-dac9-4acd-ba1d-07a142c6118f

            if (args.Parameters.Length != 1)
            {
                throw new ArgumentException(
                    $"LN function takes only 1 argument, got {args.Parameters.Length}");
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

            args.Result = Math.Log((double)val);
        }        
    }
}