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
    public partial class FrmWordTable : Form
    {
        public FrmWordTable()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        private async void Refresh()
        {
            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            await cnn.OpenAsync();

            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM WordTables", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "WordTables");

            dataGridViewWordTable.DataSource = ds.Tables["WordTables"]!.DefaultView;

            labelRows.Text = $"Rows: {dataGridViewWordTable.RowCount - 1}";
        }

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            string query = textBoxQuery.Text;

            await Execute(query);

        }
        private async Task Execute(string query)
        {
            if (query != "")
            {
                SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
                await cnn.OpenAsync();

                SqlDataAdapter da = new SqlDataAdapter($"{query}", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "WordTables");

                dataGridViewWordTable.DataSource = ds.Tables["WordTables"]!.DefaultView;

                labelRows.Text = $"Rows: {dataGridViewWordTable.RowCount - 1}";
            }
        }

        private async void buttonSelectedCell_Click(object sender, EventArgs e)
        {
            var dateTime = dataGridViewWordTable.Rows[dataGridViewWordTable.CurrentRow.Index].Cells[7].Value;

            string newDateTime = string.Empty;
            //'2025-01-14 21:03:00.0000000'
            string getDateTime = dateTime.ToString()!;

            string[] arrDateTime = getDateTime.Split('г');

            string getDate = arrDateTime[0];
            string getTime = arrDateTime[1].Replace(".", "");

            string[] arrDate = getDate.Split('.', StringSplitOptions.TrimEntries);

            for (int i = arrDate.Length - 1; i >= 0; i--)
            {
                newDateTime += arrDate[i];

                if (i != 0)
                {
                    newDateTime += '-';
                }
            }

            newDateTime += " ";
            newDateTime += getTime + ".0000000";

            string query = @$"SELECT * FROM WordTables WHERE DateTime = '{newDateTime}'";

            await Execute(query);

            textBoxQuery.Text = $"SELECT * FROM WordTables WHERE DateTime = '{newDateTime}'";
        }

        private async void buttonTruncate_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            await cnn.OpenAsync();

            SqlDataAdapter da = new SqlDataAdapter($"TRUNCATE TABLE WordTables", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "WordTables");

            Refresh();
        }
    }
}
