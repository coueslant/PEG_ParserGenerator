using System;

namespace ParserGenerator {
    public class ParserGenerator {

        static void Main (string[] args) {
            Parse (args[0]);
        }
        public static void Parse (string grammarFile) {
            string grammarString = System.IO.File.ReadAllText (grammarFile);

            PEGParser parser = new PEGParser (grammarString);

            parser.Grammar ();

        }
    }
}