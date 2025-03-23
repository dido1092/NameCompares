namespace NameCompares
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolStripButtonInsertIntoDB = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonExportTable = new ToolStripButton();
            enBgMatchesWordsBindingSource = new BindingSource(components);
            checkedListBoxBg = new CheckedListBox();
            buttonAdd = new Button();
            richTextBoxSelectedLetters = new RichTextBox();
            checkBoxSameLength = new CheckBox();
            label2 = new Label();
            checkedListBoxLatSpecial = new CheckedListBox();
            label3 = new Label();
            richTextBoxResult = new RichTextBox();
            comboBoxChoise = new ComboBox();
            buttonRemove = new Button();
            progressBarLoad = new ProgressBar();
            buttonClearResult = new Button();
            buttonClearSelectedLetters = new Button();
            labelResultItems = new Label();
            label4 = new Label();
            buttonCompareWordlBgNames = new Button();
            label5 = new Label();
            textBoxViewId = new TextBox();
            label7 = new Label();
            labelInfo = new Label();
            buttonViewID = new Button();
            buttonViewDescription = new Button();
            buttonDuplicatedWorldNames = new Button();
            buttonAddInTable = new Button();
            textBoxLetterRelationsName = new TextBox();
            label6 = new Label();
            buttonLoadLetterRelations = new Button();
            comboBoxLetterRelationsName = new ComboBox();
            buttonViewTableResult = new Button();
            buttonInsertInTableResult = new Button();
            buttonViewTableLetRelat = new Button();
            checkBoxDontRepeatLetters = new CheckBox();
            comboBoxLanguage = new ComboBox();
            label1 = new Label();
            buttonCompareForeignBgWords = new Button();
            buttonDuplicatedForeignWords = new Button();
            buttonCompareWordsWithEqualsLetters = new Button();
            buttonCompareNamesWithEqualsLetters = new Button();
            buttonInsertInTablesWords = new Button();
            buttonViewTableWords = new Button();
            toolStripButtonCompareBgWord = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enBgMatchesWordsBindingSource).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonInsertIntoDB, toolStripSeparator1, toolStripButtonCompareBgWord, toolStripSeparator2, toolStripButtonExportTable, toolStripSeparator3 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1294, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonInsertIntoDB
            // 
            toolStripButtonInsertIntoDB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonInsertIntoDB.Image = (Image)resources.GetObject("toolStripButtonInsertIntoDB.Image");
            toolStripButtonInsertIntoDB.ImageTransparentColor = Color.Magenta;
            toolStripButtonInsertIntoDB.Name = "toolStripButtonInsertIntoDB";
            toolStripButtonInsertIntoDB.Size = new Size(23, 22);
            toolStripButtonInsertIntoDB.Text = "InsertIntoDB";
            toolStripButtonInsertIntoDB.Click += toolStripButtonInsertIntoDB_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButtonExportTable
            // 
            toolStripButtonExportTable.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonExportTable.Image = (Image)resources.GetObject("toolStripButtonExportTable.Image");
            toolStripButtonExportTable.ImageTransparentColor = Color.Magenta;
            toolStripButtonExportTable.Name = "toolStripButtonExportTable";
            toolStripButtonExportTable.Size = new Size(23, 22);
            toolStripButtonExportTable.Text = "ExportTable";
            toolStripButtonExportTable.Click += toolStripButtonExportTable_Click;
            // 
            // enBgMatchesWordsBindingSource
            // 
            enBgMatchesWordsBindingSource.DataSource = typeof(EnBgMatchesWords);
            // 
            // checkedListBoxBg
            // 
            checkedListBoxBg.FormattingEnabled = true;
            checkedListBoxBg.Items.AddRange(new object[] { "А", "Б ", "В ", "Г ", "Д ", "Е ", "Ж ", "З ", "И ", "Й ", "К ", "Л ", "М ", "Н ", "О ", "П ", "Р ", "С ", "Т ", "У ", "Ф ", "Х ", "Ц ", "Ч ", "Ъ ", "Ю ", "Я" });
            checkedListBoxBg.Location = new Point(929, 109);
            checkedListBoxBg.Name = "checkedListBoxBg";
            checkedListBoxBg.Size = new Size(59, 526);
            checkedListBoxBg.TabIndex = 5;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(1027, 110);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(109, 26);
            buttonAdd.TabIndex = 7;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // richTextBoxSelectedLetters
            // 
            richTextBoxSelectedLetters.Location = new Point(1142, 110);
            richTextBoxSelectedLetters.Name = "richTextBoxSelectedLetters";
            richTextBoxSelectedLetters.Size = new Size(137, 524);
            richTextBoxSelectedLetters.TabIndex = 8;
            richTextBoxSelectedLetters.Text = "";
            // 
            // checkBoxSameLength
            // 
            checkBoxSameLength.AutoSize = true;
            checkBoxSameLength.Location = new Point(371, 138);
            checkBoxSameLength.Name = "checkBoxSameLength";
            checkBoxSameLength.Size = new Size(95, 19);
            checkBoxSameLength.TabIndex = 9;
            checkBoxSameLength.Text = "Same Length";
            checkBoxSameLength.UseVisualStyleBackColor = true;
            checkBoxSameLength.CheckedChanged += checkBoxSameLength_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(929, 93);
            label2.Name = "label2";
            label2.Size = new Size(21, 15);
            label2.TabIndex = 11;
            label2.Text = "Bg";
            // 
            // checkedListBoxLatSpecial
            // 
            checkedListBoxLatSpecial.FormattingEnabled = true;
            checkedListBoxLatSpecial.Items.AddRange(new object[] { "A", "B", "C ", "D ", "E", "F ", "G ", "H ", "I", "J ", "K ", "L ", "M ", "N", "O ", "P", "Q ", "R", "S ", "T ", "U ", "V ", "W ", "X", "Y ", "Z", "Ā", "Ă", "À ", "Á", "Â ", "Ã", "Ä", "Å", "Ē", "Ĕ", "È", "É ", "Ê", "Ë", "Ī", "Ĩ", "Ĭ", "Ì", "Í", "Î", "Ï", "Ō", "Ŏ", "Ő", "Ò", "Ó", "Ô", "Õ", "Ö", "Ū", "Ù", "Ú", "Û", "Ũ", "Ü", "Ý", "Ç" });
            checkedListBoxLatSpecial.Location = new Point(813, 110);
            checkedListBoxLatSpecial.Name = "checkedListBoxLatSpecial";
            checkedListBoxLatSpecial.Size = new Size(74, 526);
            checkedListBoxLatSpecial.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(813, 94);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 13;
            label3.Text = "Lat/Spec";
            // 
            // richTextBoxResult
            // 
            richTextBoxResult.Location = new Point(12, 108);
            richTextBoxResult.Name = "richTextBoxResult";
            richTextBoxResult.Size = new Size(353, 525);
            richTextBoxResult.TabIndex = 14;
            richTextBoxResult.Text = "";
            // 
            // comboBoxChoise
            // 
            comboBoxChoise.FormattingEnabled = true;
            comboBoxChoise.Items.AddRange(new object[] { "EnWord > BgWord", "EnWord < BgWord", "", "PtWord > BgWord", "PtWord < BgWord", "", "LatWord > BgWord", "LatWord < BgWord", "", "WorldName > BgName", "WorldName < BgName" });
            comboBoxChoise.Location = new Point(371, 109);
            comboBoxChoise.Name = "comboBoxChoise";
            comboBoxChoise.Size = new Size(199, 23);
            comboBoxChoise.TabIndex = 17;
            comboBoxChoise.SelectedIndexChanged += comboBoxChoise_SelectedIndexChanged;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(1027, 156);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(109, 26);
            buttonRemove.TabIndex = 19;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // progressBarLoad
            // 
            progressBarLoad.Location = new Point(12, 640);
            progressBarLoad.Name = "progressBarLoad";
            progressBarLoad.Size = new Size(353, 10);
            progressBarLoad.TabIndex = 21;
            // 
            // buttonClearResult
            // 
            buttonClearResult.Location = new Point(12, 50);
            buttonClearResult.Name = "buttonClearResult";
            buttonClearResult.Size = new Size(75, 28);
            buttonClearResult.TabIndex = 22;
            buttonClearResult.Text = "CLEAR";
            buttonClearResult.UseVisualStyleBackColor = true;
            buttonClearResult.Click += buttonClearResult_Click;
            // 
            // buttonClearSelectedLetters
            // 
            buttonClearSelectedLetters.Location = new Point(1196, 50);
            buttonClearSelectedLetters.Name = "buttonClearSelectedLetters";
            buttonClearSelectedLetters.Size = new Size(83, 28);
            buttonClearSelectedLetters.TabIndex = 23;
            buttonClearSelectedLetters.Text = "CLEAR";
            buttonClearSelectedLetters.UseVisualStyleBackColor = true;
            buttonClearSelectedLetters.Click += buttonClearSelectedLetters_Click;
            // 
            // labelResultItems
            // 
            labelResultItems.AutoSize = true;
            labelResultItems.Location = new Point(12, 653);
            labelResultItems.Name = "labelResultItems";
            labelResultItems.Size = new Size(39, 15);
            labelResultItems.TabIndex = 24;
            labelResultItems.Text = "Items:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1142, 93);
            label4.Name = "label4";
            label4.Size = new Size(137, 15);
            label4.TabIndex = 25;
            label4.Text = "Don't repeat spec. letters";
            // 
            // buttonCompareWordlBgNames
            // 
            buttonCompareWordlBgNames.Location = new Point(371, 356);
            buttonCompareWordlBgNames.Name = "buttonCompareWordlBgNames";
            buttonCompareWordlBgNames.Size = new Size(102, 58);
            buttonCompareWordlBgNames.TabIndex = 29;
            buttonCompareWordlBgNames.Text = "Compare World-Bg Names";
            buttonCompareWordlBgNames.UseVisualStyleBackColor = true;
            buttonCompareWordlBgNames.Click += buttonCompareWordlBgNames_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(371, 91);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 30;
            label5.Text = "Comparison";
            // 
            // textBoxViewId
            // 
            textBoxViewId.Location = new Point(371, 524);
            textBoxViewId.Name = "textBoxViewId";
            textBoxViewId.Size = new Size(134, 23);
            textBoxViewId.TabIndex = 37;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(371, 506);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 38;
            label7.Text = "Enter Word";
            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(12, 681);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(28, 15);
            labelInfo.TabIndex = 40;
            labelInfo.Text = "Info";
            // 
            // buttonViewID
            // 
            buttonViewID.Location = new Point(371, 553);
            buttonViewID.Name = "buttonViewID";
            buttonViewID.Size = new Size(134, 31);
            buttonViewID.TabIndex = 41;
            buttonViewID.Text = "View ID";
            buttonViewID.UseVisualStyleBackColor = true;
            buttonViewID.Click += buttonViewID_Click;
            // 
            // buttonViewDescription
            // 
            buttonViewDescription.Location = new Point(371, 591);
            buttonViewDescription.Name = "buttonViewDescription";
            buttonViewDescription.Size = new Size(134, 43);
            buttonViewDescription.TabIndex = 42;
            buttonViewDescription.Text = "View Descripton";
            buttonViewDescription.UseVisualStyleBackColor = true;
            buttonViewDescription.Click += buttonViewDescription_Click;
            // 
            // buttonDuplicatedWorldNames
            // 
            buttonDuplicatedWorldNames.Enabled = false;
            buttonDuplicatedWorldNames.Location = new Point(489, 356);
            buttonDuplicatedWorldNames.Name = "buttonDuplicatedWorldNames";
            buttonDuplicatedWorldNames.Size = new Size(81, 58);
            buttonDuplicatedWorldNames.TabIndex = 46;
            buttonDuplicatedWorldNames.Text = "Duplicated World Names";
            buttonDuplicatedWorldNames.UseVisualStyleBackColor = true;
            buttonDuplicatedWorldNames.Click += buttonDuplicatedWorldNames_Click;
            // 
            // buttonAddInTable
            // 
            buttonAddInTable.Location = new Point(1030, 326);
            buttonAddInTable.Name = "buttonAddInTable";
            buttonAddInTable.Size = new Size(109, 27);
            buttonAddInTable.TabIndex = 47;
            buttonAddInTable.Text = "Add In Table";
            buttonAddInTable.UseVisualStyleBackColor = true;
            buttonAddInTable.Click += buttonAddInTable_Click;
            // 
            // textBoxLetterRelationsName
            // 
            textBoxLetterRelationsName.Location = new Point(1030, 295);
            textBoxLetterRelationsName.Name = "textBoxLetterRelationsName";
            textBoxLetterRelationsName.Size = new Size(109, 23);
            textBoxLetterRelationsName.TabIndex = 48;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1031, 276);
            label6.Name = "label6";
            label6.Size = new Size(69, 15);
            label6.TabIndex = 49;
            label6.Text = "Enter Name";
            // 
            // buttonLoadLetterRelations
            // 
            buttonLoadLetterRelations.Location = new Point(1031, 555);
            buttonLoadLetterRelations.Name = "buttonLoadLetterRelations";
            buttonLoadLetterRelations.Size = new Size(108, 29);
            buttonLoadLetterRelations.TabIndex = 50;
            buttonLoadLetterRelations.Text = "Load FromTable";
            buttonLoadLetterRelations.UseVisualStyleBackColor = true;
            buttonLoadLetterRelations.Click += buttonLoadLetterRelations_Click;
            // 
            // comboBoxLetterRelationsName
            // 
            comboBoxLetterRelationsName.FormattingEnabled = true;
            comboBoxLetterRelationsName.Location = new Point(1031, 478);
            comboBoxLetterRelationsName.Name = "comboBoxLetterRelationsName";
            comboBoxLetterRelationsName.Size = new Size(106, 23);
            comboBoxLetterRelationsName.TabIndex = 51;
            // 
            // buttonViewTableResult
            // 
            buttonViewTableResult.Location = new Point(532, 524);
            buttonViewTableResult.Name = "buttonViewTableResult";
            buttonViewTableResult.Size = new Size(89, 50);
            buttonViewTableResult.TabIndex = 52;
            buttonViewTableResult.Text = "View Table Names";
            buttonViewTableResult.UseVisualStyleBackColor = true;
            buttonViewTableResult.Click += buttonViewTableResult_Click;
            // 
            // buttonInsertInTableResult
            // 
            buttonInsertInTableResult.Location = new Point(371, 433);
            buttonInsertInTableResult.Name = "buttonInsertInTableResult";
            buttonInsertInTableResult.Size = new Size(352, 38);
            buttonInsertInTableResult.TabIndex = 53;
            buttonInsertInTableResult.Text = "Insert In Table Names";
            buttonInsertInTableResult.UseVisualStyleBackColor = true;
            buttonInsertInTableResult.Click += buttonInsertInTableResult_Click;
            // 
            // buttonViewTableLetRelat
            // 
            buttonViewTableLetRelat.Location = new Point(1033, 515);
            buttonViewTableLetRelat.Name = "buttonViewTableLetRelat";
            buttonViewTableLetRelat.Size = new Size(103, 23);
            buttonViewTableLetRelat.TabIndex = 54;
            buttonViewTableLetRelat.Text = "View Table";
            buttonViewTableLetRelat.UseVisualStyleBackColor = true;
            buttonViewTableLetRelat.Click += buttonViewTableLetRelat_Click;
            // 
            // checkBoxDontRepeatLetters
            // 
            checkBoxDontRepeatLetters.AutoSize = true;
            checkBoxDontRepeatLetters.Location = new Point(597, 180);
            checkBoxDontRepeatLetters.Name = "checkBoxDontRepeatLetters";
            checkBoxDontRepeatLetters.Size = new Size(126, 19);
            checkBoxDontRepeatLetters.TabIndex = 59;
            checkBoxDontRepeatLetters.Text = "Don't repeat letters";
            checkBoxDontRepeatLetters.UseVisualStyleBackColor = true;
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Items.AddRange(new object[] { "PT-BG", "LAT-BG", "EN-BG" });
            comboBoxLanguage.Location = new Point(597, 109);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(126, 23);
            comboBoxLanguage.TabIndex = 60;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(597, 91);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 61;
            label1.Text = "Language";
            // 
            // buttonCompareForeignBgWords
            // 
            buttonCompareForeignBgWords.Location = new Point(371, 205);
            buttonCompareForeignBgWords.Name = "buttonCompareForeignBgWords";
            buttonCompareForeignBgWords.Size = new Size(104, 60);
            buttonCompareForeignBgWords.TabIndex = 63;
            buttonCompareForeignBgWords.Text = "Compare Foreign-Bg Words";
            buttonCompareForeignBgWords.UseVisualStyleBackColor = true;
            buttonCompareForeignBgWords.Click += buttonCompareForeignBgWords_Click;
            // 
            // buttonDuplicatedForeignWords
            // 
            buttonDuplicatedForeignWords.Enabled = false;
            buttonDuplicatedForeignWords.Location = new Point(489, 205);
            buttonDuplicatedForeignWords.Name = "buttonDuplicatedForeignWords";
            buttonDuplicatedForeignWords.Size = new Size(81, 60);
            buttonDuplicatedForeignWords.TabIndex = 64;
            buttonDuplicatedForeignWords.Text = "Duplicated Foreign Words";
            buttonDuplicatedForeignWords.UseVisualStyleBackColor = true;
            buttonDuplicatedForeignWords.Click += buttonDuplicatedForeignWords_Click;
            // 
            // buttonCompareWordsWithEqualsLetters
            // 
            buttonCompareWordsWithEqualsLetters.Location = new Point(597, 206);
            buttonCompareWordsWithEqualsLetters.Name = "buttonCompareWordsWithEqualsLetters";
            buttonCompareWordsWithEqualsLetters.Size = new Size(126, 59);
            buttonCompareWordsWithEqualsLetters.TabIndex = 65;
            buttonCompareWordsWithEqualsLetters.Text = "Compare Words With Equals Letters";
            buttonCompareWordsWithEqualsLetters.UseVisualStyleBackColor = true;
            buttonCompareWordsWithEqualsLetters.Click += buttonCompareWordsWithEqualsLetters_Click;
            // 
            // buttonCompareNamesWithEqualsLetters
            // 
            buttonCompareNamesWithEqualsLetters.Location = new Point(597, 356);
            buttonCompareNamesWithEqualsLetters.Name = "buttonCompareNamesWithEqualsLetters";
            buttonCompareNamesWithEqualsLetters.Size = new Size(126, 58);
            buttonCompareNamesWithEqualsLetters.TabIndex = 66;
            buttonCompareNamesWithEqualsLetters.Text = "Compare Names With Equals Letters";
            buttonCompareNamesWithEqualsLetters.UseVisualStyleBackColor = true;
            buttonCompareNamesWithEqualsLetters.Click += buttonCompareNamesWithEqualsLetters_Click;
            // 
            // buttonInsertInTablesWords
            // 
            buttonInsertInTablesWords.Location = new Point(371, 286);
            buttonInsertInTablesWords.Name = "buttonInsertInTablesWords";
            buttonInsertInTablesWords.Size = new Size(352, 38);
            buttonInsertInTablesWords.TabIndex = 67;
            buttonInsertInTablesWords.Text = "Insert In Table Words";
            buttonInsertInTablesWords.UseVisualStyleBackColor = true;
            buttonInsertInTablesWords.Click += buttonInsertInTablesWords_Click;
            // 
            // buttonViewTableWords
            // 
            buttonViewTableWords.Location = new Point(634, 524);
            buttonViewTableWords.Name = "buttonViewTableWords";
            buttonViewTableWords.Size = new Size(89, 50);
            buttonViewTableWords.TabIndex = 68;
            buttonViewTableWords.Text = "View Table Words";
            buttonViewTableWords.UseVisualStyleBackColor = true;
            buttonViewTableWords.Click += buttonViewTableWords_Click;
            // 
            // toolStripButtonCompareBgWord
            // 
            toolStripButtonCompareBgWord.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonCompareBgWord.Image = (Image)resources.GetObject("toolStripButtonCompareBgWord.Image");
            toolStripButtonCompareBgWord.ImageTransparentColor = Color.Magenta;
            toolStripButtonCompareBgWord.Name = "toolStripButtonCompareBgWord";
            toolStripButtonCompareBgWord.Size = new Size(23, 22);
            toolStripButtonCompareBgWord.Text = "CompareBgWord";
            toolStripButtonCompareBgWord.Click += toolStripButtonCompareBgWord_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 714);
            Controls.Add(buttonViewTableWords);
            Controls.Add(buttonInsertInTablesWords);
            Controls.Add(buttonCompareNamesWithEqualsLetters);
            Controls.Add(buttonCompareWordsWithEqualsLetters);
            Controls.Add(buttonDuplicatedForeignWords);
            Controls.Add(buttonCompareForeignBgWords);
            Controls.Add(label1);
            Controls.Add(comboBoxLanguage);
            Controls.Add(checkBoxDontRepeatLetters);
            Controls.Add(buttonViewTableLetRelat);
            Controls.Add(buttonInsertInTableResult);
            Controls.Add(buttonViewTableResult);
            Controls.Add(comboBoxLetterRelationsName);
            Controls.Add(buttonLoadLetterRelations);
            Controls.Add(label6);
            Controls.Add(textBoxLetterRelationsName);
            Controls.Add(buttonAddInTable);
            Controls.Add(buttonDuplicatedWorldNames);
            Controls.Add(buttonViewDescription);
            Controls.Add(buttonViewID);
            Controls.Add(labelInfo);
            Controls.Add(label7);
            Controls.Add(textBoxViewId);
            Controls.Add(label5);
            Controls.Add(buttonCompareWordlBgNames);
            Controls.Add(label4);
            Controls.Add(labelResultItems);
            Controls.Add(buttonClearSelectedLetters);
            Controls.Add(buttonClearResult);
            Controls.Add(progressBarLoad);
            Controls.Add(buttonRemove);
            Controls.Add(comboBoxChoise);
            Controls.Add(richTextBoxResult);
            Controls.Add(label3);
            Controls.Add(checkedListBoxLatSpecial);
            Controls.Add(label2);
            Controls.Add(checkBoxSameLength);
            Controls.Add(richTextBoxSelectedLetters);
            Controls.Add(buttonAdd);
            Controls.Add(checkedListBoxBg);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Name Compares";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)enBgMatchesWordsBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonInsertIntoDB;
        private BindingSource enBgMatchesWordsBindingSource;
        private CheckedListBox checkedListBoxBg;
        private Button buttonAdd;
        private RichTextBox richTextBoxSelectedLetters;
        private CheckBox checkBoxSameLength;
        private Label label2;
        private CheckedListBox checkedListBoxLatSpecial;
        private Label label3;
        private RichTextBox richTextBoxResult;
        private ComboBox comboBoxChoise;
        private Button buttonRemove;
        private ProgressBar progressBarLoad;
        private Button buttonClearResult;
        private Button buttonClearSelectedLetters;
        private Label labelResultItems;
        private Label label4;
        private Button buttonCompareWordlBgNames;
        private Label label5;
        private TextBox textBoxViewId;
        private Label label7;
        private Label labelInfo;
        private Button buttonViewID;
        private Button buttonViewDescription;
        private Button buttonDuplicatedWorldNames;
        private Button buttonAddInTable;
        private TextBox textBoxLetterRelationsName;
        private Label label6;
        private Button buttonLoadLetterRelations;
        private ComboBox comboBoxLetterRelationsName;
        private Button buttonViewTableResult;
        private Button buttonInsertInTableResult;
        private Button buttonViewTableLetRelat;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonExportTable;
        private CheckBox checkBoxDontRepeatLetters;
        private ComboBox comboBoxLanguage;
        private Label label1;
        private Button buttonCompareForeignBgWords;
        private Button buttonDuplicatedForeignWords;
        private Button buttonCompareWordsWithEqualsLetters;
        private Button buttonCompareNamesWithEqualsLetters;
        private Button buttonInsertInTablesWords;
        private Button buttonViewTableWords;
        private ToolStripButton toolStripButtonCompareBgWord;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
    }
}
