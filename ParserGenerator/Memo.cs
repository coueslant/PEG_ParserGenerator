using System;

namespace ParserGenerator
{
    public class Memo
    {
        private Tuple<Func<T>, string> _key;
        private Func<T> _func;
        private Tuple<T, int> _result;

        public Tuple<Func<T>, string> Key { get { return _key; } }

        public Memo()
        {

        }
    }
}