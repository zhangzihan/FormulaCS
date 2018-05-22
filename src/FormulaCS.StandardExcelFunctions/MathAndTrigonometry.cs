using System;
using System.Collections.Generic;
using System.Linq;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public static class MathAndTrigonometry
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static MathAndTrigonometry()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {
                {"LN", LnFunction},
                {"LOG", LogFunction},
                {"POWER", PowerFunction},
                {"PI", PiFunction},
                {"RADIANS", RadiansFunction},
                {"ROUND", RoundFunction},
                {"ROUNDUP", RoundUpFunction},
                {"ROUNDDOWN", RoundDownFunction},
                {"SQRT", SqrtFunction},
                {"SUM", SumFunction},
                {"RANGE", RangeFunction},
                {"STDEV", StdevFunction},
            };
        }

        #region Support methods

        /// <remarks>
        /// This function is used by the RoundUp and RoundDown functions in this file.
        /// See <a href="http://stackoverflow.com/a/13483008">this link</a> for the original example.
        /// </remarks>
        private static decimal RoundFactor(int places)
        {
            var factor = 1m;

            if (places < 0)
            {
                places = -places;
                for (var i = 0; i < places; i++)
                {
                    factor /= 10m;
                }
            }

            else
            {
                for (var i = 0; i < places; i++)
                {
                    factor *= 10m;
                }
            }

            return factor;
        }

        #endregion

        private static void LnFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void LogFunction(IFunctionArgs args, IExcelCaller caller)
        {
            switch (args.Parameters.Length)
            {
                case 1:
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

                    args.Result = Math.Log((double)val, 10);
                    break;

                case 2:
                    var arg1 = args.Parameters[0].Evaluate();
                    if (arg1 is ErrorValue)
                    {
                        args.Result = arg1;
                        return;
                    }

                    var arg2 = args.Parameters[1].Evaluate();
                    if (arg2 is ErrorValue)
                    {
                        args.Result = arg2;
                        return;
                    }

                    var val1 = Conversion.ToDoubleOrErrorValue(arg1);
                    if (val1 is ErrorValue)
                    {
                        args.Result = val1;
                        return;
                    }

                    var val2 = Conversion.ToDoubleOrErrorValue(arg2);
                    if (val2 is ErrorValue)
                    {
                        args.Result = val2;
                        return;
                    }

                    var logE = Math.Log((double)val1);
                    var base1 = (double)val2;
                    if (base1 == Math.E)
                    {
                        args.Result = logE;
                    }
                    else
                    {
                        args.Result = logE / Math.Log(base1);
                    }
                    break;

                default:
                    throw new ArgumentException(
                        $"LOG function takes only 1 or 2 arguments, got {args.Parameters.Length}",
                        nameof(args));
            }
        }

        private static void PowerFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    $"POWER function takes 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = args.Parameters[1].Evaluate();
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
            }

            var val1 = Conversion.ToDoubleOrErrorValue(arg1);
            if (val1 is ErrorValue)
            {
                args.Result = val1;
                return;
            }

            var val2 = Conversion.ToDoubleOrErrorValue(arg2);
            if (val2 is ErrorValue)
            {
                args.Result = val2;
                return;
            }

            args.Result = Math.Pow((double)val1, (double)val2);
        }

        private static void SumFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void StdevFunction(IFunctionArgs args, IExcelCaller caller)
        {
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

        private static void RangeFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    $"RANGE function takes 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = args.Parameters[1].Evaluate();
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
            }

            var val1 = Conversion.ToDoubleOrErrorValue(arg1);
            if (val1 is ErrorValue)
            {
                args.Result = val1;
                return;
            }

            var val2 = Conversion.ToDoubleOrErrorValue(arg2);
            if (val2 is ErrorValue)
            {
                args.Result = val2;
                return;
            }

            args.Result = (double)val2 - (double)val1;
        }

        private static void PiFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 0)
            {
                throw new ArgumentException(
                    $"PI function takes no arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            args.Result = Math.PI;
        }

        private static void RadiansFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 1)
            {
                throw new ArgumentException(
                    $"RADIANS function takes only 1 argument, got {args.Parameters.Length}");
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

            var angle = (double)val;
            args.Result = Math.PI / 180 * angle;
        }

        private static void RoundFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length < 1)
            {
                throw new ArgumentException(
                    $"ROUND function takes at least 1 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }
            var arg2 = new object();

            if (args.Parameters.Length == 2)
            {
                arg2 = args.Parameters[1].Evaluate();
                if (arg2 is ErrorValue)
                {
                    args.Result = arg2;
                    return;
                }
            }
            else
            {
                arg2 = 0;
            }

            var val1 = Conversion.ToDoubleOrErrorValue(arg1);
            if (val1 is ErrorValue)
            {
                args.Result = val1;
                return;
            }

            var val2 = Conversion.ToInt32OrErrorValue(arg2);
            if (val2 is ErrorValue)
            {
                args.Result = val2;
                return;
            }

            // Based on MathX.round(double n, int p) from NPOI

            var n = (double)val1;
            var p = (int)val2;

            if (p != 0)
            {
                var temp = Math.Pow(10, p);
                args.Result = Math.Round(n * temp) / temp;
                return;
            }

            args.Result = Math.Round(n);
        }

        private static void RoundUpFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException("ROUNDUP function takes 2 arguments, " +
                                            $"got {args.Parameters.Length}");
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = args.Parameters[1].Evaluate();
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
            }

            var val = Conversion.ToDoubleOrErrorValue(arg1);
            if (val is ErrorValue)
            {
                args.Result = val;
                return;
            }

            var negative = (double)val < 0;

            // This code snippet based on example at http://stackoverflow.com/a/13483008
            var @decimal = new decimal(Math.Abs((double)val));
            var places = Convert.ToInt32(arg2);
            var factor = RoundFactor(places);
            @decimal *= factor;
            @decimal = Math.Ceiling(@decimal);
            @decimal /= factor;

            args.Result = Convert.ToDouble(negative ? -@decimal : @decimal);
        }

        private static void RoundDownFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException("ROUNDDOWN function takes 2 arguments, " +
                                            $"got {args.Parameters.Length}");
            }

            var arg1 = args.Parameters[0].Evaluate();
            if (arg1 is ErrorValue)
            {
                args.Result = arg1;
                return;
            }

            var arg2 = args.Parameters[1].Evaluate();
            if (arg2 is ErrorValue)
            {
                args.Result = arg2;
                return;
            }

            var val = Conversion.ToDoubleOrErrorValue(arg1);
            if (val is ErrorValue)
            {
                args.Result = val;
                return;
            }

            var negative = (double)val < 0;

            // This code snippet based on example at http://stackoverflow.com/a/13483008
            var @decimal = new decimal(Math.Abs((double)val));
            var places = Convert.ToInt32(arg2);
            var factor = RoundFactor(places);
            @decimal *= factor;
            @decimal = Math.Floor(@decimal);
            @decimal /= factor;

            args.Result = Convert.ToDouble(negative ? -@decimal : @decimal);
        }

        private static void SqrtFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 1)
            {
                throw new ArgumentException(
                    $"SQRT function takes only 1 argument, got {args.Parameters.Length}");
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

            args.Result = Conversion.ErrorValueOnInvalidDouble(Math.Sqrt((double)val));
        }
    }
}
