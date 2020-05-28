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
            }
            else
            {
                System.Console.WriteLine("No file provided.");
            }
        }
        public static void Parse(string grammarFile)
        {
            string grammarString = System.IO.File.ReadAllText(grammarFile);

            PEGParser parser = new PEGParser(grammarString);

            List<Rule> _rules = new List<Rule>();

            _rules = parser.Grammar();

            foreach (Rule _rule in _rules)
            {
                System.Console.WriteLine(_rule.ToString());
            }

        }
    }
}