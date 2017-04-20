using System;
using Xunit;

namespace FormulaCS.Common.Tests
{
    public class ConversionTests
    {
        [Fact]
        public void ConvertsDateTimeToDoubleUsedByExcel()
        {
            Assert.Equal(42826.0, Conversion.ToDoubleOrErrorValue(DateTime.Parse("2017-04-01")));
            Assert.Equal(new decimal(42826.8807986111), new decimal((double)Conversion.ToDoubleOrErrorValue(DateTime.Parse("2017-04-01 21:08:21"))));
        }
    }
}