namespace ParserGenerator {
    public class Token {
        String _type;
        String _string;

        public String Type {
            get { return _type; }
            set { _type = value; }
        }

        public String @String {
            get { return _string; }
            set { _string = value; }
        }

        public Token (String type, String @string) {
            _type = type;
            _string = @string;
        }
    }
}