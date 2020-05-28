using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserGenerator
{
    public class GrammarParser : Parser
    {
        public GrammarParser(string grammarString)
        {
            GetTokenizer().SetTokenGenerator(new TokenGenerator(grammarString));
            _lineCount = 0;
        }

        public Grammar Grammar()
        {
            _parsing = "Grammar";
            int _pos = Mark();
            List<Rule> _rules;
            List<Meta> _metas;
            _metas = Metas();
            _rules = Rules();
            Token _endmarkerToken = Expect("ENDMARKER");
            if (_endmarkerToken != null)
            {
                System.Console.WriteLine("Successfully parsed grammar.");
                if (_metas != null)
                {
                    return new Grammar(_metas, _rules);
                }
                return new Grammar(new List<Meta>(), _rules);
            }
            Reset(_pos);
            // failed to parse grammar
            System.Console.WriteLine("Failed to parse grammar.");
            return null;
        }

        private List<Meta> Metas()
        {
            _parsing = "Metas";
            int _pos = Mark();
            Meta _meta = Meta();
            if (_meta != null)
            {
                List<Meta> _metas = new List<Meta>();
                _metas.Add(_meta);
                List<Meta> _newMetas = Metas();
                if (_newMetas != null)
                {
                    _metas.AddRange(_newMetas);
                }
                return _metas;
            }
            Reset(_pos);
            return null;
        }

        private Meta Meta()
        {
            _parsing = "Meta";
            int _pos = Mark();
            Token _at = Expect("@");
            Token _name = Expect("NAME");
            Token _string = Expect("STRING");
            Token _newline = Expect("NEWLINE");
            if (_at == null || _name == null || _string == null || _newline == null)
            {
                // failed to parse a meta
                System.Console.WriteLine("Failed to parse meta at: [ position: " + GetTokenizer().Mark().ToString() + ", line: " + _lineCount + " ]");
                Reset(_pos);
                return null;
            }

            return new Meta(_name.String, _string.String);
        }

        private List<Rule> Rules()
        {
            Rule _rule = Rule();
            if (_rule != null)
            {
                List<Rule> _rules = new List<Rule>();
                _rules.Add(_rule);
                List<Rule> _newRules = Rules();
                if (_newRules != null)
                {
                    _rules.AddRange(_newRules);
                }
                return _rules;
            }
            return null;
        }

        private Rule Rule()
        {
            _parsing = "Rule";
            int _pos = Mark();
            Token _name = Expect("NAME");
            Token _separator = Expect(":");
            List<Alternative> _alternatives = Alternatives();
            Token _newline = Expect("NEWLINE");

            if (_name == null || _separator == null || _alternatives == null || _newline == null)
            {
                // failed to parse rule somewhere
                System.Console.WriteLine("Failed to parse rule at: [ position: " + GetTokenizer().Mark().ToString() + ", line: " + _lineCount + " ]");
                Reset(_pos);
                return null;
            }

            _lineCount++;
            return new Rule(_name.String, _alternatives);
            // if (_name != null)
            // {
            //     Token _separator = Expect(":");
            //     if (_separator != null)
            //     {
            //         List<string> _alt = Alternative();
            //         if (_alt != null)
            //         {
            //             List<List<string>> _alternatives = new List<List<string>>();
            //             _alternatives.Add(_alt);
            //             int _altPos = Mark();
            //             _separator = Expect("|");
            //             _alt = Alternative();
            //             while (_separator != null && _alt != null)
            //             {
            //                 _alternatives.Add(_alt);
            //                 _altPos = Mark();
            //                 _separator = Expect("|");
            //                 _alt = Alternative();
            //             }
            //             Reset(_altPos);
            //             Token _newLine = Expect("NEWLINE");
            //             if (_newLine != null)
            //             {
            //                 _lineCount++;
            //                 return new Rule(_name.String, _alternatives);
            //             }
            //         }
            //     }
            // }
        }

        private List<Alternative> Alternatives()
        {
            _parsing = "Alternatives";
            Alternative _alternative = Alternative();
            if (_alternative != null)
            {
                List<Alternative> _alternatives = new List<Alternative>();
                _alternatives.Add(_alternative);
                Token _separator = Expect("|");
                if (_separator != null)
                {
                    // if we see a separator, continue parsing alternatives
                    _alternatives.AddRange(Alternatives());
                }

                return _alternatives;
            }
            // failed to parse any alternatives
            return null;
        }

        private Alternative Alternative()
        {
            _parsing = "Alternative";
            List<String> _items = Items();
            string _action = Action();
            if (_items == null || _action == null)
            {
                // failed to parse alternative somewhere
                System.Console.WriteLine("Failed to parse alternative at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
                return null;
            }
            return new Alternative(_items, _action);
        }

        private List<string> Items()
        {
            _parsing = "Items";
            string _item = Item();
            if (_item != null)
            {
                List<string> _items = new List<string>();
                _items.Add(_item);
                List<string> _newItems = Items();
                if (_newItems != null)
                {
                    // parsed more items, add them to our list
                    _items.AddRange(_newItems);
                }
                // didn't see any more items, return what we have already
                return _items;
            }
            // didn't see any new item, return an indicator of that
            return null;
        }

        private string Item()
        {
            _parsing = "Item";
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

            // failed to parse item
            System.Console.WriteLine("Failed to parse item at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
            return null;
        }

        private string Action()
        {
            _parsing = "Action";
            int _pos = Mark();
            Token _leftBrace = Expect("{");
            string _contents = ActionContents();
            Token _rightBrace = Expect("}");

            if (_leftBrace == null || _contents.Length != 0 || _rightBrace == null)
            {
                // failed to parse action
                System.Console.WriteLine("Failed to parse action at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
                Reset(_pos);
                return null;
            }
            return _contents;
        }

        private string ActionContents()
        {
            _parsing = "ActionContents";
            StringBuilder _contentsString = new StringBuilder();
            string _content = Content();
            if (_content.Length != 0)
            {
                _contentsString.Append(_content);
                string _furtherContents = ActionContents();
                if (_furtherContents.Length != 0)
                {
                    _contentsString.Append(_furtherContents);
                }
                return _content.ToString();
            }
            return "";
        }

        private string Content()
        {
            _parsing = "Content";
            int _pos = Mark();

            Token _leftBrace = Expect("{");
            if (_leftBrace != null)
            {
                string _contents = ActionContents();
                Token _rightBrace = Expect("}");
                if (_leftBrace != null && _contents.Length != 0 && _rightBrace != null)
                {
                    return "{" + _contents + "}";
                }
            }

            Reset(_pos);

            Token _name = Expect("NAME");

            if (_name != null)
            {
                return _name.String;
            }

            Reset(_pos);

            Token _number = Expect("NUMBER");

            if (_number != null)
            {
                return _number.String;
            }

            Reset(_pos);

            Token _string = Expect("STRING");

            if (_string != null)
            {
                return _string.String;
            }

            Reset(_pos);

            Token _symbol = Expect("SYMBOL");

            if (_symbol != null)
            {
                return _symbol.String;
            }

            Reset(_pos);

            return "";
        }
    }
}