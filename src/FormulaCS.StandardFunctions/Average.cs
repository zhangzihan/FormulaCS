using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Average
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: AVERAGE(number1, [number2], ...)
            // https://support.office.com/en-GB/article/AVERAGE-function-047bac88-d466-426c-a32b-8f33eb960cf6

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"AVERAGE function takes at least 1 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var numbers = new List<double>();

            foreach (var argsParameter in args.Parameters)
            {
                var arg = argsParameter.Evaluate();
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

                numbers.Add((double)val);
            }

            args.Result = numbers.Average();
        }        
    }
}