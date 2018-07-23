using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions.Libraries
{
    public class Statistical
    {
        public static Dictionary<string, Function> Functions { get; }

        static Statistical()
        {
            Functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase)
            {
                {"AVERAGE", new Function {Delegate = new Average().Function, IsThreadSafe = true}},
                {"MAX", new Function {Delegate = new Max().Function, IsThreadSafe = true}},
                {"MIN", new Function {Delegate = new Min().Function, IsThreadSafe = true}},
                {"COUNT", new Function {Delegate = new Count().Function, IsThreadSafe = true}},
                {"MEDIAN", new Function {Delegate = new Median().Function, IsThreadSafe = true}},
            };
        }
    }
}