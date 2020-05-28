using System.Collections.Generic;
namespace ParserGenerator
{
    public class Rule
    {
        private string _name;
        private List<List<string>> _alternatives;

        public Rule(string name, List<List<string>> alternatives)
        {
            _name = name;
            _alternatives = alternatives;
        }

        public string GetName()
        {
            return _name;
        }

        public List<List<string>> GetAlternatives()
        {
            return _alternatives;
        }
    }
}