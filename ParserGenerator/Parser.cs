namespace ParserGenerator
{
    public class Parser
    {
        private Tokenizer _tokenizer;
        public string _parsing;
        public int _lineCount;
        public Parser()
        {
            _tokenizer = new Tokenizer();
        }

        public int Mark()
        {
            return _tokenizer.Mark();
        }

        public void Reset(int pos)
        {
            _tokenizer.Reset(pos);
        }

        public Token Expect(string argument)
        {
            Token _token = _tokenizer.PeekToken();

            while (_token.Type.ToString() == "WHITESPACE" || string.IsNullOrWhiteSpace(_token.String))
            {
                _token = _tokenizer.PeekToken();
                if (_token.Type.ToString() == "WHITESPACE" || string.IsNullOrWhiteSpace(_token.String))
                {
                    _token = _tokenizer.NextToken();
                }
            }

            if (_token.Type.ToString() == argument || _token.String == argument)
            {
                System.Console.WriteLine("Expected [ " + argument + " ] and saw [ type: " + _token.Type.ToString() + " string: " + _token.String + " ] while parsing [ " + _parsing + " ]");
                _tokenizer.NextToken();
                return _token;
            }
            // did not see expected token
            System.Console.WriteLine("Expected [ " + argument + " ] but saw [ type: " + _token.Type.ToString() + " string: " + _token.String + " ] while parsing [ " + _parsing + " ]");
            return null;
        }

        public Tokenizer GetTokenizer()
        {
            return _tokenizer;
        }
    }
}