using System.Linq;
namespace ParserGenerator {
    public class Tokenizer {

        private int _pos = 0;
        private TokenGenerator _tokenGen = new ITokenGenerator ();
        private List<Token> _tokens = new List<Token> ();

        public Token NextToken () {
            Token _token = PeekToken ();
            _pos++;
            return _token;
        }

        public Token PeekToken () {
            if (_pos = Length (_tokens)) {
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
    }
}