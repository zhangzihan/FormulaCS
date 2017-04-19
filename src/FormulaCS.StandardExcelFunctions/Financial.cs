using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class Financial
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Financial()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {

            };
        }
    }
}