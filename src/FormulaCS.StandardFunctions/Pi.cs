using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Pi
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: PI()
            // https://support.office.com/en-gb/article/PI-function-264199d0-a3ba-46b8-975a-c4a04608989b

            if (args.Parameters.Length != 0)
            {
                throw new ArgumentException(
                    $"PI function takes no arguments, got {args.Parameters.Length}",
                    nameof(args));
            }

            args.Result = Math.PI;
        }        
    }
}