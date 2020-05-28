namespace ParserGenerator {
    public class Token {
        TokenKind _type;
        string _string;

        public TokenKind Type {
            get { return _type; }
            set { _type = value; }
        }

        public string @String {
            get { return _string; }
            set { _string = value; }
        }

        public Token (TokenKind type, string @string) {
            _type = type;
            _string = @string;
        }
    }
}