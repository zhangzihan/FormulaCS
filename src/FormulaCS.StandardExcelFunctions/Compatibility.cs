using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class Compatibility
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Compatibility()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {

            };
        }
    }
}