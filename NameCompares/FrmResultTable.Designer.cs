namespace NameCompares
{
    partial class FrmResultTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResultTable));
            dataGridViewTempTable = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            Comparison = new DataGridViewTextBoxColumn();
            LettRelationsId = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            resultTableBindingSource = new BindingSource(components);
            temporaryTableBindingSource = new BindingSource(components);
            textBoxQuery = new TextBox();
            buttonExecute = new Button();
            buttonRefresh = new Button();
            buttonSelectCell = new Button();
            labelRows = new Label();
            buttonTruncate = new Button();
            toolStrip1 = new ToolStrip();
            toolStripButtonExport = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTempTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)resultTableBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)temporaryTableBindingSource).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewTempTable
            // 
            dataGridViewTempTable.AutoGenerateColumns = false;
            dataGridViewTempTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTempTable.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, Comparison, LettRelationsId, dataGridViewTextBoxColumn6 });
            dataGridViewTempTable.DataSource = resultTableBindingSource;
            dataGridViewTempTable.Location = new Point(12, 101);
            dataGridViewTempTable.Name = "dataGridViewTempTable";
            dataGridViewTempTable.RowTemplate.Height = 25;
            dataGridViewTempTable.Size = new Size(1230, 596);
            dataGridViewTempTable.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            dataGridViewTextBoxColumn1.HeaderText = "Id";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn2.DataPropertyName = "LatWord";
            dataGridViewTextBoxColumn2.HeaderText = "LatWord";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn3.DataPropertyName = "BgWord";
            dataGridViewTextBoxColumn3.HeaderText = "BgWord";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn4.DataPropertyName = "LatWordLength";
            dataGridViewTextBoxColumn4.HeaderText = "LatWordLength";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn5.DataPropertyName = "BgWordLength";
            dataGridViewTextBoxColumn5.HeaderText = "BgWordLength";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // Comparison
            // 
            Comparison.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Comparison.DataPropertyName = "Comparison";
            Comparison.HeaderText = "Comparison";
            Comparison.Name = "Comparison";
            // 
            // LettRelationsId
            // 
            LettRelationsId.DataPropertyName = "LettRelationsId";
            LettRelationsId.HeaderText = "LettRelationsId";
            LettRelationsId.Name = "LettRelationsId";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn6.DataPropertyName = "DateTime";
            dataGridViewTextBoxColumn6.HeaderText = "DateTime";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // resultTableBindingSource
            // 
            resultTableBindingSource.DataSource = typeof(ResultTable);
            // 
            // textBoxQuery
            // 
            textBoxQuery.Location = new Point(12, 28);
            textBoxQuery.Name = "textBoxQuery";
            textBoxQuery.Size = new Size(787, 23);
            textBoxQuery.TabIndex = 1;
            // 
            // buttonExecute
            // 
            buttonExecute.Location = new Point(805, 28);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(93, 27);
            buttonExecute.TabIndex = 2;
            buttonExecute.Text = "Execute";
            buttonExecute.UseVisualStyleBackColor = true;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(1140, 23);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(102, 37);
            buttonRefresh.TabIndex = 3;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonSelectCell
            // 
            buttonSelectCell.Location = new Point(12, 72);
            buttonSelectCell.Name = "buttonSelectCell";
            buttonSelectCell.Size = new Size(75, 23);
            buttonSelectCell.TabIndex = 4;
            buttonSelectCell.Text = "Select Line";
            buttonSelectCell.UseVisualStyleBackColor = true;
            buttonSelectCell.Click += buttonSelectCell_Click;
            // 
            // labelRows
            // 
            labelRows.AutoSize = true;
            labelRows.Location = new Point(12, 709);
            labelRows.Name = "labelRows";
            labelRows.Size = new Size(38, 15);
            labelRows.TabIndex = 5;
            labelRows.Text = "Rows:";
            // 
            // buttonTruncate
            // 
            buttonTruncate.BackColor = Color.IndianRed;
            buttonTruncate.Location = new Point(995, 23);
            buttonTruncate.Name = "buttonTruncate";
            buttonTruncate.Size = new Size(116, 37);
            buttonTruncate.TabIndex = 6;
            buttonTruncate.Text = "TRUNCATE TABLE";
            buttonTruncate.UseVisualStyleBackColor = false;
            buttonTruncate.Click += buttonTruncate_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonExport });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1254, 25);
            toolStrip1.TabIndex = 7;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonExport
            // 
            toolStripButtonExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonExport.Image = (Image)resources.GetObject("toolStripButtonExport.Image");
            toolStripButtonExport.ImageTransparentColor = Color.Magenta;
            toolStripButtonExport.Name = "toolStripButtonExport";
            toolStripButtonExport.Size = new Size(23, 22);
            toolStripButtonExport.Text = "Export";
            toolStripButtonExport.Click += toolStripButtonExport_Click;
            // 
            // FrmResultTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 742);
            Controls.Add(toolStrip1);
            Controls.Add(buttonTruncate);
            Controls.Add(labelRows);
            Controls.Add(buttonSelectCell);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonExecute);
            Controls.Add(textBoxQuery);
            Controls.Add(dataGridViewTempTable);
            Name = "FrmResultTable";
            Text = "Result Table";
            ((System.ComponentModel.ISupportInitialize)dataGridViewTempTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)resultTableBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)temporaryTableBindingSource).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewTempTable;
        private TextBox textBoxQuery;
        private Button buttonExecute;
        private Button buttonRefresh;
        private Button buttonSelectCell;
        private Label labelRows;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn latWordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bgWordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn latWordLengthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bgWordLengthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
        private BindingSource temporaryTableBindingSource;
        private Button buttonTruncate;
        private BindingSource resultTableBindingSource;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn Comparison;
        private DataGridViewTextBoxColumn LettRelationsId;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonExport;
    }
}