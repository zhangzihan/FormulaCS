using System.Collections.Generic;
using Xunit;

namespace FormulaCS.Common.Tests
{
    public class VariableParserTests
    {
        public static Dictionary<string, object> values = new Dictionary<string, object>()
        {
            { "ID5", 1},
            { "ID6", 2}
        };

        [Theory]
        [InlineData("=SUM([ID5])", "=SUM(1)")]
        [InlineData("=SUM([ID5], [ID6])", "=SUM(1, 2)")]
        [InlineData("=SUM([ID5], [ID6], 3)", "=SUM(1, 2, 3)")]
        public void should_return_expected_formula_with_replaced_values(string formula, string expectedResult)
        {
            var variableParser = new VariableParser(formula, values);

            var result = variableParser.Parse();

            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData("=SUM([ID5])", "ID5", 1)]
        [InlineData("=SUM([ID6])", "ID6", 2)]
        public void should_return_correct_value_for_variable(string formula, string variableName, int expectedVariableValue)
        {
            var variableParser = new VariableParser(formula, values);

            variableParser.Parse();

            Assert.Equal(variableParser.Variables[variableName], expectedVariableValue);
        }

        [Fact]
        public void should_return_correct_value_for_range_variable()
        {
            var formula = "=SUM([ID5:ID6])";
            var variableParser = new VariableParser(formula, values);

            variableParser.Parse();
        }
    }
}