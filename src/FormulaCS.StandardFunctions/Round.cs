using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Round
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: ROUND(number, num_digits)
            // https://support.office.com/en-gb/article/ROUND-function-c018c5d8-40fb-4053-90b1-b3e7f61a213c

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

        /// <remarks>
        /// This function is used by the RoundUp and RoundDown functions in this file.
        /// See <a href="http://stackoverflow.com/a/13483008">this link</a> for the original example.
        /// </remarks>
        internal static decimal RoundFactor(int places)
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
    }
}