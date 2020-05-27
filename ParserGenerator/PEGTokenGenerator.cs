using System;
using System.Text;
namespace ParserGenerator {
    public class PEGTokenGenerator : ITokenGenerator {
        private String _inputPEG;
        private int _pos;
        public PEGTokenGenerator (String inputPEG) {
            _inputPEG = inputPEG;
            _pos = 0;
        }
        public Token NextToken () {
            Token _token = ConsumeToken ();
            if (_token == null) {
                return new Token (TokenKind.ENDMARKER, "ENDMARKER");
            }
            return _token;
        }

        private Token ConsumeToken () {
            if (Current () == '"') {
                return ConsumeString ();
            }

            if (Char.IsLetter (Current ())) {
                return ConsumeName ();
            }

            if (Char.IsDigit (Current ())) {
                return ConsumeNumber ();
            }

            return null;
        }

        private Token ConsumeString () {
            StringBuilder _string = new StringBuilder ();
            _string.Append (Current ());
            Next ();
            while (Current () != '"') {
                if (Current () == '\\') {
                    _string.Append (Current ());
                    Next ();
                    _string.Append (Current ());
                    Next ();
                } else {
                    _string.Append (Current ());
                    Next ();
                }
            }
            _string.Append (Current ());

            return new Token (TokenKind.STRING, _string.ToString ());
        }

        private Token ConsumeName () {
            StringBuilder _string = new StringBuilder ();
            _string.Append (Current);
            Next ();
            while (Current ().IsLetter ()) {
                _string.Append (Current ());
                Next ();
            }

            return new Token (TokenKind.NAME, _string.ToString ());
        }

        private Token ConsumeNumber () {
            StringBuilder _string = new StringBuilder ();
            _string.Append (Current ());
            Next ();
            while (Current ().IsDigit ()) {
                _string.Append (Current ());
                Next ();
            }

            return new Token (TokenKind.NUMBER, _string.ToString ());
        }

        private char Current () {
            return _inputPEG.Substring (_pos, 1);
        }

        private void Next () {
            _pos++;
        }
    }
}