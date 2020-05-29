using System;
using System.Collections.Generic;

namespace ParserGenerator
{
    public class Memo
    {
        private Dictionary<Tuple<Func<Object>, Tuple<Object>>, Tuple<Object, int>> _memos;
        public Dictionary<Tuple<Func<Object>, Tuple<Object>>, Tuple<Object, int>> Memos { get { return _memos; } }

        public Memo()
        {
            _memos = new Dictionary<Tuple<Func<object>, Tuple<object>>, Tuple<object, int>>();
        }

        public void AddMemo(Tuple<Func<Object>, Tuple<Object>> key, Tuple<Object, int> value)
        {
            _memos.Add(key, value);
        }

        public Tuple<Object, int> GetMemo(Tuple<Func<Object>, Tuple<Object>> key)
        {
            Tuple<Object, int> _memoResult;
            _memos.TryGetValue(key, out _memoResult);
            return _memoResult;
        }
    }
}