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
            StringBuilder _parserCodeString = new StringBuilder();

            _parserCodeString.AppendLine("namespace Parser {");
            _parserCodeString.AppendLine();
            _parserCodeString.AppendLine("class GrammarParser : Parser {");
            foreach (Rule _rule in grammar.Rules)
            {
                _parserCodeString.AppendLine($"public void {_rule.Name}() {{");
                _parserCodeString.AppendLine("int _pos = Mark();");
                foreach (Alternative _alt in _rule.Alternatives)
                {
                    List<string> _items = new List<string>();
                    foreach (string _item in _alt.Items)
                    {
                        if (_item[0] == '"' || _item[0] == '\'')
                        {
                            _parserCodeString.AppendLine($"Expect({_item.ToString()});");
                        }
                        else
                        {
                            string _var = _item.ToLower();
                            if (_items.Contains(_var))
                            {
                                _var = _var + _items.Count.ToString();
                            }
                            _items.Add(_var);
                            if (_item.Equals(_item.ToUpper()))
                            {
                                _parserCodeString.AppendLine($"Object _{_var};");
                            }
                        }
                    }
                }
                _parserCodeString.AppendLine("}");
            }

            _parserCodeString.AppendLine("}");
            _parserCodeString.AppendLine("}");

            File.Delete(@".\GeneratedParsers\Parser.cs");
            File.WriteAllText(@".\GeneratedParsers\Parser.cs", _parserCodeString.ToString());
        }
    }
}