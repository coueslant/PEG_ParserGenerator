using System.Collections.Generic;
namespace ParserGenerator
{
    public class Rule
    {
        private string _name;
        private List<Alternative> _alternatives;

        public Rule(string name, List<Alternative> alternatives)
        {
            _name = name;
            _alternatives = alternatives;
        }

        public string GetName()
        {
            return _name;
        }

        public List<Alternative> GetAlternatives()
        {
            return _alternatives;
        }
    }
}