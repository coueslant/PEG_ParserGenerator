namespace ParserGenerator {
    public class Parser {
        private Tokenizer _tokenizer;
        public Parser () {
            _tokenizer = new Tokenizer ();
        }

        public int Mark () {
            return _tokenizer.Mark ();
        }

        public void Reset (int pos) {
            _tokenizer.Reset (pos);
        }

        public Token Expect (string argument) {
            Token _token = _tokenizer.PeekToken ();
            if (_token.Type.ToString () == argument || _token.String == argument) {
                return _tokenizer.NextToken ();
            }
            return null;
        }

        public Tokenizer GetTokenizer () {
            return _tokenizer;
        }
    }
}