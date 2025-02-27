namespace NameCompares
{
    partial class FrmExport
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
            comboBoxTables = new ComboBox();
            label1 = new Label();
            dataGridViewTable = new DataGridView();
            buttonRefresh = new Button();
            labelRows = new Label();
            textBoxQuery = new TextBox();
            label2 = new Label();
            buttonExecute = new Button();
            buttonSelectCell = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTable).BeginInit();
            SuspendLayout();
            // 
            // comboBoxTables
            // 
            comboBoxTables.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Items.AddRange(new object[] { "BgWords", "EnWords", "PtWords", "LatWords", "BgNames", "EnNames", "PtNames", "WorldNames" });
            comboBoxTables.Location = new Point(40, 49);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(121, 23);
            comboBoxTables.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 32);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "Tables";
            // 
            // dataGridViewTable
            // 
            dataGridViewTable.AllowUserToAddRows = false;
            dataGridViewTable.AllowUserToDeleteRows = false;
            dataGridViewTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTable.Location = new Point(40, 122);
            dataGridViewTable.Name = "dataGridViewTable";
            dataGridViewTable.ReadOnly = true;
            dataGridViewTable.RowTemplate.Height = 25;
            dataGridViewTable.Size = new Size(895, 496);
            dataGridViewTable.TabIndex = 2;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(40, 78);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(121, 27);
            buttonRefresh.TabIndex = 3;
            buttonRefresh.Text = "Load / Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // labelRows
            // 
            labelRows.AutoSize = true;
            labelRows.Location = new Point(40, 629);
            labelRows.Name = "labelRows";
            labelRows.Size = new Size(38, 15);
            labelRows.TabIndex = 4;
            labelRows.Text = "Rows:";
            // 
            // textBoxQuery
            // 
            textBoxQuery.Location = new Point(211, 49);
            textBoxQuery.Name = "textBoxQuery";
            textBoxQuery.Size = new Size(643, 23);
            textBoxQuery.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(211, 32);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 6;
            label2.Text = "Query";
            // 
            // buttonExecute
            // 
            buttonExecute.Location = new Point(860, 49);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(75, 23);
            buttonExecute.TabIndex = 7;
            buttonExecute.Text = "Execute";
            buttonExecute.UseVisualStyleBackColor = true;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // buttonSelectCell
            // 
            buttonSelectCell.Location = new Point(211, 78);
            buttonSelectCell.Name = "buttonSelectCell";
            buttonSelectCell.Size = new Size(86, 23);
            buttonSelectCell.TabIndex = 8;
            buttonSelectCell.Text = "Select Cell";
            buttonSelectCell.UseVisualStyleBackColor = true;
            buttonSelectCell.Click += buttonSelectCell_Click;
            // 
            // FrmExport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 681);
            Controls.Add(buttonSelectCell);
            Controls.Add(buttonExecute);
            Controls.Add(label2);
            Controls.Add(textBoxQuery);
            Controls.Add(labelRows);
            Controls.Add(buttonRefresh);
            Controls.Add(dataGridViewTable);
            Controls.Add(label1);
            Controls.Add(comboBoxTables);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmExport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmExport";
            ((System.ComponentModel.ISupportInitialize)dataGridViewTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxTables;
        private Label label1;
        private DataGridView dataGridViewTable;
        private Button buttonRefresh;
        private Label labelRows;
        private TextBox textBoxQuery;
        private Label label2;
        private Button buttonExecute;
        private Button buttonSelectCell;
    }
}