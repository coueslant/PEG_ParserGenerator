/*
This is @generated code, do not modify!
*/

/*
Required usings. Eventually this will import pieces from the ParserGenerator
itself and allow the generated parser to exist in it's own namespace.
*/
using System;
using System.Collections.Generic;

namespace ParserGenerator
{

    class GeneratedParser : Parser
    {
        public GeneratedParser(string parsingInput) : base()
        {
            Tokenizer.SetTokenGenerator(new TokenGenerator(parsingInput));
        }
        public Object Parse()
        {
            return start();
        }
        public Object start()
        {
            int _pos = Mark();
            Object _metas = Memoize(metas);
            Object _rules = Memoize(rules);
            Object _endmarker = Expect("ENDMARKER");
            if (_metas != null && _rules != null && _endmarker != null)
            {
                Console.WriteLine("Recognized [ start ]");
                return true;
            }
            Object _rules0 = Memoize(rules);
            Object _endmarker1 = Expect("ENDMARKER");
            if (_rules0 != null && _endmarker1 != null)
            {
                Console.WriteLine("Recognized [ start ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object metas()
        {
            int _pos = Mark();
            Object _meta = Memoize(meta);
            if (_meta != null)
            {
                List<Object> _metas = new List<Object>();
                _metas.Add(_meta);
                List<Object> _newmetas = (List<Object>)Memoize(metas);
                if (_newmetas != null)
                {
                    _metas.AddRange(_newmetas);
                    Console.WriteLine("Recognized [ metas ]");
                    return true;
                }
                /*
                return _metas
                */
            }
            Object _meta0 = Memoize(meta);
            if (_meta0 != null)
            {
                Console.WriteLine("Recognized [ metas ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object meta()
        {
            int _pos = Mark();
            Object _stringItem = Expect("@");
            Object _name = Expect("NAME");
            Object _string = Expect("STRING");
            Object _newline = Expect("NEWLINE");
            if (_stringItem != null && _name != null && _string != null && _newline != null)
            {
                Console.WriteLine("Recognized [ meta ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object rules()
        {
            int _pos = Mark();
            Object _rule = Memoize(rule);
            if (_rule != null)
            {
                List<Object> _rules = new List<Object>();
                _rules.Add(_rule);
                List<Object> _newrules = (List<Object>)Memoize(rules);
                if (_newrules != null)
                {
                    _rules.AddRange(_newrules);
                    Console.WriteLine("Recognized [ rules ]");
                    return true;
                }
                /*
                return _rules
                */
            }
            Object _rule0 = Memoize(rule);
            if (_rule0 != null)
            {
                Console.WriteLine("Recognized [ rules ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object rule()
        {
            int _pos = Mark();
            Object _name = Expect("NAME");
            Object _stringItem = Expect(":");
            Object _alts = Memoize(alts);
            Object _newline = Expect("NEWLINE");
            if (_name != null && _stringItem != null && _alts != null && _newline != null)
            {
                Console.WriteLine("Recognized [ rule ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object alts()
        {
            int _pos = Mark();
            Object _alt = Memoize(alt);
            if (_alt != null)
            {
                Object _stringItem = Expect("|");
                /*
                return _"|"
                */
                List<Object> _alts = new List<Object>();
                _alts.Add(_alt);
                List<Object> _newalts = (List<Object>)Memoize(alts);
                if (_newalts != null)
                {
                    _alts.AddRange(_newalts);
                    Console.WriteLine("Recognized [ alts ]");
                    return true;
                }
                /*
                return _alts
                */
            }
            Object _alt0 = Memoize(alt);
            if (_alt0 != null)
            {
                Console.WriteLine("Recognized [ alts ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object alt()
        {
            int _pos = Mark();
            Object _items = Memoize(items);
            Object _action = Memoize(action);
            if (_items != null && _action != null)
            {
                Console.WriteLine("Recognized [ alt ]");
                return true;
            }
            Object _items0 = Memoize(items);
            if (_items0 != null)
            {
                Console.WriteLine("Recognized [ alt ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object items()
        {
            int _pos = Mark();
            Object _item = Memoize(item);
            if (_item != null)
            {
                List<Object> _items = new List<Object>();
                _items.Add(_item);
                List<Object> _newitems = (List<Object>)Memoize(items);
                if (_newitems != null)
                {
                    _items.AddRange(_newitems);
                    Console.WriteLine("Recognized [ items ]");
                    return true;
                }
                /*
                return _items
                */
            }
            Object _item0 = Memoize(item);
            if (_item0 != null)
            {
                Console.WriteLine("Recognized [ items ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object item()
        {
            int _pos = Mark();
            Object _name = Expect("NAME");
            if (_name != null)
            {
                Console.WriteLine("Recognized [ item ]");
                return true;
            }
            Object _string = Expect("STRING");
            if (_string != null)
            {
                Console.WriteLine("Recognized [ item ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object action()
        {
            int _pos = Mark();
            Object _stringItem = Expect("{");
            Object _stuffs = Memoize(stuffs);
            Object _stringItem2 = Expect("}");
            if (_stringItem != null && _stuffs != null && _stringItem2 != null)
            {
                Console.WriteLine("Recognized [ action ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object stuffs()
        {
            int _pos = Mark();
            Object _stuff = Memoize(stuff);
            if (_stuff != null)
            {
                List<Object> _stuffs = new List<Object>();
                _stuffs.Add(_stuff);
                List<Object> _newstuffs = (List<Object>)Memoize(stuffs);
                if (_newstuffs != null)
                {
                    _stuffs.AddRange(_newstuffs);
                    Console.WriteLine("Recognized [ stuffs ]");
                    return true;
                }
                /*
                return _stuffs
                */
            }
            Object _stuff0 = Memoize(stuff);
            if (_stuff0 != null)
            {
                Console.WriteLine("Recognized [ stuffs ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object stuff()
        {
            int _pos = Mark();
            Object _stringItem = Expect("{");
            Object _stuffs = Memoize(stuffs);
            Object _stringItem2 = Expect("}");
            if (_stringItem != null && _stuffs != null && _stringItem2 != null)
            {
                Console.WriteLine("Recognized [ stuff ]");
                return true;
            }
            Object _name = Expect("NAME");
            if (_name != null)
            {
                Console.WriteLine("Recognized [ stuff ]");
                return true;
            }
            Object _number = Expect("NUMBER");
            if (_number != null)
            {
                Console.WriteLine("Recognized [ stuff ]");
                return true;
            }
            Object _string = Expect("STRING");
            if (_string != null)
            {
                Console.WriteLine("Recognized [ stuff ]");
                return true;
            }
            Object _op = Expect("OP");
            if (_op != null)
            {
                Console.WriteLine("Recognized [ stuff ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
    }
}
