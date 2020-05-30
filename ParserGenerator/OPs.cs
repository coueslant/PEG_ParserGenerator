using System.Collections.Generic;

namespace ParserGenerator
{
    public class Ops
    {
        private HashSet<char> _OPs = new HashSet<char>();
        public HashSet<char> OPs { get { return _OPs; } }

        public Ops()
        {
            GenerateOPSet();
        }
        public HashSet<char> GetOPs()
        {
            return _OPs;
        }

        private void GenerateOPSet()
        {
            _OPs.Add('|');
            _OPs.Add('*');
            _OPs.Add('+');
            _OPs.Add('?');
            _OPs.Add('&');
            _OPs.Add('!');
            _OPs.Add(':');
        }
    }
}