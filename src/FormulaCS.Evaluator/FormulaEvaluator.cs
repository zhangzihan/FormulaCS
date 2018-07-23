using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using FormulaCS.Common;
using FormulaCS.Parser;
using FormulaCS.StandardExtraFunctions;
using FormulaCS.StandardFunctions.Libraries;
using Range = FormulaCS.StandardExtraFunctions.Range;

namespace FormulaCS.Evaluator
{
    public class FormulaEvaluator
    {
        public readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, object> Variables { get; set; }

        public FormulaEvaluator()
        {
            Variables = new Dictionary<string, object>();
        }

        public void AddStandardFunctions()
        {
            AddFunctions(Logical.Functions);
            AddFunctions(MathAndTrigonometry.Functions);
            AddFunctions(Statistical.Functions);
            AddFunction("RANGE", new Range().Function);
            AddFunction("WEIGHTED", new Weighted().Function);
        }

        public void AddFunction(string name, FunctionDelegate function, bool isThreadSafe = true)
        {
            Functions.Add(name, new Function {Delegate = function, IsThreadSafe = isThreadSafe});
        }

        public void AddFunctions(Dictionary<string, Function> delegates)
        {
            foreach (var f in delegates)
            {
                Functions.Add(f.Key, f.Value);
            }
        }

        public object Evaluate(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return 0;
            }

            var parsedFormula = new VariableParser(formula, Variables);
            formula = parsedFormula.Parse();

            var inputStream = new AntlrInputStream(formula);
            var lexer = new FormulaLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new FormulaParser(tokens);

            var errorListener = new FormulaErrorListener();
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);
            var parseTree = parser.main();

            if (!errorListener.IsValid)
            {
                throw new FormulaException(
                    errorListener.ErrorLocation,
                    errorListener.ErrorMessage);
            }

            return new EvaluationVisitor(Functions).VisitMain(parseTree);
        }
    }
}