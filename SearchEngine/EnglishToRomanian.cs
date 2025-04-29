namespace SearchEngine
{
    class EnglishToRomanian : Definition
    {
        public EnglishToRomanian(string word)
            : base(word)
        {
            definitionPath = @"C:\Users\roman\source\repos\Dictionary\Definitions\EnglishToRomanian\" + word + ".txt";
            site = "https://www.dictionarenglez.ro/englez-roman/" + word;
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
