using System;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Log
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: LOG(number, [base])
            // https://support.office.com/en-gb/article/LOG-function-4e82f196-1ca9-4747-8fb0-6c4a3abb3280

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
    }
}