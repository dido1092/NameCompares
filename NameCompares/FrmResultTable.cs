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
using Syncfusion;
using Syncfusion.XlsIO;

namespace NameCompares
{
    public partial class FrmResultTable : Form
    {
        NameComparesContext context = new NameComparesContext();

        //private HashSet<string> hsWords = new HashSet<string>();

        //private string newDirectoryPath = string.Empty;

        public FrmResultTable()
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

            SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM ResultTables", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ResultTables");

            dataGridViewTempTable.DataSource = ds.Tables["ResultTables"]!.DefaultView;

            labelRows.Text = $"Rows: {dataGridViewTempTable.RowCount - 1}";
        }

        private async void buttonSelectCell_Click(object sender, EventArgs e)
        {
            var dateTime = dataGridViewTempTable.Rows[dataGridViewTempTable.CurrentRow.Index].Cells[7].Value;

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

            string query = @$"SELECT * FROM ResultTables WHERE DateTime = '{newDateTime}'";

            await Execute(query);

            textBoxQuery.Text = $"SELECT * FROM ResultTables WHERE DateTime = '{newDateTime}'";
        }
        private async Task Execute(string query)
        {
            if (query != "")
            {
                SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
                await cnn.OpenAsync();

                SqlDataAdapter da = new SqlDataAdapter($"{query}", cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ResultTables");

                dataGridViewTempTable.DataSource = ds.Tables["ResultTables"]!.DefaultView;

                labelRows.Text = $"Rows: {dataGridViewTempTable.RowCount - 1}";
            }
        }

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            string query = textBoxQuery.Text;

            await Execute(query);
        }

        private async void buttonTruncate_Click(object sender, EventArgs e) //Delete Button
        {
            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            await cnn.OpenAsync();

            SqlDataAdapter da = new SqlDataAdapter($"TRUNCATE TABLE ResultTables", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ResultTables");

            Refresh();
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog savefile = new SaveFileDialog();
            //savefile.InitialDirectory = ("E:");
            //savefile.Title = "Save as Excel File";
            //savefile.FileName = "";
            //savefile.Filter = "Excel Files(2003)|*.xls| Excel Files(2007)|*.xlsx";

            //string newDirectoryPath = string.Empty;

            Save();

            MessageBox.Show("Done!");
        }
        private void Save()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.RestoreDirectory = true;
            //savefile.InitialDirectory = "e:\\faktur";
            savefile.FileName = String.Format("{0}.txt", Text);
            savefile.DefaultExt = "*.txt*";
            savefile.Filter = "TEXT Files|*.txt";


            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(savefile.FileName)) { }

                string newDirectoryPath = savefile.FileName;

                dataGridViewTempTable.SelectAll();

                Clipboard.SetDataObject(dataGridViewTempTable.GetClipboardContent());

                File.WriteAllText(newDirectoryPath, Clipboard.GetText());

                ////File.WriteAllText(newDirectoryPath, string.Join("\n", hsWords));
            }
            //return newDirectoryPath;
        }
    }
}
