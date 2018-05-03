using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using FormulaCS.Common;
using FormulaCS.Parser;
using FormulaCS.StandardExcelFunctions;

namespace FormulaCS.Evaluator
{
    public class FormulaEvaluator
    {
        public readonly Dictionary<string, FunctionDelegate> Functions = new Dictionary<string, FunctionDelegate>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string,object> Variables { get; set; }

        public void AddStandardFunctions()
        {
            AddFunctions(DateAndTime.FunctionDelegates);
            AddFunctions(Logical.FunctionDelegates);
            AddFunctions(LookupAndReference.FunctionDelegates);
            AddFunctions(MathAndTrigonometry.FunctionDelegates);
            AddFunctions(Statistical.FunctionDelegates);
            AddFunctions(Text.FunctionDelegates);
        }

        public void AddFunctions(Dictionary<string, FunctionDelegate> delegates)
        {
            foreach (var f in delegates)
            {
                Functions.Add(f.Key, f.Value);
            }
        }

        public object Evaluate(string formula, Dictionary<string, object> values = null)
        {
            if (values == null)
                values = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(formula))
            {
                return 0;
            }

            var parsedFormula = new VariableParser(formula, values);
            formula = parsedFormula.Parse();

            var inputStream = new AntlrInputStream(formula);
            var lexer = new FormulaLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new FormulaParser(tokens);

            Variables = parsedFormula.Variables;

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