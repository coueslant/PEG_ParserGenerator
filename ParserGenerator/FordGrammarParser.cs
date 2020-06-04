using System.Collections.Generic;

namespace ParserGenerator
{
    public class FordGrammarParser
    {
        public List<Rule> Parse(string parseString)
        {
            List<Rule> _rules = ParseGrammar(parseString);

            return _rules;
        }
    }
}