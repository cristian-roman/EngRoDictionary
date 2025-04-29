namespace SearchEngine
{
    class RomanianToEnglish : Definition
    {
        public RomanianToEnglish(string word)
            : base(word)
        {
            definitionPath = @"C:\Users\roman\source\repos\Dictionary\Definitions\RomanianToEnglish\" + word + ".txt";
            site = "https://www.dictionarenglez.ro/roman-englez/panza/" + word;
        }

        public override bool GetDefinition(out string definition)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetSiteAsString(string site, int startIndex, int lastIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}
