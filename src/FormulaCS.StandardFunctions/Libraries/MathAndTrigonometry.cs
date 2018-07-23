using System;
using System.Collections.Generic;
using FormulaCS.Common;

namespace FormulaCS.StandardFunctions.Libraries
{
    public static class MathAndTrigonometry
    {
        public static Dictionary<string, Function> Functions { get; }

        static MathAndTrigonometry()
        {
            Functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase)
            {
                {"LN", new Function {Delegate = new Ln().Function, IsThreadSafe = true}},
                {"LOG", new Function {Delegate = new Log().Function, IsThreadSafe = true}},
                {"POWER", new Function {Delegate = new Power().Function, IsThreadSafe = true}},
                {"PI", new Function {Delegate = new Pi().Function, IsThreadSafe = true}},
                {"RADIANS", new Function {Delegate = new Radians().Function, IsThreadSafe = true}},
                {"ROUND", new Function {Delegate = new Round().Function, IsThreadSafe = true}},
                {"ROUNDUP", new Function {Delegate = new RoundUp().Function, IsThreadSafe = true}},
                {"ROUNDDOWN", new Function {Delegate = new RoundDown().Function, IsThreadSafe = true}},
                {"SQRT", new Function {Delegate = new Sqrt().Function, IsThreadSafe = true}},
                {"SUM", new Function {Delegate = new Sum().Function, IsThreadSafe = true}},
                {"STDEV", new Function {Delegate = new Stdev().Function, IsThreadSafe = true}},
            };
        }
    }
}