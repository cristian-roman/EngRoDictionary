using Dictionary;
using Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace SearchEngine
{
    partial class EnglishToEnglish : Definition
    {
        public static string ErrorMessage = "The word does not exist in dictionary or is wrong written. Please type another word or check the spelling."; 
        public EnglishToEnglish(string word)
            :base(word)
        {
            definitionPath = @"C:\Users\roman\source\repos\Dictionary\Definitions\EnglishToEnglish\" + word + ".txt";
            site = "https://www.collinsdictionary.com/dictionary/english/" + word;
        }

        public override bool GetDefinition(out string definition)
        {
            if (File.Exists(definitionPath))
            {
                definition = ExtractTextFromFile(definitionPath);
                return true;
            }
            else
            {
                string brutePage = GetSiteAsString(site, 140000, 180000);
                definition = ExtractDefinition(brutePage);
                WriteTextInFile(definitionPath, definition);
                return true;
            }
        }

        protected override string GetSiteAsString(string site, int startIndex = 0, int lastIndex = 0)
        {
            byte[] siteInBytes = DownloadSiteInBytes(site);

            if (lastIndex == 0)
            {
                lastIndex = siteInBytes.Length - 1;
            }

            try
            {
                return Encoding.Default.GetString(siteInBytes, startIndex, lastIndex - startIndex + 1);
            }
            catch
            {
                throw new DefinitionNotFoundedException("EnglishToEnglish");
            }
        }

        private string ExtractDefinition(string brutePage)
        {
            string pattern = GenerateMatchCode(searchedWord) + @"([a-zA-Z ]*?)1\.(.*?)2\.";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(brutePage);

            if ( match.Success == false )
            {
                string bruteDefinition;
                if (TrySecondExtraction(brutePage, out bruteDefinition) == true)
                {
                    return FormatText(bruteDefinition);
                }
                else
                {
                    throw new WrongWordExceptionEng("EnglishToEnglish", "ExtractDefinition");
                }
            }

            StringBuilder definition = new StringBuilder();

            if (match.Groups[1].Value.Length != 0)
            {
                definition.Append(match.Groups[1].Value);
            }

            definition.Append(match.Groups[2].Value);
            return FormatText(definition.ToString());
        }

        private bool TrySecondExtraction(string brutePage, out string def)
        {
            string pattern = GenerateMatchCode(searchedWord) + @"(.*?)\.\.\.";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(brutePage);

            if (match.Success)
            {
                def = match.Groups[1].Value;
                return true;
            }
            else
            {
                def = "";
                return false;
            }
        }

        private string FormatText(string bruteDefinition)
        {
            int spaceCount = 0;
            bool isNewLine = false;
            StringBuilder definition = new StringBuilder();
            bruteDefinition = bruteDefinition.Trim();
            definition.Append(char.ToUpper(bruteDefinition[0]));

            for (int i = 1; i < bruteDefinition.Length && ( bruteDefinition[i] != '.'|| !char.IsLetter(bruteDefinition[i - 1])); i++)
            {
                if (bruteDefinition[i] == '[')
                {
                    while (bruteDefinition[i] != ']')
                        i++;
                    i++;
                }
                else if ( spaceCount<2 && bruteDefinition[i] == ' ' )
                {
                    spaceCount++;
                    if (spaceCount == 2)
                    {
                        if (bruteDefinition[i + 1] >= 'A' && bruteDefinition[i + 1] <= 'Z')
                        {
                            definition.Append("\r\n");
                            i++;
                            isNewLine = true;
                        }
                    }
                }

                definition.Append(bruteDefinition[i]);
            }

            definition.Append('.');

            if (isNewLine == false)
            {
                string returnedDefinition;

                if (TryAddNewLine(definition.ToString(), out returnedDefinition) == true)
                {
                    return returnedDefinition;
                }
                else
                {
                    definition = new StringBuilder(returnedDefinition);
                    int index = 1;

                    while (index < definition.Length && (definition[index] <= 'A' || definition[index] >= 'Z'))
                    {
                        index++;
                    }

                    definition.Insert(index, "\r\n");
                    return definition.ToString();
                }
            }
            else
            {
                return definition.ToString();
            }
        }

        private enum WordType
        {
            Noun, Pronoun, Verb, Adverb, Adjective, Preposition, Determiner, Conjunction
        }

        private bool TryAddNewLine(string bruteDefinition, out string modifiedDefinition)
        {
            List<WordType> typeList = new List<WordType>(6);
            StringBuilder definition = new StringBuilder(bruteDefinition);
            bool isSuccesful = false;

            typeList.Add(WordType.Noun);
            typeList.Add(WordType.Pronoun);
            typeList.Add(WordType.Adjective);
            typeList.Add(WordType.Verb);
            typeList.Add(WordType.Adverb);
            typeList.Add(WordType.Preposition);
            typeList.Add(WordType.Determiner);
            typeList.Add(WordType.Conjunction);

            foreach ( var type in typeList )
            {
                string stringForm = type.ToString();
                int index = bruteDefinition.LastIndexOf(stringForm, StringComparison.InvariantCultureIgnoreCase);
                if ( index != -1)
                {
                    isSuccesful = true;
                    index += stringForm.Length +1;
                    definition[index] = char.ToUpper(definition[index]);

                    if (definition[index] == ' ')
                        index++;

                    definition.Insert(index, "\r\n");
                    index = bruteDefinition.IndexOf('&');

                    if (index != -1)
                    {
                        definition.Remove(index, 4);
                        definition[index] = '&';
                    }

                    break;
                }
            }
            
            if ( isSuccesful == true )
            {
                modifiedDefinition = definition.ToString();
                return true;
            }
            else
            {
                modifiedDefinition = bruteDefinition;
                return false;
            }
        }

        private string GenerateMatchCode(string word)
        {
            string beginingCode = @"{""@type"":""DefinedTerm"",""name"":""";
            StringBuilder matchCode = new StringBuilder(beginingCode);

            matchCode.Append(word);
            string endCode = @""",""description"":""";
            matchCode.Append(endCode);

            return matchCode.ToString();
        }
    }
}
