using System;
using ExcelRangeExpander;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FormulaCS.Common
{
    public class VariableParser
    {
        private string formula;
        private readonly Dictionary<string, object> _values;

        private const string VariableFinderRegex = @"\[(\w)*\]";
        private const string RangeFinderRegex = @"\[(\w)*((:)(\w)*)\]";

        public Dictionary<string, object> Variables { get; set; }

        private IRangeExpander excelRangeExpander;

        public VariableParser(string formula, Dictionary<string, object> Values)
        {
            this.formula = formula;
            _values = Values;
            Variables = new Dictionary<string, object>();
            excelRangeExpander = new RangeExpander();
        }

        public string Parse()
        {
            this.formula = ParseRanges(this.formula);
            this.formula = ParseVariables(this.formula);

            return this.formula;
        }

        private string ParseRanges(string formula)
        {
            var matches = Regex.Matches(formula, RangeFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                var expandedRange = excelRangeExpander.Expand(variable).Split(new string[] { "," }, StringSplitOptions.None).Select(x => string.Format("[{0}]", x));

                formula = formula.Replace(match.Value, string.Join(",", expandedRange));
            }

            return formula;
        }

        private string ParseVariables(string formula)
        {
            var matches = Regex.Matches(formula, VariableFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                Variables[variable] = _values[variable];

                formula = formula.Replace(match.Value, Variables[variable]?.ToString());
            }

            return formula;
        }
    }
}
