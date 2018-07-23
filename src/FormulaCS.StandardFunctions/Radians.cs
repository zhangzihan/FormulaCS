using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Radians : IFunction
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: RADIANS(angle)
            // https://support.office.com/en-gb/article/RADIANS-function-ac409508-3d48-45f5-ac02-1497c92de5bf

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
    }
}