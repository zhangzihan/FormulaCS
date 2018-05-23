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

                var expandedRange = excelRangeExpander.ExpandList(variable).Select(x => string.Format("[{0}]", x)).ToList();

                formula = formula.Replace(match.Value, string.Join(",", expandedRange));
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

                if (!_values.ContainsKey(variable))
                {
                    _values.Add(variable, null);
                }

                Variables[variable] = _values[variable];

                formula = formula.Replace(match.Value, Variables[variable]?.ToString());
            }
        }
    }
}
