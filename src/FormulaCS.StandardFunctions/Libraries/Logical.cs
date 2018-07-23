using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions.Libraries
{
    public static class Logical
    {
        public static Dictionary<string, Function> Functions { get; }

        static Logical()
        {
            Functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase)
            {
                {"AND", new Function {Delegate = new And().Function, IsThreadSafe = true}},
                {"IF", new Function {Delegate = new If().Function, IsThreadSafe = true}},
                {"IFERROR", new Function {Delegate = new IfError().Function, IsThreadSafe = true}},
                {"OR", new Function {Delegate = new Or().Function, IsThreadSafe = true}},
            };
        }
    }
}
