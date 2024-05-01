// Converter.cs
// Roman Popp

namespace truth_table_generator
{
    public static class Converter
    {
        /// <summary>
        /// Converts binary inputs (0 or 1) into "T" or "F".
        /// </summary>
        /// <param name="i">Input number.</param>
        /// <returns>"T" if input is 1, "F" if input is 0.</returns>
        public static char ConvertBinaryToChar(int i)
        {
            if (i == 1) return 'T';
            else if (i == 0) return 'F';
            else throw new ArgumentException();
        }

        /// <summary>
        /// Converts binary inputs (0 or 1) into true or false.
        /// </summary>
        /// <param name="i">Input number.</param>
        /// <returns>True if input is 1, false if input is 0.</returns>
        public static bool ConvertBinaryToBool(int i)
        {
            if (i == 1) return true;
            else if (i == 0) return false;
            else throw new ArgumentException();
        }

        /// <summary>
        /// Converts boolean inputs into "T" or "F".
        /// </summary>
        /// <param name="b">Input boolean.</param>
        /// <returns>"T" if input is true, "F" if input is false.</returns>
        public static char ConvertBoolToChar(bool b)
        {
            if (b) return 'T';
            return 'F';
        }

        /// <summary>
        /// Converts char inputs into true or false.
        /// </summary>
        /// <param name="c">Input char.</param>
        /// <returns>True if input is "T", false if input is "F".</returns>
        public static bool ConvertCharToBool(char c)
        {
            if (c == 'T') return true;
            return false;
        }
    }
}
