// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.0
// Machine:  CELESTINO
// DateTime: 01/07/2013 17.21.54
// UserName: Alessio
// Input file <D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 17.18.09>

// options: lines report gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace DanglingLang.Tokenizer
{
public enum Tokens {
    error=127,EOF=128,NUM=129,ID=130,MAX=131,MIN=132,
    NEWLINE=133,IF=134,WHILE=135,STRUCT=136,INT=137,BOOL=138,
    PRINT=139,TRUE=140,FALSE=141,AND=142,OR=143,EQUAL=144,
    LESS_THAN=145,LEQ=146,DOT=147,UMINUS=148};

public struct ValueType
#line 2 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{
#line 3 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal int intValue;
#line 4 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal string identifier;
#line 5 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Exp exp;
#line 6 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal StructValue structValue;
#line 7 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal StructDecl structDecl;
#line 8 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Stmt stmt;
#line 9 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal List<Stmt> stmts;
#line 10 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
}
// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 17.18.09
#line 32 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Prog Prog;
  // End verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 17.18.09

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[43];
  private static State[] states = new State[102];
  private static string[] nonTerms = new string[] {
      "exp", "structFieldValues", "structFieldDecl", "stmt", "stmts", "prog", 
      "$accept", };

  static Parser() {
    states[0] = new State(-3,new int[]{-6,1,-5,3});
    states[1] = new State(new int[]{128,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{133,5,129,33,140,34,141,35,136,65,43,43,45,45,131,47,132,53,40,59,130,80,126,63,123,84,134,87,135,92,139,97,128,-2},new int[]{-4,4,-1,6});
    states[4] = new State(-4);
    states[5] = new State(-5);
    states[6] = new State(new int[]{133,7,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[7] = new State(-6);
    states[8] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,9});
    states[9] = new State(new int[]{43,-17,45,-17,42,12,47,14,37,16,33,18,94,19,142,-17,143,-17,144,-17,146,-17,145,-17,147,31,133,-17,125,-17,44,-17,41,-17});
    states[10] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,11});
    states[11] = new State(new int[]{43,-18,45,-18,42,12,47,14,37,16,33,18,94,19,142,-18,143,-18,144,-18,146,-18,145,-18,147,31,133,-18,125,-18,44,-18,41,-18});
    states[12] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,13});
    states[13] = new State(new int[]{43,-19,45,-19,42,-19,47,-19,37,-19,33,18,94,19,142,-19,143,-19,144,-19,146,-19,145,-19,147,31,133,-19,125,-19,44,-19,41,-19});
    states[14] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,15});
    states[15] = new State(new int[]{43,-20,45,-20,42,-20,47,-20,37,-20,33,18,94,19,142,-20,143,-20,144,-20,146,-20,145,-20,147,31,133,-20,125,-20,44,-20,41,-20});
    states[16] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,17});
    states[17] = new State(new int[]{43,-21,45,-21,42,-21,47,-21,37,-21,33,18,94,19,142,-21,143,-21,144,-21,146,-21,145,-21,147,31,133,-21,125,-21,44,-21,41,-21});
    states[18] = new State(-24);
    states[19] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,20});
    states[20] = new State(new int[]{43,-25,45,-25,42,-25,47,-25,37,-25,33,-25,94,19,142,-25,143,-25,144,-25,146,-25,145,-25,147,31,133,-25,125,-25,44,-25,41,-25});
    states[21] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,22});
    states[22] = new State(new int[]{43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,-31,143,-31,144,25,146,27,145,29,147,31,133,-31,125,-31,44,-31,41,-31});
    states[23] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,24});
    states[24] = new State(new int[]{43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,-32,144,25,146,27,145,29,147,31,133,-32,125,-32,44,-32,41,-32});
    states[25] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,26});
    states[26] = new State(new int[]{43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,-33,143,-33,144,-33,146,27,145,29,147,31,133,-33,125,-33,44,-33,41,-33});
    states[27] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,28});
    states[28] = new State(new int[]{43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,-34,143,-34,144,-34,146,-34,145,-34,147,31,133,-34,125,-34,44,-34,41,-34});
    states[29] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,30});
    states[30] = new State(new int[]{43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,-35,143,-35,144,-35,146,-35,145,-35,147,31,133,-35,125,-35,44,-35,41,-35});
    states[31] = new State(new int[]{130,32});
    states[32] = new State(-36);
    states[33] = new State(-13);
    states[34] = new State(-14);
    states[35] = new State(-15);
    states[36] = new State(new int[]{130,37});
    states[37] = new State(new int[]{123,38});
    states[38] = new State(-41,new int[]{-2,39});
    states[39] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,40});
    states[40] = new State(new int[]{125,41,44,42,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[41] = new State(-16);
    states[42] = new State(-42);
    states[43] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,44});
    states[44] = new State(new int[]{43,-22,45,-22,42,-22,47,-22,37,-22,33,18,94,19,142,-22,143,-22,144,-22,146,-22,145,-22,147,31,133,-22,125,-22,44,-22,41,-22});
    states[45] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,46});
    states[46] = new State(new int[]{43,-23,45,-23,42,-23,47,-23,37,-23,33,18,94,19,142,-23,143,-23,144,-23,146,-23,145,-23,147,31,133,-23,125,-23,44,-23,41,-23});
    states[47] = new State(new int[]{40,48});
    states[48] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,49});
    states[49] = new State(new int[]{44,50,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[50] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,51});
    states[51] = new State(new int[]{41,52,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[52] = new State(-26);
    states[53] = new State(new int[]{40,54});
    states[54] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,55});
    states[55] = new State(new int[]{44,56,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[56] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,57});
    states[57] = new State(new int[]{41,58,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[58] = new State(-27);
    states[59] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,60});
    states[60] = new State(new int[]{41,61,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[61] = new State(-28);
    states[62] = new State(-29);
    states[63] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,64});
    states[64] = new State(new int[]{43,-30,45,-30,42,-30,47,-30,37,-30,33,-30,94,-30,142,-30,143,-30,144,-30,146,-30,145,-30,147,31,133,-30,125,-30,44,-30,41,-30});
    states[65] = new State(new int[]{130,66});
    states[66] = new State(new int[]{123,67});
    states[67] = new State(new int[]{129,-41,140,-41,141,-41,136,-37,43,-41,45,-41,131,-41,132,-41,40,-41,130,-41,126,-41,125,-37,137,-37,138,-37},new int[]{-2,39,-3,68});
    states[68] = new State(new int[]{125,69,137,70,138,73,136,76});
    states[69] = new State(-12);
    states[70] = new State(new int[]{130,71});
    states[71] = new State(new int[]{59,72});
    states[72] = new State(-38);
    states[73] = new State(new int[]{130,74});
    states[74] = new State(new int[]{59,75});
    states[75] = new State(-39);
    states[76] = new State(new int[]{130,77});
    states[77] = new State(new int[]{130,78});
    states[78] = new State(new int[]{59,79});
    states[79] = new State(-40);
    states[80] = new State(new int[]{61,81,133,-29,43,-29,45,-29,42,-29,47,-29,37,-29,33,-29,94,-29,142,-29,143,-29,144,-29,146,-29,145,-29,147,-29});
    states[81] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,82});
    states[82] = new State(new int[]{133,83,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[83] = new State(-7);
    states[84] = new State(-3,new int[]{-5,85});
    states[85] = new State(new int[]{125,86,133,5,129,33,140,34,141,35,136,65,43,43,45,45,131,47,132,53,40,59,130,80,126,63,123,84,134,87,135,92,139,97},new int[]{-4,4,-1,6});
    states[86] = new State(-8);
    states[87] = new State(new int[]{40,88});
    states[88] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,89});
    states[89] = new State(new int[]{41,90,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[90] = new State(new int[]{133,5,129,33,140,34,141,35,136,65,43,43,45,45,131,47,132,53,40,59,130,80,126,63,123,84,134,87,135,92,139,97},new int[]{-4,91,-1,6});
    states[91] = new State(-9);
    states[92] = new State(new int[]{40,93});
    states[93] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,94});
    states[94] = new State(new int[]{41,95,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[95] = new State(new int[]{133,5,129,33,140,34,141,35,136,65,43,43,45,45,131,47,132,53,40,59,130,80,126,63,123,84,134,87,135,92,139,97},new int[]{-4,96,-1,6});
    states[96] = new State(-10);
    states[97] = new State(new int[]{40,98});
    states[98] = new State(new int[]{129,33,140,34,141,35,136,36,43,43,45,45,131,47,132,53,40,59,130,62,126,63},new int[]{-1,99});
    states[99] = new State(new int[]{41,100,43,8,45,10,42,12,47,14,37,16,33,18,94,19,142,21,143,23,144,25,146,27,145,29,147,31});
    states[100] = new State(new int[]{133,101});
    states[101] = new State(-11);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-7, new int[]{-6,128});
    rules[2] = new Rule(-6, new int[]{-5});
    rules[3] = new Rule(-5, new int[]{});
    rules[4] = new Rule(-5, new int[]{-5,-4});
    rules[5] = new Rule(-4, new int[]{133});
    rules[6] = new Rule(-4, new int[]{-1,133});
    rules[7] = new Rule(-4, new int[]{130,61,-1,133});
    rules[8] = new Rule(-4, new int[]{123,-5,125});
    rules[9] = new Rule(-4, new int[]{134,40,-1,41,-4});
    rules[10] = new Rule(-4, new int[]{135,40,-1,41,-4});
    rules[11] = new Rule(-4, new int[]{139,40,-1,41,133});
    rules[12] = new Rule(-4, new int[]{136,130,123,-3,125});
    rules[13] = new Rule(-1, new int[]{129});
    rules[14] = new Rule(-1, new int[]{140});
    rules[15] = new Rule(-1, new int[]{141});
    rules[16] = new Rule(-1, new int[]{136,130,123,-2,-1,125});
    rules[17] = new Rule(-1, new int[]{-1,43,-1});
    rules[18] = new Rule(-1, new int[]{-1,45,-1});
    rules[19] = new Rule(-1, new int[]{-1,42,-1});
    rules[20] = new Rule(-1, new int[]{-1,47,-1});
    rules[21] = new Rule(-1, new int[]{-1,37,-1});
    rules[22] = new Rule(-1, new int[]{43,-1});
    rules[23] = new Rule(-1, new int[]{45,-1});
    rules[24] = new Rule(-1, new int[]{-1,33});
    rules[25] = new Rule(-1, new int[]{-1,94,-1});
    rules[26] = new Rule(-1, new int[]{131,40,-1,44,-1,41});
    rules[27] = new Rule(-1, new int[]{132,40,-1,44,-1,41});
    rules[28] = new Rule(-1, new int[]{40,-1,41});
    rules[29] = new Rule(-1, new int[]{130});
    rules[30] = new Rule(-1, new int[]{126,-1});
    rules[31] = new Rule(-1, new int[]{-1,142,-1});
    rules[32] = new Rule(-1, new int[]{-1,143,-1});
    rules[33] = new Rule(-1, new int[]{-1,144,-1});
    rules[34] = new Rule(-1, new int[]{-1,146,-1});
    rules[35] = new Rule(-1, new int[]{-1,145,-1});
    rules[36] = new Rule(-1, new int[]{-1,147,130});
    rules[37] = new Rule(-3, new int[]{});
    rules[38] = new Rule(-3, new int[]{-3,137,130,59});
    rules[39] = new Rule(-3, new int[]{-3,138,130,59});
    rules[40] = new Rule(-3, new int[]{-3,136,130,130,59});
    rules[41] = new Rule(-2, new int[]{});
    rules[42] = new Rule(-2, new int[]{-2,-1,44});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // prog -> stmts
#line 36 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{Prog = new Prog(ValueStack[ValueStack.Depth-1].stmts);}
        break;
      case 3: // stmts -> /* empty */
#line 39 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmts = new List<Stmt>();}
        break;
      case 4: // stmts -> stmts, stmt
#line 40 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ if ((ValueStack[ValueStack.Depth-1].stmt) != null) ValueStack[ValueStack.Depth-2].stmts.Add(ValueStack[ValueStack.Depth-1].stmt); CurrentSemanticValue.stmts = ValueStack[ValueStack.Depth-2].stmts; }
        break;
      case 5: // stmt -> NEWLINE
#line 43 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = null; }
        break;
      case 6: // stmt -> exp, NEWLINE
#line 44 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new EvalExp(ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 7: // stmt -> ID, '=', exp, NEWLINE
#line 45 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new Assignment(ValueStack[ValueStack.Depth-4].identifier, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 8: // stmt -> '{', stmts, '}'
#line 46 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new Block(ValueStack[ValueStack.Depth-2].stmts); }
        break;
      case 9: // stmt -> IF, '(', exp, ')', stmt
#line 47 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new If(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>())); }
        break;
      case 10: // stmt -> WHILE, '(', exp, ')', stmt
#line 48 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new While(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>())); }
        break;
      case 11: // stmt -> PRINT, '(', exp, ')', NEWLINE
#line 49 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new Print(ValueStack[ValueStack.Depth-3].exp); }
        break;
      case 12: // stmt -> STRUCT, ID, '{', structFieldDecl, '}'
#line 50 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-2].structDecl.Name = ValueStack[ValueStack.Depth-4].identifier; CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-2].structDecl;}
        break;
      case 13: // exp -> NUM
#line 53 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new IntLiteral(ValueStack[ValueStack.Depth-1].intValue); }
        break;
      case 14: // exp -> TRUE
#line 54 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(true); }
        break;
      case 15: // exp -> FALSE
#line 55 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(false); }
        break;
      case 16: // exp -> STRUCT, ID, '{', structFieldValues, exp, '}'
#line 56 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); ValueStack[ValueStack.Depth-3].structValue.Name = ValueStack[ValueStack.Depth-5].identifier; CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-3].structValue; }
        break;
      case 17: // exp -> exp, '+', exp
#line 57 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Sum(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 18: // exp -> exp, '-', exp
#line 58 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Subtraction(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 19: // exp -> exp, '*', exp
#line 59 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Product(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 20: // exp -> exp, '/', exp
#line 60 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Division(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 21: // exp -> exp, '%', exp
#line 61 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Remainder(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 22: // exp -> '+', exp
#line 62 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= ValueStack[ValueStack.Depth-1].exp; }
        break;
      case 23: // exp -> '-', exp
#line 63 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Minus(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 24: // exp -> exp, '!'
#line 64 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Factorial(ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 25: // exp -> exp, '^', exp
#line 65 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Power(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 26: // exp -> MAX, '(', exp, ',', exp, ')'
#line 66 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Max(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 27: // exp -> MIN, '(', exp, ',', exp, ')'
#line 67 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Min(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 28: // exp -> '(', exp, ')'
#line 68 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp=ValueStack[ValueStack.Depth-2].exp; }
        break;
      case 29: // exp -> ID
#line 69 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Id(ValueStack[ValueStack.Depth-1].identifier); }
        break;
      case 30: // exp -> '~', exp
#line 70 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Not(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 31: // exp -> exp, AND, exp
#line 71 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new And(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 32: // exp -> exp, OR, exp
#line 72 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Or(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 33: // exp -> exp, EQUAL, exp
#line 73 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Equal(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 34: // exp -> exp, LEQ, exp
#line 74 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessEqual(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 35: // exp -> exp, LESS_THAN, exp
#line 75 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessThan(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 36: // exp -> exp, DOT, ID
#line 76 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Dot(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].identifier); }
        break;
      case 37: // structFieldDecl -> /* empty */
#line 79 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structDecl = new StructDecl();}
        break;
      case 38: // structFieldDecl -> structFieldDecl, INT, ID, ';'
#line 80 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].structDecl.AddField(ValueStack[ValueStack.Depth-2].identifier, "int"); CurrentSemanticValue.structDecl = ValueStack[ValueStack.Depth-4].structDecl;}
        break;
      case 39: // structFieldDecl -> structFieldDecl, BOOL, ID, ';'
#line 81 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].structDecl.AddField(ValueStack[ValueStack.Depth-2].identifier, "bool"); CurrentSemanticValue.structDecl = ValueStack[ValueStack.Depth-4].structDecl;}
        break;
      case 40: // structFieldDecl -> structFieldDecl, STRUCT, ID, ID, ';'
#line 82 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-5].structDecl.AddField(ValueStack[ValueStack.Depth-2].identifier, ValueStack[ValueStack.Depth-3].identifier); CurrentSemanticValue.structDecl = ValueStack[ValueStack.Depth-5].structDecl;}
        break;
      case 41: // structFieldValues -> /* empty */
#line 85 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structValue = new StructValue();}
        break;
      case 42: // structFieldValues -> structFieldValues, exp, ','
#line 86 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); CurrentSemanticValue.structValue = ValueStack[ValueStack.Depth-3].structValue;}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 90 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	public Parser(Scanner s) : base(s) {}
}
}
