using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NameCompares
{
    public partial class FrmLettRelations : Form
    {
        public FrmLettRelations()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        private async void Refresh()
        {
            //var getIds = context.ResultTables!.Select(t => t.Id).OrderBy(t => t).ToList();
            //int lastId = 0;

            //foreach (int Id in getIds)
            //{
            //    lastId = Id;
            //}

            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            await cnn.OpenAsync();

            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM LettRelations", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "LettRelations");

            dataGridViewLetRelat.DataSource = ds.Tables["LettRelations"]!.DefaultView;

            //labelRows.Text = $"Rows: {lastId}";
            labelRows.Text = $"Rows: {dataGridViewLetRelat.RowCount - 1}";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This Will Remove All Records From Table 'Result'!", "Are You Sure!", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                DataGridViewCell cell = dataGridViewLetRelat.SelectedCells[0] as DataGridViewCell;
                string value = cell.Value.ToString()!;
                int getID = int.Parse(value);

                //Remove row from DataGridView
                int rowIndex = dataGridViewLetRelat.CurrentCell.RowIndex;
                this.dataGridViewLetRelat.Rows.RemoveAt(rowIndex);

                //String Connection
                string connetionString = null;
                connetionString = DbConfig.ConnectionString;
                SqlConnection cnn = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;

                //Delete from DB
                cmd.CommandText = ($"Delete From LettRelations Where Id=" + getID + "");

                try
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The row has been deleted ! ");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot open connection ! ");
                }

            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = null;
            connectionString = DbConfig.ConnectionString;

            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;

            int rowindex = dataGridViewLetRelat.CurrentRow.Index;
            int colindex = dataGridViewLetRelat.CurrentCell.ColumnIndex;

            string? columnName = dataGridViewLetRelat.Columns[colindex].HeaderText;

            string? getValue = dataGridViewLetRelat.CurrentCell.Value.ToString();
            string? id = dataGridViewLetRelat.Rows[rowindex].Cells[0].Value.ToString();

            //MessageBox.Show(columnName.ToString());

            try
            {
                using (cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    string sqlCommand = $"Update LettRelations set {columnName}=@{columnName} Where Id={id}";
                    cmd = new SqlCommand(sqlCommand, cnn);
                    cmd.Parameters.AddWithValue($"@{columnName}", getValue);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Information Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
