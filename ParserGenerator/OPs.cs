using System.Collections.Generic;

namespace ParserGenerator
{
    public class OPs
    {
        private HashSet<char> _OPs = new HashSet<char>();

        public OPs()
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