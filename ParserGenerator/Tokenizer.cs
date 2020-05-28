using System.Collections.Generic;
using System.Linq;
namespace ParserGenerator {
    public class Tokenizer {
        private int _pos;
        private ITokenGenerator _tokenGen;
        private List<Token> _tokens;

        public Tokenizer () {
            _pos = 0;
            _tokens = new List<Token> ();
        }

        public Token NextToken () {
            Token _token = PeekToken ();
            _pos++;
            return _token;
        }

        public Token PeekToken () {
            if (_pos == _tokens.Count) {
                _tokens.Append (_tokenGen.NextToken ());
            }
            return _tokens[_pos];
        }

        public int Mark () {
            return _pos;
        }

        public void Reset (int position) {
            _pos = position;
        }

        public void SetTokenGenerator (ITokenGenerator _tokenGenerator) {
            _tokenGen = _tokenGenerator;
        }
    }
}