using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
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

        private StringBuilder sbWords = new StringBuilder();

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


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedLetters = string.Empty;

            countAddLetters++;

            row++;

            if (row <= 26)
            {
                var latSpecialLetter = checkedListBoxLatSpecial.CheckedItems;

                var bgLetter = checkedListBoxBg.CheckedItems;

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

            HashSet<string> hsForeignWords = new HashSet<string>();

            if (comboBoxLanguage.Text == "PT-BG")
            {
                var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

                hsForeignWords = getPtWords!.Select(w => w.PtName).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "LAT-BG")
            {
                var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

                hsForeignWords = getLatWords!.Select(w => w.LatWord).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "EN-BG")
            {
                var getEnWords = context.EnNames?.Select(w => new { w.Id, w.EnName }).ToHashSet();

                hsForeignWords = getEnWords!.Select(w => w.EnName).ToHashSet();
            }
            else
            {
                MessageBox.Show("Choice Lenguage From");
                return;
            }

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.Split('\n', '-');

            string[] arrSelectedLetters = new string[selectedLetters.Split('\n').Length * 2];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
            }

            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = hsForeignWords!.Count;

            foreach (var fWword in hsForeignWords!)
            {
                progressBarLoad.Value++;

                string getCurrentForeignWord = fWword.ToString()!;

                //id = ptW.Id;

                string[] getFWord = getCurrentForeignWord!.Split(',', StringSplitOptions.TrimEntries);

                if (checkBoxSameLength.Checked)
                {
                    if (getFWord.Length > 1)
                    {
                        for (int i = 0; i < getFWord.Length; i++)
                        {
                            string currentLatWord = getFWord[i].ToUpper();

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
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentLatWord, currentBgWord);
                                    }
                                }
                            }
                        }
                    }
                    else if (getFWord.Length == 1)
                    {
                        string currentPtWord = getFWord[0];

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
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (!checkBoxSameLength.Checked)
                {
                    if (comboBoxChoise.Text == "PtWord > BgWord" || comboBoxChoise.Text == "EnWord > BgWord" || comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getFWord.Length > 1)
                        {
                            for (int i = 0; i < getFWord.Length; i++)
                            {
                                string currentPtWord = getFWord[i].ToUpper();
                                //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

                                if (getBgWords != null)
                                {
                                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
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
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getFWord.Length == 1)
                        {
                            string currentPtWord = getFWord[0].ToUpper();
                            //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

                            if (getBgWords != null)
                            {
                                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
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
                                        AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord);
                                    }
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "PtWord < BgWord" || comboBoxChoise.Text == "EnWord < BgWord" || comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        string getCurrentPtLetter = string.Empty;
                        string getCurrentBgLetter = string.Empty;

                        if (getFWord.Length > 1)
                        {
                            for (int i = 0; i < getFWord.Length; i++)
                            {
                                string currentPtWord = getFWord[i].ToUpper();
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
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord);
                                        }
                                    }
                                }
                            }
                        }
                        else if (getFWord.Length == 1)
                        {
                            string currentPtWord = getFWord[0].ToUpper();
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
                                            AddSpecialBgWords(hsCheckForRepeatSpecialAndBgWordLine, currentPtWord, currentBgWord);
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
                sbResult.AppendLine($"{word.SpecialWord!.Trim()} <> {word.BgWord!.Trim()}");
            }
            //string[] arrResult = hsResult.ToString()!.Split('-').ToArray();

            //richTextBoxResult.Text = string.Join('\n', hsResult.Select(w => new { w.SpecialWord, w.BgWord}).OrderBy(w => w.SpecialWord));

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultSpecialWordToBgWord.Count}";
        }

        private void AddSpecialBgWords(HashSet<string> hsCheckForRepeatSpecialAndBgWordsLine, string currentSpecialWord, string currentBgWord)
        {
            int countOnHs = hsCheckForRepeatSpecialAndBgWordsLine.Count();

            hsCheckForRepeatSpecialAndBgWordsLine.Add($"{currentSpecialWord} - {currentBgWord}");

            if (hsCheckForRepeatSpecialAndBgWordsLine.Count == countOnHs + 1)
            {
                SpecialWordToBgWord specialWordToBgWord = new SpecialWordToBgWord();

                //specialWordToBgWord.Id = id;
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

            progressBarLoad.Maximum = 0;

            labelResultItems.Text = $"Items:";

            hsResultSpecialWordToBgWord.Clear();
            hsResultWordNameToBgName.Clear();
        }
        private void buttonCompareWordlBgNames_Click(object sender, EventArgs e)
        {
            StringBuilder sbRepeatsLatLetters = new StringBuilder();

            string getRepeatWord = string.Empty;


            //buttonName = "CompareWordlBgNames";

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

            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = getWorldNames!.Count();

            foreach (var latW in getWorldNames!)
            {
                progressBarLoad.Value++;

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
                sbResult.AppendLine($"{name.WorldName} <> {name.BgName}");
            }
            //string[] arrResult = hsResult.ToString()!.Split('-').ToArray();

            //richTextBoxResult.Text = string.Join('\n', hsResult.Select(w => new { w.SpecialWord, w.BgWord}).OrderBy(w => w.SpecialWord));

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultWordNameToBgName.Count}";

            buttonDuplicatedWorldNames.Enabled = true;
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
            //comboBoxChoise.DropDownStyle = ComboBoxStyle.DropDownList;
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
                sbDuplicateLatWords.AppendLine($"{duplicate.SpecialWord} <-> {duplicate.BgWord}");
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
                sbDuplicateLatWords.AppendLine($"{duplicate.WorldName} <-> {duplicate.BgName}");
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
                        string[] arrLines = result.Split('\n');

                        for (int i = 0; i < arrLines.Length; i++)
                        {
                            string getWords = arrLines[i];
                            string[] arrWords = getWords.Split('<', '>');

                            if (getWords != "")
                            {
                                WorldBgNames worldBgNames = new WorldBgNames();

                                //worldBgNames.Name = lettersRelationsName;

                                worldBgNames.WorldName = arrWords[0].Replace(" ", "");
                                worldBgNames.WorldNameLength = arrWords[0].Replace(" ", "").Length;

                                worldBgNames.BgName = arrWords[2].Replace(" ", "");
                                worldBgNames.BgNameLength = arrWords[2].Replace(" ", "").Length;

                                worldBgNames.LetRelatId = id;

                                worldBgNames.Comparison = comparison;

                                lsWords.Add(worldBgNames);
                            }
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
                sbDuplicatePtWords.AppendLine($"{duplicate.SpecialWord} <-> {duplicate.BgWord}");
            }

            richTextBoxResult.Text = string.Join("\n", sbDuplicatePtWords);

            labelResultItems.Text = $"Items: {countLines}";
        }

        private void buttonCompareEqualsPtBgLetters_Click(object sender, EventArgs e)
        {
            //hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            //StringBuilder sbMatchedWords = new StringBuilder();

            //HashSet<string> hsCheckForRepeatSpecialAndBgWordLine = new HashSet<string>();

            // buttonName = "ComparePtBgWords";

            //string getRepeatWord = string.Empty;


            //string[] arrLatinLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //string[] arrBgLetters = { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ъ", "Ю", "Я" };

            //var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).Where(w => w.PtName.Length > 1).ToHashSet();

            richTextBoxResult.Text = string.Empty;

            HashSet<string> hsForeignWords = new HashSet<string>();

            if (comboBoxLanguage.Text == "PT-BG")
            {
                var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

                hsForeignWords = getPtWords!.Select(w => w.PtName).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "LAT-BG")
            {
                var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

                hsForeignWords = getLatWords!.Select(w => w.LatWord).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "EN-BG")
            {
                var getEnWords = context.EnNames?.Select(w => new { w.Id, w.EnName }).ToHashSet();

                hsForeignWords = getEnWords!.Select(w => w.EnName).ToHashSet();
            }
            else
            {
                MessageBox.Show("Choice Lenguage From");
                return;
            }

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).Where(w => w.BgWord.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.ToString().Trim().Split('\n', '-');

            string[] arrSelectedLetters = new string[arrOriginalSelectedLetters.Length];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                if (arrOriginalSelectedLetters[i] != "")
                {
                    arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
                }
            }
            //arrSelectedLetters
            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = hsForeignWords!.Count;

            //============ Convert Special words to Bg letters =======================================================

            //progressBarConvert.Minimum = 0;
            //progressBarConvert.Maximum = hsForeignWords.Count();

            //HashSet<string> hsForeignWords = getPtWords.Select(w => w.PtName).ToHashSet();

            //HashSet<string> hsConvertedWords = ConvertForeignWordsToBgLetters(hsForeignWords, arrSelectedLetters);

            //=========================================================================================================
            StringBuilder sbWords = new StringBuilder();
            int countMatchWords = 0;

            foreach (string fWord in hsForeignWords)
            {
                StringBuilder convertedWord = new StringBuilder();

                // ============ Convert each foreign word to Bg letters ==================
                for (int j = 0; j < fWord.Length; j++)
                {
                    string specialLetter = fWord[j].ToString();

                    for (int i = 0; i < arrSelectedLetters.Length - 1; i++)
                    {
                        if (specialLetter == arrSelectedLetters[i])
                        {
                            convertedWord.Append(arrSelectedLetters[i + 1].ToUpper());
                        }
                    }
                }
                // =======================================================================

                if (checkBoxSameLength.Checked)
                {
                    if (checkBoxDontRepeatLetters.Checked == false)
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == convertedWord.Length))
                        {
                            if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                            {
                                sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                countMatchWords++;
                            }
                        }
                    }
                    else if (checkBoxDontRepeatLetters.Checked)
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == convertedWord.Length))
                        {
                            HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                            HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                            HashSet<char> hsLetters = new HashSet<char>();

                            for (int i = 0; i < bgW.BgWord.Length; i++)
                            {
                                if (!hsLetters.Contains(bgW.BgWord[i]))
                                {
                                    hsLetters.Add(bgW.BgWord[i]);
                                }
                            }

                            if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                            {
                                sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                countMatchWords++;
                            }
                        }
                    }
                }
                else
                {
                    if (comboBoxChoise.Text == "EnWord > BgWord" || comboBoxChoise.Text == "PtWord > BgWord" || comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < convertedWord.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                    countMatchWords++;
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < convertedWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgWord.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgWord[i]))
                                    {
                                        hsLetters.Add(bgW.BgWord[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                    countMatchWords++;
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "EnWord < BgWord" || comboBoxChoise.Text == "PtWord < BgWord" || comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > convertedWord.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                    countMatchWords++;
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > convertedWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgWord.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgWord[i]))
                                    {
                                        hsLetters.Add(bgW.BgWord[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgWord}");
                                    countMatchWords++;
                                }
                            }
                        }
                    }
                }
                progressBarLoad.Value++;
            }


            //foreach (var ptW in getPtWords!)
            //{
            //    progressBarLoad.Value++;

            //    string getCurrentPtWord = ptW.PtName.ToString()!;

            //    id = ptW.Id;

            //    string[] getPtWord = getCurrentPtWord!.Split(',', StringSplitOptions.TrimEntries);

            //    if (checkBoxSameLength.Checked)
            //    {
            //        if (getPtWord.Length > 1)
            //        {
            //            for (int i = 0; i < getPtWord.Length; i++)
            //            {
            //                string currentPtWord = getPtWord[i].ToUpper();

            //                if (getBgWords != null)
            //                {
            //                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentPtWord.Length))
            //                    {
            //                        string currentBgWord = bgW.BgWord.ToUpper();

            //                      countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                    }
            //                }
            //            }
            //        }
            //        else if (getPtWord.Length == 1)
            //        {
            //            string currentPtWord = getPtWord[0];

            //            //if (checkBoxSameLength.Checked)
            //            //{
            //            if (getBgWords != null)
            //            {
            //                foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == currentPtWord.Length))
            //                {
            //                    string currentBgWord = bgW.BgWord;

            //                    countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                }
            //            }
            //            //}
            //        }
            //    }
            //    else if (!checkBoxSameLength.Checked)
            //    {
            //        if (comboBoxChoise.Text == "PtWord > BgWord")
            //        {
            //            string getCurrentPtLetter = string.Empty;
            //            string getCurrentBgLetter = string.Empty;

            //            if (getPtWord.Length > 1)
            //            {
            //                for (int i = 0; i < getPtWord.Length; i++)
            //                {
            //                    string currentPtWord = getPtWord[i].ToUpper();

            //                    if (getBgWords != null)
            //                    {
            //                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
            //                        {
            //                            string currentBgWord = bgW.BgWord.ToUpper();

            //                            countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                        }
            //                    }
            //                }
            //            }
            //            else if (getPtWord.Length == 1)
            //            {
            //                string currentPtWord = getPtWord[0].ToUpper();

            //                //if (checkBoxSameLength.Checked)
            //                //{
            //                if (getBgWords != null)
            //                {
            //                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
            //                    {
            //                        string currentBgWord = bgW.BgWord.ToUpper();

            //                        countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                    }
            //                }
            //                //}
            //            }
            //        }
            //        else if (comboBoxChoise.Text == "PtWord < BgWord")
            //        {
            //            string getCurrentPtLetter = string.Empty;
            //            string getCurrentBgLetter = string.Empty;

            //            if (getPtWord.Length > 1)
            //            {
            //                for (int i = 0; i < getPtWord.Length; i++)
            //                {
            //                    string currentPtWord = getPtWord[i].ToUpper();
            //                    //string currentLatWord = "NISI"; //getLatWord[i].ToUpper();

            //                    if (getBgWords != null)
            //                    {
            //                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
            //                        {
            //                            string currentBgWord = bgW.BgWord.ToUpper();
            //                            //string currentBgWord = "НИ"; //bgW.BgWord.ToUpper();

            //                            countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                        }
            //                    }
            //                }
            //            }
            //            else if (getPtWord.Length == 1)
            //            {
            //                string currentPtWord = getPtWord[0].ToUpper();
            //                //string currentLatWord = "NISI"; // getLatWord[0].ToUpper();

            //                //if (checkBoxSameLength.Checked)
            //                //{
            //                if (getBgWords != null)
            //                {
            //                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > currentPtWord.Length && w.BgWord.Length > 1).ToHashSet())
            //                    {
            //                        string currentBgWord = bgW.BgWord.ToUpper();
            //                        //string currentBgWord = "НИ"; // bgW.BgWord.ToUpper();

            //                        countMatchedWords = AddMatchedWords(countMatchedWords, sbMatchedWords, arrSelectedLetters, currentPtWord, currentBgWord);
            //                    }
            //                }
            //                //}
            //            }
            //        }
            //    }
            //}

            richTextBoxResult.Text = sbWords.ToString();
            //richTextBoxResult.Text = sbMatchedWords.ToString();

            labelResultItems.Text = $"Items: {countMatchWords}";
        }


        private void button1_Click(object sender, EventArgs e)
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

            //var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).Where(w => w.PtName.Length > 1).ToHashSet();
            HashSet<string> hsForeignWords = new HashSet<string>();

            if (comboBoxLanguage.Text == "PT-BG")
            {
                var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

                hsForeignWords = getPtWords!.Select(w => w.PtName).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "LAT-BG")
            {
                var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

                hsForeignWords = getLatWords!.Select(w => w.LatWord).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "EN-BG")
            {
                var getEnWords = context.EnNames?.Select(w => new { w.Id, w.EnName }).ToHashSet();

                hsForeignWords = getEnWords!.Select(w => w.EnName).ToHashSet();
            }
            else
            {
                MessageBox.Show("Choice Lenguage From");
                return;
            }

            var getBgNames = context.BgNames?.Select(w => new { w.Id, w.BgName, w.Length }).Where(w => w.BgName.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.ToString().Trim().Split('\n', '-');

            string[] arrSelectedLetters = new string[arrOriginalSelectedLetters.Length];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                if (arrOriginalSelectedLetters[i] != "")
                {
                    arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
                }
            }
            //arrSelectedLetters
            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = hsForeignWords!.Count;

            //============ Convert Special words to Bg letters =======================================================

            //progressBarConvert.Minimum = 0;
            //progressBarConvert.Maximum = hsForeignWords.Count();

            //HashSet<string> hsForeignWords = getPtWords.Select(w => w.PtName).ToHashSet();

            //HashSet<string> hsConvertedWords = ConvertForeignWordsToBgLetters(hsForeignWords, arrSelectedLetters);

            //=========================================================================================================
            StringBuilder sbWords = new StringBuilder();
            int countMatchWords = 0;

            foreach (string fWord in hsForeignWords)
            {
                StringBuilder convertedWord = new StringBuilder();

                for (int j = 0; j < fWord.Length; j++)
                {
                    string specialLetter = fWord[j].ToString();

                    for (int i = 0; i < arrSelectedLetters.Length - 1; i++)
                    {
                        if (specialLetter == arrSelectedLetters[i])
                        {
                            convertedWord.Append(arrSelectedLetters[i + 1].ToUpper());
                        }
                    }
                }

                if (checkBoxSameLength.Checked)
                {
                    if (checkBoxDontRepeatLetters.Checked == false)
                    {
                        foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == convertedWord.Length))
                        {
                            if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                            {
                                sbWords.AppendLine($"{fWord} <> {bgW.BgName}");

                                SpecialWordToBgWord specialWordToBgWord = new SpecialWordToBgWord()
                                {
                                    SpecialWord = fWord,
                                    BgWord = bgW.BgName
                                };


                                //hsResultSpecialWordToBgWord.Add()
                                countMatchWords++;
                            }
                        }
                    }
                    else if (checkBoxDontRepeatLetters.Checked)
                    {
                        foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == convertedWord.Length))
                        {
                            HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                            HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                            HashSet<char> hsLetters = new HashSet<char>();

                            for (int i = 0; i < bgW.BgName.Length; i++)
                            {
                                if (!hsLetters.Contains(bgW.BgName[i]))
                                {
                                    hsLetters.Add(bgW.BgName[i]);
                                }
                            }

                            if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                            {
                                sbWords.AppendLine($"{fWord} <> {bgW.BgName}");
                                countMatchWords++;
                            }
                        }
                    }
                }
                else
                {
                    if (comboBoxChoise.Text == "EnWord > BgWord" || comboBoxChoise.Text == "PtWord > BgWord" || comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < convertedWord.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgName}");
                                    countMatchWords++;
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < convertedWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgName.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgName[i]))
                                    {
                                        hsLetters.Add(bgW.BgName[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgName}");
                                    countMatchWords++;
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "EnWord < BgWord" || comboBoxChoise.Text == "PtWord < BgWord" || comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > convertedWord.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgName}");
                                    countMatchWords++;
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > convertedWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgName.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgName[i]))
                                    {
                                        hsLetters.Add(bgW.BgName[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && hsLetters.Count() == convertedWord.Length)
                                {
                                    sbWords.AppendLine($"{fWord} <> {bgW.BgName}");
                                    countMatchWords++;
                                }
                            }
                        }
                    }
                }
                progressBarLoad.Value++;
            }
        }

        private void buttonCompareForeignBgWords_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text = string.Empty;

            hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            HashSet<string> hsForeignWords = new HashSet<string>();

            if (comboBoxLanguage.Text == "PT-BG")
            {
                var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

                hsForeignWords = getPtWords!.Select(w => w.PtName).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "LAT-BG")
            {
                var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

                hsForeignWords = getLatWords!.Select(w => w.LatWord).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "EN-BG")
            {
                var getEnWords = context.EnNames?.Select(w => new { w.Id, w.EnName }).ToHashSet();

                hsForeignWords = getEnWords!.Select(w => w.EnName).ToHashSet();
            }
            else
            {
                MessageBox.Show("Choice Lenguage From");
                return;
            }

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).Where(w => w.BgWord.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.ToString().Trim().Split('\n', '-');

            string[] arrSelectedLetters = new string[arrOriginalSelectedLetters.Length];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                if (arrOriginalSelectedLetters[i] != "")
                {
                    arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
                }
            }
            //arrSelectedLetters
            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = hsForeignWords!.Count;

            //============ Convert Special words to Bg letters =======================================================

            //progressBarConvert.Minimum = 0;
            //progressBarConvert.Maximum = hsForeignWords.Count();

            //HashSet<string> hsForeignWords = getPtWords.Select(w => w.PtName).ToHashSet();

            //HashSet<string> hsConvertedWords = ConvertForeignWordsToBgLetters(hsForeignWords, arrSelectedLetters);

            //=========================================================================================================
            //StringBuilder sbWords = new StringBuilder();

            int countMatchWords = 0;

            foreach (string fWord in hsForeignWords)
            {
                StringBuilder convertedWord = new StringBuilder();

                // ============ Convert each foreign word to Bg letters ==================

                for (int j = 0; j < fWord.Length; j++)
                {
                    string specialLetter = fWord[j].ToString().ToUpper();

                    for (int i = 0; i < arrSelectedLetters.Length - 1; i++)
                    {
                        if (specialLetter == arrSelectedLetters[i])
                        {
                            convertedWord.Append(arrSelectedLetters[i + 1].ToUpper().Trim());
                        }
                    }
                }

                // =======================================================================

                if (checkBoxSameLength.Checked)
                {
                    foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == convertedWord.Length))
                    {
                        if (convertedWord.ToString().Contains(bgW.BgWord))
                        {
                            AddWord(fWord, bgW.BgWord);
                        }
                    }
                }
                else
                {
                    if (comboBoxChoise.Text == "EnWord > BgWord" || comboBoxChoise.Text == "PtWord > BgWord" || comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < convertedWord.Length))
                        {
                            if (convertedWord.ToString().Contains(bgW.BgWord))
                            {
                                AddWord(fWord, bgW.BgWord);
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "EnWord < BgWord" || comboBoxChoise.Text == "PtWord < BgWord" || comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > convertedWord.Length))
                        {
                            if (bgW.BgWord.Contains(convertedWord.ToString()))
                            {
                                AddWord(fWord, bgW.BgWord);
                            }
                        }
                    }
                }
                progressBarLoad.Value++;
            }
            StringBuilder sbResult = new StringBuilder();

            foreach (var w in hsResultSpecialWordToBgWord.OrderBy(w => w.SpecialWord).ThenBy(w => w.BgWord))
            {
                sbResult.AppendLine($"{w.SpecialWord} <> {w.BgWord}");
            }

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultSpecialWordToBgWord.Count()}";

            buttonDuplicatedForeignWords.Enabled = true;
        }

        private void AddWord(string fWord, string bgW)
        {
            SpecialWordToBgWord specialWordToBgWord = new SpecialWordToBgWord()
            {
                SpecialWord = fWord,
                BgWord = bgW
            };

            hsResultSpecialWordToBgWord.Add(specialWordToBgWord);

            //sbWords.AppendLine($"{fWord} <> {bgW}");

            //countMatchWords++;

            //return countMatchWords;
        }

        private void buttonDuplicatedForeignWords_Click(object sender, EventArgs e)
        {
            StringBuilder sbDuplicatePtWords = new StringBuilder();
            int countLines = 0;

            var duplicates = hsResultSpecialWordToBgWord.GroupBy(line => line.SpecialWord).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

            foreach (var duplicate in duplicates.OrderBy(w => w.SpecialWord))
            {
                countLines++;
                sbDuplicatePtWords.AppendLine($"{duplicate.SpecialWord} <-> {duplicate.BgWord}");
            }

            richTextBoxResult.Text = string.Join("\n", sbDuplicatePtWords);

            labelResultItems.Text = $"Items: {countLines}";
        }

        private void buttonCompareWordsWithEqualsLetters_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text = string.Empty;

            hsResultSpecialWordToBgWord = new HashSet<SpecialWordToBgWord>();

            HashSet<string> hsForeignWords = new HashSet<string>();

            if (comboBoxLanguage.Text == "PT-BG")
            {
                var getPtWords = context.PtNames?.Select(w => new { w.Id, w.PtName }).ToHashSet();

                hsForeignWords = getPtWords!.Select(w => w.PtName).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "LAT-BG")
            {
                var getLatWords = context.LatWords?.Select(w => new { w.Id, w.LatWord }).ToHashSet();

                hsForeignWords = getLatWords!.Select(w => w.LatWord).ToHashSet();
            }
            else if (comboBoxLanguage.Text == "EN-BG")
            {
                var getEnWords = context.EnNames?.Select(w => new { w.Id, w.EnName }).ToHashSet();

                hsForeignWords = getEnWords!.Select(w => w.EnName).ToHashSet();
            }
            else
            {
                MessageBox.Show("Choice Lenguage From");
                return;
            }

            var getBgWords = context.BgWords?.Select(w => new { w.Id, w.BgWord, w.Length }).Where(w => w.BgWord.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.ToString().Trim().Split('\n', '-');

            string[] arrSelectedLetters = new string[arrOriginalSelectedLetters.Length];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                if (arrOriginalSelectedLetters[i] != "")
                {
                    arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
                }
            }

            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = hsForeignWords!.Count;

            //============ Convert Special words to Bg letters =======================================================

            //progressBarConvert.Minimum = 0;
            //progressBarConvert.Maximum = hsForeignWords.Count();

            //HashSet<string> hsForeignWords = getPtWords.Select(w => w.PtName).ToHashSet();

            //HashSet<string> hsConvertedWords = ConvertForeignWordsToBgLetters(hsForeignWords, arrSelectedLetters);

            //=========================================================================================================


            string convertedForeignWord = string.Empty;

            HashSet<string> hs = new HashSet<string>();


            foreach (string fWord in hsForeignWords)
            {
                StringBuilder sbConvertedWord = new StringBuilder();
                StringBuilder sbLatinWord = new StringBuilder();

                // ============ Convert each foreign word to Bg letters ==================

                for (int j = 0; j < fWord.Length; j++)
                {
                    string specialLetter = fWord[j].ToString().ToUpper();

                    for (int i = 0; i < arrSelectedLetters.Length - 1; i++)
                    {
                        if (specialLetter == arrSelectedLetters[i])
                        {
                            sbConvertedWord.Append(arrSelectedLetters[i + 1].ToUpper());
                            sbLatinWord.Append(arrSelectedLetters[i].ToUpper());
                        }
                    }
                }

                // ========================================================================

                convertedForeignWord = sbConvertedWord.ToString().Trim().ToUpper();

                if (checkBoxSameLength.Checked)
                {
                    if (checkBoxDontRepeatLetters.Checked == false)
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == convertedForeignWord.Length))
                        {
                            if (new HashSet<char>(convertedForeignWord).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                            {

                                AddWord(fWord.ToString().ToUpper(), bgW.BgWord.Trim());
                            }
                        }
                    }
                    else if (checkBoxDontRepeatLetters.Checked)
                    {
                        foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length == fWord.Length))
                        {
                            HashSet<char> set1 = new HashSet<char>(convertedForeignWord);
                            HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                            StringBuilder sbBgWord = new StringBuilder();

                            for (int i = 0; i < bgW.BgWord.Length; i++)
                            {
                                if (!sbBgWord.ToString().Contains(bgW.BgWord[i]))
                                {
                                    sbBgWord.Append(bgW.BgWord[i]);
                                }
                            }

                            if (set1.SetEquals(set2) && sbBgWord.Length == convertedForeignWord.Length && convertedForeignWord.Length == fWord.Length)
                            {
                                AddWord(fWord.ToUpper(), bgW.BgWord.Trim());
                            }
                        }
                    }
                }
                else
                {
                    if (comboBoxChoise.Text == "EnWord > BgWord" || comboBoxChoise.Text == "PtWord > BgWord" || comboBoxChoise.Text == "LatWord > BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < convertedForeignWord.Length))
                            {
                                if (new HashSet<char>(convertedForeignWord).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                                {
                                    AddWord(fWord.ToUpper(), bgW.BgWord.Trim());
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length < convertedForeignWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedForeignWord);
                                HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                                StringBuilder sbBgWord = new StringBuilder();

                                for (int i = 0; i < bgW.BgWord.Length; i++)
                                {
                                    if (!sbBgWord.ToString().Contains(bgW.BgWord[i]))
                                    {
                                        sbBgWord.Append(bgW.BgWord[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && sbBgWord.Length == convertedForeignWord.Length && convertedForeignWord.Length == fWord.Length)
                                {
                                    AddWord(fWord.ToUpper(), bgW.BgWord.Trim());
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "EnWord < BgWord" || comboBoxChoise.Text == "PtWord < BgWord" || comboBoxChoise.Text == "LatWord < BgWord")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > convertedForeignWord.Length))
                            {
                                if (new HashSet<char>(convertedForeignWord).SetEquals(new HashSet<char>(bgW.BgWord.ToUpper())))
                                {
                                    AddWord(fWord.ToUpper(), bgW.BgWord.Trim());
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgWords!.Where(w => w.BgWord.Length > convertedForeignWord.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedForeignWord);
                                HashSet<char> set2 = new HashSet<char>(bgW.BgWord);

                                StringBuilder sbBgWord = new StringBuilder();

                                for (int i = 0; i < bgW.BgWord.Length; i++)
                                {
                                    if (!sbBgWord.ToString().Contains(bgW.BgWord[i]))
                                    {
                                        sbBgWord.Append(bgW.BgWord[i]);
                                    }
                                }

                                if (set1.SetEquals(set2) && sbBgWord.Length == convertedForeignWord.Length && convertedForeignWord.Length == fWord.Length)
                                {
                                    AddWord(fWord.ToUpper(), bgW.BgWord.Trim());
                                }
                            }
                        }
                    }
                }
                progressBarLoad.Value++;
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var w in hsResultSpecialWordToBgWord.OrderBy(w => w.SpecialWord).ThenBy(w => w.BgWord))
            {
                sbResult.AppendLine($"{w.SpecialWord!.ToUpper()} <> {w.BgWord!.ToUpper()}");
            }

            richTextBoxResult.Text = sbResult.ToString();

            labelResultItems.Text = $"Items: {hsResultSpecialWordToBgWord.Count()}";

            buttonDuplicatedForeignWords.Enabled = true;
        }

        private void buttonCompareNamesWithEqualsLetters_Click(object sender, EventArgs e)
        {
            richTextBoxResult.Text = string.Empty;

            hsResultWordNameToBgName = new HashSet<WorldBgNames>();

            var getWorldNames = context.WorldNames?.Select(w => new { w.Id, w.WorldName }).ToHashSet();

            var getBgNames = context.BgNames?.Select(w => new { w.Id, w.BgName, w.Length }).Where(w => w.BgName.Length > 1).ToHashSet();

            string selectedLetters = richTextBoxSelectedLetters.Text;

            string[] arrOriginalSelectedLetters = selectedLetters.ToString().Trim().Split('\n', '-');

            string[] arrSelectedLetters = new string[arrOriginalSelectedLetters.Length];

            for (int i = 0; i < arrOriginalSelectedLetters.Length; i++)
            {
                if (arrOriginalSelectedLetters[i] != "")
                {
                    arrSelectedLetters[i] = arrOriginalSelectedLetters[i].Replace(" ", "");
                }
            }

            progressBarLoad.Minimum = 0;
            progressBarLoad.Maximum = 0; //Clear
            progressBarLoad.Maximum = getWorldNames!.Count();

            foreach (var fWord in getWorldNames!)
            {
                StringBuilder convertedWord = new StringBuilder();

                // ============ Convert each foreign word to Bg letters ==================

                for (int j = 0; j < fWord.WorldName.Length; j++)
                {
                    string specialLetter = fWord.WorldName[j].ToString();

                    for (int i = 0; i < arrSelectedLetters.Length - 1; i++)
                    {
                        if (specialLetter == arrSelectedLetters[i])
                        {
                            convertedWord.Append(arrSelectedLetters[i + 1].ToUpper());
                        }
                    }
                }
                // =======================================================================

                if (checkBoxSameLength.Checked)
                {
                    if (checkBoxDontRepeatLetters.Checked == false)
                    {
                        foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == fWord.WorldName.Length))
                        {
                            if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                            {
                                AddWorldBgNames(fWord.WorldName, bgW.BgName);
                            }
                        }
                    }
                    else if (checkBoxDontRepeatLetters.Checked)
                    {
                        foreach (var bgW in getBgNames!.Where(w => w.BgName.Length == fWord.WorldName.Length))
                        {
                            HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                            HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                            HashSet<char> hsLetters = new HashSet<char>();

                            for (int i = 0; i < bgW.BgName.Length; i++)
                            {
                                if (!hsLetters.Contains(bgW.BgName[i]))
                                {
                                    hsLetters.Add(bgW.BgName[i]);
                                }
                            }
                            if (set1.SetEquals(set2) && hsLetters.Count == convertedWord.Length && convertedWord.Length == fWord.WorldName.Length)
                            {
                                AddWorldBgNames(fWord.WorldName, bgW.BgName);
                            }
                        }
                    }
                }
                else
                {
                    if (comboBoxChoise.Text == "WorldName > BgName")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < fWord.WorldName.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                                {
                                    AddWorldBgNames(fWord.WorldName, bgW.BgName);
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length < fWord.WorldName.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgName.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgName[i]))
                                    {
                                        hsLetters.Add(bgW.BgName[i]);
                                    }
                                }
                                if (set1.SetEquals(set2) && hsLetters.Count == convertedWord.Length && convertedWord.Length == fWord.WorldName.Length)
                                {
                                    AddWorldBgNames(fWord.WorldName, bgW.BgName);
                                }
                            }
                        }
                    }
                    else if (comboBoxChoise.Text == "WorldName < BgName")
                    {
                        if (checkBoxDontRepeatLetters.Checked == false)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > fWord.WorldName.Length))
                            {
                                if (new HashSet<char>(convertedWord.ToString()).SetEquals(new HashSet<char>(bgW.BgName.ToUpper())))
                                {
                                    AddWorldBgNames(fWord.WorldName, bgW.BgName);
                                }
                            }
                        }
                        else if (checkBoxDontRepeatLetters.Checked)
                        {
                            foreach (var bgW in getBgNames!.Where(w => w.BgName.Length > fWord.WorldName.Length))
                            {
                                HashSet<char> set1 = new HashSet<char>(convertedWord.ToString());
                                HashSet<char> set2 = new HashSet<char>(bgW.BgName);

                                HashSet<char> hsLetters = new HashSet<char>();

                                for (int i = 0; i < bgW.BgName.Length; i++)
                                {
                                    if (!hsLetters.Contains(bgW.BgName[i]))
                                    {
                                        hsLetters.Add(bgW.BgName[i]);
                                    }
                                }
                                if (set1.SetEquals(set2) && hsLetters.Count == convertedWord.Length && convertedWord.Length == fWord.WorldName.Length)
                                {
                                    AddWorldBgNames(fWord.WorldName, bgW.BgName);
                                }
                            }
                        }
                    }
                }
                progressBarLoad.Value++;
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var bgW in hsResultWordNameToBgName.OrderBy(w => w.WorldName).ThenBy(w => w.BgName))
            {
                sbResult.AppendLine($"{bgW.WorldName} <> {bgW.BgName}");
            }

            richTextBoxResult.Text = sbResult.ToString();

            buttonDuplicatedWorldNames.Enabled = true;

            labelResultItems.Text = $"Items: {hsResultWordNameToBgName.Count()}";

            MessageBox.Show("Done!");
        }

        private void buttonInsertInTablesWords_Click(object sender, EventArgs e)
        {
            List<WordTable> lsWords = new List<WordTable>();
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
                        string[] arrLines = result.Split('\n');

                        for (int i = 0; i < arrLines.Length; i++)
                        {
                            string getWords = arrLines[i];
                            string[] arrWords = getWords.Split('<', '>');

                            if (getWords != "")
                            {
                                WordTable wt = new WordTable();

                                //worldBgNames.Name = lettersRelationsName;

                                wt.FWord = arrWords[0].Replace(" ", "");
                                wt.FWordLength = arrWords[0].Replace(" ", "").Length;

                                wt.BgWord = arrWords[2].Replace(" ", "");
                                wt.BgWordLength = arrWords[2].Replace(" ", "").Length;

                                wt.LettRelationsId = id;

                                wt.Comparison = comparison;

                                lsWords.Add(wt);
                            }
                        }

                        foreach (var w in lsWords)
                        {
                            WordTable wt = new WordTable()
                            {
                                FWord = w.FWord!,
                                FWordLength = w.FWordLength,
                                BgWord = w.BgWord!,
                                BgWordLength = w.BgWordLength,
                                LettRelationsId = id,
                                Comparison = comparison,
                                DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                            };
                            context.WordTables!.Add(wt);
                        }
                        context.SaveChanges();
                        MessageBox.Show("Done!");

                        FrmWordTable wTable = new FrmWordTable();
                        wTable.Show();
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

        private void buttonViewTableWords_Click(object sender, EventArgs e)
        {
            FrmWordTable wordTable = new FrmWordTable();
            wordTable.Show();
        }

        private void toolStripButtonCompareBgWord_Click(object sender, EventArgs e)
        {
            FrmCompareBgWords_Names frmCompareBgWords_Names = new FrmCompareBgWords_Names(richTextBoxResult, labelResultItems);
            frmCompareBgWords_Names.Show();
        }
    }
}
