using ExcelRangeExpander;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelRangeExpander.Interfaces;

namespace FormulaCS.Common
{
    public class VariableParser
    {
        private string _formula;

        private const string VariableFinderRegex = @"\[(\w)*\]";
        private const string RangeFinderRegex = @"\[(\w)*((:)(\w)*)\]";

        public Dictionary<string, object> Variables { get; }

        private readonly IRangeExpander _excelRangeExpander;

        public VariableParser(string formula, Dictionary<string, object> variables)
        {
            _formula = formula;
            Variables = variables;
            _excelRangeExpander = new RangeExpander();
        }

        public string Parse()
        {
            ParseRanges();
            ParseVariables();

            return _formula;
        }

        private void ParseRanges()
        {
            var matches = Regex.Matches(_formula, RangeFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                var expandedRange = _excelRangeExpander.ExpandList(variable).Select(x => string.Format("{0}", x)).ToList();
                var valuesWithValue = expandedRange.Where(x => Variables.ContainsKey(x)).Select(x => string.Format("{0}", Variables[x])).ToList();

                _formula = _formula.Replace(match.Value, string.Join(",", valuesWithValue));
            }
        }

        private void ParseVariables()
        {
            var matches = Regex.Matches(_formula, VariableFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                if (!Variables.ContainsKey(variable))
                {
                    Variables.Add(variable, null);
                }

                _formula = _formula.Replace(match.Value, Variables[variable]?.ToString());
            }
        }
    }
}
