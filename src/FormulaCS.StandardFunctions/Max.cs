using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Max : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: MAX(number1, [number2], ...)
            // https://support.office.com/en-GB/article/MAX-function-e0012414-9ac8-4b34-9a47-73e662c08098

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"MAX function takes at least 1 arguments, got {args.Parameters.Length}",
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

            args.Result = numbers.Max();
        }        
    }
}