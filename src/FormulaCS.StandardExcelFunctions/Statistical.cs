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
                {"MAX", MaxFunction},
                {"MIN", MinFunction},
                {"COUNT", CountFunction},
                {"MEDIAN", MedianFunction},
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

        private static void MaxFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void MinFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void CountFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void MedianFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

            args.Result = Median(numbers.ToArray());
        }

        public static double Median(double[] assessments)
        {
            Array.Sort(assessments);

            var arrayLength = assessments.Length;

            if (arrayLength % 2 == 0)
            {
                return ((assessments[(arrayLength / 2) - 1] + assessments[arrayLength / 2]) / 2);
            }

            return assessments[(int)Math.Floor((double)arrayLength / 2)];
        }
    }
}