// UserInterface.cs
// Roman Popp

namespace truth_table_generator
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
            uxInputTxt.KeyDown += new KeyEventHandler(EnterPress);
        }

        /// <summary>
        /// Handles click event on Generate button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Information about the event.</param>
        private void GenerateClick(object? sender, EventArgs e)
        {
            try
            {
                TruthTable newTable = new(uxInputTxt.Text);
                uxOutputTxt.Text = newTable.ToString();
            }
            catch (Exception ex)
            {
                uxOutputTxt.Text = "Solution not generated: " + ex.Message;
            }
        }

        /// <summary>
        /// Calls GenerateClick when Enter key is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Information about the key pressed.</param>
        private void EnterPress(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GenerateClick(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
