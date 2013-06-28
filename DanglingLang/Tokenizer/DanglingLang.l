%namespace DanglingLang.Tokenizer
%%
\n|\r\n?				{ return (int)Tokens.NEWLINE; }
[0-9]+					{ yylval.intValue = int.Parse(yytext); return (int)Tokens.NUM; }
[ \t]					/* empty */
[iI][fF]				{ return (int)Tokens.IF; }
[mM][aA][xX]			{ return (int)Tokens.MAX; }
[mM][iI][nN]			{ return (int)Tokens.MIN; }
[pP][rR][iI][nN][tT]	{ return (int)Tokens.PRINT; }
[tT][rR][uU][eE]		{ return (int)Tokens.TRUE; }
[fF][aA][lL][sS][eE]	{ return (int)Tokens.FALSE; }
[a-zA-Z_][a-zA-Z_0-9]*	{ yylval.identifier=yytext; return (int)Tokens.ID; }
\&\&					{ return (int)Tokens.AND; }
==						{ return (int)Tokens.EQUAL; }
\<=						{ return (int)Tokens.LEQ; }
\<						{ return (int)Tokens.LESS_THAN; }
\+|\-|\*|\/|\^|n|\(|\)|,|!|\{|\}|\~|=	{ return yytext[0]; }
.						{ Console.WriteLine("Invalid char: {0}", yytext[0]); }
%%
 public override void yyerror(string msg, params object[] args) {
   Console.WriteLine(msg, args);
 }
