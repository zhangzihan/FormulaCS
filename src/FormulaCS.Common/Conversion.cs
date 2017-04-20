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

            var n = Convert.ToDouble(obj);

            if (double.IsNaN(n) || double.IsInfinity(n))
            {
                return ErrorValue.Value;
            }

            return n;
        }

        public static object ToInt32OrErrorValue(object obj)
        {
            if (obj is DateTime)
            {
                throw new NotImplementedException();
            }

            if (obj is char)
            {
                return ErrorValue.Value;
            }

            if (obj is string)
            {
                return ErrorValue.Value;
            }

            return Convert.ToInt32(obj);
        }

        public static object ErrorValueOnInvalidDouble(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                return ErrorValue.Num;
            }

            return value;
        }
    }
}