%namespace DanglingLang.Tokenizer
%union {
	internal int intValue;
	internal string identifier;
	internal Exp exp;
	internal Stmt stmt;
	internal List<Stmt> stmts;
}
%token <intValue> NUM
%token <identifier> ID
%token MAX MIN NEWLINE IF WHILE PRINT TRUE FALSE AND OR EQUAL LESS_THAN LEQ
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
%type <exp> exp
%type <stmt> stmt
%type <stmts> stmts 
%type <void> prog
%{
	internal Prog Prog;
%}
%%
prog: stmts { this.Prog = new Prog($1); }
	;

stmts: /* empty */ { $$ = new List<Stmt>(); }
	| stmts stmt { if (($2)!=null) $1.Add($2); $$ = $1; }
	;

stmt: NEWLINE { $$ = null; }
	| exp NEWLINE { $$ = new EvalExp($1); }
	| ID '=' exp NEWLINE { $$ = new Assignment($1, $3); }
	| '{' stmts '}' { $$ = new Block($2); }
	| IF '(' exp ')' stmt { $$ = new If($3, $5 ?? new Block(new List<Stmt>())); }
	| WHILE '(' exp ')' stmt { $$ = new While($3, $5 ?? new Block(new List<Stmt>())); }
	| PRINT '(' exp ')' NEWLINE { $$ = new Print($3); }
	;

exp: NUM  { $$ = new IntLiteral($1); }
	| TRUE { $$ = new BoolLiteral(true); }
	| FALSE { $$ = new BoolLiteral(false); }
	| exp '+' exp { $$ = new Sum($1, $3); }
	| exp '-' exp { $$ = new Subtraction($1, $3); }
	| exp '*' exp { $$ = new Product($1, $3); }
	| exp '/' exp { $$ = new Division($1, $3); }
	| exp '%' exp { $$ = new Remainder($1, $3); }
	| '+' exp %prec UMINUS { $$= $2; }
	| '-' exp %prec UMINUS { $$= new Minus($2); }
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
	;
%%
	public Parser(Scanner s) : base(s) {}
