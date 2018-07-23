using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Sum
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: SUM(number1,[number2],...)
            // https://support.office.com/en-GB/article/SUM-function-043e1c7d-7726-4e80-8f32-07b23e057f89

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"SUM function takes at least 1 arguments, got {args.Parameters.Length}",
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

            args.Result = numbers.Sum();
        }        
    }
}