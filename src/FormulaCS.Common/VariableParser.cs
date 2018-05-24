using ExcelRangeExpander;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelRangeExpander.Interfaces;

namespace FormulaCS.Common
{
    public class VariableParser
    {
        private string formula;

        private const string VariableFinderRegex = @"\[(\w)*\]";
        private const string RangeFinderRegex = @"\[(\w)*((:)(\w)*)\]";

        public Dictionary<string, object> Variables { get; set; }

        private IRangeExpander excelRangeExpander;

        public VariableParser(string formula, Dictionary<string, object> Variables)
        {
            this.formula = formula;
            this.Variables = Variables;
            excelRangeExpander = new RangeExpander();
        }

        public string Parse()
        {
            ParseRanges();
            ParseVariables();

            return this.formula;
        }

        private void ParseRanges()
        {
            var matches = Regex.Matches(formula, RangeFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                var expandedRange = excelRangeExpander.ExpandList(variable).Select(x => string.Format("{0}", x)).ToList();
                var valuesWithValue = expandedRange.Where(x => Variables.ContainsKey(x)).Select(x => string.Format("{0}", Variables[x])).ToList();

                formula = formula.Replace(match.Value, string.Join(",", valuesWithValue));
            }
        }

        private void ParseVariables()
        {
            var matches = Regex.Matches(formula, VariableFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                if (!Variables.ContainsKey(variable))
                {
                    Variables.Add(variable, null);
                }

                formula = formula.Replace(match.Value, Variables[variable]?.ToString());
            }
        }
    }
}
