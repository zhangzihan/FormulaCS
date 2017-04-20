using System;

namespace FormulaCS.Common
{
    public class Range
    {
        // NOTE: Worksheet and workbook specifications and limits
        // http://office.microsoft.com/en-gb/excel-help/excel-specifications-and-limits-HP010342495.aspx
        //
        //      Worksheet size: 1,048,576 rows by 16,384 columns.
        //          * 1048575 is the highest number representable in 20 bits.
        //          * 16383 is the highest number representable in 14 bits. That leaves 2 spare bits in a 16-bit word.
        //      Column width: 255 characters
        //      Total number of characters that a cell can contain: 32,767 characters
        //          * 32766 is the highest number representable in 15 bits. That leaves 1 spare bit in a 16-bit word.
        //
        private const int ExcelMaxRows = 1048576;
        private const int ExcelMaxColumns = 16384;

        public static int ConvertToColumnNumber(string columnName)
        {
            var number = 0;
            var pow = 1;
            for (var i = columnName.Length - 1; i >= 0; i--)
            {
                number += (columnName[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            if (number > ExcelMaxColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(columnName),
                    "Column number from columnName exceeds Excel column number limit");
            }

            return number;
        }

        public static string ConvertToColumnName(int columnNumber)
        {
            if (columnNumber < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnNumber),
                    "Column number less than zero not allowed");
            }

            if (columnNumber > ExcelMaxColumns)
            {
                throw new ArgumentOutOfRangeException(nameof(columnNumber),
                    "Column number exceeds Excel column number limit");
            }

            var dividend = columnNumber;
            var columnName = string.Empty;
            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        public Range(string token)
        {
            // TODO: Parse token?
        }
    }
}
