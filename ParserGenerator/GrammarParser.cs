using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserGenerator
{
    public class GrammarParser : Parser
    {
        public GrammarParser(string grammarString) : base()
        {
            Tokenizer.SetTokenGenerator(new TokenGenerator(grammarString));
            _lineCount = 0;
        }

        public Grammar Grammar()
        {
            _parsing = "Grammar";
            int _pos = Mark();
            List<Meta> _metas;
            List<Rule> _rules;
            _metas = (List<Meta>)Memoize(Metas);
            _rules = (List<Rule>)Memoize(Rules);
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
            Meta _meta = (Meta)Memoize(Meta);
            if (_meta != null)
            {
                List<Meta> _metas = new List<Meta>();
                _metas.Add(_meta);
                List<Meta> _newMetas = (List<Meta>)Memoize(Metas);
                if (_newMetas != null)
                {
                    _metas.AddRange(_newMetas);
                }
                System.Console.WriteLine("Successfully parsed metas.");
                return _metas;
            }
            System.Console.WriteLine("Failed to parse metas at: [ position: " + _pos.ToString() + " ]");
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
                System.Console.WriteLine("Failed to parse meta at: [ position: " + _pos.ToString() + ", line: " + _lineCount + " ]");
                Reset(_pos);
                return null;
            }
            System.Console.WriteLine("Successfully parsed meta.");
            return new Meta(_name.String, _string.String);
        }

        private List<Rule> Rules()
        {
            int _pos = Mark();
            Rule _rule = (Rule)Memoize(Rule);
            if (_rule != null)
            {
                List<Rule> _rules = new List<Rule>();
                _rules.Add(_rule);
                List<Rule> _newRules = (List<Rule>)Memoize(Rules);
                if (_newRules != null)
                {
                    _rules.AddRange(_newRules);
                }
                System.Console.WriteLine("Successfully parsed rules.");
                return _rules;
            }
            System.Console.WriteLine("Failed to parse rules at: [ position: " + _pos.ToString() + " ]");
            return null;
        }

        private Rule Rule()
        {
            // rule: NAME ":" alts NEWLINE INDENT more_alts DEDENT { Rule(name.string, alts + more_alts) }
            //       | NAME ":" alts NEWLINE { Rule(name.string, alts) }
            //       | NAME ":" NEWLINE INDENT more_alts DEDENT { Rule(name.string, more_alts) }
            _parsing = "Rule";
            int _pos = Mark();
            Token _name = Expect("NAME");
            Token _separator = Expect(":");
            List<Alternative> _alternatives = (List<Alternative>)Memoize(Alternatives);
            Token _newline = Expect("NEWLINE");
            Token _indent = Expect("INDENT");
            List<Alternative> _moreAlts = (List<Alternative>)Memoize(MoreAlternatives);
            Token _dedent = Expect("NEWLINE");

            if (_name != null && _separator != null && _alternatives != null && _newline != null && _indent != null && _moreAlts != null && _dedent != null)
            {
                System.Console.WriteLine("Successfully parsed rule.");
                _lineCount++;
                _alternatives.AddRange(_moreAlts);
                return new Rule(_name.String, _alternatives);
            }

            Reset(_pos);

            _name = Expect("NAME");
            _separator = Expect(":");
            _alternatives = (List<Alternative>)Memoize(Alternatives);
            _newline = Expect("NEWLINE");

            if (_name != null && _separator != null && _alternatives != null && _newline != null)
            {
                System.Console.WriteLine("Successfully parsed rule.");
                _lineCount++;
                return new Rule(_name.String, _alternatives);
            }

            Reset(_pos);

            _name = Expect("NAME");
            _separator = Expect(":");
            _newline = Expect("NEWLINE");
            _indent = Expect("INDENT");
            _moreAlts = (List<Alternative>)Memoize(MoreAlternatives);
            _dedent = Expect("NEWLINE");

            if (_name != null && _separator != null && _newline != null && _indent != null && _moreAlts != null && _dedent != null)
            {
                System.Console.WriteLine("Successfully parsed rule.");
                _lineCount++;
                return new Rule(_name.String, _alternatives);
            }


            // failed to parse rule somewhere
            System.Console.WriteLine("Failed to parse rule at: [ position: " + _pos.ToString() + ", line: " + _lineCount + " ]");
            return null;
        }
        private List<Alternative> Alternatives()
        {
            _parsing = "Alternatives";
            int _pos = Mark();
            Alternative _alternative = (Alternative)Memoize(Alternative);
            if (_alternative != null)
            {
                List<Alternative> _alternatives = new List<Alternative>();
                _alternatives.Add(_alternative);
                Token _separator = Expect("|");
                if (_separator != null)
                {
                    // if we see a separator, continue parsing alternatives
                    _alternatives.AddRange((List<Alternative>)Memoize(Alternatives));
                }
                System.Console.WriteLine("Successfully parsed alternatives.");
                return _alternatives;
            }
            // failed to parse any alternatives
            System.Console.WriteLine("Failed to parse alternatives at: [ position: " + _pos.ToString() + " ]");
            return null;
        }

        private Alternative Alternative()
        {
            _parsing = "Alternative";
            int _pos = Mark();
            List<string> _items = (List<string>)Memoize(Items);
            string _action = (string)Memoize(Action);
            if (_items == null || _action == null)
            {
                // failed to parse alternative somewhere
                System.Console.WriteLine("Failed to parse alternative at: [ position: " + _pos.ToString() + " ]");
                Reset(_pos);
                return null;
            }
            System.Console.WriteLine("Successfully parsed alternative.");
            return new Alternative(_items, _action);
        }

        public List<Alternative> MoreAlternatives()
        {
            // more_alts:"|" alts NEWLINE more_alts { alts + more_alts }
            //           | "|" alts NEWLINE { alts }
            // TODO: figure out a fix for the unconditional infinite recursion happening here
            int _pos = Mark();
            Token _separator = Expect("|");
            List<Alternative> _alternatives = (List<Alternative>)Memoize(Alternatives);
            Token _newline = Expect("NEWLINE");
            List<Alternative> _moreAlts;
            if (_newline != null)
            {
                _moreAlts = (List<Alternative>)Memoize(MoreAlternatives);
            }
            else
            {
                _moreAlts = null;
            }

            if (_separator != null && _alternatives != null && _newline != null && _moreAlts != null)
            {
                _alternatives.AddRange(_moreAlts);
                return _alternatives;
            }

            Reset(_pos);


            _separator = Expect("|");
            _alternatives = (List<Alternative>)Memoize(Alternatives);
            _newline = Expect("NEWLINE");

            if (_separator != null && _alternatives != null && _newline != null)
            {
                return _alternatives;
            }

            // failed to parse more alternatives
            System.Console.WriteLine("Failed to parse more alternatives at: [ position: " + _pos.ToString() + " ]");
            return null;
        }

        private List<string> Items()
        {
            _parsing = "Items";
            int _pos = Mark();
            string _item = (string)Memoize(Item);
            if (_item != null)
            {
                List<string> _items = new List<string>();
                _items.Add(_item);
                List<string> _newItems = (List<string>)Memoize(Items);
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
            System.Console.WriteLine("Failed to parse items at: [ position: " + _pos.ToString() + " ]");
            return null;
        }

        private string Item()
        {
            _parsing = "Item";
            int _pos = Mark();
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
            System.Console.WriteLine("Failed to parse item at: [ position: " + _pos.ToString() + " ]");
            Reset(_pos);
            return null;
        }

        private string Action()
        {
            _parsing = "Action";
            int _pos = Mark();
            Token _leftBrace = Expect("{");
            if (_leftBrace != null)
            {
                string _contents = (string)Memoize(ActionContents);
                Token _rightBrace = Expect("}");
                if (_leftBrace == null || _contents.Length == 0 || _rightBrace == null)
                {
                    // failed to parse action
                    System.Console.WriteLine("Failed to parse action at: [ position: " + _pos.ToString() + " ]");
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
            int _pos = Mark();
            string _contentsString = "";
            string _content = (string)Memoize(Content);
            if (_content.Length != 0)
            {
                _contentsString = _contentsString + _content;
                string _furtherContents = (string)Memoize(ActionContents);
                if (_furtherContents.Length != 0)
                {
                    _contentsString = _contentsString + _furtherContents;
                }
                return _contentsString;
            }
            System.Console.WriteLine("Failed to parse action contents at: [ position: " + _pos.ToString() + " ]");
            return "";
        }

        private string Content()
        {
            _parsing = "Content";
            int _pos = Mark();
            Token _leftBrace = Expect("{");
            if (_leftBrace != null)
            {
                string _contents = (string)Memoize(ActionContents);
                Token _rightBrace = Expect("}");
                if (_leftBrace != null && _contents.Length != 0 && _rightBrace != null)
                {
                    System.Console.WriteLine("Successfully parsed content.");
                    return "{" + _contents + "}";
                }
            }
            Reset(_pos);

            Token _name = Expect("NAME");

            if (_name != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _name.String;
            }

            Reset(_pos);

            Token _number = Expect("NUMBER");

            if (_number != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _number.String;
            }

            Reset(_pos);

            Token _string = Expect("STRING");

            if (_string != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return " " + _string.String + " ";
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
                return " " + _op.String + " ";
            }

            Reset(_opPos);

            int _spacePos = Mark();

            Token _space = Expect(" ");

            if (_space != null)
            {
                System.Console.WriteLine("Successfully parsed content.");
                return _space.String;
            }

            Reset(_spacePos);

            System.Console.WriteLine("Failed to parse action contents at: [ position: " + Tokenizer.Mark().ToString() + " ]");
            return "";

        }
    }
}