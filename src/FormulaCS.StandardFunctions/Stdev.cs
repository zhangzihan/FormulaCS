using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Stdev : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: STDEV(number1,[number2],...)
            // https://support.office.com/en-gb/article/stdev-function-51fecaaa-231e-4bbb-9230-33650a72c9b0

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"STDEV function takes at least 1 arguments, got {args.Parameters.Length}",
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
            var mean = numbers.Average();
            var sum = 0.0;
            foreach (var number in numbers)
            {
                sum += Math.Pow(number - mean, 2);
            }

            args.Result = Math.Sqrt(sum / (numbers.Count - 1));
        }        
    }
}