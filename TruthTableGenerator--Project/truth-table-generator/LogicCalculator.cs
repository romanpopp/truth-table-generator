// LogicCalculator.cs
// Roman Popp

using System.Text;

namespace truth_table_generator
{
    public static class LogicCalculator
    {
        /// <summary>
        /// Gets the truth statement for the given inputs.
        /// </summary>
        /// <param name="statement">The truth statement to evaluate.</param>
        /// <param name="p">First input.</param>
        /// <param name="q">Second input.</param>
        /// <param name="r">Third input. Use -1 if not used.</param>
        /// <param name="s">Fourth input. Use -1 if not used.</param>
        /// <param name="p_char">The character for variable 'p'.</param>
        /// <param name="q_char">The character for variable 'q'.</param>
        /// <param name="r_char">The character for variable 'r'.</param>
        /// <param name="s_char">The character for variable 's'.</param>
        /// <param name="solution">The final evaluation for the truth statement.</param>
        /// <returns>The truth statement for the given inputs.</returns>
        public static string GetTruthStatement(string statement, int p, int q, int r, int s, char p_char, char q_char, 
            char r_char, char s_char, out bool? solution, out int lastOperation)
        {
            char[] text = statement.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case ' ': case '!': case '¬': case '-': case '>': case '→':
                    case '^': case '∧': case 'V': case '∨': case '(': case ')':
                        break;
                    default:
                        if (text[i] == p_char) {
                            text[i] = Converter.ConvertBinaryToChar(p);
                            break;
                        }
                        else if (text[i] == q_char) {
                            text[i] = Converter.ConvertBinaryToChar(q);
                            break;
                        }
                        else if (text[i] == r_char) {
                            text[i] = Converter.ConvertBinaryToChar(r);
                            break;
                        }
                        else if (text[i] == s_char) {
                            text[i] = Converter.ConvertBinaryToChar(s);
                            break;
                        }
                        throw new Exception("'" + text[i] + "' is not a valid character.");

                }
            }
            PriorityQueue<Statement, double> priorityQueue = GetStatementOrder(text);
            string truth = GenerateSolution(priorityQueue, text, out bool? b, out int last);
            solution = b;
            lastOperation = last;
            return truth;
        }

        /// <summary>
        /// Creates a priority queue that stores every operation within the given statement in order of excecution.
        /// </summary>
        /// <param name="text">The statement to analyze.</param>
        /// <returns>A priority queue containing the order of each statement.</returns>
        private static PriorityQueue<Statement, double> GetStatementOrder(char[] text)
        {
            PriorityQueue<Statement, double> queue = new();

            int count = 0;
            int depth = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // Check for open parentheses
                if (text[i] == '(')
                {
                    depth++;
                }

                // Check for close parentheses
                else if (text[i] == ')')
                {
                    depth--;
                    if (depth < 0) { throw new Exception("Imbalanced parentheses, too many ')'."); }
                }

                else 
                {
                    int prio = 0;
                    int c = 0;
                    // Check for 'not' operator (priority 1, priority decreases for duplicates)
                    if (text[i] == '!' || text[i] == '¬')
                    {
                        c = -1;
                        prio = 100000;
                    }
                    // Check for 'and' operator (priority 2, priority increases for duplicates)
                    else if (text[i] == '^' || text[i] == '∧')
                    {
                        c = 1;
                        prio = 200000;
                    }
                    // Check for 'or' operator (priority 3, priority increases for duplicates)
                    else if (text[i] == 'V' || text[i] == '∨')
                    {
                        c = 1;
                        prio = 300000;
                    }
                    // Check for 'implies' operator (priority 4, priority decreases for duplicates)
                    else if (text[i] == '-' || text[i] == '→')
                    {
                        c = -1;
                        prio = 400000;
                    }
                    
                    if (prio > 0)
                    {
                        // Creating the new statement
                        Statement operation = new(text[i], i);
                        queue.Enqueue(operation, (prio) / (Math.Pow(10, depth)) + (count * c));
                        count++;
                    }
                }
            }

            if (depth != 0)
            {
                throw new Exception("Imbalanced parentheses, too many '('.");
            }
            if (queue.Count == 0)
            {
                throw new Exception("No operations detected.");
            }
            return queue;
        }

        /// <summary>
        /// Generates a solution for the current line.
        /// </summary>
        /// <param name="queue">A priority queue containing all statements in order of excecution.</param>
        /// <param name="text">The statement to analyze.</param>
        /// <param name="solution">The solution for this statement.</param>
        /// <param name="lastOperation">The final operation in this statement.</param>
        /// <returns>A solution in the form of a string.</returns>
        private static string GenerateSolution(PriorityQueue<Statement, double> queue, char[] text, out bool? solution, out int lastOperation)
        {
            bool[] keepText = new bool[text.Length];
            bool[] used = new bool[text.Length];
            solution = null;
            lastOperation = -1;

            while (queue.Count > 0)
            {
                Statement current = queue.Dequeue();
                keepText[current.Index] = true;
                int leftIndex = current.Index - 1;
                int rightIndex = current.Index + 1;

                // handling 'not' operations seperately, they only have 1 input
                if (current.Operation == '!' || current.Operation == '¬')
                {
                    try
                    {
                        while (!(text[rightIndex] == 'T' || text[rightIndex] == 'F') || used[rightIndex])
                        {
                            rightIndex++;
                        }
                    }
                    catch
                    {
                        throw new Exception("'" + current.Operation + "' operation had no target.");
                    }
                    current.Right = Converter.ConvertCharToBool(text[rightIndex]);
                    current.CalculateOutput();

                    if (keepText[rightIndex])
                    { used[rightIndex] = true; }
                    else
                    { text[rightIndex] = ' '; }

                    text[current.Index] = Converter.ConvertBoolToChar(current.Output);

                }

                // 'and', 'or', and 'implies' can be handled together
                else
                {
                    // getting left and right truth assignments for this operator
                    try
                    {
                        while (!(text[leftIndex] == 'T' || text[leftIndex] == 'F') || used[leftIndex])
                        {
                            leftIndex--;
                        }
                        while (!(text[rightIndex] == 'T' || text[rightIndex] == 'F') || used[rightIndex])
                        {
                            rightIndex++;
                        }
                    }
                    catch
                    {
                        string operation = current.Operation.ToString();
                        if (current.Operation == '-') { operation = "->"; }
                        throw new Exception("'" + operation + "' operation had no target.");
                    }

                    current.Left = Converter.ConvertCharToBool(text[leftIndex]);
                    current.Right = Converter.ConvertCharToBool(text[rightIndex]);
                    current.CalculateOutput();

                    if (keepText[leftIndex])
                    { used[leftIndex] = true; }
                    else
                    { text[leftIndex] = ' '; }

                    if (keepText[rightIndex])
                    { used[rightIndex] = true; }
                    else
                    { text[rightIndex] = ' '; }

                    text[current.Index] = Converter.ConvertBoolToChar(current.Output);

                    if (queue.Count == 0)
                    {
                        solution = current.Output;
                        lastOperation = current.Index;
                    }
                }
            }
            
            string finalSolution = SolutionToString(text);
            for (int i = 0; i < finalSolution.Length; i++)
            {
                if (finalSolution[i] != ' ' && !keepText[i])
                {
                    throw new Exception("Variable with no operation.");
                }
            }
            return finalSolution;
        }

        /// <summary>
        /// Converts a char array to a string.
        /// </summary>
        /// <param name="solution">The char array.</param>
        /// <returns>A string.</returns>
        private static string SolutionToString(char[] solution)
        {
            StringBuilder sb = new();
            foreach (char c in solution)
            {
                // cleaning up unneccesary characters
                if (c == '(' || c == ')' || c == '>')
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
