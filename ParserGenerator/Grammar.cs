using System.Collections.Generic;

namespace ParserGenerator
{
    public class Grammar : List<Rule>
    {
        private List<Meta> _metas;
        private List<Rule> _rules;

        public List<Meta> Metas { get { return _metas; } }
        public List<Rule> Rules { get { return _rules; } }

        public Grammar(List<Meta> metas, List<Rule> rules)
        {
            _metas = metas;
            _rules = rules;
        }
    }
}