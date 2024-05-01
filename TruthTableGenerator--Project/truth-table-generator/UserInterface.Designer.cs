namespace truth_table_generator
{
    partial class UserInterface
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            uxOutputTxt = new TextBox();
            uxInputTxt = new TextBox();
            uxGenerate = new Button();
            uxToolTip = new ToolTip(components);
            SuspendLayout();
            // 
            // uxOutputTxt
            // 
            uxOutputTxt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            uxOutputTxt.BackColor = Color.FromArgb(30, 30, 30);
            uxOutputTxt.BorderStyle = BorderStyle.None;
            uxOutputTxt.Font = new Font("Consolas", 9.134328F, FontStyle.Regular, GraphicsUnit.Point, 0);
            uxOutputTxt.ForeColor = Color.RoyalBlue;
            uxOutputTxt.Location = new Point(0, 71);
            uxOutputTxt.Multiline = true;
            uxOutputTxt.Name = "uxOutputTxt";
            uxOutputTxt.ReadOnly = true;
            uxOutputTxt.ScrollBars = ScrollBars.Vertical;
            uxOutputTxt.Size = new Size(677, 390);
            uxOutputTxt.TabIndex = 0;
            uxOutputTxt.Text = "No truth table generated.";
            // 
            // uxInputTxt
            // 
            uxInputTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            uxInputTxt.Location = new Point(26, 20);
            uxInputTxt.Name = "uxInputTxt";
            uxInputTxt.PlaceholderText = "Enter statement here . . .";
            uxInputTxt.Size = new Size(505, 30);
            uxInputTxt.TabIndex = 2;
            // 
            // uxGenerate
            // 
            uxGenerate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            uxGenerate.Location = new Point(537, 19);
            uxGenerate.Name = "uxGenerate";
            uxGenerate.Size = new Size(113, 31);
            uxGenerate.TabIndex = 3;
            uxGenerate.Text = "Generate";
            uxGenerate.UseVisualStyleBackColor = true;
            uxGenerate.Click += GenerateClick;
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(677, 463);
            Controls.Add(uxGenerate);
            Controls.Add(uxInputTxt);
            Controls.Add(uxOutputTxt);
            ForeColor = Color.FromArgb(40, 40, 40);
            Name = "UserInterface";
            Text = "Truth Table Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox uxOutputTxt;
        private TextBox uxInputTxt;
        private Button uxGenerate;
        private ToolTip uxToolTip;
    }
}
