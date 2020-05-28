using System;
using System.Text;
namespace ParserGenerator
{
    public class PEGTokenGenerator : ITokenGenerator
    {
        private string _inputPEG;
        private int _pos;
        public PEGTokenGenerator(string inputPEG)
        {
            _inputPEG = inputPEG;
            _pos = 0;
        }
        public Token NextToken()
        {
            Token _token = ConsumeToken();
            if (_token == null)
            {
                // raise token exception
                return _token;
            }
            return _token;
        }

        private Token ConsumeToken()
        {
            if ((char)Current() == '"' || (char)Current() == '\'')
            {
                return ConsumeString();
            }

            if (char.IsLetter((char)Current()))
            {
                return ConsumeName();
            }

            if (char.IsDigit((char)Current()))
            {
                return ConsumeNumber();
            }

            if ((char)Current() == '\n')
            {
                Token _token = new Token(TokenKind.NEWLINE, "NEWLINE");
                Next();
                return _token;
            }

            if ((char)Current() == ':' || (char)Current() == '(' || (char)Current() == ')' || (char)Current() == '&' || (char)Current() == '*' || (char)Current() == '*' || (char)Current() == '+' || (char)Current() == '?' || (char)Current() == '|' || (char)Current() == '[' || (char)Current() == ']' || (char)Current() == '-' || (char)Current() == '/')
            {
                Token _token = new Token(TokenKind.SYMBOL, ((char)Current()).ToString());
                Next();
                return _token;
            }

            if (char.IsWhiteSpace((char)Current()))
            {
                Token _token = new Token(TokenKind.WHITESPACE, "WHITESPACE");
                Next();
                return _token;
            }

            if (Current() == -1) // denotes the end of the file
            {
                return new Token(TokenKind.ENDMARKER, "ENDMARKER");
            }

            // failed to consume token
            System.Console.WriteLine("Failed to consume token at: [ position: " + _pos + " string: " + ((char)Current()).ToString() + " ]");
            return null;
        }

        private Token ConsumeString()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append((char)Current());
            char _quoteType = (char)Current();
            Next();
            while ((char)Current() != _quoteType)
            {
                if ((char)Current() == '\\')
                {
                    _string.Append((char)Current());
                    Next();
                    _string.Append((char)Current());
                    Next();
                }
                else
                {
                    _string.Append((char)Current());
                    Next();
                }
            }
            _string.Append((char)Current());
            Next();
            return new Token(TokenKind.STRING, _string.ToString());
        }

        private Token ConsumeName()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append((char)Current());
            Next();
            while (char.IsLetter((char)Current()) || (char)Current() == '_')
            {
                _string.Append((char)Current());
                Next();
            }

            return new Token(TokenKind.NAME, _string.ToString());
        }

        private Token ConsumeNumber()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append((char)Current());
            Next();
            while (char.IsDigit((char)Current()))
            {
                _string.Append((char)Current());
                Next();
            }

            return new Token(TokenKind.NUMBER, _string.ToString());
        }

        private int Current()
        {
            if (_pos < _inputPEG.Length)
            {
                return (int)_inputPEG[_pos];
            }
            return -1;
        }

        private void Next()
        {
            _pos++;
        }
    }
}