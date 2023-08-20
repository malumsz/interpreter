using VerySimpleInterpreter.Lexer;
using VerySimpleInterpreter;
using VerySimpleInterpreter.Parser;

var st = new SymbolTable();
var basicLexer = new BasicLexer("docs/example.lang", st);
var basicParser = new BasicParser(basicLexer, st);

basicParser.Prog();

// Token t = null;
// do {
//     t = basicLexer.GetNextToken();
//     Console.WriteLine($"<{t.Type},{t.Value}>");
// } while (t.Type != ETokenType.EOF);

Console.WriteLine(st);