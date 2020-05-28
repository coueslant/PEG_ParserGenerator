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
                return new Token(TokenKind.ENDMARKER, "ENDMARKER");
            }
            return _token;
        }

        private Token ConsumeToken()
        {
            if (Current() == '"' || Current() == '\'')
            {
                return ConsumeString();
            }

            if (char.IsLetter(Current()))
            {
                return ConsumeName();
            }

            if (char.IsDigit(Current()))
            {
                return ConsumeNumber();
            }

            if (Current() == '\n')
            {
                Token _token = new Token(TokenKind.NEWLINE, "NEWLINE");
                Next();
                return _token;
            }

            if (Current() == ':' || char.IsSymbol(Current()))
            {
                Token _token = new Token(TokenKind.SYMBOL, Current().ToString());
                Next();
                return _token;
            }

            if (char.IsWhiteSpace(Current()))
            {
                Token _token = new Token(TokenKind.WHITESPACE, "WHITESPACE");
                Next();
                return _token;
            }

            return null;
        }

        private Token ConsumeString()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append(Current());
            char _quoteType = Current();
            Next();
            while (Current() != _quoteType)
            {
                if (Current() == '\\')
                {
                    _string.Append(Current());
                    Next();
                    _string.Append(Current());
                    Next();
                }
                else
                {
                    _string.Append(Current());
                    Next();
                }
            }
            _string.Append(Current());
            Next();
            return new Token(TokenKind.STRING, _string.ToString());
        }

        private Token ConsumeName()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append(Current());
            Next();
            while (char.IsLetter(Current()) || Current() == '_')
            {
                _string.Append(Current());
                Next();
            }

            return new Token(TokenKind.NAME, _string.ToString());
        }

        private Token ConsumeNumber()
        {
            StringBuilder _string = new StringBuilder();
            _string.Append(Current());
            Next();
            while (char.IsDigit(Current()))
            {
                _string.Append(Current());
                Next();
            }

            return new Token(TokenKind.NUMBER, _string.ToString());
        }

        private char Current()
        {
            return _inputPEG[_pos];
        }

        private void Next()
        {
            _pos++;
        }
    }
}