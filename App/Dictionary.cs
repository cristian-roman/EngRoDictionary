using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exceptions;
using SearchEngine;

namespace Dictionary
{
    public partial class Dictionary : Form
    {
        private Stack<Control> labelList = new Stack<Control>(4);
        string tempSite = @"C:\Users\Cristian\source\repos\Dictionary\Definitions\Temp\TempSite.txt";
        string tempFoundWords = @"C:\Users\Cristian\source\repos\Dictionary\Definitions\Temp\tempFoundWords.txt";

        public Dictionary()
        {
            InitializeComponent();

            if (File.Exists(tempSite) == false)
                File.Create(tempSite);

            if (File.Exists(tempFoundWords) == false)
                File.Create(tempFoundWords);
        }

        private void WhenClick_Search(object sender, EventArgs e)
        {
            RemoveLinkLabels();
            StreamWriter writer = new StreamWriter(tempFoundWords);
            using (writer)
                writer.Write("");
            DefinitionBox.Font = new Font(this.Font.FontFamily, 14.25f);

            if (IsSelectedDictionary() == true)
            {
                WordBox.Text = WordBox.Text.ToLower();
                WordBox.Text = WordBox.Text.Trim();
                string word = WordBox.Text;

                if (word.Length != 0)
                {
                    if (IsCorrect(word))
                    {
                        try
                        {
                            Definition definition;
                            if ( EnglishButton.Checked == true )
                            {
                                definition = new EnglishToEnglish(word);
                            }
                            else if ( RomanianButton.Checked == true )
                            {
                                definition = new RomanianToRomanian(word);
                            }
                            else if ( EngRomButton.Checked == true )
                            {
                                definition = new EnglishToRomanian(word);
                            }
                            else
                            {
                                definition = new RomanianToEnglish(word);
                            }

                            string displayedText;
                            if (definition.GetDefinition(out displayedText)==true)
                            {
                                DefinitionBox.Text = displayedText;
                                DefinitionBox.Visible = true;
                            }
                            else
                            {
                                if (RomanianButton.Checked == true)
                                {
                                    DisplayLinkLabels();
                                }
                                else
                                    throw new WrongWordExceptionEng("Dictionary", "WhenClick_SearchButton");
                            }
                        }
                        catch(DefinitionNotFoundedException)
                        {
                            DefinitionBox.Visible = false;
                            if (RomanianButton.Checked == true)
                                MessageBox.Show(RomanianToRomanian.ErrorMessage);
                            else if (EnglishButton.Checked == true)
                                MessageBox.Show(EnglishToEnglish.ErrorMessage);
                        }
                    
                        catch(System.Net.WebException)
                        {
                            DefinitionBox.Visible = false;
                            if (RomanianButton.Checked == true)
                                MessageBox.Show(RomanianToRomanian.ErrorMessage);
                            else if (EnglishButton.Checked == true)
                                MessageBox.Show(EnglishToEnglish.ErrorMessage);
                        }
                        catch (IOException exception)
                        {
                            DefinitionBox.Visible = false;
                            throw new IOException("The system can't acces files.", exception); 
                        }
                        catch (WrongWordExceptionEng)
                        {
                            DefinitionBox.Visible = false;
                            if (RomanianButton.Checked == true)
                                MessageBox.Show("Cuvantul este scris gresit. Scrieti cuvantul corect si apasati butonul de cautare");
                            else if (EnglishButton.Checked == true)
                                MessageBox.Show("The word is wrong written. Please type a correct word and press the search button.");
                        }
                    }
                    else
                    {
                        DefinitionBox.Visible = false;
                        MessageBox.Show("Please type a valid word for the picked dictionay.");
                    }
                }
                else
                {
                    MessageBox.Show("Please type a word.");
                }
            }
            else
            {
                MessageBox.Show("Please select a dictionary.");
            }
        }

        private void WhenClick_MyLinkLabel(object sender, EventArgs e)
        {
            var label = sender as LinkLabel;
            RemoveLinkLabels();

            StreamWriter writer = new StreamWriter(tempFoundWords);
            using (writer)
                writer.Write("");

            WordBox.Text = label.Text;
            var definate = new RomanianToRomanian(label.Text, label.Tag.ToString());

            string definition;
            if (definate.ExtractDefinition(label.Text, label.Tag.ToString(), out definition) == true)
            {
                DefinitionBox.Text = definition;
                DefinitionBox.Visible = true;
            }
            else
            {
                DefinitionBox.Visible = false;
                MessageBox.Show(RomanianToRomanian.ErrorMessage);
            }
        }
      
        private void DisplayLinkLabels()
        {    
            StreamReader reader = new StreamReader(tempFoundWords);

            int optionsCount = 0;
            using (reader)
            {
                int plusY = 0;
                string word = reader.ReadLine();

                if(word!="")
                while (reader.EndOfStream == false)
                {
                    CreateLinkLabels(word, reader.ReadLine(), plusY);
                    plusY += 40;
                    optionsCount++;
                    word = reader.ReadLine();
                }
            }

            if (optionsCount == 0)
            {
                MessageBox.Show(RomanianToRomanian.ErrorMessage);
                DefinitionBox.Visible = false;
            }
            else
            {
                DefinitionBox.Visible = true;
                DefinitionBox.Text = "Selectati cuvantul al carui definitie o cautati:";
            }
        }

        private void CreateLinkLabels(string word, string link, int lineSpace)
        {
            int x = 25;
            int y = 250 + lineSpace;
            var linkLabel = new LinkLabel { Text = word, Tag = link };
            this.Controls.Add(linkLabel);

            linkLabel.Location = new Point(x, y);
            linkLabel.BackColor = TextBox.DefaultBackColor;
            linkLabel.LinkColor = Color.Black;
            linkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabel.Font = new Font(this.Font.FontFamily, 16.5f, FontStyle.Bold);
            linkLabel.AutoSize = true;

            linkLabel.Click += WhenClick_MyLinkLabel;  
            linkLabel.BringToFront();
            labelList.Push(linkLabel);
        }

        private void RemoveLinkLabels()
        {
            while (labelList.Count != 0)
            {
                this.Controls.Remove(labelList.Peek());
                labelList.Pop();
            }
        }

        private bool IsCorrect(string text)
        {
            bool isRomanian = false;

            for ( int i = 0; i < text.Length; i++ )
            {
                if ( text[i]<97 || text[i]>122 )
                {
                    if (text[i] == 'ț')
                        isRomanian = true;
                    else if (text[i] == 'ș')
                        isRomanian = true;
                    else if (text[i] == 'ă')
                        isRomanian = true;
                    else if (text[i] == 'î')
                        isRomanian = true;
                    else if (text[i] == 'â')
                        isRomanian = true;
                    else
                        return false;
                }
            }

            if (isRomanian == true && (EnglishButton.Checked == true || EngRomButton.Checked == true))
                return false;

            return true;
        }

        private bool IsSelectedDictionary( )
        {
            if (EnglishButton.Checked == true)
            {
                return true;
            }
            if (RomanianButton.Checked == true)
            {
                return true;
            }
            if (EngRomButton.Checked == true)
            {
                return true;
            }
            if (RomEngButton.Checked == true)
            {
                return true;
            }
            return false;
        }
    }
}
