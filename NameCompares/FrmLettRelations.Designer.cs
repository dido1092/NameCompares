namespace NameCompares
{
    partial class FrmLettRelations
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
            dataGridViewLetRelat = new DataGridView();
            lettRelationsBindingSource1 = new BindingSource(components);
            lettRelationsBindingSource = new BindingSource(components);
            letterRelationsBindingSource = new BindingSource(components);
            labelRows = new Label();
            buttonRefresh = new Button();
            buttonDelete = new Button();
            buttonUpdate = new Button();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lettersDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dateTimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLetRelat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lettRelationsBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lettRelationsBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)letterRelationsBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewLetRelat
            // 
            dataGridViewLetRelat.AutoGenerateColumns = false;
            dataGridViewLetRelat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLetRelat.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, lettersDataGridViewTextBoxColumn, dateTimeDataGridViewTextBoxColumn });
            dataGridViewLetRelat.DataSource = lettRelationsBindingSource1;
            dataGridViewLetRelat.Location = new Point(12, 79);
            dataGridViewLetRelat.Name = "dataGridViewLetRelat";
            dataGridViewLetRelat.RowTemplate.Height = 25;
            dataGridViewLetRelat.Size = new Size(1294, 538);
            dataGridViewLetRelat.TabIndex = 0;
            // 
            // lettRelationsBindingSource1
            // 
            lettRelationsBindingSource1.DataSource = typeof(LettRelations);
            // 
            // lettRelationsBindingSource
            // 
            lettRelationsBindingSource.DataSource = typeof(LettRelations);
            // 
            // letterRelationsBindingSource
            // 
            letterRelationsBindingSource.DataSource = typeof(LettRelations);
            // 
            // labelRows
            // 
            labelRows.AutoSize = true;
            labelRows.Location = new Point(13, 686);
            labelRows.Name = "labelRows";
            labelRows.Size = new Size(35, 15);
            labelRows.TabIndex = 1;
            labelRows.Text = "Rows";
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(1211, 12);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(95, 34);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(1211, 623);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(95, 34);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(1034, 623);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(95, 34);
            buttonUpdate.TabIndex = 4;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.Width = 384;
            // 
            // lettersDataGridViewTextBoxColumn
            // 
            lettersDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lettersDataGridViewTextBoxColumn.DataPropertyName = "Letters";
            lettersDataGridViewTextBoxColumn.HeaderText = "Letters";
            lettersDataGridViewTextBoxColumn.Name = "lettersDataGridViewTextBoxColumn";
            // 
            // dateTimeDataGridViewTextBoxColumn
            // 
            dateTimeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
            dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
            dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
            // 
            // FrmLettRelations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1318, 716);
            Controls.Add(buttonUpdate);
            Controls.Add(buttonDelete);
            Controls.Add(buttonRefresh);
            Controls.Add(labelRows);
            Controls.Add(dataGridViewLetRelat);
            Name = "FrmLettRelations";
            Text = "FrmLettRelations";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLetRelat).EndInit();
            ((System.ComponentModel.ISupportInitialize)lettRelationsBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)lettRelationsBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)letterRelationsBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewLetRelat;
        private BindingSource letterRelationsBindingSource;
        private Label labelRows;
        private Button buttonRefresh;
        private Button buttonDelete;
        private Button buttonUpdate;
        private BindingSource lettRelationsBindingSource;
        private BindingSource lettRelationsBindingSource1;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lettersDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
    }
}