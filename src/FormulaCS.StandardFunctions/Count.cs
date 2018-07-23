using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Count : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: COUNT(value1, [value2], ...)
            // https://support.office.com/en-gb/article/COUNT-function-a59cd7fc-b623-4d93-87a4-d23bf411294c

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"COUNT function takes at least 1 arguments, got {args.Parameters.Length}",
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

            args.Result = (double)numbers.Count;
        }        
    }
}