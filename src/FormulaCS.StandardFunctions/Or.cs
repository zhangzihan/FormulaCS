using FormulaCS.Common;

namespace FormulaCS.StandardFunctions
{
    public class Or
    {
        public void Function(IFunctionArgs args, IExcelCaller caller)
        {
            // Syntax: OR(logical1, [logical2], ...)
            // https://support.office.com/en-gb/article/OR-function-7d17ad14-8700-4281-b308-00b131e22af0

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