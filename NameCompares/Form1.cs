using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NameCompares
{
    public partial class Form1 : Form
    {
        NameComparesContext context = new NameComparesContext();

        private HashSet<SpecialWordToBgWord> hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();
        private HashSet<WorldBgNames> hsResultWordNameToBgName = new HashSet<WorldBgNames>();

        private List<string> lsDuplicateLatWord = new List<string>();
        private List<string> lsAllLatBgWords = new List<string>();

        private string[] enLetters = new string[30];
        private string[] ptLetters = new string[30];
        private string[] bgLetters = new string[30];

        private string buttonName = string.Empty;

        private bool sameLengthIsChecked = false;

        private int id = 0;
        private int row = -1;
        private int col = 0;
        private int countAddLetters = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButtonInsertIntoDB_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert();
            insert.Show();
        }

        private void buttonComapareEnBgWords_Click(object sender, EventArgs e)
        {
            buttonName = "CompareEnBgWords";

            string convertToBg = string.Empty;

            var getEnWords = context.EnNames!.Select(w => w.EnName).ToHashSet();

            foreach (var enWord in getEnWords)
            {
                var getBgWords = context.BgWords?.Select(w => new { w.BgWord, w.Length }).Where(w => w.Length == enWord.Length).ToHashSet();

                foreach (var bgWord in getBgWords!)
                {
                    int countMatchLetters = 0;

                    for (int i = 0; i < enWord.Length; i++)
                    {
                        for (int j = 0; j < bgWord.Length; j++)
                        {

                            string enLetter = enWord[i].ToString();
                            string bgLetter = bgWord.BgWord[j].ToString();

                            for (int m = 0; m < ptLetters.Length; m++)
                            {

                            }

                        }
                    }
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedLetters = string.Empty;

            countAddLetters++;

            row++;

            if (row <= 26)
            {
                var enLetter = checkedListBoxEn.CheckedItems;

                var latSpecialLetter = checkedListBoxLatSpecial.CheckedItems;

                var bgLetter = checkedListBoxBg.CheckedItems;

                if (enLetter != null)
                {
                    foreach (var enL in enLetter)
                    {
                        string let = enL.ToString()!;
                        let = let.Replace(" ", "");
                        enLetters[row] = let;
                        selectedLetters += let.ToString() + " ";
                        //col++;
                    }
                }
                if (latSpecialLetter != null)
                {
                    foreach (var latSpecialL in latSpecialLetter)
                    {
                        string let = latSpecialL.ToString()!;
                        let = let.Replace(" ", "");
                        ptLetters[row] = let;
                        selectedLetters += let.ToString() + " ";
                        //col++;
                    }
                }
                selectedLetters += "- ";
                col = 0;

                foreach (var bgL in bgLetter)
                {
                    string let = bgL.ToString()!;
                    let = let.Replace(" ", "");
                    bgLetters[row] = let.ToString();
                    selectedLetters += let.ToString() + " ";
                    //col++;
                }
            }
            else
            {
                row = -1;
            }

            richTextBoxSelectedLetters.Text += selectedLetters + "\n";

            for (int i = 0; i < checkedListBoxEn.Items.Count; i++)
            {
                checkedListBoxEn.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBoxLatSpecial.Items.Count; i++)
            {
                checkedListBoxLatSpecial.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBoxBg.Items.Count; i++)
            {
                checkedListBoxBg.SetItemChecked(i, false);
            }
        }

        private void buttonComparePtBgWords_Click(object sender, EventArgs e)
        {
            StringBuilder sbRepeatsLatLetters = new StringBuilder();
            HashSet<string> hsCheckForRepeatSpecialAndBgWordLine = new HashSet<string>();

            buttonName = "ComparePtBgWords";

            string getRepeatWord = string.Empty;

            //string[] arrLatinLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //string[] arrBgLetters = { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ъ", "Ю", "Я" };

            hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            richTextBoxResult.Text = string.Empty;

            var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.Split('\n', '-');

            string[] arrSelectedLetters = new string[selectedLetters.Split('\n').Length * 2];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
            }

            progressBarCompare.Minimum = 0;
            progressBarCompare.Maximum = 0; //Clear
            progressBarCompare.Maximum = getPtWords!.Count;

            foreach (var ptW in getPtWords!)
            {
                progressBarCompare.Value++;

                string getCurrentPtWord = ptW.PtName.ToString()!;

                id = ptW.Id;

                string[] getPtWord = getCurrentPtWord!.Split(',', StringSplitOptions.TrimEntries);

                if (checkBoxSameLength.Checked)
                {
                    if (getPtWord.Length > 1)
                    {
                        for (int i = 0; i < getPtWord.Length; i++)
                        {
                            string currentLatWord = getPtWord[i].ToUpper();

                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentLatWord.Length && w.Length > 1))
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (convertedNewBgWord == currentLatWord)
                                    {
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                    }
                                }
                            }
                        }
                    }
                    else if (getPtWord.Length == 1)
                    {
                        string currentPtWord = getPtWord[0];

                        if (checkBoxSameLength.Checked)
                        {
                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentPtWord.Length && w.Length > 1))
                                {
                                    string currentBgWord = bgW.BgWord;

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (convertedNewBgWord == currentPtWord)
                                    {
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord, id);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (!checkBoxSameLength.Checked)
                {
                    if (comboBoxChoise.Text == "PtWord > BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getPtWord.Length > 1)
                        {
                            for (int i = 0; i < getPtWord.Length; i++)
                            {
                                string currentPtWord = getPtWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                if (arrSelectedLetters[l] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (currentPtWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord, id);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getPtWord.Length == 1)
                        {
                            string currentPtWord = getPtWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.Length > 1).ToHashSet())
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();
                                    //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < arrSelectedLetters.Length; k++)
                                    {
                                        for (int l = 0; l < currentBgWord.Length; l++)
                                        {
                                            string currentBgLetter = currentBgWord[l].ToString();

                                            if (arrSelectedLetters[k] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[k - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (currentPtWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                    {
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord, id);
                                    }
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "PtWord < BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getPtWord.Length > 1)
                        {
                            for (int i = 0; i < getPtWord.Length; i++)
                            {
                                string currentPtWord = getPtWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                if (arrSelectedLetters[l] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Contains(currentPtWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord, id);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getPtWord.Length == 1)
                        {
                            string currentPtWord = getPtWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            if (checkBoxSameLength.Checked)
                            {
                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < arrSelectedLetters.Length; k++)
                                        {
                                            for (int l = 0; l < currentBgWord.Length; l++)
                                            {
                                                string currentBgLetter = currentBgWord[l].ToString();

                                                if (arrSelectedLetters[k] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[k - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Contains(currentPtWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord, id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var word in hsResultSpecialWordToBgWord.OrderBy(w => w.SpecialWordLength))
            {
                sbResult.AppendLine($"{word.SpecialWord!.Trim()} - {word.BgWord!.Trim()}");
            }
            //string[] arrResult = hsResult.ToString()!.Split('-').ToArray();

            //richTextBoxResult.Text = string.Join('\n', hsResult.Select(w => new { w.SpecialWord, w.BgWord}).OrderBy(w => w.SpecialWord));

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultSpecialWordToBgWord.Count}";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonComapreLatBgWords_Click(object sender, EventArgs e)
        {
            StringBuilder sbRepeatsLatLetters = new StringBuilder();
            HashSet<string> hsCheckForRepeatSpecialAndBgWordLine = new HashSet<string>();

            buttonName = "CompareLatBgWords";

            string getRepeatWord = string.Empty;

            //string[] arrLatinLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //string[] arrBgLetters = { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ъ", "Ю", "Я" };

            hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            richTextBoxResult.Text = string.Empty;

            var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.Split('\n', '-');

            string[] arrSelectedLetters = new string[selectedLetters.Split('\n').Length * 2];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
            }

            progressBarCompare.Minimum = 0;
            progressBarCompare.Maximum = 0; //Clear
            progressBarCompare.Maximum = getLatWords!.Count;

            foreach (var latW in getLatWords!)
            {
                progressBarCompare.Value++;

                string getCurrentLatWord = latW.LatWord.ToString()!;

                id = latW.Id;

                string[] getLatWord = getCurrentLatWord!.Split(',', StringSplitOptions.TrimEntries);

                if (checkBoxSameLength.Checked)
                {
                    if (getLatWord.Length > 1)
                    {
                        for (int i = 0; i < getLatWord.Length; i++)
                        {
                            string currentLatWord = getLatWord[i].ToUpper();

                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentLatWord.Length && w.Length > 1))
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (convertedNewBgWord == currentLatWord)
                                    {
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                    }
                                }
                            }
                        }
                    }
                    else if (getLatWord.Length == 1)
                    {
                        string currentLatWord = getLatWord[0];

                        if (checkBoxSameLength.Checked)
                        {
                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentLatWord.Length && w.Length > 1))
                                {
                                    string currentBgWord = bgW.BgWord;

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (convertedNewBgWord == currentLatWord)
                                    {
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (!checkBoxSameLength.Checked)
                {
                    if (comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        string getCurrentLatLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getLatWord.Length > 1)
                        {
                            for (int i = 0; i < getLatWord.Length; i++)
                            {
                                string currentLatWord = getLatWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentLatWord.Length && w.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                if (arrSelectedLetters[l] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (currentLatWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getLatWord.Length == 1)
                        {
                            string currentLatWord = getLatWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            //if (checkBoxSameLength.Checked)
                            //{
                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentLatWord.Length && w.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < arrSelectedLetters.Length; k++)
                                        {
                                            for (int l = 0; l < currentBgWord.Length; l++)
                                            {
                                                string currentBgLetter = currentBgWord[l].ToString();

                                                if (arrSelectedLetters[k] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[k - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (currentLatWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                        }
                                    }
                                }
                            //}
                        }
                    }
                    else if (comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        string getCurrentLatLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getLatWord.Length > 1)
                        {
                            for (int i = 0; i < getLatWord.Length; i++)
                            {
                                string currentLatWord = getLatWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentLatWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                if (arrSelectedLetters[l] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Contains(currentLatWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getLatWord.Length == 1)
                        {
                            string currentLatWord = getLatWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            //if (checkBoxSameLength.Checked)
                            //{
                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentLatWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < arrSelectedLetters.Length; k++)
                                        {
                                            for (int l = 0; l < currentBgWord.Length; l++)
                                            {
                                                string currentBgLetter = currentBgWord[l].ToString();

                                                if (arrSelectedLetters[k] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[k - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Contains(currentLatWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord, id);
                                        }
                                    }
                                }
                            //}
                        }
                    }
                }
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var word in hsResultSpecialWordToBgWord.OrderBy(w => w.SpecialWordLength))
            {
                sbResult.AppendLine($"{word.SpecialWord!.Trim()} - {word.BgWord!.Trim()}");
            }
            //string[] arrResult = hsResult.ToString()!.Split('-').ToArray();

            //richTextBoxResult.Text = string.Join('\n', hsResult.Select(w => new { w.SpecialWord, w.BgWord}).OrderBy(w => w.SpecialWord));

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultSpecialWordToBgWord.Count}";
        }

        private void AddSpecialBgWords(HashSet<string> hsCheckForRepeatSpecialAndBgWordsLine, string currentSpecialWord, string currentBgWord, int id)
        {
            int countOnHs = hsCheckForRepeatSpecialAndBgWordsLine.Count();

            hsCheckForRepeatSpecialAndBgWordsLine.Add($"{currentSpecialWord} - {currentBgWord}");

            if (hsCheckForRepeatSpecialAndBgWordsLine.Count == countOnHs + 1)
            {
                SpecialWordToBgWord specialWordToBgWord = new SpecialWordToBgWord();

                specialWordToBgWord.Id = id;
                specialWordToBgWord.SpecialWord = currentSpecialWord;
                specialWordToBgWord.SpecialWordLength = currentSpecialWord.Length;
                specialWordToBgWord.BgWord = currentBgWord;
                specialWordToBgWord.BgWordLength = currentBgWord.Length;

                hsResultSpecialWordToBgWord.Add(specialWordToBgWord);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            richTextBoxSelectedLetters.SelectedText = "";
        }

        private void buttonClearSelectedLetters_Click(object sender, EventArgs e)
        {
            richTextBoxSelectedLetters.Text = "";
        }

        private void buttonClearResult_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text = string.Empty;

            progressBarCompare.Maximum = 0;

            labelResultItems.Text = $"Items:";

            hsResultSpecialWordToBgWord.Clear();
            hsResultWordNameToBgName.Clear();
        }
        private void buttonCompareWordlBgNames_Click(object sender, EventArgs e)
        {
            StringBuilder sbRepeatsLatLetters = new StringBuilder();

            string getRepeatWord = string.Empty;

            buttonName = "CompareWordlBgNames";

            hsResultWordNameToBgName = new HashSet<WorldBgNames>();

            richTextBoxResult.Text = string.Empty;

            var getWorldNames = context.WorldNames?.Select(w => new { w.Id, w.WorldName }).ToHashSet();

            var getBgNames = context.BgNames?.Select(w => new { w.Id, w.BgName, w.BgName.Length }).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.Split('\n', '-');

            string[] arrSelectedLetters = new string[selectedLetters.Split('\n').Length * 2];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
            }

            progressBarCompare.Minimum = 0;
            progressBarCompare.Maximum = 0; //Clear
            progressBarCompare.Maximum = getWorldNames!.Count();

            foreach (var latW in getWorldNames!)
            {
                progressBarCompare.Value++;

                id = latW.Id;

                string getCurrentWorldName = latW.WorldName.ToString()!;

                string[] getWorldName = getCurrentWorldName.Split(',', StringSplitOptions.TrimEntries);

                if (checkBoxSameLength.Checked)
                {
                    if (getWorldName.Length > 1)
                    {
                        for (int i = 0; i < getWorldName.Length; i++)
                        {
                            string currentLatWord = getWorldName[i].ToUpper().Replace(" ", "");

                            if (getBgNames != null)
                            {
                                foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == currentLatWord.Length && w.Length > 1))
                                {
                                    string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }

                                    if (convertedNewBgWord == currentLatWord)
                                    {
                                        AddWorldBgNames(currentLatWord, currentBgWord);
                                    }
                                }
                            }
                        }
                    }
                    else if (getWorldName.Length == 1)
                    {
                        string currentLatWord = getWorldName[0].Replace(" ", "");

                        if (getBgNames != null)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == currentLatWord.Length && w.Length > 1))
                            {
                                string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");

                                string convertedNewBgWord = string.Empty;

                                for (int k = 0; k < currentBgWord.Length; k++)
                                {
                                    string currentBgLetter = currentBgWord[k].ToString();

                                    for (int l = 0; l < arrSelectedLetters.Length; l++)
                                    {
                                        if (arrSelectedLetters[l] == currentBgLetter)
                                        {
                                            convertedNewBgWord += arrSelectedLetters[l - 1];
                                            break;
                                        }
                                    }
                                    if (convertedNewBgWord.Length == currentBgWord.Length)
                                    {
                                        break;
                                    }
                                }

                                if (convertedNewBgWord == currentLatWord)
                                {
                                    AddWorldBgNames(currentLatWord, currentBgWord);
                                }
                            }
                        }
                    }
                }
                else if (!checkBoxSameLength.Checked)
                {
                    if (comboBoxChoise.Text == "WorldName > BgName")
                    {
                        string getCurrentLatLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getWorldName.Length > 1)
                        {
                            for (int i = 0; i < getWorldName.Length; i++)
                            {
                                string currentLatWord = getWorldName[i].ToUpper().Replace(" ", "");
                                //string currentLatWord = "AARAN"; // getWorldName[i].ToUpper().Replace(" ", "");

                                if (getBgNames != null)
                                {
                                    foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < currentLatWord.Length && w.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");
                                        //string currentBgWord = "АДИ"; // bgW.BgName.ToUpper().Replace(" ", "");

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                string convertToLatinLetter = arrSelectedLetters[l - 1];

                                                if (currentLatWord.Contains(convertToLatinLetter))
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }

                                        if (currentLatWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            AddWorldBgNames(currentLatWord, currentBgWord);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getWorldName.Length == 1)
                        {
                            string currentLatWord = getWorldName[0].ToUpper().Replace(" ", "");
                            //string currentLatWord = "AARAN"; //getWorldName[0].ToUpper().Replace(" ", "");

                            if (getBgNames != null)
                            {
                                foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < currentLatWord.Length && w.Length > 1).ToHashSet())
                                {
                                    //string currentBgWord = "АРН"; //bgW.BgName.ToUpper().Replace(" ", "");
                                    string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                string convertToLatinLetter = arrSelectedLetters[l - 1];

                                                if (currentLatWord.Contains(convertToLatinLetter))
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }

                                    if (currentLatWord.Contains(convertedNewBgWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                    //if (convertedNewBgWord.Length == currentBgWord.Length)
                                    {
                                        AddWorldBgNames(currentLatWord, currentBgWord);
                                    }
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "WorldName < BgName")
                    {
                        string getCurrentLatLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getWorldName.Length > 1)
                        {
                            for (int i = 0; i < getWorldName.Length; i++)
                            {
                                string currentLatWord = getWorldName[i].ToUpper().Replace(" ", "");

                                //var getBgWordsByLength = context.BgNames?.Select(w => new { w.Id, w.BgName, w.Length }).Where(w => w.Length < currentLatWord.Length && w.Length > 1);

                                if (getBgNames != null)
                                {
                                    foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > currentLatWord.Length && w.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");

                                        string convertedNewBgWord = string.Empty;

                                        for (int k = 0; k < currentBgWord.Length; k++)
                                        {
                                            string currentBgLetter = currentBgWord[k].ToString();

                                            for (int l = 0; l < arrSelectedLetters.Length; l++)
                                            {
                                                if (arrSelectedLetters[l] == currentBgLetter)
                                                {
                                                    convertedNewBgWord += arrSelectedLetters[l - 1];
                                                    break;
                                                }
                                            }
                                            if (convertedNewBgWord.Length == currentBgWord.Length)
                                            {
                                                break;
                                            }
                                        }

                                        if (convertedNewBgWord.Contains(currentLatWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                        //if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            WorldBgNames WorldBgNames = new WorldBgNames();

                                            WorldBgNames.WorldName = currentLatWord;
                                            WorldBgNames.WorldNameLength = currentLatWord.Length;
                                            WorldBgNames.BgName = currentBgWord;
                                            WorldBgNames.BgNameLength = currentBgWord.Length;

                                            hsResultWordNameToBgName.Add(WorldBgNames);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getWorldName.Length == 1)
                        {
                            string currentLatWord = getWorldName[0].ToUpper().Replace(" ", "");

                            //var getBgWordsByLength = context.BgNames?.Select(w => new { w.Id, w.BgName, w.Length }).Where(w => w.Length < currentLatWord.Length && w.Length > 1);

                            if (getBgNames != null)
                            {
                                foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > currentLatWord.Length && w.Length > 1).ToHashSet())
                                {
                                    string currentBgWord = bgW.BgName.ToUpper().Replace(" ", "");

                                    string convertedNewBgWord = string.Empty;

                                    for (int k = 0; k < currentBgWord.Length; k++)
                                    {
                                        string currentBgLetter = currentBgWord[k].ToString();

                                        for (int l = 0; l < arrSelectedLetters.Length; l++)
                                        {
                                            if (arrSelectedLetters[l] == currentBgLetter)
                                            {
                                                convertedNewBgWord += arrSelectedLetters[l - 1];
                                                break;
                                            }
                                        }
                                        if (convertedNewBgWord.Length == currentBgWord.Length)
                                        {
                                            break;
                                        }
                                    }

                                    if (convertedNewBgWord.Contains(currentLatWord) && convertedNewBgWord.Length == currentBgWord.Length)
                                    //if (convertedNewBgWord.Length == currentBgWord.Length)
                                    {
                                        WorldBgNames WorldBgNames = new WorldBgNames();

                                        WorldBgNames.WorldName = currentLatWord;
                                        WorldBgNames.WorldNameLength = currentLatWord.Length;
                                        WorldBgNames.BgName = currentBgWord;
                                        WorldBgNames.BgNameLength = currentBgWord.Length;

                                        hsResultWordNameToBgName.Add(WorldBgNames);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var name in hsResultWordNameToBgName.OrderBy(w => w.WorldName))
            {
                sbResult.AppendLine($"{name.WorldName} - {name.BgName}");
            }
            //string[] arrResult = hsResult.ToString()!.Split('-').ToArray();

            //richTextBoxResult.Text = string.Join('\n', hsResult.Select(w => new { w.SpecialWord, w.BgWord}).OrderBy(w => w.SpecialWord));

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultWordNameToBgName.Count}";
        }

        private void AddWorldBgNames(string currentLatWord, string currentBgWord)
        {
            WorldBgNames WorldBgNames = new WorldBgNames();

            WorldBgNames.Id = id;
            WorldBgNames.WorldName = currentLatWord;
            WorldBgNames.WorldNameLength = currentLatWord.Length;
            WorldBgNames.BgName = currentBgWord;
            WorldBgNames.BgNameLength = currentBgWord.Length;

            hsResultWordNameToBgName.Add(WorldBgNames);
        }

        private void comboBoxChoise_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxSameLength.Checked = false;
            comboBoxChoise.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void checkBoxSameLength_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxChoise.Text = string.Empty;
            sameLengthIsChecked = true;
        }

        private void buttonEqualsLatWords_Click(object sender, EventArgs e)
        {
            StringBuilder sbDuplicateLatWords = new StringBuilder();
            int countLines = 0;

            var duplicates = hsResultSpecialWordToBgWord.GroupBy(line => line.SpecialWord).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

            foreach (var duplicate in duplicates.OrderBy(w => w.SpecialWord))
            {
                countLines++;
                sbDuplicateLatWords.AppendLine($"{duplicate.SpecialWord} - {duplicate.BgWord}");
            }

            richTextBoxResult.Text = string.Join("\n", sbDuplicateLatWords);

            labelResultItems.Text = $"Items: {countLines}";
        }

        private void buttonEnterWord_Click(object sender, EventArgs e)
        {
            string word = textBoxViewId.Text.Trim();

            if (hsResultSpecialWordToBgWord.Count > 1)
            {
                var getId = hsResultSpecialWordToBgWord.Select(w => new { w.Id, w.SpecialWord }).Where(w => w.SpecialWord == word);

                foreach (var currentId in getId)
                {
                    id = currentId.Id;
                }

                labelInfo.Text = $"Id = {id.ToString()}";
            }
            else if (hsResultWordNameToBgName.Count > 1)
            {
                var getId = hsResultWordNameToBgName.Select(w => new { w.Id, w.WorldName }).Where(w => w.WorldName == word);

                foreach (var currentId in getId)
                {
                    id = currentId.Id;
                }
                labelInfo.Text = $"Id = {id.ToString()}";
            }
        }

        private void buttonViewID_Click(object sender, EventArgs e)
        {
            string word = textBoxViewId.Text.Trim();

            int getCurrentId = 0;

            if (hsResultSpecialWordToBgWord.Count > 1)
            {
                var getId = hsResultSpecialWordToBgWord.Select(w => new { w.Id, w.SpecialWord }).Where(w => w.SpecialWord == word);

                foreach (var currentId in getId)
                {
                    getCurrentId = currentId.Id;
                }
            }
            else if (hsResultWordNameToBgName.Count > 1)
            {
                var getId = hsResultWordNameToBgName.Select(w => new { w.Id, w.WorldName }).Where(w => w.WorldName == word);

                foreach (var currentId in getId)
                {
                    getCurrentId = currentId.Id;
                }
            }

            labelInfo.Text = $"Id = {getCurrentId.ToString()}";
        }

        private void buttonViewDescription_Click(object sender, EventArgs e)
        {
            string word = textBoxViewId.Text.Trim();

            string description = string.Empty;


            if (buttonName == "CompareEnBgWords")
            {

            }
            else if (buttonName == "ComparePtBgWords")
            {
                var query = context.PtNames!.Select(w => new { w.PtName, w.Description }).Where(w => EF.Functions.Like(w.PtName, @$"%{word}%"));

                foreach (var currentDesc in query)
                {
                    description = currentDesc.Description;
                }
            }
            else if (buttonName == "CompareLatBgWords")
            {
                var query = context.LatWords!.Select(w => new { w.LatWord, w.EnWord }).Where(w => EF.Functions.Like(w.LatWord, @$"%{word}%"));

                foreach (var currentDesc in query)
                {
                    description = currentDesc.EnWord;
                }
            }
            else if (buttonName == "CompareWordlBgNames")
            {
                var getDescription = context.WorldNames!.Select(w => new { w.WorldName, w.Description }).Where(w => w.WorldName == word);

                foreach (var currentDesc in getDescription)
                {
                    description = currentDesc.Description;
                }
            }

            labelInfo.Text = $"Description: {description.ToString()}";
        }

        private void buttonTempTable_Click(object sender, EventArgs e)
        {
            //await DeleteResultTable();

            HashSet<WorldBgNames> hsWords = new HashSet<WorldBgNames>();
            ResultTable tempTable = new ResultTable();
            var src = DateTime.Now;
            string comparison = string.Empty;
            List<LettRelations> letRelatId = new List<LettRelations>();
            int id = 0;
            string lettersRelations = richTextBoxSelectedLetters.Text;

            string lettersRelationsName = textBoxLetterRelationsName.Text;

            if (comboBoxChoise.Text != "")
            {
                comparison = comboBoxChoise.Text;
            }
            else if (sameLengthIsChecked)
            {
                comparison = "SameLength";
            }

            var getLastId = context.LettRelations!.Select(l => new { l.Id, l.Name }).ToList().OrderByDescending(l => l.Id).FirstOrDefault();

            if (getLastId != null)
            {
                id = int.Parse(getLastId!.Id.ToString());

                LettRelations letterRelations = new LettRelations();

                letterRelations.Id = id;

                letRelatId.Add(letterRelations);
            }

            string result = richTextBoxResult.Text;

            if (lettersRelations.Length > 0)
            {
                if (comparison.Length > 0)
                {
                    if (result.Length > 0)
                    {
                        if (lettersRelationsName.Length > 0)
                        {
                            string[] arrLines = result.Split('-', '\n');

                            for (int i = 0; i < arrLines.Length - 1; i += 2)
                            {
                                WorldBgNames worldBgNames = new WorldBgNames();

                                worldBgNames.Name = lettersRelationsName;

                                worldBgNames.WorldName = arrLines[i].Replace(" ", "");
                                worldBgNames.WorldNameLength = arrLines[i].Replace(" ", "").Length;

                                worldBgNames.BgName = arrLines[i + 1].Replace(" ", "");
                                worldBgNames.BgNameLength = arrLines[i + 1].Replace(" ", "").Length;

                                worldBgNames.LetRelatId = id;

                                worldBgNames.Comparison = comparison;

                                hsWords.Add(worldBgNames);
                            }

                            foreach (var w in hsWords)
                            {
                                ResultTable wBg = new ResultTable()
                                {
                                    LatWord = w.WorldName!,
                                    LatWordLength = w.WorldNameLength,
                                    BgWord = w.BgName!,
                                    BgWordLength = w.BgNameLength,
                                    //LetRelatId = w.LetRelatId,
                                    LettRelationsId = w.LetRelatId,
                                    Comparison = comparison,
                                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                                };
                                context.ResultTables!.Add(wBg);
                            }
                            context.SaveChanges();

                            FrmResultTable temporaryTable = new FrmResultTable();
                            temporaryTable.Show();
                        }
                        else
                        {
                            MessageBox.Show("Give a Name on Letters Relations!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choice button for Compare!");
                    }
                }
                else
                {
                    MessageBox.Show("Choice Comparison!");
                }
            }
            else
            {
                MessageBox.Show("Enter Letters!");
            }
        }

        private static async Task DeleteResultTable()
        {
            SqlConnection cnn = new SqlConnection(DbConfig.ConnectionString);
            await cnn.OpenAsync();

            SqlDataAdapter da = new SqlDataAdapter($"DELETE FROM ResultTables", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ResultTables");
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    StringBuilder sbDuplicateLatWords = new StringBuilder();
        //    int countLines = 0;

        //    var duplicates = hsResultSpecialWordToBgWord.GroupBy(line => line.SpecialWord).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

        //    foreach (var duplicate in duplicates.OrderBy(w => w.SpecialWord))
        //    {
        //        countLines++;
        //        sbDuplicateLatWords.AppendLine($"{duplicate.SpecialWord} - {duplicate.BgWord}");
        //    }

        //    richTextBoxResult.Text = string.Join("\n", sbDuplicateLatWords);

        //    labelResultItems.Text = $"Items: {countLines}";
        //}

        private void buttonDuplicatedWorldNames_Click(object sender, EventArgs e)
        {
            StringBuilder sbDuplicateLatWords = new StringBuilder();
            int countLines = 0;

            var duplicates = hsResultWordNameToBgName.GroupBy(line => line.WorldName).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

            foreach (var duplicate in duplicates.OrderBy(w => w.WorldName))
            {
                countLines++;
                sbDuplicateLatWords.AppendLine($"{duplicate.WorldName} - {duplicate.BgName}");
            }

            richTextBoxResult.Text = string.Join("\n", sbDuplicateLatWords);

            labelResultItems.Text = $"Items: {countLines}";
        }

        private void buttonAddInTable_Click(object sender, EventArgs e)
        {
            var src = DateTime.Now;

            string letRelatName = textBoxLetterRelationsName.Text;

            var getNamesFromTable = context.LettRelations!.Select(n => n.Name).ToHashSet();

            string letters = richTextBoxSelectedLetters.Text;

            var getLetters = context.LettRelations!.Select(l => new { l.Id, l.Name, l.Letters }).ToList();
            string existLetters = string.Empty;

            foreach (var l in getLetters)
            {
                if (letters.Replace("\n", "") == l.Letters.Replace("\n", ""))
                {
                    existLetters = l.Letters;
                }
            }
            if (existLetters.Length == 0)
            {
                if (letRelatName.Length > 0)
                {
                    if (letters.Length > 0)
                    {
                        if (!getNamesFromTable.Contains(letRelatName))
                        {
                            LettRelations lr = new LettRelations()
                            {
                                //Letters = newLetters,
                                //ResultTableId = 1,
                                Letters = letters,
                                Name = letRelatName,
                                DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                            };
                            context.LettRelations!.Add(lr);

                            context.SaveChanges();
                            MessageBox.Show("Done!");
                        }
                        else
                        {
                            MessageBox.Show("The Name exist!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Have NOT Letters!");
                    }
                }
                else
                {
                    MessageBox.Show("Have NOT Name!");
                }
            }
            else
            {
                MessageBox.Show("Have this combination of letters!");
            }
        }

        private void buttonInsertInTableResult_Click(object sender, EventArgs e)
        {
            //await DeleteResultTable();

            List<WorldBgNames> lsWords = new List<WorldBgNames>();
            ResultTable tempTable = new ResultTable();
            var src = DateTime.Now;
            int id = 0;
            string comparison = string.Empty;

            //List<LettRelations> letRelatId = new List<LettRelations>();

            string letRelatName = textBoxLetterRelationsName.Text;
            string letters = richTextBoxSelectedLetters.Text;
            var getNamesFromTable = context.LettRelations!.Select(n => n.Name).ToHashSet();
            string lettersRelations = richTextBoxSelectedLetters.Text;
            //string lettersRelationsName = textBoxLetterRelationsName.Text;

            if (comboBoxChoise.Text != "")
            {
                comparison = comboBoxChoise.Text;
            }
            else if (sameLengthIsChecked)
            {
                comparison = "SameLength";
            }

            var getLetters = context.LettRelations!.Select(l => new { l.Id, l.Name, l.Letters }).ToList();
            string lettersNew = string.Empty;

            foreach (var l in getLetters)
            {
                if (letters.Replace("\n", "") == l.Letters.Replace("\n", ""))
                {
                    lettersNew = l.Letters;
                }
            }

            var getCurrentId = context.LettRelations!.Select(l => new { l.Id, l.Name, l.Letters }).Where(l => l.Letters == lettersNew).FirstOrDefault();

            if (getCurrentId != null)
            {
                id = int.Parse(getCurrentId!.Id.ToString());
            }
            else
            {
                MessageBox.Show("Unable to find letters in database!");
                MessageBox.Show("Add First letters in database!");

                return;
            }
            string result = richTextBoxResult.Text;

            if (lettersRelations.Length > 0)
            {
                if (comparison.Length > 0)
                {
                    if (result.Length > 0)
                    {
                        //if (lettersRelationsName.Length > 0)
                        //{
                        string[] arrLines = result.Split('-', '\n');

                        for (int i = 0; i < arrLines.Length - 1; i += 2)
                        {
                            WorldBgNames worldBgNames = new WorldBgNames();

                            //worldBgNames.Name = lettersRelationsName;

                            worldBgNames.WorldName = arrLines[i].Replace(" ", "");
                            worldBgNames.WorldNameLength = arrLines[i].Replace(" ", "").Length;

                            worldBgNames.BgName = arrLines[i + 1].Replace(" ", "");
                            worldBgNames.BgNameLength = arrLines[i + 1].Replace(" ", "").Length;

                            worldBgNames.LetRelatId = id;

                            worldBgNames.Comparison = comparison;

                            lsWords.Add(worldBgNames);
                        }

                        foreach (var w in lsWords)
                        {
                            ResultTable wBg = new ResultTable()
                            {
                                LatWord = w.WorldName!,
                                LatWordLength = w.WorldNameLength,
                                BgWord = w.BgName!,
                                BgWordLength = w.BgNameLength,
                                LettRelationsId = id,
                                Comparison = comparison,
                                DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                            };
                            context.ResultTables!.Add(wBg);
                        }
                        context.SaveChanges();
                        MessageBox.Show("Done!");

                        FrmResultTable temporaryTable = new FrmResultTable();
                        temporaryTable.Show();
                    }
                    else
                    {
                        MessageBox.Show("Choice button for Compare!");
                    }
                }
                else
                {
                    MessageBox.Show("Choice Comparison!");
                }
            }
            else
            {
                MessageBox.Show("Enter Letters!");
            }
        }

        private void buttonViewTableResult_Click(object sender, EventArgs e)
        {
            FrmResultTable frmResultTable = new FrmResultTable();
            frmResultTable.Show();
        }

        private void buttonLoadLetterRelations_Click(object sender, EventArgs e)
        {
            string name = comboBoxLetterRelationsName.Text;

            HashSet<string> hsCombLetRelat = new HashSet<string>();

            var getLetRelatNames = context.LettRelations!.Select(n => new { n.Id, n.Name }).OrderBy(n => n.Name);

            string getLetters = string.Empty;

            string letters = string.Empty;

            if (getLetRelatNames != null)
            {
                foreach (var n in getLetRelatNames)
                {
                    hsCombLetRelat.Add(n.Name);

                }

                comboBoxLetterRelationsName.Items!.Clear();

                foreach (var n in hsCombLetRelat)
                {
                    comboBoxLetterRelationsName.Items!.Add(n);
                }
            }
            if (name != "")
            {
                var query = context.LettRelations!.Select(n => new { n.Name, n.Letters }).Where(l => l.Name == name);

                foreach (var l in query)
                {
                    getLetters = l.Letters;

                }

                string[] arrLetters = getLetters.Split("\n");

                for (int i = 0; i < arrLetters.Length; i++)
                {
                    letters += arrLetters[i] + "\n";
                }
                richTextBoxSelectedLetters.Text = letters;

            }
        }

        private void buttonViewTableLetRelat_Click(object sender, EventArgs e)
        {
            FrmLettRelations frmLettersRelations = new FrmLettRelations();
            frmLettersRelations.Show();
        }

        private void toolStripButtonExportTable_Click(object sender, EventArgs e)
        {
            FrmExport frmExport = new FrmExport();
            frmExport.Show();
        }

        private void buttonEqualsPtWords_Click(object sender, EventArgs e)
        {
            StringBuilder sbDuplicatePtWords = new StringBuilder();
            int countLines = 0;

            var duplicates = hsResultSpecialWordToBgWord.GroupBy(line => line.SpecialWord).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

            foreach (var duplicate in duplicates.OrderBy(w => w.SpecialWord))
            {
                countLines++;
                sbDuplicatePtWords.AppendLine($"{duplicate.SpecialWord} - {duplicate.BgWord}");
            }

            richTextBoxResult.Text = string.Join("\n", sbDuplicatePtWords);

            labelResultItems.Text = $"Items: {countLines}";
        }

        private void buttonCompareEqualsPtBgLetters_Click(object sender, EventArgs e)
        {
            int countMatchedWords = 0;

            hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            StringBuilder sbMatchedWords = new StringBuilder();

            HashSet<string> hsCheckForRepeatSpecialAndBgWordLine = new HashSet<string>();

            buttonName = "ComparePtBgWords";

            string getRepeatWord = string.Empty;

            richTextBoxResult.Text = string.Empty;

            //string[] arrLatinLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //string[] arrBgLetters = { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ъ", "Ю", "Я" };

            var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).Where(w => w.PtName.Length > 1).ToHashSet();

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).Where(w => w.BgWord.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.Split('\n', '-');

            string[] arrSelectedLetters = new string[selectedLetters.Split('\n').Length * 2];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
            }

            progressBarCompare.Minimum = 0;
            progressBarCompare.Maximum = 0; //Clear
            progressBarCompare.Maximum = getPtWords!.Count;

            foreach (var ptW in getPtWords!)
            {
                progressBarCompare.Value++;

                string getCurrentPtWord = ptW.PtName.ToString()!;

                id = ptW.Id;

                string[] getPtWord = getCurrentPtWord!.Split(',', StringSplitOptions.TrimEntries);

                if (checkBoxSameLength.Checked)
                {
                    if (getPtWord.Length > 1)
                    {
                        for (int i = 0; i < getPtWord.Length; i++)
                        {
                            string currentPtWord = getPtWord[i].ToUpper();

                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentPtWord.Length))
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();

                                    countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                                }
                            }
                        }
                    }
                    else if (getPtWord.Length == 1)
                    {
                        string currentPtWord = getPtWord[0];

                        //if (checkBoxSameLength.Checked)
                        //{
                        if (getBgWords != null)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentPtWord.Length))
                            {
                                string currentBgWord = bgW.BgWord;

                                countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                            }
                        }
                        //}
                    }
                }
                else if (!checkBoxSameLength.Checked)
                {
                    if (comboBoxChoise.Text == "PtWord > BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getPtWord.Length > 1)
                        {
                            for (int i = 0; i < getPtWord.Length; i++)
                            {
                                string currentPtWord = getPtWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();

                                        countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                                    }
                                }
                            }
                        }
                        else if (getPtWord.Length == 1)
                        {
                            string currentPtWord = getPtWord[0].ToUpper();

                            //if (checkBoxSameLength.Checked)
                            //{
                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();

                                    countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                                }
                            }
                            //}
                        }
                    }
                    else if (comboBoxChoise.Text == "PtWord < BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getPtWord.Length > 1)
                        {
                            for (int i = 0; i < getPtWord.Length; i++)
                            {
                                string currentPtWord = getPtWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                    {
                                        string currentBgWord = bgW.BgWord.ToUpper();
                                        //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

                                        countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                                    }
                                }
                            }
                        }
                        else if (getPtWord.Length == 1)
                        {
                            string currentPtWord = getPtWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            //if (checkBoxSameLength.Checked)
                            //{
                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
                                {
                                    string currentBgWord = bgW.BgWord.ToUpper();
                                    //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

                                    countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
                                }
                            }
                            //}
                        }
                    }
                }
            }

            richTextBoxResult.Text = sbMatchedWords.ToString();

            labelResultItems.Text = $"Items: {countMatchedWords}";
        }

        private static int AddMatchedWords(int countMatchedWords, StringBuilder sbMatchedWords, string[] arrSelectedLetters, string currentPtWord, string currentBgWord)
        {
            StringBuilder sbEqualWord = new StringBuilder();

            HashSet<char> hsLetters = new HashSet<char>();

            string convertedNewBgWord = string.Empty;

            bool isSameLenght = false;

            for (int k = 0; k < currentBgWord.Length; k++)
            {
                string currentBgLetter = currentBgWord[k].ToString();

                for (int l = 0; l < arrSelectedLetters.Length; l++)
                {
                    if (arrSelectedLetters[l] == currentBgLetter)
                    {
                        convertedNewBgWord += arrSelectedLetters[l - 1];
                        break;
                    }
                }
                if (convertedNewBgWord.Length == currentBgWord.Length)
                {
                    isSameLenght = true;
                    break;
                }
            }
            if (isSameLenght)
            {
                bool isNotContain = false;

                for (int m = 0; m < convertedNewBgWord.Length; m++)
                {
                    if (!hsLetters.Contains(convertedNewBgWord[m])) //Add letters to HashSet for ignore repeating letters in word
                    {
                        hsLetters.Add(convertedNewBgWord[m]);
                    }
                    if (!currentPtWord.Contains(convertedNewBgWord[m]))
                    {
                        isNotContain = true;
                        break;
                    }
                }
                for (int l = 0; l < currentPtWord.Length; l++)
                {
                    if (!convertedNewBgWord.Contains(currentPtWord[l]))
                    {
                        isNotContain = true;
                        break;
                    }
                }
                if (!isNotContain && hsLetters.Count == currentPtWord.Length)
                {
                    countMatchedWords++;

                    sbMatchedWords.AppendLine($"{currentPtWord} - {currentBgWord}");
                }
            }

            return countMatchedWords;
        }
    }
}
