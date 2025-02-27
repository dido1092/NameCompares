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
    public partial class FrmExport : Form
    {
        public FrmExport()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            string table = comboBoxTables.Text;

            Refresh(table);
        }
        public void Refresh(string table)
        {
            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;

            try
            {
                TableEvents(DbConfig.ConnectionString, table);

                CountRows(DbConfig.ConnectionString, table);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TableEvents(string connectionString, string table)
        {
            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM {table}", connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds, table);
            dataGridViewTable.DataSource = ds.Tables[table]?.DefaultView;
        }
        private void CountRows(string connectionString, string table)
        {
            // Create the connection.
            SqlConnection conn = new SqlConnection(DbConfig.ConnectionString);

            // Build the query to count, including CustomerID criteria if specified.
            string selectText = $"SELECT COUNT(*) FROM {table}";

            // Create the command to count the records.
            SqlCommand cmd = new SqlCommand(selectText, conn);

            // Execute the command, storing the results.
            conn.Open();
            int recordCount = (int)cmd.ExecuteScalar();
            conn.Close();
            //labelEventNums.Text = $"Събития бр: {recordCount}";
            labelRows.Text = $"{table}: {recordCount}";
        }

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            string query = textBoxQuery.Text;

            string table = comboBoxTables.Text;

            await Execute(query, table);
        }
        private async Task Execute(string query, string tableName)
        {

            if (query != "")
            {
                SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
                await cnn.OpenAsync();

                SqlDataAdapter da = new SqlDataAdapter($"{query}", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, tableName);
                dataGridViewTable.DataSource = ds.Tables[tableName].DefaultView;

                labelRows.Text = $"Rows: {dataGridViewTable.RowCount}";
            }
        }

        private async void buttonSelectCell_Click(object sender, EventArgs e)
        {
            string table = comboBoxTables.Text;

            await SelectCell(table);
        }

        private async Task SelectCell(string table)
        {
            int id = 0;

            id = Convert.ToInt32(dataGridViewTable.Rows[dataGridViewTable.CurrentRow.Index].Cells[0].Value);

            string query = $"SELECT * FROM {table} WHERE Id = " + id;

            await Execute(query, table);

            textBoxQuery.Text = $"SELECT * FROM {table} WHERE Id = {id}";
        }
    }
}
