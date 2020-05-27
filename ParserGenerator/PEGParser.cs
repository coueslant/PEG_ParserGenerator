namespace ParserGenerator {
    public class PEGParser : Parser {
        public List<Rule> Grammar () {
            int _pos = Mark ();
            Rule _rule;
            _rule = Rule ();
            if (_rule != null) {
                List<Rule> _rules = new List<Rule> ();
                while (_rule != null) {
                    _rules.append (_rule);
                    _rule = Rule ();
                }
                if (Expect (ENDMARKER)) {
                    return _rules;
                }
            }
            Reset (_pos);
            return null;
        }
    }
}