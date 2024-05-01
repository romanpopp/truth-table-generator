// Statement.cs
// Roman Popp

namespace truth_table_generator
{
    public class Statement
    {
        public int Index;
        public bool Output;
        public char Operation;
        
        public bool Left { private get; set; }
        public bool Right { private get; set; }

        public Statement(char operation, int index)
        {
            Index = index;
            Operation = operation;
            CalculateOutput();
        }

        /// <summary>
        /// Gets the output of this statement and stores it in the statement's public variable.
        /// </summary>
        public void CalculateOutput()
        {
            if (Operation == '^' || Operation == '∧')
            {
                Output = Left && Right;
            }
            else if (Operation == 'V' || Operation == '∨')
            {
                Output = Left || Right;
            }
            else if (Operation == '-' || Operation == '→')
            {
                Output = Right || !Left;
            }
            else
            {
                Output = !Right;
            }
        }
    }
}
