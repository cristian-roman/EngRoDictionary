using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Exceptions;
using Dictionary;
using System.Runtime.InteropServices;

namespace SearchEngine
{
    class RomanianToRomanian : Definition
    {
        public const string ErrorMessage = "Cuvantul este scris gresit sau nu exista in dictionar. Incercati sa urmati regulile de cautare.";

        private const int extractStartIndex = 20000;
        private const int extractEndIndex = 0;

        private const int searchStartIndex = 10000;
        private const int searchEndIndex = 20000;

        private static string tempFoundWords = @"C:\Users\roman\source\repos\Dictionary\Definitions\Temp\tempFoundWords.txt";
        public static string baseDefinitionPath = @"C:\Users\roman\source\repos\Dictionary\Definitions\RomanianToRomanian\";

        public RomanianToRomanian(string word)
            : base(word)
        {
            definitionPath = baseDefinitionPath + word + ".txt";
            site = "https://dexonline.ro/definitie/" + word;
        }

        public RomanianToRomanian(string word, string site)
            :base(word)
        {
            definitionPath = baseDefinitionPath + word + ".txt";
            this.site = site;
        }

        public override bool GetDefinition(out string definition)
        {
            if (File.Exists(definitionPath) == true)
            {
                definition = ExtractTextFromFile(definitionPath);
                return true;
            }

            int foundWordNr;
            if (TryCorrectWord(searchedWord, out foundWordNr) == true)
            {
                if (foundWordNr == 1)
                {
                    if (ExtractDefinition(searchedWord, site, out definition) == true)
                        return true;
                    else
                        throw new DefinitionNotFoundedException("RomanianToRomanian class the ExtractDefinition method breaks when is founded a word after correction. Method: GetDefinition");
                }
                else
                {
                    definition = "";
                    return false;
                }
            }
            else if (ExtractDefinition(searchedWord, site, out definition) == true)
            {
                return true;
            }

            definition = "";
            return false;
        }

        public bool ExtractDefinition(string word, string site, out string definition)
        {
            if ( File.Exists(definitionPath) == true )
            {
                definition = ExtractTextFromFile(definitionPath);
                return true;
            }

            string bruteDefinition;
            try
            {
                string pattern = Generate_AccentPattern(word);
                bruteDefinition = ExtractBruteDefinition(word, site, pattern);
            }
            catch(DefinitionNotFoundedException)
            {
                try
                {
                    string pattern = Generate_DiacriticsAccentPattern(word);
                    bruteDefinition = ExtractBruteDefinition(word, site, pattern);
                }
                catch(DefinitionNotFoundedException)
                {
                    definition = "";
                    return false;
                }
            }

            definition = FormatText(bruteDefinition);
            WriteTextInFile(definitionPath, definition);
            return true;
        }

        private string ExtractBruteDefinition(string word, string site, string pattern)
        {
            string bruteSite = GetSiteAsString(site, extractStartIndex, extractEndIndex);
            WriteTextInFile(tempFilePath, bruteSite);
            Regex regex = new Regex(pattern);

            Match match = regex.Match(bruteSite);

            if (match.Success == false)
            {
                throw new DefinitionNotFoundedException("Regex was unable to find a definition about the word given. Method: ExtractBruteDefinition.");
            }

            StringBuilder longestDefinition = new StringBuilder("");

            while (match.Success == true)
            {
                if (longestDefinition.Length < match.Groups[2].Value.Length)
                    longestDefinition = new StringBuilder(match.Groups[1].Value + "\r\n" + match.Groups[2].Value);

                match = match.NextMatch();
            }

            return longestDefinition.ToString();
        }

        private bool TryCorrectWord(string word, out int foundWordNr)
        {
            string bruteSite = GetSiteAsString(site, searchStartIndex, searchEndIndex);
            string pattern = Generate_LinkPattern(word);
            Regex regex = new Regex(pattern);
            Match matches = regex.Match(bruteSite);

            if (matches.Success == false)
            {
                foundWordNr = 0;
                return false;
            }

            Dictionary<string, string> keyWords = new Dictionary<string, string>(4);
            StreamWriter writer = new StreamWriter(tempFoundWords);
            string siteBeginning = "https://dexonline.ro/";

            while (matches.Success == true)
            {
                if (keyWords.ContainsKey(matches.Groups[2].Value) == false)
                {
                    string site = string.Format(siteBeginning + matches.Groups[1].Value);

                    keyWords.Add(matches.Groups[2].Value, site);
                }
                matches = matches.NextMatch();
            }

            foundWordNr = 0;
            using (writer)
            {
                foreach (var key in keyWords.Keys)
                {
                    foundWordNr++;
                    writer.WriteLine(key);
                    writer.WriteLine(keyWords[key]);
                }
            }

            return true;
        }
        private string FormatText(string text)
        {
            return RemoveTags(text);
        }

        private string Generate_AccentPattern(string word)
        {
            StringBuilder pattern = new StringBuilder(@"<span class=""def"" title=""Clic pentru a naviga la acest cuvânt""> ?<b>(");

            for (int i = 0; i < word.Length; i++)
            {
                pattern.Append('[');

                switch (word[i])
                {
                    case 'a':
                        pattern.Append("AaÁá");
                        break;
                    case 'e':
                        pattern.Append("EeÉé");
                        break;
                    case 'i':
                        pattern.Append("IiÍí");
                        break;
                    case 'o':
                        pattern.Append("OoÓó");
                        break;
                    case 'u':
                        pattern.Append("UuÚú");
                        break;
                    case 'â':
                        pattern.Append("ÂâẤấ");
                        break;
                    default:
                        pattern.Append(char.ToUpper(word[i]).ToString() + word[i].ToString());
                        break;
                }

                pattern.Append(@"]");
            }
            pattern.Append(@")(.*?)sursa:");

            return pattern.ToString();
        }
        private string Generate_DiacriticsAccentPattern(string word)
        {
            StringBuilder pattern = new StringBuilder(@"<span class=""def"" title=""Clic pentru a naviga la acest cuvânt""> ?<b>([");

            switch (word[0])
            {
                case 'u':
                    pattern.Append("UuÚú");
                    break;
                case 'o':
                    pattern.Append("OoÓó");
                    break;
                case 'e':
                    pattern.Append("EeÉé");
                    break;
                case 'i':
                    pattern.Append("IiÍíÎî");
                    break;
                case 'a':
                    pattern.Append("AaÁá");
                    break;
                case 't':
                    pattern.Append("TtȚț");
                    break;
                case 's':
                    pattern.Append("SsȘș");
                    break;
                default:
                    pattern.Append(char.ToUpper(word[0]).ToString() + word[0].ToString());
                    break;
            }

            pattern.Append(']');

            for (int i = 1; i < word.Length - 1; i++)
            {
                pattern.Append('[');
                switch (word[i])
                {
                    case 'â':
                        pattern.Append("ÂâẤấ");
                        break;
                    case 'u':
                        pattern.Append("UuÚú");
                        break;
                    case 'o':
                        pattern.Append("OoÓó");
                        break;
                    case 'e':
                        pattern.Append("EeÉé");
                        break;
                    case 'i':
                        pattern.Append("IiÍí");
                        break;
                    case 'a':
                        pattern.Append("AaÁáÂâẤấĂă");
                        break;
                    case 't':
                        pattern.Append("TtȚț");
                        break;
                    case 's':
                        pattern.Append("SsȘș");
                        break;
                    default:
                        pattern.Append(char.ToUpper(word[i]).ToString()+ word[i].ToString());
                        break;
                }
                pattern.Append(']');
            }

            pattern.Append('[');

            switch (word[word.Length - 1])
            {
                case 'u':
                    pattern.Append("UuÚú");
                    break;
                case 'o':
                    pattern.Append("OoÓó");
                    break;
                case 'e':
                    pattern.Append("EeÉé");
                    break;
                case 'i':
                    pattern.Append("IiÍíÎî");
                    break;
                case 'a':
                    pattern.Append("AaÁáĂă");
                    break;
                case 't':
                    pattern.Append("TtȚț");
                    break;
                case 's':
                    pattern.Append("SsȘș");
                    break;
                default:
                    pattern.Append(char.ToUpper(word[word.Length - 1]).ToString() + word[word.Length - 1].ToString());
                    break;
            }
            pattern.Append(@"])(.*?)sursa:");

            return pattern.ToString();
        }
        private string Generate_LinkPattern(string word)
        {
            StringBuilder pattern = new StringBuilder(@"<a href=""\/(intrare\/(");

            switch(word[0])
            {
                case 'i':
                    pattern.Append("[iî]");
                    break;
                case 'a':
                    pattern.Append("[aâă]");
                    break;
                case 't':
                    pattern.Append("[tț]");
                    break;
                case 's':
                    pattern.Append("[sș]");
                    break;
                default:
                    pattern.Append(word[0]);
                    break;
            }

            for(int i = 1; i < word.Length-1; i++)
            {
                switch(word[i])
                {
                    case 'a':
                        pattern.Append("[aâă]");
                        break;
                    case 't':
                        pattern.Append("[tț]");
                        break;
                    case 's':
                        pattern.Append("[sș]");
                        break;
                    default:
                        pattern.Append(word[i]);
                        break;
                }
            }

            switch (word[word.Length-1])
            {
                case 'i':
                    pattern.Append("[iî]");
                    break;
                case 'a':
                    pattern.Append("[aâă]");
                    break;
                case 't':
                    pattern.Append("[tț]");
                    break;
                case 's':
                    pattern.Append("[sș]");
                    break;
                default:
                    pattern.Append(word[word.Length-1]);
                    break;
            }

            pattern.Append(@")\/\d+)"">");
            return pattern.ToString();
        }
        protected override string GetSiteAsString(string site, int startIndex = 0, int lastIndex = 0)
        {
            byte[] siteInBytes = DownloadSiteInBytes(site);

            if (lastIndex == 0)
            {
                lastIndex = siteInBytes.Length - 1;
            }

            if ( startIndex >= siteInBytes.Length )
            {
                startIndex = siteInBytes.Length - 1;
            }

            if (lastIndex >= siteInBytes.Length)
            {
                lastIndex = siteInBytes.Length - 1;
            }

            return Encoding.UTF8.GetString(siteInBytes, startIndex, lastIndex - startIndex + 1);
        } 
    }
}
