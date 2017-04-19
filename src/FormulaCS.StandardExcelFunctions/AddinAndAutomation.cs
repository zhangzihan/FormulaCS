using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class AddinAndAutomation
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static AddinAndAutomation()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {

            };
        }
    }
}