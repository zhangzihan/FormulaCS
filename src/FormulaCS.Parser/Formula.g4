grammar Formula;

/* Parser rules */
main : '=' expr EOF;
expr : expr ':' expr                         #Range
     | sign=('+'|'-') expr                   #Unary
     | expr '%'                              #Percentage
     | expr '^' expr                         #Pow
     | expr op=('*'|'/') expr                #MulDiv
     | expr op=('+'|'-') expr                #AddSub
     | expr '&' expr                         #Concatenate
     | expr op=('='|'<>') expr               #Equality
     | expr op=('<'|'>'|'<='|'>=') expr      #Relational
     | '(' expr ')'                          #Parenthesis
     | name=NAME '(' (expr (',' expr)*)? ')' #Function
     | STRING                                #String
     | CELLREF                               #CellRef
     | BOOLEAN                               #Boolean
     | NUMBER                                #Number
     | NAME                                  #Name
     | ERROR                                 #Error
     ;

/* Lexer rules */
STRING  : '"' ~["]* '"' ;
CELLREF : SHEET? '$'? COL '$'? DIGIT+ ;
BOOLEAN : TRUE|FALSE ;
NUMBER  : FLOAT|DIGIT+ ;
NAME    : [a-zA-Z_\\] ([0-9a-zA-Z_.\\])* ;
ERROR       :   '#NULL!'
            |   '#DIV/0!'
            |   '#VALUE!'
            |   '#NAME?'
            |   '#NUM!'
            |   '#N/A'
            |   '#REF!'
            ;
S       : (' '|'\t'|'\r'|'\n'|'\u000C') -> skip ;
INVALID : . ;

/* Lexer fragments */
fragment COL        :   [a-zA-Z]? [a-zA-Z]? [a-zA-Z] ; // Max col is XFD
fragment DIGIT      :   [0-9] ;
fragment TRUE       :   [Tt][Rr][Uu][Ee] ;
fragment FALSE      :   [Ff][Aa][Ll][Ss][Ee] ;
fragment FLOAT      :   DIGIT+ '.' DIGIT+ E? | DIGIT+ E ;
fragment E          :   [Ee] ('+'?|'-') DIGIT+ ;
fragment SHEET      :   FILE? SHEET_C+ '!'
                    |   FILE? SHEET_C+ ':' + SHEET_C+ '!'
                    |   '\'' WIN_PATH? FILE? Q_SHEET_C+ '\'!'
                    |   '\'' WIN_PATH? FILE? Q_SHEET_C+ ':' Q_SHEET_C+ '\'!'
                    ;
fragment FILE       :   '[' DIGIT+ ']'
                    |   '[' FILENAME_C+ ']'
                    ;
fragment WIN_PATH   :   [A-Z] ':\\' (FILENAME_C+ '\\')* ;
fragment FILENAME_C :   ~["*[\]\\:/?<>|] ;
fragment SHEET_C    :   ~['*[\]\\:/?<>();{}#"=&+^%,!\u2229\u222A \-] ;
fragment Q_SHEET_C  :   ~['*[\]\\:/?] ;
