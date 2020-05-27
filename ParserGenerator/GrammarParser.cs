using System.Reflection.Emit;
namespace ParserGenerator {
    public class GrammarParser {
        private GrammerTokenizer _tokenizer;
        public GrammarParser () {
            _tokenizer = new GrammarTokenizer ();
        }

        public int Mark () {
            return _tokenizer.Mark ();
        }

        public void Reset (int pos) {
            _tokenizer.Reset (pos);
        }

        public expect (String argument) {
            return _tokenizer.NextToken ();
        }
    }
}