using System.Collections.Generic;
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

        public VariableParser(string formula, Dictionary<string, object> Values)
        {
            this.formula = formula;
            _values = Values;
            Variables = new Dictionary<string, object>();
        }

        public string Parse()
        {
            return ParseVariables();
        }

        private string ParseVariables()
        {
            var matches = Regex.Matches(this.formula, VariableFinderRegex);

            foreach (Match match in matches)
            {
                var variable = match.Value.Replace("[", string.Empty).Replace("]", string.Empty);

                Variables[variable] = _values[variable];

                this.formula = this.formula.Replace(match.Value, Variables[variable]?.ToString());
            }

            return this.formula;
        }
    }
}
