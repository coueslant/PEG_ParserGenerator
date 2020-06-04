/*
This is @generated code, do not modify!
*/

/*
Required usings. Eventually this will import pieces from the ParserGenerator
itself and allow the generated parser to exist in it's own namespace.
*/
using System;
using System.Collections.Generic;

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
            return Program();
        }
        public Object Program()
        {
            int _pos = Mark();
            Object _block = Memoize(Block);
            Object _stringItem = Expect("$");
            if (_block != null && _stringItem != null)
            {
                Console.WriteLine("Recognized [ Program ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Block()
        {
            int _pos = Mark();
            Object _stringItem = Expect("{");
            Object _statementlist = Memoize(StatementList);
            Object _stringItem2 = Expect("}");
            if (_stringItem != null && _statementlist != null && _stringItem2 != null)
            {
                Console.WriteLine("Recognized [ Block ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object StatementList()
        {
            int _pos = Mark();
            Object _statement = Memoize(Statement);
            if (_statement != null)
            {
                List<Object> _StatementList = new List<Object>();
                _StatementList.Add(_statement);
                List<Object> _newStatementList = (List<Object>)Memoize(StatementList);
                if (_newStatementList != null)
                {
                    _StatementList.AddRange(_newStatementList);
                    Console.WriteLine("Recognized [ StatementList ]");
                    return true;
                }
                /*
                return _StatementList
                */
            }
            Object _stringItem = Expect("");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ StatementList ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Statement()
        {
            int _pos = Mark();
            Object _printstatement = Memoize(PrintStatement);
            if (_printstatement != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Object _assignmentstatement = Memoize(AssignmentStatement);
            if (_assignmentstatement != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Object _vardecl = Memoize(VarDecl);
            if (_vardecl != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Object _whilestatement = Memoize(WhileStatement);
            if (_whilestatement != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Object _ifstatement = Memoize(IfStatement);
            if (_ifstatement != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Object _block = Memoize(Block);
            if (_block != null)
            {
                Console.WriteLine("Recognized [ Statement ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object PrintStatement()
        {
            int _pos = Mark();
            Object _stringItem = Expect("print");
            Object _stringItem1 = Expect("(");
            Object _expr = Memoize(Expr);
            Object _stringItem3 = Expect(")");
            if (_stringItem != null && _stringItem1 != null && _expr != null && _stringItem3 != null)
            {
                Console.WriteLine("Recognized [ PrintStatement ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object AssignmentStatement()
        {
            int _pos = Mark();
            Object _id = Memoize(Id);
            Object _stringItem = Expect("=");
            Object _expr = Memoize(Expr);
            if (_id != null && _stringItem != null && _expr != null)
            {
                Console.WriteLine("Recognized [ AssignmentStatement ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object VarDecl()
        {
            int _pos = Mark();
            Object _type = Memoize(type);
            Object _id = Memoize(Id);
            if (_type != null && _id != null)
            {
                Console.WriteLine("Recognized [ VarDecl ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object WhileStatement()
        {
            int _pos = Mark();
            Object _stringItem = Expect("while");
            Object _booleanexpr = Memoize(BooleanExpr);
            Object _block = Memoize(Block);
            if (_stringItem != null && _booleanexpr != null && _block != null)
            {
                Console.WriteLine("Recognized [ WhileStatement ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object IfStatement()
        {
            int _pos = Mark();
            Object _stringItem = Expect("if");
            Object _booleanexpr = Memoize(BooleanExpr);
            Object _block = Memoize(Block);
            if (_stringItem != null && _booleanexpr != null && _block != null)
            {
                Console.WriteLine("Recognized [ IfStatement ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Expr()
        {
            int _pos = Mark();
            Object _intexpr = Memoize(IntExpr);
            if (_intexpr != null)
            {
                Console.WriteLine("Recognized [ Expr ]");
                return true;
            }
            Object _stringexpr = Memoize(StringExpr);
            if (_stringexpr != null)
            {
                Console.WriteLine("Recognized [ Expr ]");
                return true;
            }
            Object _booleanexpr = Memoize(BooleanExpr);
            if (_booleanexpr != null)
            {
                Console.WriteLine("Recognized [ Expr ]");
                return true;
            }
            Object _id = Memoize(Id);
            if (_id != null)
            {
                Console.WriteLine("Recognized [ Expr ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object IntExpr()
        {
            int _pos = Mark();
            Object _digit = Memoize(digit);
            Object _intop = Memoize(intop);
            Object _expr = Memoize(Expr);
            if (_digit != null && _intop != null && _expr != null)
            {
                Console.WriteLine("Recognized [ IntExpr ]");
                return true;
            }
            Object _digit3 = Memoize(digit);
            if (_digit3 != null)
            {
                Console.WriteLine("Recognized [ IntExpr ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object StringExpr()
        {
            int _pos = Mark();
            Object _stringItem = Expect("\"");
            Object _charlist = Memoize(CharList);
            Object _stringItem2 = Expect("\"");
            if (_stringItem != null && _charlist != null && _stringItem2 != null)
            {
                Console.WriteLine("Recognized [ StringExpr ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object BooleanExpr()
        {
            int _pos = Mark();
            Object _stringItem = Expect("(");
            Object _expr = Memoize(Expr);
            Object _boolop = Memoize(boolop);
            Object _expr3 = Memoize(Expr);
            Object _stringItem4 = Expect(")");
            if (_stringItem != null && _expr != null && _boolop != null && _expr3 != null && _stringItem4 != null)
            {
                Console.WriteLine("Recognized [ BooleanExpr ]");
                return true;
            }
            Object _boolval = Memoize(boolval);
            if (_boolval != null)
            {
                Console.WriteLine("Recognized [ BooleanExpr ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object Id()
        {
            int _pos = Mark();
            Object _charliteral = Memoize(charLiteral);
            if (_charliteral != null)
            {
                Console.WriteLine("Recognized [ Id ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object CharList()
        {
            int _pos = Mark();
            Object _charliteral = Memoize(charLiteral);
            if (_charliteral != null)
            {
                List<Object> _CharList = new List<Object>();
                _CharList.Add(_charliteral);
                List<Object> _newCharList = (List<Object>)Memoize(CharList);
                if (_newCharList != null)
                {
                    _CharList.AddRange(_newCharList);
                    Console.WriteLine("Recognized [ CharList ]");
                    return true;
                }
                /*
                return _CharList
                */
            }
            Object _space = Memoize(space);
            if (_space != null)
            {
                List<Object> _CharList = new List<Object>();
                _CharList.Add(_space);
                List<Object> _newCharList = (List<Object>)Memoize(CharList);
                if (_newCharList != null)
                {
                    _CharList.AddRange(_newCharList);
                    Console.WriteLine("Recognized [ CharList ]");
                    return true;
                }
                /*
                return _CharList
                */
            }
            Object _stringItem = Expect("");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ CharList ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object type()
        {
            int _pos = Mark();
            Object _stringItem = Expect("int");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ type ]");
                return true;
            }
            Object _stringItem1 = Expect("string");
            if (_stringItem1 != null)
            {
                Console.WriteLine("Recognized [ type ]");
                return true;
            }
            Object _stringItem2 = Expect("boolean");
            if (_stringItem2 != null)
            {
                Console.WriteLine("Recognized [ type ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object charLiteral()
        {
            int _pos = Mark();
            Object _stringItem = Expect("a");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ charLiteral ]");
                return true;
            }
            Object _stringItem1 = Expect("b");
            if (_stringItem1 != null)
            {
                Console.WriteLine("Recognized [ charLiteral ]");
                return true;
            }
            Object _stringItem2 = Expect("c");
            if (_stringItem2 != null)
            {
                Console.WriteLine("Recognized [ charLiteral ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object space()
        {
            int _pos = Mark();
            Object _stringItem = Expect(" ");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ space ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object digit()
        {
            int _pos = Mark();
            Object _stringItem = Expect("0");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem1 = Expect("1");
            if (_stringItem1 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem2 = Expect("2");
            if (_stringItem2 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem3 = Expect("3");
            if (_stringItem3 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem4 = Expect("4");
            if (_stringItem4 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem5 = Expect("5");
            if (_stringItem5 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem6 = Expect("6");
            if (_stringItem6 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem7 = Expect("7");
            if (_stringItem7 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem8 = Expect("8");
            if (_stringItem8 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Object _stringItem9 = Expect("9");
            if (_stringItem9 != null)
            {
                Console.WriteLine("Recognized [ digit ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object boolop()
        {
            int _pos = Mark();
            Object _stringItem = Expect("==");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ boolop ]");
                return true;
            }
            Object _stringItem1 = Expect("!=");
            if (_stringItem1 != null)
            {
                Console.WriteLine("Recognized [ boolop ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object boolval()
        {
            int _pos = Mark();
            Object _stringItem = Expect("false");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ boolval ]");
                return true;
            }
            Object _stringItem1 = Expect("true");
            if (_stringItem1 != null)
            {
                Console.WriteLine("Recognized [ boolval ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
        public Object intop()
        {
            int _pos = Mark();
            Object _stringItem = Expect("+");
            if (_stringItem != null)
            {
                Console.WriteLine("Recognized [ intop ]");
                return true;
            }
            Reset(_pos);
            return null;
        }
    }
}
