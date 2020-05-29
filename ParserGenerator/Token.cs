namespace ParserGenerator
{
    public class Token
    {
        TokenKind _type;
        string _string;

        public TokenKind Type { get { return _type; } }

        public string @String { get { return _string; } }

        public Token(TokenKind type, string @string)
        {
            _type = type;
            _string = @string;
        }
    }
}