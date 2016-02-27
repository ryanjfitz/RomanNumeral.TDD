using System.Linq;

namespace RomanNumeral.TDD.Tests
{
    public static class RomanNumeralConverter
    {
        public static string Convert(int arabicNumber)
        {
            string thousandsNumeral = CalculateThousandsNumeral(arabicNumber);

            string hundredsNumeral = CalculateHundredsNumeral(arabicNumber);

            string tensNumeral = CalculateTensNumeral(arabicNumber);

            string onesNumeral = CalculateOnesNumeral(arabicNumber);

            return thousandsNumeral + hundredsNumeral + tensNumeral + onesNumeral;
        }

        private static string CalculateThousandsNumeral(int arabicNumber)
        {
            string romanNumeral = "";

            int thousandsDigit = GetDigitAtPlace(arabicNumber, Place.Thousands);

            for (int i = 0; i < thousandsDigit; i++)
                romanNumeral += "M";

            return romanNumeral;
        }

        private static string CalculateHundredsNumeral(int arabicNumber)
        {
            return CalculateNumeral(arabicNumber, Place.Hundreds, "C", "D", "CM");
        }

        private static string CalculateTensNumeral(int arabicNumber)
        {
            return CalculateNumeral(arabicNumber, Place.Tens, "X", "L", "XC");
        }

        private static string CalculateOnesNumeral(int arabicNumber)
        {
            return CalculateNumeral(arabicNumber, Place.Ones, "I", "V", "IX");
        }

        private static string CalculateNumeral(int arabicNumber, Place place, string unitNumeral, string midwayNumeral, string endNumeral)
        {
            string romanNumeral = "";

            int digitAtPlace = GetDigitAtPlace(arabicNumber, place);

            if (digitAtPlace < 4)
                for (int i = 0; i < digitAtPlace; i++)
                    romanNumeral += unitNumeral;
            else if (digitAtPlace < 9)
            {
                romanNumeral += midwayNumeral;

                if (digitAtPlace < 5)
                    romanNumeral = romanNumeral.Insert(romanNumeral.Length - 1, unitNumeral);
                else if (digitAtPlace > 5)
                    for (int i = 0; i < digitAtPlace - 5; i++)
                        romanNumeral = romanNumeral.Insert(romanNumeral.Length, unitNumeral);
            }
            else if (digitAtPlace == 9)
                romanNumeral += endNumeral;

            return romanNumeral;
        }

        private static int GetDigitAtPlace(int arabicNumber, Place place)
        {
            char[] s = arabicNumber.ToString().Reverse().ToArray();

            switch (place)
            {
                case Place.Thousands:
                    if (s.Length >= 4)
                        return int.Parse(s[3].ToString());
                    break;
                case Place.Hundreds:
                    if (s.Length >= 3)
                        return int.Parse(s[2].ToString());
                    break;
                case Place.Tens:
                    if (s.Length >= 2)
                        return int.Parse(s[1].ToString());
                    break;
                case Place.Ones:
                    if (s.Length >= 1)
                        return int.Parse(s[0].ToString());
                    break;
            }

            return 0;
        }

        private enum Place
        {
            Ones,
            Tens,
            Hundreds,
            Thousands
        }
    }
}