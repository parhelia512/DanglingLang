%namespace DanglingLang.Tokenizer
%union {
	internal int intValue;
	internal string identifier;
	internal Exp exp;
	internal StructValue structValue;
	internal StructDecl structDecl;
	internal FunctionDecl functionDecl;
	internal FunctionCall functionCall;
	internal Stmt stmt;
	internal List<Stmt> stmts;
}
%token <intValue> NUM
%token <identifier> ID
%token MAX MIN NEWLINE IF WHILE STRUCT PRINT TRUE FALSE AND OR EQUAL LESS_THAN LEQ DOT
%token BOOL INT VOID /* Types */
%token RETURN /* Keywords */
%left OR
%left AND
%left EQUAL 
%left LESS_THAN LEQ
%left '+' '-'
%left '*' '/' '%'
%left UMINUS
%left '!'
%right '^'
%left '~' MIN MAX
%left DOT
%type <exp> exp
%type <structValue> structFieldValues
%type <structDecl> structFieldDecl
%type <functionDecl> funcParams
%type <functionCall> funcArgs
%type <stmt> stmt
%type <stmts> stmts 
%type <void> prog
%type <identifier> type
%{
	internal FunctionDecl Prog;
%}

%%
prog: stmts {Prog = new FunctionDecl(); Prog.Name = "$Main"; Prog.ReturnTypeName = "void"; Prog.Body = new Block($1);}
	;

stmts: /* empty */ {$$ = new List<Stmt>();}
	 | stmts stmt { if (($2) != null) $1.Add($2); $$ = $1; }
	 ;

stmt: NEWLINE {$$ = null;}
	| ID '=' exp NEWLINE { $$ = new Assignment($1, $3); }
	| exp {$$ = new EvalExp($1);}
	| '{' stmts '}' { $$ = new Block($2); }
	| IF '(' exp ')' stmt { $$ = new If($3, $5 ?? new Block(new List<Stmt>())); }
	| WHILE '(' exp ')' stmt { $$ = new While($3, $5 ?? new Block(new List<Stmt>())); }
	| PRINT '(' exp ')' NEWLINE {$$ = new Print($3);}
	| STRUCT ID '{' structFieldDecl '}' {$4.Name = $2; $$ = $4;}
	| type ID '(' funcParams ')' '{' stmts '}' {$4.Name = $2; $4.ReturnTypeName = $1; $4.Body = new Block($7); $$ = $4;}
	| VOID ID '(' funcParams ')' '{' stmts '}' {$4.Name = $2; $4.ReturnTypeName = "void"; $4.Body = new Block($7); $$ = $4;}
	| RETURN {$$ = new Return();}
	| RETURN exp {$$ = new Return($2);}
	;

exp: NUM  { $$ = new IntLiteral($1); }
   | TRUE { $$ = new BoolLiteral(true); }
   | FALSE { $$ = new BoolLiteral(false); }
   | STRUCT ID '{' structFieldValues exp '}' {$4.AddValue($5); $4.Name = $2; $$ = $4;}
   | exp '+' exp { $$ = new Sum($1, $3); }
   | exp '-' exp { $$ = new Subtraction($1, $3); }
   | exp '*' exp { $$ = new Product($1, $3); }
   | exp '/' exp { $$ = new Division($1, $3); }
   | exp '%' exp { $$ = new Remainder($1, $3); }
   | '+' exp %prec UMINUS { $$= $2; }
   | '-' exp %prec UMINUS { $$= new Minus($2); }
   | exp '!' { $$= new Factorial($1); }
   | exp '^' exp { $$ = new Power($1, $3); }
   | MAX '(' exp ',' exp ')' { $$ = new Max($3, $5); }
   | MIN '(' exp ',' exp ')' { $$ = new Min($3, $5); }
   | '(' exp ')' {$$ = $2;}
   | ID { $$ = new Id($1); }
   | '~' exp { $$ = new Not($2); }
   | exp AND exp { $$ = new And($1, $3); }
   | exp OR exp { $$ = new Or($1, $3); }
   | exp EQUAL exp { $$ = new Equal($1, $3); }
   | exp LEQ exp { $$ = new LessEqual($1, $3); }
   | exp LESS_THAN exp { $$ = new LessThan($1, $3); }
   | exp DOT ID {$$ = new Dot($1, $3);}
   | ID '(' funcArgs ')' {$3.FunctionName = $1; $$ = $3;}
   ;

structFieldDecl: /* Empty */ {$$ = new StructDecl();}
               | structFieldDecl type ID ';' {$1.AddField($3, $2); $$ = $1;}
			   ;

structFieldValues: /* Empty */ {$$ = new StructValue();}
                 | structFieldValues exp ',' {$1.AddValue($2); $$ = $1;}
                 ;

funcParams: /* Empty */ {$$ = new FunctionDecl();}
		  | type ID {$$ = new FunctionDecl(); $$.AddParam($2, $1);}
          | funcParams ',' type ID {$1.AddParam($4, $3); $$ = $1;}
          ;

funcArgs: /* Empty */ {$$ = new FunctionCall();}
		| exp {$$ = new FunctionCall(); $$.AddArgument($1);}
        | funcArgs ',' exp {$1.AddArgument($3); $$ = $1;}
        ;

type: BOOL {$$ = "bool";}
    | INT {$$ = "int";}
	| STRUCT ID {$$ = $2;}
	;

%%
	public Parser(Scanner s) : base(s) {}
