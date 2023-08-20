namespace VerySimpleInterpreter.Lexer
{
    public class BasicLexer
    {

        public Int32 Line { get; protected set; }
        public Int32 Column { get; protected set; }

        public string Filename { get; protected set; }
        public SymbolTable SymbolTable { get; protected set; }

        private char? _peek;
        private StreamReader _reader;

        public BasicLexer(string filename, SymbolTable? st = null)
        {
            Filename = filename;

            if (st == null)
            {
                st = new SymbolTable();
            }

            SymbolTable = st;

            _reader = new StreamReader(Filename);

            Column = Line = 1;
        }

        public Token GetNextToken()
        {
            if (_reader.EndOfStream)
                return new Token(ETokenType.EOF);

            else
            {
                while (_peek == null || _peek == ' ' || _peek == '\t' || _peek == '\r')
                {
                    _peek = NextChar();
                }


                switch (_peek)
                {
                    case '+': _peek = null; return new Token(ETokenType.SUM);
                    case '-': _peek = null; return new Token(ETokenType.SUB);
                    case '*': _peek = null; return new Token(ETokenType.MULT);
                    case '/': _peek = null; return new Token(ETokenType.DIV);
                    case '(': _peek = null; return new Token(ETokenType.OE);
                    case ')': _peek = null; return new Token(ETokenType.CE);
                    case '=': _peek = null; return new Token(ETokenType.AT);
                    case '\n':
                        _peek = null;
                        Column = 1;
                        Line++;
                        return new Token(ETokenType.EOL);
                }

                if (_peek == '$')  //$[a-z]+
                {
                    var varName = " ";
                    do
                    {
                        _peek = NextChar();
                        if (Char.IsLetter(_peek.Value))
                            varName += _peek;
                    } while (Char.IsLetter(_peek.Value));

                    var key = SymbolTable.Put(varName, null);

                    return new Token(ETokenType.VAR, key);
                }

                if (_peek == 'r')  //'read'
                {
                    if (testSufix("ead"))
                        if (NextChar() == ' ')
                        {
                            if (NextChar() == '$')
                            {
                                var varName = " ";

                                do
                                {
                                    _peek = NextChar();
                                    if (Char.IsLetter(_peek.Value))
                                        varName += _peek;
                                } while (Char.IsLetter(_peek.Value));

                                var x = Console.ReadLine();
                                var k = SymbolTable.Put(varName, int.Parse(x));
                            }
                        }
                    return new Token(ETokenType.INPUT);
                }

                if (_peek == 'w')  //'write'
                {
                    if (testSufix("rite"))
                        return new Token(ETokenType.OUTPUT);
                }
                if (Char.IsDigit(_peek.Value))  //[0-9]+
                {
                    var value = 0;
                    do
                    {
                        value = 10 * value + (int)Char.GetNumericValue(_peek.Value);
                        _peek = NextChar();
                    } while (Char.IsDigit(_peek.Value));

                    return new Token(ETokenType.NUM, value);
                }

                return new Token(ETokenType.ERR);
            }
        }

        public char NextChar()
        {
            Column++;
            Char c = '\0';
            if (!_reader.EndOfStream)
                c = (Char)_reader.Read();
            return c;
        }

        private bool testSufix(String suffix)
        {
            var res = true;
            suffix.ToCharArray().ToList().ForEach(c =>
            {
                _peek = NextChar();
                if (_peek != c)
                {
                    res = false;
                    return;
                }
            });
            _peek = null;
            return res;
        }

        private int GetValue(Char? c)
        {
            if (c == '0') return 0;
            if (c == '1') return 1;
            if (c == '2') return 2;
            if (c == '3') return 3;
            if (c == '4') return 4;
            if (c == '5') return 5;
            if (c == '6') return 6;
            if (c == '7') return 7;
            if (c == '8') return 8;
            if (c == '9') return 9;

            return 0;
        }

    }
}