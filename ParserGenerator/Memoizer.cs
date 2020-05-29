using System;

namespace ParserGenerator
{
    public class Memoizer
    {
        public Object Memoize(Parser parser, Func<Object> f)
        {
            // for now, since we are not memoizing any functions with arguments, doing this here is okay
            // TODO: Improve ability to handle arguments in the memoize wrapper, we're halfway there but not quite
            return MemoizeWrapper(parser, f, Tuple.Create(new Object()));
        }

        private Object MemoizeWrapper(Parser parser, Func<Object> f, Tuple<Object> args)
        {
            int _pos = parser.Mark();
            Memo _memo;
            Object _result;
            int _endPos;
            if (!(parser.Memos.TryGetValue(_pos, out _memo)))
            {
                Memo _newMemo = new Memo();
                parser.Memos.Add(_pos, _newMemo);
                _memo = _newMemo;
            }
            Tuple<Func<Object>, Tuple<Object>> _key = Tuple.Create(f, args);
            if (_memo.Memos.ContainsKey(_key))
            {
                Tuple<Object, int> _memoEntry;
                _memo.Memos.TryGetValue(_key, out _memoEntry);
                _result = _memoEntry.Item1;
                _endPos = _memoEntry.Item2;
                parser.Reset(_endPos);
                System.Console.WriteLine("RETURNING MEMOIZED RESULT");
            }
            else
            {
                _result = f();
                _endPos = parser.Mark();
                _memo.Memos.Add(_key, Tuple.Create(_result, _endPos));
            }
            return _result;
        }
    }
}