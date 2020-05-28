using System;
using System.Collections.Generic;

namespace ParserGenerator
{
    public class ParserGenerator
    {

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Parse(args[0]);
                // Tokenizer _tokenizer = new Tokenizer();
                // _tokenizer.SetTokenGenerator(new TokenGenerator(System.IO.File.ReadAllText(args[0])));

                // foreach (Token _token in _tokenizer.Tokenize())
                // {
                //     System.Console.WriteLine(_token.Type.ToString() + " : " + _token.String);
                // }
            }
            else
            {
                System.Console.WriteLine("No file provided.");
            }
        }
        public static void Parse(string grammarFile)
        {
            string grammarString = System.IO.File.ReadAllText(grammarFile);

            GrammarParser parser = new GrammarParser(grammarString);

            List<Rule> _rules = new List<Rule>();

            _rules = parser.Grammar();

            if (_rules != null)
            {
                foreach (Rule _rule in _rules)
                {
                    System.Console.WriteLine("Name: " + _rule.GetName());
                    System.Console.WriteLine("Alternatives:");
                    foreach (List<string> _alternative in _rule.GetAlternatives())
                    {
                        System.Console.Write("->\t");
                        foreach (string _alternativeItem in _alternative)
                        {
                            System.Console.Write(_alternativeItem + " ");
                        }
                        System.Console.WriteLine();
                    }
                }

            }
        }
    }
}