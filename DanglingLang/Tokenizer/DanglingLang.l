%namespace DanglingLang.Tokenizer
%x COMMENT
%%

"/*"                     BEGIN(COMMENT);
<COMMENT>[^*\n]*         /* eat anything that’s not a ’*’ */
<COMMENT>"*"+[^*/\n]*    /* eat up ’*’s not followed by ’/’s */
<COMMENT>\n              /* ... */
<COMMENT>"*"+"/"         BEGIN(INITIAL);

\n|\r\n?					{ return (int) Tokens.NEWLINE; }
[0-9]+						{ yylval.intValue = int.Parse(yytext); return (int) Tokens.NUM; }
[ \t]						/* empty */

[iI][fF]					{ return (int) Tokens.IF; }
[wW][hH][iI][lL][eE]		{ return (int) Tokens.WHILE; }
[mM][aA][xX]				{ return (int) Tokens.MAX; }
[mM][iI][nN]				{ return (int) Tokens.MIN; }
[pP][rR][iI][nN][tT]		{ return (int) Tokens.PRINT; }
[tT][rR][uU][eE]			{ return (int) Tokens.TRUE; }
[fF][aA][lL][sS][eE]		{ return (int) Tokens.FALSE; }
[sS][tT][rR][uU][cC][tT]	{ return (int) Tokens.STRUCT; }
[rR][eE][tT][uU][rR][nN]	{ return (int) Tokens.RETURN; }
[lL][oO][aA][dD]        	{ return (int) Tokens.LOAD; }

[bB][oO][oO][lL]			{ return (int) Tokens.BOOL; }
[iI][nN][tT]				{ return (int) Tokens.INT; }
[vV][oO][iI][dD]			{ return (int) Tokens.VOID; }

[a-zA-Z_][a-zA-Z_0-9]*		{ yylval.identifier = yytext; return (int) Tokens.ID; }
\&\&						{ return (int) Tokens.AND; }
\|\|						{ return (int) Tokens.OR; }
==							{ return (int) Tokens.EQUAL; }
\<=							{ return (int) Tokens.LEQ; }
\<							{ return (int) Tokens.LESS_THAN; }
\.							{ return (int) Tokens.DOT; }
\%|\+|\-|\*|\/|\^|n|\(|\)|,|!|\{|\}|\~|=|;	{ return yytext[0]; }
.							{ Console.WriteLine("Invalid char: {0}", yytext[0]); }

%%
 public override void yyerror(string msg, params object[] args) {
   Console.WriteLine(msg, args);
 }
