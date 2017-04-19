using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardExcelFunctions
{
    public class Cube
    {
        public static readonly Dictionary<string, FunctionDelegate> FunctionDelegates;

        static Cube()
        {
            FunctionDelegates = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase)
            {

            };
        }
    }
}