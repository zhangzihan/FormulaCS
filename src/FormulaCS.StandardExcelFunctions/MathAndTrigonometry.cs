using System;
using System.Collections.Generic;
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
//                {"LOG", LogFunction},
//                {"POWER", PowerFunction},
//                {"PI", PiFunction},
//                {"ROUND", RoundFunction},
                {"ROUNDUP", RoundUpFunction},
                {"ROUNDDOWN", RoundDownFunction},
//                {"RADIANS", RadiansFunction},
//                {"SQRT", SqrtFunction},
//                {"SUM", SumFunction},
//                {"TAN", TanFunction},
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
    }
}
