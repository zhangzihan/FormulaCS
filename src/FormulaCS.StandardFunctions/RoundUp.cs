using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class RoundUp
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: ROUNDUP(number, num_digits)
            // https://support.office.com/en-gb/article/ROUNDUP-function-f8bc9b23-e795-47db-8703-db171d0c42a7

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
            var factor = Round.RoundFactor(places);
            @decimal *= factor;
            @decimal = Math.Ceiling(@decimal);
            @decimal /= factor;

            args.Result = Convert.ToDouble(negative ? -@decimal : @decimal);
        }        
    }
}