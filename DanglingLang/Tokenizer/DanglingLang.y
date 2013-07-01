%namespace DanglingLang.Tokenizer
%union {
	internal int intValue;
	internal string identifier;
	internal Exp exp;
	internal StructValue structValue;
	internal StructDecl structDecl;
	internal Stmt stmt;
	internal List<Stmt> stmts;
}
%token <intValue> NUM
%token <identifier> ID
%token MAX MIN NEWLINE IF WHILE STRUCT INT BOOL PRINT TRUE FALSE AND OR EQUAL LESS_THAN LEQ DOT
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
%type <stmt> stmt
%type <stmts> stmts 
%type <void> prog
%{
	internal Prog Prog;
%}

%%
prog: stmts {Prog = new Prog($1);}
	;

stmts: /* empty */ {$$ = new List<Stmt>();}
	 | stmts stmt { if (($2) != null) $1.Add($2); $$ = $1; }
	 ;

stmt: NEWLINE { $$ = null; }
	| exp NEWLINE { $$ = new EvalExp($1); }
	| ID '=' exp NEWLINE { $$ = new Assignment($1, $3); }
	| '{' stmts '}' { $$ = new Block($2); }
	| IF '(' exp ')' stmt { $$ = new If($3, $5 ?? new Block(new List<Stmt>())); }
	| WHILE '(' exp ')' stmt { $$ = new While($3, $5 ?? new Block(new List<Stmt>())); }
	| PRINT '(' exp ')' NEWLINE { $$ = new Print($3); }
	| STRUCT ID '{' structFieldDecl '}' {$4.Name = $2; $$ = $4;}
	;

exp: NUM  { $$ = new IntLiteral($1); }
   | TRUE { $$ = new BoolLiteral(true); }
   | FALSE { $$ = new BoolLiteral(false); }
   | STRUCT ID '{' structFieldValues exp '}' { $4.AddValue($5); $4.Name = $2; $$ = $4; }
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
   | '(' exp ')' { $$=$2; }
   | ID { $$ = new Id($1); }
   | '~' exp { $$ = new Not($2); }
   | exp AND exp { $$ = new And($1, $3); }
   | exp OR exp { $$ = new Or($1, $3); }
   | exp EQUAL exp { $$ = new Equal($1, $3); }
   | exp LEQ exp { $$ = new LessEqual($1, $3); }
   | exp LESS_THAN exp { $$ = new LessThan($1, $3); }
   | exp DOT ID { $$ = new Dot($1, $3); }
   ;

structFieldDecl: /* Empty */ {$$ = new StructDecl();}
               | structFieldDecl INT ID ';' {$1.AddField($3, "int"); $$ = $1;}
			   | structFieldDecl BOOL ID ';' {$1.AddField($3, "bool"); $$ = $1;}
			   | structFieldDecl STRUCT ID ID ';' {$1.AddField($4, $3); $$ = $1;}
			   ;

structFieldValues: /* Empty */ {$$ = new StructValue();}
                 | structFieldValues exp ',' {$1.AddValue($2); $$ = $1;}
                 ;

%%
	public Parser(Scanner s) : base(s) {}
