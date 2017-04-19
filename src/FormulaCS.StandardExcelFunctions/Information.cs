using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class Information
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Information()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {

            };
        }
    }
}