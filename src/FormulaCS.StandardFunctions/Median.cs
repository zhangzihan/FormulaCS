using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Median
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: MEDIAN(number1, [number2], ...)
            // https://support.office.com/en-gb/article/median-function-d0916313-4753-414c-8537-ce85bdd967d2

            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"MEDIAN function takes at least 1 arguments, got {args.Parameters.Length}",
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

            double F(double[] assessments)
            {
                Array.Sort(assessments);

                var arrayLength = assessments.Length;

                if (arrayLength % 2 == 0)
                {
                    return ((assessments[(arrayLength / 2) - 1] + assessments[arrayLength / 2]) / 2);
                }

                return assessments[(int)Math.Floor((double)arrayLength / 2)];
            }

            args.Result = F(numbers.ToArray());
        }        
    }
}