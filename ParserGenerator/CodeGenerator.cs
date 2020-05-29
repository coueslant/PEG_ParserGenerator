using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParserGenerator
{
    public class CodeGenerator
    {
        public static void GenerateParserCode(Grammar grammar)
        {
            StringBuilder _parserCodeString = new StringBuilder();

            _parserCodeString.AppendLine("/*");
            _parserCodeString.AppendLine("This is @generated code, do not modify!");
            _parserCodeString.AppendLine("*/");

            _parserCodeString.AppendLine("using System;");

            _parserCodeString.AppendLine("namespace Parser {");
            _parserCodeString.AppendLine();
            _parserCodeString.AppendLine("class GeneratedParser : Parser {");

            _parserCodeString.AppendLine("public GeneratedParser(string parsingInput) : base() {");
            _parserCodeString.AppendLine("Tokenizer.SetTokenGenerator(new TokenGenerator(parsingInput));");
            _parserCodeString.AppendLine("}");

            foreach (Rule _rule in grammar.Rules)
            {
                _parserCodeString.AppendLine($"public Object {_rule.Name}() {{");
                _parserCodeString.AppendLine("int _pos = Mark();");
                List<string> _vars = new List<string>();
                foreach (Alternative _alt in _rule.Alternatives)
                {
                    List<string> _items = new List<string>();
                    foreach (string _item in _alt.Items)
                    {
                        bool _isString = _item.ToLower()[0] == '"' || _item.ToLower()[0] == '\'';
                        string _var = "_" + _item.ToLower();
                        if (_isString)
                        {
                            _var = "_string";
                        }

                        if (_items.Contains(_var) || _vars.Contains(_var))
                        {
                            _var = _var + _vars.Count.ToString();
                        }

                        _items.Add(_var);
                        _vars.Add(_var);
                        _parserCodeString.Append($"Object {_var} = ");

                        if (_isString)
                        {
                            _parserCodeString.AppendLine($"Expect({_item.ToString()});");
                        }
                        else if (_item.Equals(_item.ToUpper()))
                        {
                            _parserCodeString.AppendLine($"Expect(\"{_item}\");");
                        }
                        else
                        {
                            _parserCodeString.AppendLine($"Memoize({_item});");
                        }
                    }

                    _parserCodeString.Append("if(");
                    foreach (string var in _items)
                    {
                        if (var != _items[_items.Count - 1])
                        {
                            _parserCodeString.Append($"{var} != null || ");

                        }
                        else
                        {
                            _parserCodeString.Append($"{var} != null");
                        }
                    }
                    _parserCodeString.AppendLine(") {");
                    // node returning code here
                    _parserCodeString.Append($"Console.WriteLine(\"Recognized [ {_rule.Name} ]\")");
                    _parserCodeString.Append("return true;");
                    _parserCodeString.AppendLine("}");
                }
                _parserCodeString.AppendLine($"Reset(_pos);");
                _parserCodeString.AppendLine("return null;");
                _parserCodeString.AppendLine("}");
            }
            _parserCodeString.AppendLine("}");
            _parserCodeString.AppendLine("}");

            File.Delete(@".\GeneratedParsers\GeneratedParser.cs");
            File.WriteAllText(@".\GeneratedParsers\GeneratedParser.cs", _parserCodeString.ToString());
        }
    }
}