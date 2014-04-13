using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AM.Interop.Excel
{
    public class RangeName
    {
        public static string GetColumnName(int columnNumber)
        {
            if (columnNumber <= 0)
                throw new ArgumentOutOfRangeException("columnNumber");

            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public static int GetColumnNumber(string columnName)
        {
            int columnNumber = 0;
            int pow = 1;
            for (int i = columnName.Length - 1; i >= 0; i--)
            {
                columnNumber += (columnName[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return columnNumber;
        }

        public static string R1C1ToA1(string R1C1Name)
        {
            string[] indices = R1C1Name.Split('R', 'C');
            if (indices.Length == 3)
            {
                int columnNumber = int.Parse(indices[2]);
                return GetColumnName(columnNumber) + indices[1];
            }
            else if (indices.Length == 2 && R1C1Name.Contains("C"))
            {
                int columnNumber = int.Parse(indices[1]);
                return GetColumnName(columnNumber);
            }
            else if (indices.Length == 2 && R1C1Name.Contains("R"))
            {
                return indices[1];
            }
            else
                throw new ApplicationException("Can't parse R1C1Name");
        }

        public static string A1ToR1C1(string A1Name)
        {
            string[] indices = Regex.Split(A1Name, @"(\D+)");
            if (indices.Length == 2)
            {
                return "R" + indices[1] + "C" + GetColumnNumber(indices[0]).ToString();
            }
            else if (indices.Length == 1 && char.IsDigit(indices[0][0]))
            {
                return "R" + indices[0];
            }
            else if (indices.Length == 1 && char.IsLetter(indices[0][0]))
            {
                return "C" + GetColumnNumber(indices[0]).ToString();
            }
            else
                throw new ApplicationException("Can't parse A1Name");
        }
    }
}
