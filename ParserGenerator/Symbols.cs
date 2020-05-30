using System.Collections.Generic;

namespace ParserGenerator
{
    public class Symbols
    {
        private HashSet<char> _symbols = new HashSet<char>();

        public Symbols()
        {
            GenerateSymbolSet();
        }
        public HashSet<char> GetSymbols()
        {
            return _symbols;
        }

        private void GenerateSymbolSet()
        {
            _symbols.Add('(');
            _symbols.Add(')');
            _symbols.Add('[');
            _symbols.Add(']');
            _symbols.Add(',');
            _symbols.Add(';');
            _symbols.Add('-');
            _symbols.Add('/');
            _symbols.Add('<');
            _symbols.Add('>');
            _symbols.Add('=');
            _symbols.Add('.');
            _symbols.Add('%');
            _symbols.Add('{');
            _symbols.Add('}');
            _symbols.Add('~');
            _symbols.Add('^');
            _symbols.Add('@');
        }
    }
}