namespace NameCompares
{
    partial class FrmCompareBgWords_Names
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxWord = new TextBox();
            label1 = new Label();
            comboBoxTables = new ComboBox();
            label2 = new Label();
            buttonCompare = new Button();
            checkBoxRepeatLetters = new CheckBox();
            progressBarCompare = new ProgressBar();
            checkBoxSameLength = new CheckBox();
            SuspendLayout();
            // 
            // textBoxWord
            // 
            textBoxWord.Location = new Point(28, 50);
            textBoxWord.Name = "textBoxWord";
            textBoxWord.Size = new Size(241, 23);
            textBoxWord.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 32);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 1;
            label1.Text = "Enter Word";
            // 
            // comboBoxTables
            // 
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Items.AddRange(new object[] { "BgWords", "BgNames" });
            comboBoxTables.Location = new Point(275, 50);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(90, 23);
            comboBoxTables.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(275, 32);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Tables";
            // 
            // buttonCompare
            // 
            buttonCompare.Location = new Point(147, 96);
            buttonCompare.Name = "buttonCompare";
            buttonCompare.Size = new Size(218, 41);
            buttonCompare.TabIndex = 4;
            buttonCompare.Text = "Compare";
            buttonCompare.UseVisualStyleBackColor = true;
            buttonCompare.Click += buttonCompare_Click;
            // 
            // checkBoxRepeatLetters
            // 
            checkBoxRepeatLetters.AutoSize = true;
            checkBoxRepeatLetters.Location = new Point(28, 96);
            checkBoxRepeatLetters.Name = "checkBoxRepeatLetters";
            checkBoxRepeatLetters.Size = new Size(100, 19);
            checkBoxRepeatLetters.TabIndex = 5;
            checkBoxRepeatLetters.Text = "Repeat Letters";
            checkBoxRepeatLetters.UseVisualStyleBackColor = true;
            // 
            // progressBarCompare
            // 
            progressBarCompare.Location = new Point(28, 159);
            progressBarCompare.Name = "progressBarCompare";
            progressBarCompare.Size = new Size(337, 12);
            progressBarCompare.TabIndex = 6;
            // 
            // checkBoxSameLength
            // 
            checkBoxSameLength.AutoSize = true;
            checkBoxSameLength.Location = new Point(29, 118);
            checkBoxSameLength.Name = "checkBoxSameLength";
            checkBoxSameLength.Size = new Size(92, 19);
            checkBoxSameLength.TabIndex = 7;
            checkBoxSameLength.Text = "SameLength";
            checkBoxSameLength.UseVisualStyleBackColor = true;
            // 
            // FrmCompareBgWords_Names
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 200);
            Controls.Add(checkBoxSameLength);
            Controls.Add(progressBarCompare);
            Controls.Add(checkBoxRepeatLetters);
            Controls.Add(buttonCompare);
            Controls.Add(label2);
            Controls.Add(comboBoxTables);
            Controls.Add(label1);
            Controls.Add(textBoxWord);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmCompareBgWords_Names";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Compare Bg Words/Names";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxWord;
        private Label label1;
        private ComboBox comboBoxTables;
        private Label label2;
        private Button buttonCompare;
        private CheckBox checkBoxRepeatLetters;
        private ProgressBar progressBarCompare;
        private CheckBox checkBoxSameLength;
    }
}