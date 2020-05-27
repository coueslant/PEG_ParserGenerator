using System.Reflection.Emit;
namespace ParserGenerator {
    public class Parser {
        public Parser () {
            _tokenizer = new GrammarTokenizer ();
        }

        public int Mark () {
            return _tokenizer.Mark ();
        }

        public void Reset (int pos) {
            _tokenizer.Reset (pos);
        }

        public Expect (String argument) {
            Tokenizer _token = _tokenizer.PeekToken ();
            if (_token.type == argument || _token.string == argument) {
                return _tokenizer.NextToken ();
            }
            return None;
        }
    }
}