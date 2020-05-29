/*
This is @generated code, do not modify!
*/
using System;
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
            Object _rules3 = Memoize(rules);
            Object _endmarker4 = Expect("ENDMARKER");
            if (_rules3 != null && _endmarker4 != null)
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
            Object _metas = Memoize(metas);
            if (_meta != null && _metas != null)
            {
                Console.WriteLine("Recognized [ metas ]");
                return true;
            }
            Object _meta2 = Memoize(meta);
            if (_meta2 != null)
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
            Object _string = Expect("@");
            Object _name = Expect("NAME");
            Object _string2 = Expect("STRING");
            Object _newline = Expect("NEWLINE");
            if (_string != null && _name != null && _string2 != null && _newline != null)
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
            Object _rules = Memoize(rules);
            if (_rule != null && _rules != null)
            {
                Console.WriteLine("Recognized [ rules ]");
                return true;
            }
            Object _rule2 = Memoize(rule);
            if (_rule2 != null)
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
            Object _string = Expect(":");
            Object _alts = Memoize(alts);
            Object _newline = Expect("NEWLINE");
            if (_name != null && _string != null && _alts != null && _newline != null)
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
            Object _string = Expect("|");
            Object _alts = Memoize(alts);
            if (_alt != null && _string != null && _alts != null)
            {
                Console.WriteLine("Recognized [ alts ]");
                return true;
            }
            Object _alt3 = Memoize(alt);
            if (_alt3 != null)
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
            Object _items2 = Memoize(items);
            if (_items2 != null)
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
            Object _items = Memoize(items);
            if (_item != null && _items != null)
            {
                Console.WriteLine("Recognized [ items ]");
                return true;
            }
            Object _item2 = Memoize(item);
            if (_item2 != null)
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
            Object _string = Expect("{");
            Object _stuffs = Memoize(stuffs);
            Object _string2 = Expect("}");
            if (_string != null && _stuffs != null && _string2 != null)
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
            Object _stuffs = Memoize(stuffs);
            if (_stuff != null && _stuffs != null)
            {
                Console.WriteLine("Recognized [ stuffs ]");
                return true;
            }
            Object _stuff2 = Memoize(stuff);
            if (_stuff2 != null)
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
            Object _string = Expect("{");
            Object _stuffs = Memoize(stuffs);
            Object _string2 = Expect("}");
            if (_string != null && _stuffs != null && _string2 != null)
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
            Object _string5 = Expect("STRING");
            if (_string5 != null)
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
