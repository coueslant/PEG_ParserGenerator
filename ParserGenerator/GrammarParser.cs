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
                System.Console.WriteLine("Successfully parsed metas.");
                return _metas;
            }
            System.Console.WriteLine("Failed to parse metas at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
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
            System.Console.WriteLine("Successfully parsed meta.");
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
                System.Console.WriteLine("Successfully parsed rules.");
                return _rules;
            }
            System.Console.WriteLine("Failed to parse rules at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
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
            System.Console.WriteLine("Successfully parsed rule.");
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
                System.Console.WriteLine("Successfully parsed alternatives.");
                return _alternatives;
            }
            // failed to parse any alternatives
            System.Console.WriteLine("Failed to parse alternatives at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
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
            System.Console.WriteLine("Successfully parsed alternative.");
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
                System.Console.WriteLine("Successfully parsed items.");
                return _items;
            }
            // didn't see any new item, return an indicator of that
            System.Console.WriteLine("Failed to parse items at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
            return null;
        }

        private string Item()
        {
            _parsing = "Item";
            Token _name = Expect("NAME");
            if (_name != null)
            {
                System.Console.WriteLine("Successfully parsed item.");
                return _name.String;
            }

            Token _string = Expect("STRING");
            if (_string != null)
            {
                System.Console.WriteLine("Successfully parsed item.");
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
            if (_leftBrace != null)
            {
                string _contents = ActionContents();
                Token _rightBrace = Expect("}");
                if (_leftBrace == null || _contents.Length == 0 || _rightBrace == null)
                {
                    // failed to parse action
                    System.Console.WriteLine("Failed to parse action at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
                    Reset(_pos);
                    return "";
                }
                System.Console.WriteLine("Successfully parsed action contents.");
                System.Console.WriteLine("Successfully parsed action.");
                return _contents;
            }
            return "";
        }

        private string ActionContents()
        {
            _parsing = "ActionContents";
            string _contentsString = "";
            string _content = Content();
            if (_content.Length != 0)
            {
                _contentsString = _contentsString + _content;
                string _furtherContents = ActionContents();
                if (_furtherContents.Length != 0)
                {
                    _contentsString = _contentsString + _furtherContents;
                }
                return _contentsString;
            }
            System.Console.WriteLine("Failed to parse action contents at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
            return "";
        }

        private string Content()
        {
            _parsing = "Content";

            Token _leftBrace = Expect("{");
            if (_leftBrace != null)
            {
                string _contents = ActionContents();
                Token _rightBrace = Expect("}");
                if (_leftBrace != null && _contents.Length != 0 && _rightBrace != null)
                {
                    System.Console.WriteLine("Successfully parsed content.");
                    return "{" + _contents + "}";
                }
            }

            Token _name = Expect("NAME");

            if (_name != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _name.String;
            }

            Token _number = Expect("NUMBER");

            if (_number != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _number.String;
            }

            Token _string = Expect("STRING");

            if (_string != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _string.String;
            }

            int _symbolPos = Mark();

            Token _symbol = Expect("SYMBOL");

            if (_symbol != null && !((new HashSet<String> { "{", "}" }).Contains(_symbol.String)))
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _symbol.String;
            }

            Reset(_symbolPos);

            int _opPos = Mark();

            Token _op = Expect("OP");

            if (_op != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _op.String;
            }

            Reset(_opPos);

            System.Console.WriteLine("Failed to parse action contents at: [ position: " + GetTokenizer().Mark().ToString() + " ]");
            return "";

        }
    }
}