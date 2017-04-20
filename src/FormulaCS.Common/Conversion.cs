using System;

namespace FormulaCS.Common
{
    public static class Conversion
    {
        public static object ToDoubleOrErrorValue(object obj)
        {
            if (obj is DateTime)
            {
                return ((DateTime)obj).ToOADate();
            }

            if (obj is char)
            {
                return ErrorValue.Value;
            }

            if (obj is string)
            {
                return ErrorValue.Value;
            }

            return Convert.ToDouble(obj);
        }
    }
}