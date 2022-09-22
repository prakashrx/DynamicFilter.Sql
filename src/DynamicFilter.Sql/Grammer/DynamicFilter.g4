grammar DynamicFilter;		
@header {#pragma warning disable 3021}

root : expr EOF;
expr : '(' expr ')'                                 #paranexpr
    | NOT expr                                      #notExpr    
    | left=expr operation=compareop right=expr      #compareExpr
    | left=expr operation=likeop right=STRING       #compareLikeExpr
    | left=expr operation=listop right=list         #listExpr
    | left=expr operation=nullop                    #nullExpr
    | left=expr operation=logicalop right=expr      #logicExpr
    | constant                                      #constantExpr
    | IDENTIFIER                                    #identifierExpr
    ;

list: '(' constant (',' constant)* ')'              #listValue
    ;

//Symbols and Constants
constant: number                                    #numericValue
    | STRING                                        #stringValue
    | BOOL                                          #booleanValue
    ;

number: DECIMAL                                     #decimalValue
    | INTEGER                                       #integerValue
    ;
DECIMAL: '-'? [0-9]+ '.' [0-9]+;
INTEGER: '-'? [0-9]+;
STRING:  '\'' ( ~'\'' | '\'\'')* '\'' | '"' ( ~'"' | '""')* '"';

//Operators
compareop: '<' | '<=' | '>' | '>=' | EQ | NEQ ;
likeop: LIKE | NOTLIKE;
nullop: ISNULL | ISNOTNULL;
logicalop: AND | OR ;
listop: IN | NOTIN;

EQ: '=' | '==';
NEQ: '!=' | '<>';


//Keywords
BOOL: T R U E | F A L S E;
NOTLIKE: N O T WS L I K E;
LIKE: L I K E;
NOTIN: N O T WS I N;
IN: I N;
ISNULL: I S WS N U L L;
ISNOTNULL: I S WS N O T WS N U L L;
NOT: N O T;
AND: A N D;
OR: O R;
IDENTIFIER: [a-zA-Z_] [a-zA-Z_0-9]*;
WS  : [ \r\t\n\u000C]+ -> skip;

//Others
fragment A : [aA];
fragment B : [bB];
fragment C : [cC];
fragment D : [dD];
fragment E : [eE];
fragment F : [fF];
fragment G : [gG];
fragment H : [hH];
fragment I : [iI];
fragment J : [jJ];
fragment K : [kK];
fragment L : [lL];
fragment M : [mM];
fragment N : [nN];
fragment O : [oO];
fragment P : [pP];
fragment Q : [qQ];
fragment R : [rR];
fragment S : [sS];
fragment T : [tT];
fragment U : [uU];
fragment V : [vV];
fragment W : [wW];
fragment X : [xX];
fragment Y : [yY];
fragment Z : [zZ];