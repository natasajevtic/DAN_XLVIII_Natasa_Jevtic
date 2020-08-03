using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class ValidationForJmbg
    {
        /// <summary>
        /// This method checks is input for username valid.
        /// </summary>
        /// <param name="JMBG">JMBG of user.</param>
        /// <returns>True if jmbg is valid, false if not.</returns>
        public bool ValidationForJMBG(string JMBG)
        {
            if (JMBG.Length == 13 && JMBG.All(Char.IsDigit))
            {
                try
                {
                    string day = JMBG.Substring(0, 2);
                    string month = JMBG.Substring(2, 2);
                    string year = JMBG.Substring(4, 3);
                    string validyear = "";
                    if (year[0] == '0')
                    {
                        validyear = "2" + year;
                    }
                    else
                    {
                        validyear = "1" + year;
                    }
                    bool conversionYear = Int32.TryParse(year, out int y);
                    bool conversionMonth = Int32.TryParse(month, out int m);
                    bool conversionDay = Int32.TryParse(day, out int d);
                    //checks if passed jmbg contains a valid date
                    var expectedDatetime = new DateTime(y, m, d);
                    return true;
                }
                //if cannot convert to DateTime, because jmbg doesn't contain a valid date
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}