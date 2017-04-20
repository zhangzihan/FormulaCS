using System;
using Xunit;

namespace FormulaCS.Common.Tests
{
    public class RangeTests
    {
        [Fact]
        public void ReturnsCorrectColumnNumberForColumnName()
        {
            Assert.Equal(1, Range.ConvertToColumnNumber("A"));
            Assert.Equal("A", Range.ConvertToColumnName(1));

            Assert.Equal(27, Range.ConvertToColumnNumber("AA"));
            Assert.Equal("AA", Range.ConvertToColumnName(27));

            Assert.Equal(16384, Range.ConvertToColumnNumber("XFD"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Range.ConvertToColumnNumber("XFE"));
        }
    }
}
