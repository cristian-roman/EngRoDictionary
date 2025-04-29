using Dictionary;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchEngine
{
    abstract class Definition
    {
        
        protected const string tempFilePath = @"C:\Users\roman\source\repos\Dictionary\Definitions\Temp\TempSite.txt";
        protected string site;
        protected string definitionPath;
        protected string searchedWord;

        public Definition(string word)
        {
            searchedWord = word;
        }

        public abstract bool GetDefinition(out string definition);
        
        protected string RemoveTags(string text, int startIndex = 0)
        {
            StringBuilder newText = new StringBuilder();
            int openedTags = 0; 

            for ( int i = startIndex; i < text.Length; i++ )
            {
                if (text[i] == '<')
                    openedTags++;
                else if (text[i] == '>')
                    openedTags--;
                else
                    if (openedTags == 0)
                    newText.Append(text[i]);
            }

            return newText.ToString();
        }

        protected abstract string GetSiteAsString(string site, int startIndex, int lastIndex);

        protected byte[] DownloadSiteInBytes(string site)
        {
            WebClient client = new WebClient();
            byte[] webPage = client.DownloadData(site);

            return webPage;
        }

        protected void WriteTextInFile(string path, string text)
        {
            StreamWriter writer = new StreamWriter(path);
            using (writer)
            {
                writer.Write(text);
            }
        }

        protected string ExtractTextFromFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            string returnText;

            using (reader)
            {
                 returnText = reader.ReadToEnd();
            }

            return returnText;
        }
    }
}
