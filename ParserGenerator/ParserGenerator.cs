using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            else if (args.Length == 2)
            {
                // TestParser(args[0], args[1]);
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

            Grammar _grammar = parser.Grammar();

            if (_grammar != null)
            {
                PrintParsedGrammar(_grammar);
                GenerateParser(_grammar);
            }
        }

        public static void PrintParsedGrammar(Grammar _grammar)
        {

            if (_grammar.Metas.Count > 0)
            {
                System.Console.WriteLine("METAS");
                System.Console.WriteLine("+++++");
                foreach (Meta _meta in _grammar.Metas)
                {
                    System.Console.WriteLine("Name: " + _meta.Name);
                    System.Console.WriteLine("\t-> " + _meta.String);
                }
                System.Console.WriteLine("+++++");
                System.Console.WriteLine();
            }
            if (_grammar.Rules != null)
            {
                System.Console.WriteLine("RULES");
                System.Console.WriteLine("+++++");
                foreach (Rule _rule in _grammar.Rules)
                {
                    System.Console.WriteLine("Name: " + _rule.Name);
                    System.Console.WriteLine("Alternatives:");
                    foreach (Alternative _alternative in _rule.Alternatives)
                    {
                        System.Console.Write("\t-> ");
                        foreach (string _alternativeItem in _alternative.Items)
                        {
                            System.Console.Write(_alternativeItem + " ");
                        }
                        System.Console.WriteLine();
                        if (_alternative.Action.Length != 0)
                        {
                            System.Console.WriteLine("\t   Action: " + _alternative.Action);
                        }
                    }
                }
                System.Console.WriteLine("+++++");
            }
        }

        public static void GenerateParser(Grammar grammar)
        {
            CodeGenerator.GenerateParserCode(grammar);
        }

        // public static void TestParser(string grammarFilePath, string parsingInput)
        // {
        //     string _parsingInput;
        //     if (File.Exists(parsingInput))
        //     {
        //         _parsingInput = File.ReadAllText(parsingInput);
        //     }
        //     else
        //     {
        //         _parsingInput = parsingInput;
        //     }

        //     GeneratedParser _parser = new GeneratedParser(_parsingInput);
        //     _parser.Parse();
        // }
    }
}