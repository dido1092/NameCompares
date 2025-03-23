using Microsoft.Office.Interop.Excel;
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
    public partial class FrmCompareBgWords_Names : Form
    {
        NameComparesContext context = new NameComparesContext();

        private RichTextBox richTBox;
        private System.Windows.Forms.Label labelItems;

        public FrmCompareBgWords_Names(RichTextBox richTBox, System.Windows.Forms.Label label)
        {
            this.richTBox = richTBox;
            this.labelItems = label;

            InitializeComponent();
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            string word = textBoxWord.Text.ToUpper();

            HashSet<string> hsWords = new HashSet<string>();

            progressBarCompare.Minimum = 0;
            progressBarCompare.Maximum = 0;

            if (comboBoxTables.Text == "BgWords")
            {
                var bgWords = context.BgWords!.Select(bgW => new { bgW.BgWord, bgW.Length }).ToHashSet();

                progressBarCompare.Maximum = bgWords.Count;

                if (bgWords != null)
                {
                    foreach (var bgW in bgWords)
                    {
                        if (checkBoxRepeatLetters.Checked)
                        {
                            int count = 0;

                            for (int i = 0; i < bgW.BgWord.Length; i++)
                            {
                                if (word.Contains(bgW.BgWord[i]))
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (count == bgW.BgWord.Length)
                            {
                                hsWords.Add(bgW.BgWord);
                            }
                        }
                        if (checkBoxSameLength.Checked)
                        {
                            if (bgW.BgWord.Distinct().OrderBy(c => c).SequenceEqual(word.Distinct().OrderBy(c => c)))
                            {
                                hsWords.Add(bgW.BgWord);
                            }
                        }
                        if (checkBoxRepeatLetters.Checked == false && checkBoxSameLength.Checked == false)
                        {
                            if (bgW.BgWord.Distinct().Count(c => word.Distinct().Contains(c)) == word.Length
                                || word.Distinct().Count(c => bgW.BgWord.Distinct().Contains(c)) == bgW.BgWord.Length)
                            {
                                hsWords.Add(bgW.BgWord);
                            }

                        }

                        progressBarCompare.Value++;
                    }
                }
            }
            else if (comboBoxTables.Text == "BgNames")
            {
                var bgNames = context.BgNames!.Select(bgN => new { bgN.BgName, bgN.Length }).ToHashSet();

                progressBarCompare.Maximum = bgNames.Count;

                if (bgNames != null)
                {
                    foreach (var bgN in bgNames)
                    {
                        if (checkBoxRepeatLetters.Checked)
                        {
                            int count = 0;

                            for (int i = 0; i < bgN.BgName.Length; i++)
                            {
                                if (word.Contains(bgN.BgName[i]))
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (count == bgN.BgName.Length)
                            {
                                hsWords.Add(bgN.BgName);
                            }
                        }
                        if (checkBoxSameLength.Checked)
                        {
                            if (bgN.BgName.Distinct().OrderBy(c => c).SequenceEqual(word.Distinct().OrderBy(c => c)))
                            {
                                hsWords.Add(bgN.BgName);
                            }
                        }
                        if (checkBoxRepeatLetters.Checked == false && checkBoxSameLength.Checked == false)
                        {
                            if (bgN.BgName.Distinct().Count(c => word.Distinct().Contains(c)) == word.Length
                                || word.Distinct().Count(c => bgN.BgName.Distinct().Contains(c)) == bgN.BgName.Length)
                            {
                                hsWords.Add(bgN.BgName);
                            }
                        }

                        progressBarCompare.Value++;
                    }
                }
            }
            StringBuilder sbResult = new StringBuilder();

            foreach (var bgWN in hsWords.OrderBy(w => w))
            {
                sbResult.AppendLine(bgWN.ToString());
            }

            richTBox.Text = sbResult.ToString();
            labelItems.Text = $"Items: {hsWords.Count()}";
        }
    }
}
