using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class Statistical
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Statistical()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {
                {"AVERAGE", AverageFunction},
//                {"AVERAGEIF", AverageIfFunction},
//                {"MAX", MaxFunction},
//                {"MIN", MinFunction},
            };
        }

        private static void AverageFunction(IFunctionArgs args, IExcelCaller caller)
        {
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