using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public static class Logical
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Logical()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {
                {"AND", AndFunction},
                {"IF", IfFunction},
                {"IFERROR", IfErrorFunction},
                {"OR", OrFunction},
            };
        }

        private static void AndFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length < 2)
            {
                throw new ArgumentException(
                    $"AND function requires at least 2 arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            var result = true;

            foreach (var fx in args.Parameters)
            {
                var arg = fx.Evaluate();

                if (arg is ErrorValue)
                {
                    args.Result = arg;
                    return;
                }

                if (!(arg is bool))
                {
                    args.Result = ErrorValue.Num;
                    return;
                }

                if ((bool)arg)
                {
                    continue;
                }

                result = false;
                break;
            }

            args.Result = result;
        }

        private static void IfFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2 && args.Parameters.Length != 3)
            {
                throw new ArgumentException(
                    "IF function takes 2 or 3 arguments",
                    nameof(args));
            }

            var arg = args.Parameters[0].Evaluate();
            if (arg is ErrorValue)
            {
                args.Result = arg;
                return;
            }

            if (args.Parameters.Length == 3)
            {
                args.Result = Convert.ToBoolean(arg)
                    ? args.Parameters[1].Evaluate()
                    : args.Parameters[2].Evaluate();
            }
            else
            {
                args.Result = Convert.ToBoolean(arg)
                    ? args.Parameters[1].Evaluate()
                    : false;
            }
        }

        private static void IfErrorFunction(IFunctionArgs args, IExcelCaller caller)
        {
            if (args.Parameters.Length != 2)
            {
                throw new ArgumentException(
                    "IFERROR function takes exactly 2 arguments",
                    nameof(args));
            }

            try
            {
                var arg = args.Parameters[0].Evaluate();
                if (arg is ErrorValue)
                {
                    args.Result = args.Parameters[1].Evaluate();
                    return;
                }

                args.Result = arg;
            }
            catch (Exception)
            {
                args.Result = args.Parameters[1].Evaluate();
            }
        }

        private static void OrFunction(IFunctionArgs args, IExcelCaller caller)
        {
            var result = false;

            foreach (var fx in args.Parameters)
            {
                var arg = fx.Evaluate();
                if (arg is ErrorValue)
                {
                    args.Result = arg;
                    return;
                }

                if (!(arg is bool))
                {
                    args.Result = ErrorValue.Num;
                    return;
                }

                if (!(bool)arg)
                {
                    continue;
                }

                result = true;
                break;
            }

            args.Result = result;
        }
    }
}