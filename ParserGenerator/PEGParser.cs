using System;
using System.Collections.Generic;
using System.Linq;
namespace ParserGenerator
{
    public class PEGParser : Parser
    {

        public PEGParser(string grammarString)
        {
            GetTokenizer().SetTokenGenerator(new PEGTokenGenerator(grammarString));
        }

        public List<Rule> Grammar()
        {
            int _pos = Mark();
            Rule _rule;
            _rule = Rule();
            if (_rule != null)
            {
                List<Rule> _rules = new List<Rule>();
                while (_rule != null)
                {
                    _rules.Add(_rule);
                    _rule = Rule();
                }
                Token _endmarkerToken = Expect("ENDMARKER");
                if (_endmarkerToken != null)
                {
                    return _rules;
                }
            }
            Reset(_pos);
            return null;
        }

        private Rule Rule()
        {
            int _pos = Mark();
            Token _name = Expect("NAME");
            if (_name != null)
            {
                Token _separator = Expect(":");
                if (_separator != null)
                {
                    List<string> _alt = Alternative();
                    if (_alt != null)
                    {
                        List<List<string>> _alternatives = new List<List<string>>();
                        _alternatives.Add(_alt);
                        int _altPos = Mark();
                        _separator = Expect("|");
                        _alt = Alternative();
                        while (_separator != null && _alt != null)
                        {
                            _alternatives.Add(_alt);
                            _altPos = Mark();
                            _separator = Expect("|");
                            _alt = Alternative();
                        }
                        Reset(_altPos);
                        Token _newLine = Expect("NEWLINE");
                        if (_newLine != null)
                        {
                            return new Rule(_name.String, _alternatives);
                        }
                    }
                }
            }
            Reset(_pos);
            return null;
        }

        private List<string> Alternative()
        {
            string _item = Item();
            if (_item != null)
            {
                List<string> _items = new List<string>();
                _items.Add(_item);
                _item = Item();
                while (_item != null)
                {
                    _items.Add(_item);
                    _item = Item();
                }
                return _items;
            }
            return null;
        }

        private string Item()
        {
            Token _token = Expect("WHITESPACE");
            while (_token != null)
            {
                _token = Expect("WHITESPACE");
            }

            Token _name = Expect("NAME");
            if (_name != null)
            {
                return _name.String;
            }

            Token _string = Expect("STRING");
            if (_string != null)
            {
                return _string.String;
            }

            Token _symbol = Expect("SYMBOL");
            if (_symbol != null)
            {
                return _symbol.String;
            }

            return null;
        }
    }
}