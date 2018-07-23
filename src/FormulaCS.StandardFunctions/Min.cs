using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Min
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: MIN(number1, [number2], ...)
            // https://support.office.com/en-GB/article/MIN-function-61635d12-920f-4ce2-a70f-96f202dcc152

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"MIN function takes at least 1 arguments, got {args.Parameters.Length}",
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

            args.Result = numbers.Min();
        }        
    }
}