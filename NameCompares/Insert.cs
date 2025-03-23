using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NameCompares
{
    public partial class Insert : Form
    {
        NameComparesContext context = new NameComparesContext();
        public Insert()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            progressBarInsert.Maximum = 0;

            OpenFile();

            if (comboBoxDBLanguage.Text == "BG_W")
            {
                HashSet<string> hsBgWords = InsertIntoHashSetBgWords();

                InsertInTableBgWords(hsBgWords);
            }
            else if (comboBoxDBLanguage.Text == "EN_W")
            {
                HashSet<string> hsEnWords = InsertIntoHashSetEn();

                InsertInTableEn(hsEnWords);
            }
            else if (comboBoxDBLanguage.Text == "PT_W")
            {
                HashSet<string> hsPtWords = InsertIntoHashSetPt();

                InsertInTablePt(hsPtWords);
            }
            else if (comboBoxDBLanguage.Text == "LAT_W")
            {
                HashSet<string> hsLatWords = InsertIntoHashSetLat();

                InsertInTableLat(hsLatWords);
            }
            else if (comboBoxDBLanguage.Text == "BG_NAMES")
            {
                HashSet<BgNames> hsBgNames = InsertIntoHashSetBgNames();

                InsertInTableBgNames(hsBgNames);
            }
            else if (comboBoxDBLanguage.Text == "W_NAMES")
            {
                HashSet<EnNames> hsWorldNames = InsertIntoHashSetWorld();

                InsertInTableWorld(hsWorldNames);
            }
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxInsertDest.Text = openFileDialog1.FileName;
            }
        }
        private void InsertInTableBgNames(HashSet<BgNames> hsBgNames)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsBgNames.Count();

            var src = DateTime.Now;

            foreach (var bgName in hsBgNames.OrderBy(bgW => bgW.BgName))
            {
                BgNames nBg = new BgNames()
                {
                    //BgName = bgName.BgName.Length > 1 ? $"{bgName.BgName.ToUpper()}" : "",//enName.EnName,
                    //Description = (bgName.Description != null) ? $"{bgName.Description}" : "",
                    //Length = bgName.BgName.Length,
                    //DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),

                    BgName = bgName.BgName,
                    Description = bgName.Description,
                    Length = bgName.BgName.Length,
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };
                context.BgNames!.Add(nBg);

                progressBarInsert.Value++;
            }
            context.SaveChanges();

            MessageBox.Show("Import Done!");
        }
        private void InsertInTableBgWords(HashSet<string> hsBgWords)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsBgWords.Count();

            var src = DateTime.Now;

            foreach (var bgW in hsBgWords.OrderBy(bgW => bgW))
            {
                BgWords nBg = new BgWords()
                {
                    //BgName = bgName.BgName.Length > 1 ? $"{bgName.BgName.ToUpper()}" : "",//enName.EnName,
                    //Description = (bgName.Description != null) ? $"{bgName.Description}" : "",
                    //Length = bgName.BgName.Length,
                    //DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),

                    BgWord = bgW,
                    Description = "",
                    Length = bgW.Length,
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };
                context.BgWords!.Add(nBg);

                progressBarInsert.Value++;
            }
            context.SaveChanges();
            MessageBox.Show("Import Done!");
        }
        private HashSet<string> InsertIntoHashSetBgWords()
        {
            //HashSet<BgNames> hsBgName = new HashSet<BgNames>();
            HashSet<string> hsBgWords = new HashSet<string>();

            //char[] arrEnLetters = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
            //char[] arrBgLetters = { 'А', 'а', 'Б', 'б', 'В', 'в', 'Г', 'г', 'Д', 'д', 'Е', 'е', 'Ж', 'ж', 'З', 'з', 'И', 'и', 'Й', 'й', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'П', 'п', 'Р', 'р', 'С', 'с', 'Т', 'т', 'У', 'у', 'Ф', 'ф', 'Х', 'х', 'Ц', 'ц', 'Ч', 'ч', 'Ъ', 'ь', 'Ю', 'ю', 'Я', 'я' };
            string word = string.Empty;
            string currentWord = string.Empty;
            string description = string.Empty;
            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

            var getBgWords = context.BgWords?.Select(w => w.BgWord).ToHashSet();

            foreach (var line in lines)
            {
                //string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                string[] allNamesInLine = line.Split('/', ' ');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)

                word = allNamesInLine[0].ToUpper();

                if (word != "")
                {
                    if (word.Length > 1)
                    {
                        if (!getBgWords!.Contains(word))
                        {
                            //BgWords bgWords = new BgWords();

                            //bgWords.BgWord = currentWord;

                            hsBgWords.Add(word!);
                        }
                    }
                }
            }
            return hsBgWords;
        }
        private void InsertInTableEn(HashSet<string> hsEnNames)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsEnNames.Count();

            var src = DateTime.Now;

            foreach (var enName in hsEnNames.OrderBy(n => n))
            {
                EnNames nEn = new EnNames()
                {
                    //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
                    //Description = enName.Description != null ? $"{enName.Description}" : "",
                    //Length = enName.EnName!.Length,
                    EnName = enName,
                    Description = "",
                    Length = enName.Length,
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };
                context.EnNames!.Add(nEn);

                progressBarInsert.Value++;
            }
            context.SaveChanges();

            MessageBox.Show("Import Done!");
        }
        private void InsertInTableWorld(HashSet<EnNames> hsWorldNames)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsWorldNames.Count();

            var src = DateTime.Now;

            foreach (var wName in hsWorldNames.OrderBy(n => n.EnName))
            {
                WorldNames nWorld = new WorldNames()
                {
                    //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
                    //Description = enName.Description != null ? $"{enName.Description}" : "",
                    //Length = enName.EnName!.Length,
                    WorldName = wName.EnName,
                    Description = wName.Description,
                    Length = wName.EnName.Length,
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };
                context.WorldNames!.Add(nWorld);

                progressBarInsert.Value++;
            }
            context.SaveChanges();

            MessageBox.Show("Import Done!");
        }
        private HashSet<BgNames> InsertIntoHashSetBgNames()
        {
            HashSet<BgNames> hsBgNames = new HashSet<BgNames>();

            string word = string.Empty;
            string currentWord = string.Empty;
            string description = string.Empty;
            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

            if (lines.Length > 1)
            {
                //var getWorldNames = context.WorldNames!.Select(w => w.WorldName).ToHashSet();

                foreach (var line in lines)
                {
                    //string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                    string[] allNamesInLine = line.Split();//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)

                    for (int i = 0; i < allNamesInLine.Length; i++)
                    {
                        word = allNamesInLine[i].ToUpper();

                        if (word != "" && word.Length > 1)
                        {
                            string[] arrGetWord = word.Split('–', StringSplitOptions.RemoveEmptyEntries);

                            //if (arrGetWord.Length > 1)
                            if (allNamesInLine.Length > 1)
                            {
                                description = string.Empty;

                                for (int j = 0; j < allNamesInLine.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        currentWord = allNamesInLine[j].Replace(" ", "");
                                    }
                                    else
                                    {
                                        description += allNamesInLine[j] + " ";
                                    }
                                }

                            }
                            else if (arrGetWord.Length == 1)
                            {
                                currentWord = arrGetWord[0].Replace(" ", "");
                                //description = arrGetWord[1];
                            }

                            if ((arrGetWord.Length == 1 || arrGetWord.Length > 1) && currentWord.Length > 1)
                            {
                                BgNames bgNames = new BgNames();
                                bgNames.BgName = currentWord;
                                bgNames.Description = description;

                                hsBgNames.Add(bgNames!);

                                break;
                            }
                        }
                    }
                }
            }
            return hsBgNames;
        }
        //private void InsertInTableWorld(HashSet<EnNames> hsWorldNames)
        //{
        //    progressBarInsert.Minimum = 0;
        //    progressBarInsert.Maximum = hsWorldNames.Count();

        //    var src = DateTime.Now;

        //    foreach (var wName in hsWorldNames.OrderBy(n => n.EnName))
        //    {
        //        WorldNames nWorld = new WorldNames()
        //        {
        //            //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
        //            //Description = enName.Description != null ? $"{enName.Description}" : "",
        //            //Length = enName.EnName!.Length,
        //            WorldName = wName.EnName,
        //            Description = wName.Description,
        //            Length = wName.EnName.Length,
        //            DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
        //        };
        //        context.WorldNames!.Add(nWorld);

        //        progressBarInsert.Value++;
        //    }
        //    context.SaveChanges();

        //    MessageBox.Show("Import Done!");
        //}
        private HashSet<EnNames> InsertIntoHashSetWorld()
        {
            HashSet<EnNames> hsWorldWords = new HashSet<EnNames>();

            string word = string.Empty;
            string currentWord = string.Empty;
            string description = string.Empty;
            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

            if (lines.Length > 1)
            {
                //var getWorldNames = context.WorldNames!.Select(w => w.WorldName).ToHashSet();

                foreach (var line in lines)
                {
                    //string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                    string[] allNamesInLine = line.Split(',');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)

                    for (int i = 0; i < allNamesInLine.Length; i++)
                    {
                        word = allNamesInLine[i].ToUpper();

                        if (word != "" && word.Length > 1)
                        {
                            string[] arrGetWord = word.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                            if (arrGetWord.Length > 1)
                            {
                                description = string.Empty;

                                for (int j = 0; j < arrGetWord.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        currentWord = arrGetWord[j];
                                    }
                                    else
                                    {
                                        description += arrGetWord[j] + " ";
                                    }
                                }

                            }
                            else if (arrGetWord.Length == 1)
                            {
                                currentWord = arrGetWord[0];
                            }

                            if ((arrGetWord.Length == 1 || arrGetWord.Length > 1) && currentWord.Length > 1)
                            {
                                EnNames enNames = new EnNames();
                                enNames.EnName = currentWord;
                                enNames.Description = description;

                                hsWorldWords.Add(enNames!);
                            }
                        }
                    }
                }
            }
            return hsWorldWords;
        }
        private HashSet<string> InsertIntoHashSetEn()
        {
            //HashSet<EnNames> hsEnWords = new HashSet<EnNames>();
            HashSet<string> hsEnWords = new HashSet<string>();

            char[] arrEnLetters = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
            //char[] arrBgLetters = { 'А', 'а', 'Б', 'б', 'С', 'с', 'Д', 'д', 'Е', 'е', 'Ф', 'ф', 'Г', 'г', 'Н', 'н', 'И', 'и', 'Ж', 'ж', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'Р', 'р', 'Q', 'q', 'R', 'r', 'S', 's', 'Т', 'т', 'U', 'u', 'V', 'v', 'W', 'w', 'Х', 'х', 'У', 'у', 'З', 'з' };
            string word = string.Empty;
            //string currentWord = string.Empty;
            //bool isEqualLength = false;
            string wordWithoutSpecialSymbols = string.Empty;
            var getEnWords = context.EnNames?.Select(w => w.EnName).ToHashSet();

            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

            if (lines.Length > 1)
            {
                var getWorldNames = context.WorldNames!.Select(w => w.WorldName).ToHashSet();

                foreach (var line in lines)
                {
                    //string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                    //string[] allNamesInLine = line.Split(',');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                    string[] allNamesInLine = line.Split('/', ' ');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)

                    word = allNamesInLine[0].ToUpper();

                    if (word != "")
                    {
                        if (word.Length > 1)
                        {
                            if (!getEnWords!.Contains(word))
                            {
                                //BgWords bgWords = new BgWords();

                                //bgWords.BgWord = currentWord;

                                hsEnWords.Add(word!);
                            }
                        }
                    }
                    //for (int i = 0; i < allNamesInLine.Length; i++)
                    //{
                    //    word = allNamesInLine[i].ToUpper();

                    //    if (word != "")
                    //    {
                    //        wordWithoutSpecialSymbols = string.Empty;
                    //        isEqualLength = false;

                    //        for (int j = 0; j < word.Length; j++)
                    //        {
                    //            char letter = word[j];

                    //            for (int k = 0; k < arrEnLetters.Length; k++)
                    //            {
                    //                if (letter == arrEnLetters[k])
                    //                {
                    //                    wordWithoutSpecialSymbols += letter;
                    //                }
                    //                if (wordWithoutSpecialSymbols.Length == word.Length)
                    //                {
                    //                    isEqualLength = true;
                    //                    break;
                    //                }
                    //            }
                    //            if (isEqualLength)
                    //            {
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    if (wordWithoutSpecialSymbols != null && wordWithoutSpecialSymbols != "" && !getWorldNames.Contains(wordWithoutSpecialSymbols))
                    //    {
                    //        hsEnWords.Add(wordWithoutSpecialSymbols!);
                    //    }
                    //}
                }
            }

            return hsEnWords;
        }
        private void InsertInTablePt(HashSet<string> hsPtNames)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsPtNames.Count();

            var src = DateTime.Now;

            foreach (var ptName in hsPtNames.OrderBy(n => n))
            {
                PtNames nPt = new PtNames()
                {
                    //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
                    //Description = enName.Description != null ? $"{enName.Description}" : "",
                    //Length = enName.EnName!.Length,
                    PtName = ptName,
                    Description = "",
                    Length = ptName.Length,
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };
                context.PtNames!.Add(nPt);

                progressBarInsert.Value++;
                //break;
            }
            context.SaveChanges();

            MessageBox.Show("Import Done!");
        }
        private void InsertInTableLat(HashSet<string> hsLatNames)
        {
            progressBarInsert.Minimum = 0;
            progressBarInsert.Maximum = hsLatNames.Count();

            var src = DateTime.Now;

            foreach (var latName in hsLatNames)
            {
                LatWords nLat = new LatWords()
                {
                    //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
                    //Description = enName.Description != null ? $"{enName.Description}" : "",
                    //Length = enName.EnName!.Length,
                    LatWord = latName,//Lat word
                    EnWord = "",//En word
                                //Length = latName.Length,
                    Type = "",
                    DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
                };

                context.LatWords!.Add(nLat);

                progressBarInsert.Value++;
            }

            //foreach (var latName in hsLatNames)
            //{
            //    string[] arrWords = latName.Split(": ", '\t', StringSplitOptions.TrimEntries);

            //    arrWords[0].Replace("\t", "");
            //    //arrWords[0].Replace(" ", "");
            //    string latWord = arrWords[0];

            //    string[] arrLatWord = latWord.Split('(');

            //    if (arrWords.Length == 1 && arrLatWord.Length == 1)
            //    {
            //        //arrWords[1] = arrWords[0];

            //        LatWords nLat = new LatWords()
            //        {
            //            //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
            //            //Description = enName.Description != null ? $"{enName.Description}" : "",
            //            //Length = enName.EnName!.Length,
            //            LatWord = arrWords[0],//Lat word
            //            EnWord = arrWords[0],//En word
            //                                 //Length = latName.Length,
            //            Type = "",
            //            DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
            //        };
            //        context.LatWords!.Add(nLat);
            //    }
            //    else if (arrWords.Length == 1 && arrLatWord.Length == 2)
            //    {
            //        //arrWords[1] = arrWords[0];
            //        arrLatWord[1] = arrLatWord[1].Replace(")", "");

            //        LatWords nLat = new LatWords()
            //        {
            //            //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
            //            //Description = enName.Description != null ? $"{enName.Description}" : "",
            //            //Length = enName.EnName!.Length,
            //            LatWord = arrLatWord[0],//Lat word
            //            EnWord = arrWords[0],//En word
            //                                 //Length = latName.Length,
            //            Type = arrLatWord[1],
            //            DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
            //        };
            //        context.LatWords!.Add(nLat);
            //    }
            //    else if (arrWords.Length == 2 && arrLatWord.Length == 1)
            //    {
            //        LatWords nLat = new LatWords()
            //        {
            //            //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
            //            //Description = enName.Description != null ? $"{enName.Description}" : "",
            //            //Length = enName.EnName!.Length,
            //            LatWord = arrWords[0],//Lat word
            //            EnWord = arrWords[1],//En word
            //                                 //Length = latName.Length,
            //            Type = "",
            //            DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
            //        };
            //        context.LatWords!.Add(nLat);
            //    }
            //    else if (arrWords.Length == 2 && arrLatWord.Length == 2)
            //    {
            //        arrLatWord[1] = arrLatWord[1].Replace(")", "");

            //        LatWords nLat = new LatWords()
            //        {
            //            //EnName = enName.EnName.Length > 1 ? $"{enName.EnName.ToUpper()}" : "",//enName.EnName,
            //            //Description = enName.Description != null ? $"{enName.Description}" : "",
            //            //Length = enName.EnName!.Length,
            //            LatWord = arrLatWord[0],//Lat word
            //            EnWord = arrWords[1],//En word
            //                                 //Length = latName.Length,
            //            Type = arrLatWord[1],
            //            DateTime = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0),
            //        };
            //        context.LatWords!.Add(nLat);
            //    }
            //    progressBarInsert.Value++;

            //    //countWords++;
            //    //break;
            //    //}
            //}
            context.SaveChanges();

            MessageBox.Show("Import Done!");
        }
        private HashSet<string> InsertIntoHashSetPt()
        {
            HashSet<string> hsPtWords = new HashSet<string>();

            //char[] arrLatLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            //char[] arrBgLetters = { 'А', 'а', 'Б', 'б', 'С', 'с', 'Д', 'д', 'Е', 'е', 'Ф', 'ф', 'Г', 'г', 'Н', 'н', 'И', 'и', 'Ж', 'ж', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'Р', 'р', 'Q', 'q', 'R', 'r', 'S', 's', 'Т', 'т', 'U', 'u', 'V', 'v', 'W', 'w', 'Х', 'х', 'У', 'у', 'З', 'з' };
            string word = string.Empty;

            string wordWithoutSpecialSymbols = string.Empty;
            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());


            var getPtWords = context.PtNames!.Select(w => w.PtName).ToHashSet();
            //var getEnWords = context.LatWords!.Select(w => w.EnWord).ToHashSet();

            foreach (var line in lines)
            {
                //string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '–', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                string[] allNamesInLine = line.Split('/', '	');
                //string[] allNamesInLine = line.Split(": ");//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                //if (lines.Length > 1)
                //{
                for (int i = 0; i < allNamesInLine.Length; i++)
                {
                    bool isDigit = false;

                    word = allNamesInLine[0].Trim().ToUpper();

                    if (word.Length > 1)
                    {
                        for (int j = 0; j < word.Length; j++)
                        {
                            if (char.IsDigit(word[j]))
                            {
                                isDigit = true;
                                break;
                            }
                        }
                        if (!isDigit)
                        {
                            if (!getPtWords.Contains(word))
                            {
                                if (word != "")
                                {
                                    hsPtWords.Add(word);

                                }
                            }
                        }
                    }
                }
                //}
            }
            //}

            return hsPtWords;
        }
        private HashSet<string> InsertIntoHashSetLat()
        {
            //HashSet<EnNames> hsEnWords = new HashSet<EnNames>();
            HashSet<string> hsLatWords = new HashSet<string>();

            //HashSet<string> hsEnWords = new HashSet<string>();

            //char[] arrLatLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            //char[] arrBgLetters = { 'А', 'а', 'Б', 'б', 'С', 'с', 'Д', 'д', 'Е', 'е', 'Ф', 'ф', 'Г', 'г', 'Н', 'н', 'И', 'и', 'Ж', 'ж', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'Р', 'р', 'Q', 'q', 'R', 'r', 'S', 's', 'Т', 'т', 'U', 'u', 'V', 'v', 'W', 'w', 'Х', 'х', 'У', 'у', 'З', 'з' };
            string word = string.Empty;

            string wordWithoutSpecialSymbols = string.Empty;
            string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

            if (lines.Length > 1)
            {
                var getLatWords = context.LatWords!.Select(w => w.LatWord).ToHashSet();
                //var getEnWords = context.LatWords!.Select(w => w.EnWord).ToHashSet();

                foreach (var line in lines)
                {
                    string[] allNamesInLine = line.Split(' ', '.', ',', '\r', '\t', '!', '?', '"', '—', '-', '\u0092', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)
                    //string[] allNamesInLine = line.Split(": ");//Split(' ', line.Length, StringSplitOptions.RemoveEmptyEntries)

                    for (int i = 0; i < allNamesInLine.Length; i++)
                    {
                        bool isDigit = false;

                        word = allNamesInLine[i].ToUpper();

                        for (int j = 0; j < word.Length; j++)
                        {
                            if (char.IsDigit(word[j]))
                            {
                                isDigit = true;
                                break;
                            }
                        }
                        if (!isDigit)
                        {
                            if (!getLatWords.Contains(word))
                            {
                                if (word != "")
                                {
                                    hsLatWords.Add(word);

                                }
                            }
                        }
                    }
                }
            }

            return hsLatWords;
        }

        private void comboBoxDBLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonInsertWord_Click(object sender, EventArgs e)
        {
            string getWord = textBoxEnterWord.Text.Trim().ToUpper();
            string table = comboBoxTables.Text;

            DateTime dateTimeNow = DateTime.Now;

            if (getWord.Length > 0 && table.Length > 0)
            {
                if (table == "BgWords")
                {
                    BgWords bgWords = new BgWords()
                    {
                        BgWord = getWord,
                        Length = getWord.Length,
                        Description = "",
                        DateTime = dateTimeNow,
                    };
                    context.Add(bgWords);

                    MessageBox.Show("Done!");
                }
                else if (table == "EnWords")
                {

                }
                else if (table == "PtWords")
                {

                }
                else if (table == "LatWords")
                {

                }
                context.SaveChanges();
            }
        }
        //private HashSet<EnNames> InsertIntoHashSetEn()
        //{
        //    HashSet<EnNames> hsEnWords = new HashSet<EnNames>();

        //    char[] arrEnLetters = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
        //    char[] arrBgLetters = { 'А', 'а', 'Б', 'б', 'С', 'с', 'Д', 'д', 'Е', 'е', 'Ф', 'ф', 'Г', 'г', 'Н', 'н', 'И', 'и', 'Ж', 'ж', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'Р', 'р', 'Q', 'q', 'R', 'r', 'S', 's', 'Т', 'т', 'U', 'u', 'V', 'v', 'W', 'w', 'Х', 'х', 'У', 'у', 'З', 'з' };
        //    string[] arrGetWords = new string[0];


        //    string[] lines = System.IO.File.ReadAllLines(textBoxInsertDest.Text.ToUpper());

        //    if (lines.Length > 1)
        //    {
        //        var getEnNames = context.EnNames!.Select(w => w.EnName).ToHashSet();

        //        foreach (var line in lines)
        //        {
        //            string[] words = line.Split(',', line.Length, StringSplitOptions.RemoveEmptyEntries);//.Split(' ', '.', ',', '!', '?', '"', '—', '-', '\'', '/', '„', '“', ':', '(', ')', ';', '«', '»', '=', '~', '♦', '•', '©', '®', '[', ']', '&', '‘', '”', '{', '}', '^', '#');

        //            bool isArrEnNameEmpty = false;

        //            arrGetWords = words[0].Split();

        //            if (arrGetWords.Length > 1)
        //            {


        //                if (arrGetWords[1].Contains("(") && arrGetWords[0] != "(" || arrGetWords[0].Contains(")"))
        //                {
        //                    string getDescription = string.Empty;

        //                    for (int i = 1; i < arrGetWords.Length; i++)
        //                    {
        //                        getDescription += arrGetWords[i] + " ";
        //                    }

        //                    string name = arrGetWords[0].ToUpper();

        //                    EnNames enNames = new EnNames();

        //                    enNames.EnName = name;

        //                    enNames.Description = getDescription;

        //                    hsEnWords.Add(enNames);
        //                    continue;
        //                }
        //            }
        //            if ((words[0].Contains("(") || words[0].Contains(")") || words[0].Contains("see")) && hsEnWords.Count > 0)
        //            {
        //                hsEnWords.OrderBy(enName => enName.EnName);

        //                var lastEnName = hsEnWords.ElementAt(hsEnWords.Count - 1);

        //                hsEnWords.Remove(lastEnName);

        //                EnNames enNames = new EnNames();

        //                enNames.EnName = lastEnName.EnName.ToUpper();
        //                enNames.Description = words[0];

        //                hsEnWords.Add(enNames);

        //                continue;
        //            }

        //            if (arrGetWords.Length >= 1)
        //            {
        //                for (int i = 0; i < words.Length; i++)
        //                {
        //                    string enName = string.Empty;
        //                    string description = string.Empty;

        //                    string[] arrEnName = words[0].ToUpper().Split("", StringSplitOptions.RemoveEmptyEntries);

        //                    string[] arrGetName = arrEnName[0].Split();

        //                    if (words[i] == " ")
        //                    {
        //                        continue;
        //                    }

        //                    if (arrGetName.Length > 1)
        //                    {
        //                        //string[] arrGetName = arrEnName[0].Split();

        //                        enName = arrGetName[0];

        //                        for (int j = 1; j < arrGetName.Length; j++)
        //                        {
        //                            if (j == arrGetName.Length - 1)
        //                            {
        //                                description += arrGetName[j];
        //                                break;
        //                            }

        //                            description += arrGetName[j] + " ";
        //                        }

        //                        for (int j = 1; j < words.Length; j++)
        //                        {
        //                            description += words[j].ToUpper() + " ";
        //                        }

        //                        arrEnName = new string[0];
        //                        isArrEnNameEmpty = true;
        //                    }
        //                    else
        //                    {
        //                        enName = words[i].ToUpper();
        //                    }

        //                    if (i == words.Length - 1 && enName != "" && enName != " ")
        //                    {
        //                        string[] getLastName = words[words.Length - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        //                        string[] arrGetLastName = getLastName[0].Split(" ");

        //                        enName = arrGetLastName[0].ToUpper();

        //                        for (int n = 1; n < getLastName.Length; n++)
        //                        {
        //                            description += getLastName[n] + " ";
        //                        }
        //                    }

        //                    string name = string.Empty;
        //                    string desc = string.Empty;

        //                    if (enName.Length > 1)
        //                    {
        //                        //string word = string.Empty;

        //                        for (int j = 0; j < enName.Length; j++)
        //                        {
        //                            char letterEn = enName[j];

        //                            if ((letterEn >= (char)65 && letterEn <= (char)90) || (letterEn >= (char)97 && letterEn <= (char)122) || letterEn == ' ')
        //                            {
        //                                name += enName[j];
        //                            }

        //                            if (letterEn >= 1040 && letterEn <= 1103 && letterEn != ' ')
        //                            {
        //                                if (arrBgLetters.Contains(letterEn))
        //                                {
        //                                    int index = Array.IndexOf(arrBgLetters, letterEn);
        //                                    char convertedLetter = arrEnLetters[index];

        //                                    name += convertedLetter;
        //                                }
        //                            }
        //                        }
        //                        name = name.Replace(" ", "");
        //                        for (int k = 0; k < description.Length; k++)
        //                        {
        //                            char letterEn = description[k];

        //                            if ((letterEn >= (char)65 && letterEn <= (char)90) || (letterEn >= (char)97 && letterEn <= (char)122) || letterEn == ' ')
        //                            {
        //                                desc += description[k];
        //                            }
        //                            if (letterEn >= 1040 && letterEn <= 1103 && letterEn == ' ')
        //                            {
        //                                if (arrBgLetters.Contains(letterEn))
        //                                {
        //                                    int index = Array.IndexOf(arrBgLetters, letterEn);
        //                                    char convertedLetter = arrEnLetters[index];

        //                                    desc += convertedLetter;
        //                                }
        //                            }
        //                        }
        //                        if ((!getEnNames.Contains(enName)) && name.Length > 0)
        //                        {
        //                            EnNames enNames = new EnNames();

        //                            enNames.EnName = name.ToUpper();
        //                            enNames.Description = desc;

        //                            hsEnWords.Add(enNames);

        //                            if (isArrEnNameEmpty)
        //                            {
        //                                break;
        //                            }
        //                        }
        //                        //}
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return hsEnWords;
        //}
    }
}
