using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParserGenerator
{
    public class CodeGenerator
    {
        public static void GenerateParserCode(Grammar grammar)
        {
            StringBuilder _codeStringBuilder = new StringBuilder();

            AddHeader(_codeStringBuilder);

            AddParseMethod(_codeStringBuilder, grammar.Rules[0].Name);

            foreach (Rule _rule in grammar.Rules)
            {
                HandleRule(_codeStringBuilder, _rule);
            }

            AddEnd(_codeStringBuilder);

            string _parserPath = @".\GeneratedParser\GeneratedParser.cs";

            WriteParserFile(_parserPath, _codeStringBuilder);
        }

        private static void AddHeader(StringBuilder codeStringBuilder)
        {
            codeStringBuilder.AppendLine("/*");
            codeStringBuilder.AppendLine("This is @generated code, do not modify!");
            codeStringBuilder.AppendLine("*/");

            codeStringBuilder.AppendLine("using System;");

            codeStringBuilder.AppendLine("namespace ParserGenerator {");
            codeStringBuilder.AppendLine();
            codeStringBuilder.AppendLine("class GeneratedParser : Parser {");

            codeStringBuilder.AppendLine("public GeneratedParser(string parsingInput) : base() {");
            codeStringBuilder.AppendLine("Tokenizer.SetTokenGenerator(new TokenGenerator(parsingInput));");
            codeStringBuilder.AppendLine("}");
        }

        private static void AddParseMethod(StringBuilder codeStringBuilder, string firstRuleName)
        {
            codeStringBuilder.AppendLine("public Object Parse() {");
            codeStringBuilder.AppendLine($"return {firstRuleName}();");
            codeStringBuilder.AppendLine("}");
        }

        private static void HandleRule(StringBuilder codeStringBuilder, Rule rule)
        {
            codeStringBuilder.AppendLine($"public Object {rule.Name}() {{");
            codeStringBuilder.AppendLine("int _pos = Mark();");
            List<string> _vars = new List<string>();
            foreach (Alternative _alt in rule.Alternatives)
            {
                List<string> _items = new List<string>();
                HandleAlternative(codeStringBuilder, _alt, _vars, _items);

                codeStringBuilder.Append("if(");
                foreach (string var in _items)
                {
                    if (var != _items[_items.Count - 1])
                    {
                        codeStringBuilder.Append($"{var} != null && ");

                    }
                    else
                    {
                        codeStringBuilder.Append($"{var} != null");
                    }
                }
                codeStringBuilder.AppendLine(") {");
                codeStringBuilder.AppendLine($"Console.WriteLine(\"Recognized [ {rule.Name} ]\");");
                /*
                    TODO: write node returning code here
                */
                codeStringBuilder.AppendLine("return true;");
                codeStringBuilder.AppendLine("}");
            }
            codeStringBuilder.AppendLine($"Reset(_pos);");
            codeStringBuilder.AppendLine("return null;");
            codeStringBuilder.AppendLine("}");
        }

        private static void HandleAlternative(StringBuilder codeStringBuilder, Alternative alt, List<string> vars, List<string> items)
        {
            // TODO: to solve the infinite recursion problem i am seeing i need to come up with
            // TODO: a general version of the code seen in GrammarParser:Metas() or GrammarParser:Rules() 
            // TODO: and also determine when to apply that form vs. the form i already have.

            /*
            private List<Rule> Rules()
            {
                int _pos = Mark();
                Rule _rule = (Rule)Memoize(Rule);
                if (_rule != null)
                {
                    List<Rule> _rules = new List<Rule>();
                    _rules.Add(_rule);
                    List<Rule> _newRules = (List<Rule>)Memoize(Rules);
                    if (_newRules != null)
                    {
                        _rules.AddRange(_newRules);
                    }
                    System.Console.WriteLine("Successfully parsed rules.");
                    return _rules;
                }
                System.Console.WriteLine("Failed to parse rules at: [ position: " + _pos.ToString() + " ]");
                return null;
            }
            */


            foreach (string _item in alt.Items)
            {
                bool _isString = _item.ToLower()[0] == '"' || _item.ToLower()[0] == '\'';
                string _var = "_" + _item.ToLower();
                if (_isString)
                {
                    _var = "_string";
                }

                if (items.Contains(_var) || vars.Contains(_var))
                {
                    _var = _var + vars.Count.ToString();
                }

                items.Add(_var);
                vars.Add(_var);
                codeStringBuilder.Append($"Object {_var} = ");

                if (_isString)
                {
                    codeStringBuilder.AppendLine($"Expect({_item.ToString().Replace('\'', '"')});");
                }
                else if (_item.Equals(_item.ToUpper()))
                {
                    codeStringBuilder.AppendLine($"Expect(\"{_item}\");");
                }
                else
                {
                    codeStringBuilder.AppendLine($"Memoize({_item});");
                }
            }
        }

        private static void AddEnd(StringBuilder codeStringBuilder)
        {
            codeStringBuilder.AppendLine("}");
            codeStringBuilder.AppendLine("}");
        }

        private static void WriteParserFile(string parserFilePath, StringBuilder codeStringBuilder)
        {
            if (File.Exists(parserFilePath))
            {
                File.Delete(parserFilePath);
            }
            File.WriteAllText(parserFilePath, codeStringBuilder.ToString());
        }
    }
}