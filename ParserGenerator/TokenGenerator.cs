using System;
using System.Collections.Generic;
using System.Text;
namespace ParserGenerator
{
    public class TokenGenerator
    {
        private string _inputPEG;
        private int _pos;

        private HashSet<char> _symbolSet;
        private HashSet<char> _OPSet;
        public TokenGenerator(string inputPEG)
        {
            _inputPEG = inputPEG;
            _symbolSet = (new Symbols()).GetSymbols();
            _OPSet = (new OPs()).GetOPs();
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

            if (_symbolSet.Contains((char)Current()))
            {
                return ConsumeSymbol();
            }

            if (_OPSet.Contains((char)Current()))
            {
                Token _token = new Token(TokenKind.OP, ((char)Current()).ToString());
                Next();
                return _token;
            }

            if (char.IsWhiteSpace((char)Current()))
            {
                Token _token = new Token(TokenKind.WHITESPACE, ((char)Current()).ToString());
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

        private Token ConsumeSymbol()
        {
            TokenKind _kind;
            switch ((char)Current())
            {
                case '(':
                    _kind = TokenKind.LPAREN;
                    break;
                case ')':
                    _kind = TokenKind.RPAREN;
                    break;
                case '[':
                    _kind = TokenKind.LSQUARE;
                    break;
                case ']':
                    _kind = TokenKind.RSQUARE;
                    break;
                case ',':
                    _kind = TokenKind.COMMA;
                    break;
                case ';':
                    _kind = TokenKind.SEMICOLON;
                    break;
                case '-':
                    _kind = TokenKind.MINUS;
                    break;
                case '/':
                    _kind = TokenKind.SLASH;
                    break;
                case '<':
                    _kind = TokenKind.LESSTHAN;
                    break;
                case '>':
                    _kind = TokenKind.GREATERTHAN;
                    break;
                case '=':
                    _kind = TokenKind.EQUAL;
                    break;
                case '.':
                    _kind = TokenKind.DOT;
                    break;
                case '%':
                    _kind = TokenKind.PERCENT;
                    break;
                case '{':
                    _kind = TokenKind.LBRACE;
                    break;
                case '}':
                    _kind = TokenKind.RBRACE;
                    break;
                case '~':
                    _kind = TokenKind.TILDE;
                    break;
                case '^':
                    _kind = TokenKind.CIRCUMFLEX;
                    break;
                case '@':
                    _kind = TokenKind.AT;
                    break;
                default:
                    _kind = TokenKind.NULLTOKEN;
                    break;
            }
            if (_kind != TokenKind.NULLTOKEN)
            {
                Token _token = new Token(TokenKind.SYMBOL, ((char)Current()).ToString());
                Next();
                return _token;
            }
            // failed to consume a symbol
            return null;
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