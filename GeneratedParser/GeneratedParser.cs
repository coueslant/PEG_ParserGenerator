/*
This is @generated code, do not modify!
*/
using System;
namespace ParserGenerator {

class GeneratedParser : Parser {
public GeneratedParser(string parsingInput) : base() {
Tokenizer.SetTokenGenerator(new TokenGenerator(parsingInput));
}
public Object Parse() {
return start();
}
public Object start() {
int _pos = Mark();
Object _expr = Memoize(expr);
Object _newline = Expect("NEWLINE");
if(_expr != null || _newline != null) {
Console.WriteLine("Recognized [ start ]");
return;
}
Reset(_pos);
return null;
}
public Object expr() {
int _pos = Mark();
Object _term = Memoize(term);
Object _string = Expect("+");
Object _expr = Memoize(expr);
if(_term != null || _string != null || _expr != null) {
Console.WriteLine("Recognized [ expr ]");
return;
}
Object _term3 = Memoize(term);
Object _string4 = Expect("-");
Object _expr5 = Memoize(expr);
if(_term3 != null || _string4 != null || _expr5 != null) {
Console.WriteLine("Recognized [ expr ]");
return;
}
Object _term6 = Memoize(term);
if(_term6 != null) {
Console.WriteLine("Recognized [ expr ]");
return;
}
Reset(_pos);
return null;
}
public Object term() {
int _pos = Mark();
Object _number = Expect("NUMBER");
if(_number != null) {
Console.WriteLine("Recognized [ term ]");
return;
}
Reset(_pos);
return null;
}
}
}
