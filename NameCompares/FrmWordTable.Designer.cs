namespace NameCompares
{
    partial class FrmWordTable
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
            components = new System.ComponentModel.Container();
            dataGridViewWordTable = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            fWordDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bgWordDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            fWordLengthDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bgWordLengthDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            comparisonDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lettRelationsIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dateTimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            wordTableBindingSource = new BindingSource(components);
            buttonRefresh = new Button();
            labelRows = new Label();
            textBoxQuery = new TextBox();
            buttonExecute = new Button();
            buttonSelectedCell = new Button();
            buttonTruncate = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewWordTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wordTableBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewWordTable
            // 
            dataGridViewWordTable.AutoGenerateColumns = false;
            dataGridViewWordTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewWordTable.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, fWordDataGridViewTextBoxColumn, bgWordDataGridViewTextBoxColumn, fWordLengthDataGridViewTextBoxColumn, bgWordLengthDataGridViewTextBoxColumn, comparisonDataGridViewTextBoxColumn, lettRelationsIdDataGridViewTextBoxColumn, dateTimeDataGridViewTextBoxColumn });
            dataGridViewWordTable.DataSource = wordTableBindingSource;
            dataGridViewWordTable.Location = new Point(21, 101);
            dataGridViewWordTable.Name = "dataGridViewWordTable";
            dataGridViewWordTable.RowTemplate.Height = 25;
            dataGridViewWordTable.Size = new Size(1159, 539);
            dataGridViewWordTable.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // fWordDataGridViewTextBoxColumn
            // 
            fWordDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fWordDataGridViewTextBoxColumn.DataPropertyName = "FWord";
            fWordDataGridViewTextBoxColumn.HeaderText = "FWord";
            fWordDataGridViewTextBoxColumn.Name = "fWordDataGridViewTextBoxColumn";
            // 
            // bgWordDataGridViewTextBoxColumn
            // 
            bgWordDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            bgWordDataGridViewTextBoxColumn.DataPropertyName = "BgWord";
            bgWordDataGridViewTextBoxColumn.HeaderText = "BgWord";
            bgWordDataGridViewTextBoxColumn.Name = "bgWordDataGridViewTextBoxColumn";
            // 
            // fWordLengthDataGridViewTextBoxColumn
            // 
            fWordLengthDataGridViewTextBoxColumn.DataPropertyName = "FWordLength";
            fWordLengthDataGridViewTextBoxColumn.HeaderText = "FWordLength";
            fWordLengthDataGridViewTextBoxColumn.Name = "fWordLengthDataGridViewTextBoxColumn";
            // 
            // bgWordLengthDataGridViewTextBoxColumn
            // 
            bgWordLengthDataGridViewTextBoxColumn.DataPropertyName = "BgWordLength";
            bgWordLengthDataGridViewTextBoxColumn.HeaderText = "BgWordLength";
            bgWordLengthDataGridViewTextBoxColumn.Name = "bgWordLengthDataGridViewTextBoxColumn";
            // 
            // comparisonDataGridViewTextBoxColumn
            // 
            comparisonDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            comparisonDataGridViewTextBoxColumn.DataPropertyName = "Comparison";
            comparisonDataGridViewTextBoxColumn.HeaderText = "Comparison";
            comparisonDataGridViewTextBoxColumn.Name = "comparisonDataGridViewTextBoxColumn";
            // 
            // lettRelationsIdDataGridViewTextBoxColumn
            // 
            lettRelationsIdDataGridViewTextBoxColumn.DataPropertyName = "LettRelationsId";
            lettRelationsIdDataGridViewTextBoxColumn.HeaderText = "LettRelationsId";
            lettRelationsIdDataGridViewTextBoxColumn.Name = "lettRelationsIdDataGridViewTextBoxColumn";
            lettRelationsIdDataGridViewTextBoxColumn.Width = 163;
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            dateTimeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
            dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            // 
            // wordTableBindingSource
            // 
            wordTableBindingSource.DataSource = typeof(WordTable);
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(1079, 23);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(101, 37);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // labelRows
            // 
            labelRows.AutoSize = true;
            labelRows.Location = new Point(23, 646);
            labelRows.Name = "labelRows";
            labelRows.Size = new Size(38, 15);
            labelRows.TabIndex = 2;
            labelRows.Text = "Rows:";
            // 
            // textBoxQuery
            // 
            textBoxQuery.Location = new Point(23, 23);
            textBoxQuery.Name = "textBoxQuery";
            textBoxQuery.Size = new Size(741, 23);
            textBoxQuery.TabIndex = 3;
            // 
            // buttonExecute
            // 
            buttonExecute.Location = new Point(770, 23);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(96, 23);
            buttonExecute.TabIndex = 4;
            buttonExecute.Text = "Execute";
            buttonExecute.UseVisualStyleBackColor = true;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // buttonSelectedCell
            // 
            buttonSelectedCell.Location = new Point(21, 72);
            buttonSelectedCell.Name = "buttonSelectedCell";
            buttonSelectedCell.Size = new Size(86, 23);
            buttonSelectedCell.TabIndex = 5;
            buttonSelectedCell.Text = "Selected Cell";
            buttonSelectedCell.UseVisualStyleBackColor = true;
            buttonSelectedCell.Click += buttonSelectedCell_Click;
            // 
            // buttonTruncate
            // 
            buttonTruncate.BackColor = Color.IndianRed;
            buttonTruncate.Location = new Point(934, 23);
            buttonTruncate.Name = "buttonTruncate";
            buttonTruncate.Size = new Size(95, 37);
            buttonTruncate.TabIndex = 6;
            buttonTruncate.Text = "TRUNCATE";
            buttonTruncate.UseVisualStyleBackColor = false;
            buttonTruncate.Click += buttonTruncate_Click;
            // 
            // FrmWordTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1208, 700);
            Controls.Add(buttonTruncate);
            Controls.Add(buttonSelectedCell);
            Controls.Add(buttonExecute);
            Controls.Add(textBoxQuery);
            Controls.Add(labelRows);
            Controls.Add(buttonRefresh);
            Controls.Add(dataGridViewWordTable);
            Name = "FrmWordTable";
            Text = "FrmWordTable";
            ((System.ComponentModel.ISupportInitialize)dataGridViewWordTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)wordTableBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewWordTable;
        private BindingSource wordTableBindingSource;
        private Button buttonRefresh;
        private Label labelRows;
        private TextBox textBoxQuery;
        private Button buttonExecute;
        private Button buttonSelectedCell;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fWordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bgWordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fWordLengthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bgWordLengthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn comparisonDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lettRelationsIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private Button buttonTruncate;
    }
}