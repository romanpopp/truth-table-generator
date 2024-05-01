// TruthTable.cs
// Roman Popp

using System.Text;

namespace truth_table_generator
{
    public class TruthTable
    {
        // variables
        private int _varCount;
        private int _val;
        private string _statement;
        private int _lastOperation;

        // characters assigned to each var
        private char p_char = ' ';
        private char q_char = ' ';
        private char r_char = ' ';
        private char s_char = ' ';

        // arrays
        private StringBuilder[] _table;
        private List<string> _true = new();
        private List<string> _false = new();

        private char[] ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUWXYZ".ToCharArray();

        public TruthTable(string statement)
        {
            if (statement.Contains(":3"))
            {
                throw new Exception("Stop it you animal.");
            }
            _statement = statement;
            for (int i = 0; i < statement.Length; i++)
            {
                bool isLetter = false;
                for (int a = 0; a < ALPHABET.Length; a++)
                {
                    if (statement[i] == ' ') { break; }
                    if (statement[i] == ALPHABET[a])
                    {
                        isLetter = true;
                        ALPHABET[a] = ' ';
                        _varCount++;
                        break;
                    }
                }
                if (isLetter) 
                {
                    if (p_char == ' ')
                    {
                        p_char = statement[i];
                        _table = new StringBuilder[2];
                        _val = 1;
                    }
                    else if (q_char == ' ')
                    {
                        q_char = statement[i];
                        _table = new StringBuilder[4];
                        _val = 3;
                    }
                    else if (r_char == ' ')
                    {
                        r_char = statement[i];
                        _table = new StringBuilder[8];
                        _val = 7;
                    }
                    else if (s_char == ' ')
                    {
                        s_char = statement[i];
                        _table = new StringBuilder[16];
                        _val = 15;
                    }
                    else
                    {
                        throw new Exception("This program only supports up to 4 variables.");
                    }
                }
            }
            if (_table == null)
            {
                throw new Exception("Statement must contain variables.");
            }
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = new();
            }

            GenerateCombinations();
        }

        /// <summary>
        /// Generates all combinations of T / F for this table's variables.
        /// </summary>
        private void GenerateCombinations()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                int p = (_val >> _varCount - 1) & 1;
                int q = -1; int r = -1; int s = -1;
                _table[i].Append(Converter.ConvertBinaryToChar(p) + " ");

                if (_varCount > 1)
                {
                    q = (_val >> _varCount - 2) & 1;
                    _table[i].Append(Converter.ConvertBinaryToChar(q) + " ");
                    if (_varCount > 2)
                    {
                        r = (_val >> _varCount - 3) & 1;
                        _table[i].Append(Converter.ConvertBinaryToChar(r) + " ");
                        if (_varCount > 3)
                        {
                            s = _val & 1;
                            _table[i].Append(Converter.ConvertBinaryToChar(s) + " ");
                        }
                    }
                }
                
                _table[i].Append("| ");

                _table[i].Append(LogicCalculator.GetTruthStatement(_statement, p, q, r, s, 
                    p_char, q_char, r_char, s_char, out bool? b, out int last));
                _lastOperation = last;
                // adds to the final T / F listsZz
                if (b != null)
                {
                    StringBuilder sb = new();
                    sb.Append("[" + Converter.ConvertBinaryToChar(p));
                    if (_varCount > 1)
                    {
                        sb.Append(" " + Converter.ConvertBinaryToChar(q));
                        if (_varCount > 2)
                        {
                            sb.Append(" " + Converter.ConvertBinaryToChar(r));
                            if (_varCount > 3)
                            {
                                sb.Append(" " + Converter.ConvertBinaryToChar(s));
                            }
                        }
                    }
                    sb.Append("]");
                    if ((bool)b)
                    {
                        _true.Add(sb.ToString());
                    }
                    else
                    {
                        _false.Add(sb.ToString());
                    }
                }
                _val -= 1;
            }
        }

        /// <summary>
        /// Converts the table into a single string.
        /// </summary>
        /// <returns>A string containing the entire table.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _varCount; i++)
            {
                sb.Append("  ");
            }
            sb.Append("  ");
            for (int i = 0; i < _lastOperation; i++)
            {
                sb.Append(" ");
            }
            sb.AppendLine("*");
            sb.AppendLine("---");
            sb.Append(GetTopRow());
            sb.AppendLine("---");
            for (int i = 0; i < _table.Length; i++)
            {
                sb.AppendLine(_table[i].ToString());
            }
            sb.AppendLine("---");
            sb.Append(GetBelowTable());
            return sb.ToString();
        }

        /// <summary>
        /// Gets the top row of the truth table.
        /// </summary>
        /// <returns>A string containing the top row of the truth table.</returns>
        private string GetTopRow()
        {
            StringBuilder sb = new();
            sb.Append(p_char + " ");
            if (_varCount > 1)
            {
                sb.Append(q_char + " ");
                if (_varCount > 2)
                {
                    sb.Append(r_char + " ");
                    if (_varCount > 3)
                    {
                        sb.Append(s_char + " ");
                    }
                }
            }
            sb.AppendLine("| " + _statement);
            return sb.ToString();
        }

        /// <summary>
        /// Gets the section below the truth table.
        /// </summary>
        /// <returns>A string containing the information below table.</returns>
        private string GetBelowTable()
        {
            StringBuilder sb = new();

            if (_true.Count == 0) 
            { 
                sb.AppendLine("Contradictory");
            }
            else if (_false.Count == 0) 
            {
                sb.AppendLine("Tautology");
            }
            else
            {
                sb.AppendLine("Contingent");
                sb.Append("- T: ");
                foreach (string truth in _true)
                {
                    sb.Append(truth + " ");
                }
                sb.AppendLine();

                sb.Append("- F: ");
                foreach (string fals in _false)
                {
                    sb.Append(fals + " ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
