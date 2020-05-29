/*
This is @generated code, do not modify!
*/
using System;
namespace ParserGenerator
{

    class GeneratedParser : Parser
    {
        public GeneratedParser(string parsingInput) : base()
        {
            Tokenizer.SetTokenGenerator(new TokenGenerator(parsingInput));
        }
        public Object Parse()
        {
            return Expr();
        }
        public Object Expr()
        {
            int _pos = Mark();
            Object _sum = Memoize(Sum);
            if (_sum != null)
            {
                Console.WriteLine("Recognized [ Expr ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Sum()
        {
            int _pos = Mark();
            Object _product = Memoize(Product);
            Object _string = Expect("+");
            Object _product2 = Memoize(Product);
            if (_product != null || _string != null || _product2 != null)
            {
                Console.WriteLine("Recognized [ Sum ]");
                return true;
            }
            Object _product3 = Memoize(Product);
            Object _string4 = Expect("-");
            Object _product5 = Memoize(Product);
            if (_product3 != null || _string4 != null || _product5 != null)
            {
                Console.WriteLine("Recognized [ Sum ]");
                return true;
            }
            Object _product6 = Memoize(Product);
            if (_product6 != null)
            {
                Console.WriteLine("Recognized [ Sum ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Product()
        {
            int _pos = Mark();
            Object _power = Memoize(Power);
            Object _string = Expect("*");
            Object _power2 = Memoize(Power);
            if (_power != null || _string != null || _power2 != null)
            {
                Console.WriteLine("Recognized [ Product ]");
                return true;
            }
            Object _power3 = Memoize(Power);
            Object _string4 = Expect("/");
            Object _power5 = Memoize(Power);
            if (_power3 != null || _string4 != null || _power5 != null)
            {
                Console.WriteLine("Recognized [ Product ]");
                return true;
            }
            Object _power6 = Memoize(Power);
            if (_power6 != null)
            {
                Console.WriteLine("Recognized [ Product ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Power()
        {
            int _pos = Mark();
            Object _value = Memoize(Value);
            Object _string = Expect("^");
            Object _power = Memoize(Power);
            if (_value != null || _string != null || _power != null)
            {
                Console.WriteLine("Recognized [ Power ]");
                return true;
            }
            Object _value3 = Memoize(Value);
            if (_value3 != null)
            {
                Console.WriteLine("Recognized [ Power ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Value()
        {
            int _pos = Mark();
            Object _number = Expect("NUMBER");
            if (_number != null)
            {
                Console.WriteLine("Recognized [ Value ]");
                return true;
            }
            Object _string = Expect("(");
            Object _expr = Memoize(Expr);
            Object _string3 = Expect(")");
            if (_string != null || _expr != null || _string3 != null)
            {
                Console.WriteLine("Recognized [ Value ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
    }
}
