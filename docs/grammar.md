prog   : line X
X      : EOF | prog
line   : stmt EOL
stmt   : in | out | atrib
in     : INPUT VAR
out    : OUTPUT VAR
atrib  : VAR AT expr
expr   : term Y
Y      : vazio | + expr | - expr
term   : factZ
Z      : vazio | * term | / term
fact   : NUM | VAR | OE expr CE

EOL    : [\n\r]+
INPUT  : 'read'
OUTPUT : 'write'
VAR    : '$'[a-z]+
AT     : '='
OE     : '('
CE     : ')'
SUM    : '+'
SUB    : '-'
DIV    : '/'
MULT   : '*'