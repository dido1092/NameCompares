namespace NameCompares
{
    partial class Insert
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
            textBoxInsertDest = new TextBox();
            comboBoxDBLanguage = new ComboBox();
            buttonInsert = new Button();
            progressBarInsert = new ProgressBar();
            label1 = new Label();
            buttonInsertWord = new Button();
            textBoxEnterWord = new TextBox();
            comboBoxTables = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // textBoxInsertDest
            // 
            textBoxInsertDest.Location = new Point(33, 53);
            textBoxInsertDest.Name = "textBoxInsertDest";
            textBoxInsertDest.Size = new Size(389, 23);
            textBoxInsertDest.TabIndex = 0;
            // 
            // comboBoxDBLanguage
            // 
            comboBoxDBLanguage.FormattingEnabled = true;
            comboBoxDBLanguage.Items.AddRange(new object[] { "BG_W", "EN_W", "PT_W", "LAT_W", "W_NAMES", "BG_NAMES" });
            comboBoxDBLanguage.Location = new Point(428, 53);
            comboBoxDBLanguage.Name = "comboBoxDBLanguage";
            comboBoxDBLanguage.Size = new Size(79, 23);
            comboBoxDBLanguage.TabIndex = 1;
            comboBoxDBLanguage.SelectedIndexChanged += comboBoxDBLanguage_SelectedIndexChanged;
            // 
            // buttonInsert
            // 
            buttonInsert.Location = new Point(542, 53);
            buttonInsert.Name = "buttonInsert";
            buttonInsert.Size = new Size(103, 23);
            buttonInsert.TabIndex = 2;
            buttonInsert.Text = "Insert";
            buttonInsert.UseVisualStyleBackColor = true;
            buttonInsert.Click += buttonInsert_Click;
            // 
            // progressBarInsert
            // 
            progressBarInsert.Location = new Point(542, 82);
            progressBarInsert.Name = "progressBarInsert";
            progressBarInsert.Size = new Size(103, 10);
            progressBarInsert.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 35);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 4;
            label1.Text = "Destination";
            // 
            // buttonInsertWord
            // 
            buttonInsertWord.Location = new Point(542, 133);
            buttonInsertWord.Name = "buttonInsertWord";
            buttonInsertWord.Size = new Size(103, 23);
            buttonInsertWord.TabIndex = 5;
            buttonInsertWord.Text = "Insert";
            buttonInsertWord.UseVisualStyleBackColor = true;
            buttonInsertWord.Click += buttonInsertWord_Click;
            // 
            // textBoxEnterWord
            // 
            textBoxEnterWord.Location = new Point(286, 134);
            textBoxEnterWord.Name = "textBoxEnterWord";
            textBoxEnterWord.Size = new Size(136, 23);
            textBoxEnterWord.TabIndex = 6;
            // 
            // comboBoxTables
            // 
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Items.AddRange(new object[] { "BgWords", "LatWords", "EnWords", "PtWords" });
            comboBoxTables.Location = new Point(428, 134);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(79, 23);
            comboBoxTables.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(286, 116);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 8;
            label2.Text = "Enter word";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(428, 116);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 9;
            label3.Text = "Table";
            // 
            // Insert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBoxTables);
            Controls.Add(textBoxEnterWord);
            Controls.Add(buttonInsertWord);
            Controls.Add(label1);
            Controls.Add(progressBarInsert);
            Controls.Add(buttonInsert);
            Controls.Add(comboBoxDBLanguage);
            Controls.Add(textBoxInsertDest);
            Name = "Insert";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Insert";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInsertDest;
        private ComboBox comboBoxDBLanguage;
        private Button buttonInsert;
        private ProgressBar progressBarInsert;
        private Label label1;
        private Button buttonInsertWord;
        private TextBox textBoxEnterWord;
        private ComboBox comboBoxTables;
        private Label label2;
        private Label label3;
    }
}