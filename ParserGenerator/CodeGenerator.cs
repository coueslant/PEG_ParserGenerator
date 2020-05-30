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
            string _testPath = @".\ParserGenerator.Tests\GeneratedParser\GeneratedParser.cs";

            WriteParserFile(_parserPath, _codeStringBuilder);
            WriteParserFile(_testPath, _codeStringBuilder);
        }

        private static void AddHeader(StringBuilder codeStringBuilder)
        {
            codeStringBuilder.AppendLine("/*");
            codeStringBuilder.AppendLine("This is @generated code, do not modify!");
            codeStringBuilder.AppendLine("*/");
            codeStringBuilder.AppendLine();

            codeStringBuilder.AppendLine("/*");
            codeStringBuilder.AppendLine("Required usings. Eventually this will import pieces from the ParserGenerator");
            codeStringBuilder.AppendLine("itself and allow the generated parser to exist in it's own namespace.");
            codeStringBuilder.AppendLine("*/");

            codeStringBuilder.AppendLine("using System;");
            codeStringBuilder.AppendLine("using System.Collections.Generic;");
            codeStringBuilder.AppendLine();

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
                HandleAlternative(codeStringBuilder, rule, _alt, _vars, _items);
            }
            codeStringBuilder.AppendLine($"Reset(_pos);");
            codeStringBuilder.AppendLine("return null;");
            codeStringBuilder.AppendLine("}");
        }

        private static void HandleAlternative(StringBuilder codeStringBuilder, Rule rule, Alternative alt, List<string> vars, List<string> items)
        {
            // TODO: to solve the infinite recursion problem i am seeing i need to come up with
            // TODO: a general version of the code seen in GrammarParser:Metas() or GrammarParser:Rules() 
            // TODO: and also determine when to apply that form vs. the form i already have.

            // detect a right recursive alternative
            if (alt.Items[alt.Items.Count - 1] == rule.Name)
            {
                HandleRightRecursiveAlternative(codeStringBuilder, rule, alt, items, vars);
            }
            else
            {
                HandleStandardAlternative(codeStringBuilder, rule, alt, items, vars);
            }
        }

        private static void HandleRightRecursiveAlternative(StringBuilder codeStringBuilder, Rule rule, Alternative alt, List<string> items, List<string> vars)
        {
            System.Console.WriteLine("Right recursive rule alternative detected.");

            string _var = CreateVar(codeStringBuilder, alt.Items[0], items, vars);

            codeStringBuilder.Append("if(");
            codeStringBuilder.Append($"{_var} != null");
            codeStringBuilder.AppendLine(") {");
            // further condition matching here
            foreach (string _item in alt.Items.GetRange(1, alt.Items.Count - 1))
            {
                if (_item != alt.Items[alt.Items.Count - 1])
                {
                    CreateVar(codeStringBuilder, _item, items, vars);
                }
                else
                {
                    codeStringBuilder.AppendLine($"List<Object> _{_item} = new List<Object>();");
                    codeStringBuilder.AppendLine($"_{_item}.Add({_var});");
                    codeStringBuilder.AppendLine($"List<Object> _new{_item} = (List<Object>)Memoize({_item});");
                    codeStringBuilder.Append($"if(_new{_item} != null");
                    codeStringBuilder.AppendLine(") {");
                    codeStringBuilder.AppendLine($"_{_item}.AddRange(_new{_item});");
                    codeStringBuilder.AppendLine($"Console.WriteLine(\"Recognized [ {rule.Name} ]\");");
                    codeStringBuilder.AppendLine("return true;");
                    codeStringBuilder.AppendLine("}");
                }
                AddComment(codeStringBuilder, $"return _{_item}");
            }

            codeStringBuilder.AppendLine("}");
        }

        private static void HandleStandardAlternative(StringBuilder codeStringBuilder, Rule rule, Alternative alt, List<string> items, List<string> vars)
        {
            foreach (string _item in alt.Items)
            {
                CreateVar(codeStringBuilder, _item, items, vars);
            }
            codeStringBuilder.Append("if(");
            foreach (string var in items)
            {
                if (var != items[items.Count - 1])
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

        private static void AddEnd(StringBuilder codeStringBuilder)
        {
            codeStringBuilder.AppendLine("}");
            codeStringBuilder.AppendLine("}");
        }

        private static void AddComment(StringBuilder codeStringBuilder, string commentString)
        {
            codeStringBuilder.AppendLine("/*");
            codeStringBuilder.AppendLine(commentString);
            codeStringBuilder.AppendLine("*/");
        }

        private static string CreateVar(StringBuilder codeStringBuilder, string varName, List<string> items, List<string> vars)
        {
            bool _isString = varName.ToLower()[0] == '"' || varName.ToLower()[0] == '\'';
            string _var = "_" + varName.ToLower();

            if (_isString)
            {
                _var = "_stringItem";
            }

            if (vars.Contains(_var))
            {
                _var = _var + items.Count;
            }

            codeStringBuilder.Append($"Object {_var} = ");

            if (_isString)
            {
                codeStringBuilder.AppendLine($"Expect({varName.ToString().Replace('\'', '"')});");
            }
            else if (varName.Equals(varName.ToUpper()))
            {
                codeStringBuilder.AppendLine($"Expect(\"{varName}\");");
            }
            else
            {
                codeStringBuilder.AppendLine($"Memoize({varName});");
            }

            items.Add(_var);
            vars.Add(_var);

            return _var;
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