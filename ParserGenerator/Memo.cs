using System;
using System.Collections.Generic;

namespace ParserGenerator
{
    public class Memo
    {
        private Tuple<Func<Object>, string> _key;
        private Dictionary<Tuple<Func<Object>, string>, Tuple<Object, int>> _memos;
        public Dictionary<Tuple<Func<Object>, string>, Tuple<Object, int>> Memos { get { return _memos; } }
        public Tuple<Func<Object>, string> Key { get { return _key; } set { _key = value; } }
        public Memo()
        {

        }

        public void AddMemo(Tuple<Func<Object>, string> key, Tuple<Object, int> value)
        {
            _memos.Add(key, value);
        }

        public Tuple<Object, int> GetMemo(Tuple<Func<Object>, string> key)
        {
            Tuple<Object, int> _memoResult;
            _memos.TryGetValue(key, out _memoResult);
            return _memoResult;
        }
    }
}