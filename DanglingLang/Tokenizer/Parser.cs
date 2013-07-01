// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.0
// Machine:  CELESTINO
// DateTime: 01/07/2013 23.40.19
// UserName: Alessio
// Input file <D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 22.30.34>

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
    NEWLINE=133,IF=134,WHILE=135,STRUCT=136,PRINT=137,TRUE=138,
    FALSE=139,AND=140,OR=141,EQUAL=142,LESS_THAN=143,LEQ=144,
    DOT=145,BOOL=146,INT=147,VOID=148,UMINUS=149};

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
	internal FunctionDecl functionDecl;
#line 9 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Stmt stmt;
#line 10 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal List<Stmt> stmts;
#line 11 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
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
  // Verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 22.30.34
#line 36 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Prog Prog;
  // End verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 01/07/2013 22.30.34

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[48];
  private static State[] states = new State[111];
  private static string[] nonTerms = new string[] {
      "exp", "structFieldValues", "structFieldDecl", "funcArgs", "stmt", "stmts", 
      "prog", "type", "$accept", };

  static Parser() {
    states[0] = new State(-3,new int[]{-7,1,-6,3});
    states[1] = new State(new int[]{128,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{133,5,130,6,123,67,134,70,135,75,137,80,136,85,146,93,147,94,148,95,128,-2},new int[]{-5,4,-8,98});
    states[4] = new State(-4);
    states[5] = new State(-5);
    states[6] = new State(new int[]{61,7});
    states[7] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,8});
    states[8] = new State(new int[]{133,9,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[9] = new State(-6);
    states[10] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,11});
    states[11] = new State(new int[]{43,-17,45,-17,42,14,47,16,37,18,33,20,94,21,140,-17,141,-17,142,-17,144,-17,143,-17,145,33,133,-17,125,-17,44,-17,41,-17});
    states[12] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,13});
    states[13] = new State(new int[]{43,-18,45,-18,42,14,47,16,37,18,33,20,94,21,140,-18,141,-18,142,-18,144,-18,143,-18,145,33,133,-18,125,-18,44,-18,41,-18});
    states[14] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,15});
    states[15] = new State(new int[]{43,-19,45,-19,42,-19,47,-19,37,-19,33,20,94,21,140,-19,141,-19,142,-19,144,-19,143,-19,145,33,133,-19,125,-19,44,-19,41,-19});
    states[16] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,17});
    states[17] = new State(new int[]{43,-20,45,-20,42,-20,47,-20,37,-20,33,20,94,21,140,-20,141,-20,142,-20,144,-20,143,-20,145,33,133,-20,125,-20,44,-20,41,-20});
    states[18] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,19});
    states[19] = new State(new int[]{43,-21,45,-21,42,-21,47,-21,37,-21,33,20,94,21,140,-21,141,-21,142,-21,144,-21,143,-21,145,33,133,-21,125,-21,44,-21,41,-21});
    states[20] = new State(-24);
    states[21] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,22});
    states[22] = new State(new int[]{43,-25,45,-25,42,-25,47,-25,37,-25,33,-25,94,21,140,-25,141,-25,142,-25,144,-25,143,-25,145,33,133,-25,125,-25,44,-25,41,-25});
    states[23] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,24});
    states[24] = new State(new int[]{43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,-31,141,-31,142,27,144,29,143,31,145,33,133,-31,125,-31,44,-31,41,-31});
    states[25] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,26});
    states[26] = new State(new int[]{43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,-32,142,27,144,29,143,31,145,33,133,-32,125,-32,44,-32,41,-32});
    states[27] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,28});
    states[28] = new State(new int[]{43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,-33,141,-33,142,-33,144,29,143,31,145,33,133,-33,125,-33,44,-33,41,-33});
    states[29] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,30});
    states[30] = new State(new int[]{43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,-34,141,-34,142,-34,144,-34,143,-34,145,33,133,-34,125,-34,44,-34,41,-34});
    states[31] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,32});
    states[32] = new State(new int[]{43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,-35,141,-35,142,-35,144,-35,143,-35,145,33,133,-35,125,-35,44,-35,41,-35});
    states[33] = new State(new int[]{130,34});
    states[34] = new State(-36);
    states[35] = new State(-13);
    states[36] = new State(-14);
    states[37] = new State(-15);
    states[38] = new State(new int[]{130,39});
    states[39] = new State(new int[]{123,40});
    states[40] = new State(-39,new int[]{-2,41});
    states[41] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,42});
    states[42] = new State(new int[]{125,43,44,44,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[43] = new State(-16);
    states[44] = new State(-40);
    states[45] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,46});
    states[46] = new State(new int[]{43,-22,45,-22,42,-22,47,-22,37,-22,33,20,94,21,140,-22,141,-22,142,-22,144,-22,143,-22,145,33,133,-22,125,-22,44,-22,41,-22});
    states[47] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,48});
    states[48] = new State(new int[]{43,-23,45,-23,42,-23,47,-23,37,-23,33,20,94,21,140,-23,141,-23,142,-23,144,-23,143,-23,145,33,133,-23,125,-23,44,-23,41,-23});
    states[49] = new State(new int[]{40,50});
    states[50] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,51});
    states[51] = new State(new int[]{44,52,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[52] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,53});
    states[53] = new State(new int[]{41,54,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[54] = new State(-26);
    states[55] = new State(new int[]{40,56});
    states[56] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,57});
    states[57] = new State(new int[]{44,58,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[58] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,59});
    states[59] = new State(new int[]{41,60,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[60] = new State(-27);
    states[61] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,62});
    states[62] = new State(new int[]{41,63,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[63] = new State(-28);
    states[64] = new State(-29);
    states[65] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,66});
    states[66] = new State(new int[]{43,-30,45,-30,42,-30,47,-30,37,-30,33,-30,94,-30,140,-30,141,-30,142,-30,144,-30,143,-30,145,33,133,-30,125,-30,44,-30,41,-30});
    states[67] = new State(-3,new int[]{-6,68});
    states[68] = new State(new int[]{125,69,133,5,130,6,123,67,134,70,135,75,137,80,136,85,146,93,147,94,148,95},new int[]{-5,4,-8,98});
    states[69] = new State(-7);
    states[70] = new State(new int[]{40,71});
    states[71] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,72});
    states[72] = new State(new int[]{41,73,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[73] = new State(new int[]{133,5,130,6,123,67,134,70,135,75,137,80,136,85,146,93,147,94,148,95},new int[]{-5,74,-8,98});
    states[74] = new State(-8);
    states[75] = new State(new int[]{40,76});
    states[76] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,77});
    states[77] = new State(new int[]{41,78,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[78] = new State(new int[]{133,5,130,6,123,67,134,70,135,75,137,80,136,85,146,93,147,94,148,95},new int[]{-5,79,-8,98});
    states[79] = new State(-9);
    states[80] = new State(new int[]{40,81});
    states[81] = new State(new int[]{129,35,138,36,139,37,136,38,43,45,45,47,131,49,132,55,40,61,130,64,126,65},new int[]{-1,82});
    states[82] = new State(new int[]{41,83,43,10,45,12,42,14,47,16,37,18,33,20,94,21,140,23,141,25,142,27,144,29,143,31,145,33});
    states[83] = new State(new int[]{133,84});
    states[84] = new State(-10);
    states[85] = new State(new int[]{130,86});
    states[86] = new State(new int[]{123,87,130,-47});
    states[87] = new State(-37,new int[]{-3,88});
    states[88] = new State(new int[]{125,89,146,93,147,94,148,95,136,96},new int[]{-8,90});
    states[89] = new State(-11);
    states[90] = new State(new int[]{130,91});
    states[91] = new State(new int[]{59,92});
    states[92] = new State(-38);
    states[93] = new State(-44);
    states[94] = new State(-45);
    states[95] = new State(-46);
    states[96] = new State(new int[]{130,97});
    states[97] = new State(-47);
    states[98] = new State(new int[]{130,99});
    states[99] = new State(new int[]{40,100});
    states[100] = new State(new int[]{146,93,147,94,148,95,136,96,41,-41,44,-41},new int[]{-4,101,-8,109});
    states[101] = new State(new int[]{41,102,44,106});
    states[102] = new State(new int[]{123,103});
    states[103] = new State(-3,new int[]{-6,104});
    states[104] = new State(new int[]{125,105,133,5,130,6,123,67,134,70,135,75,137,80,136,85,146,93,147,94,148,95},new int[]{-5,4,-8,98});
    states[105] = new State(-12);
    states[106] = new State(new int[]{146,93,147,94,148,95,136,96},new int[]{-8,107});
    states[107] = new State(new int[]{130,108});
    states[108] = new State(-43);
    states[109] = new State(new int[]{130,110});
    states[110] = new State(-42);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-9, new int[]{-7,128});
    rules[2] = new Rule(-7, new int[]{-6});
    rules[3] = new Rule(-6, new int[]{});
    rules[4] = new Rule(-6, new int[]{-6,-5});
    rules[5] = new Rule(-5, new int[]{133});
    rules[6] = new Rule(-5, new int[]{130,61,-1,133});
    rules[7] = new Rule(-5, new int[]{123,-6,125});
    rules[8] = new Rule(-5, new int[]{134,40,-1,41,-5});
    rules[9] = new Rule(-5, new int[]{135,40,-1,41,-5});
    rules[10] = new Rule(-5, new int[]{137,40,-1,41,133});
    rules[11] = new Rule(-5, new int[]{136,130,123,-3,125});
    rules[12] = new Rule(-5, new int[]{-8,130,40,-4,41,123,-6,125});
    rules[13] = new Rule(-1, new int[]{129});
    rules[14] = new Rule(-1, new int[]{138});
    rules[15] = new Rule(-1, new int[]{139});
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
    rules[31] = new Rule(-1, new int[]{-1,140,-1});
    rules[32] = new Rule(-1, new int[]{-1,141,-1});
    rules[33] = new Rule(-1, new int[]{-1,142,-1});
    rules[34] = new Rule(-1, new int[]{-1,144,-1});
    rules[35] = new Rule(-1, new int[]{-1,143,-1});
    rules[36] = new Rule(-1, new int[]{-1,145,130});
    rules[37] = new Rule(-3, new int[]{});
    rules[38] = new Rule(-3, new int[]{-3,-8,130,59});
    rules[39] = new Rule(-2, new int[]{});
    rules[40] = new Rule(-2, new int[]{-2,-1,44});
    rules[41] = new Rule(-4, new int[]{});
    rules[42] = new Rule(-4, new int[]{-8,130});
    rules[43] = new Rule(-4, new int[]{-4,44,-8,130});
    rules[44] = new Rule(-8, new int[]{146});
    rules[45] = new Rule(-8, new int[]{147});
    rules[46] = new Rule(-8, new int[]{148});
    rules[47] = new Rule(-8, new int[]{136,130});
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
#line 40 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{Prog = new Prog(ValueStack[ValueStack.Depth-1].stmts);}
        break;
      case 3: // stmts -> /* empty */
#line 43 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmts = new List<Stmt>();}
        break;
      case 4: // stmts -> stmts, stmt
#line 44 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ if ((ValueStack[ValueStack.Depth-1].stmt) != null) ValueStack[ValueStack.Depth-2].stmts.Add(ValueStack[ValueStack.Depth-1].stmt); CurrentSemanticValue.stmts = ValueStack[ValueStack.Depth-2].stmts; }
        break;
      case 5: // stmt -> NEWLINE
#line 47 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = null;}
        break;
      case 6: // stmt -> ID, '=', exp, NEWLINE
#line 48 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new Assignment(ValueStack[ValueStack.Depth-4].identifier, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 7: // stmt -> '{', stmts, '}'
#line 49 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new Block(ValueStack[ValueStack.Depth-2].stmts); }
        break;
      case 8: // stmt -> IF, '(', exp, ')', stmt
#line 50 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new If(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>())); }
        break;
      case 9: // stmt -> WHILE, '(', exp, ')', stmt
#line 51 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.stmt = new While(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>())); }
        break;
      case 10: // stmt -> PRINT, '(', exp, ')', NEWLINE
#line 52 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Print(ValueStack[ValueStack.Depth-3].exp);}
        break;
      case 11: // stmt -> STRUCT, ID, '{', structFieldDecl, '}'
#line 53 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-2].structDecl.Name = ValueStack[ValueStack.Depth-4].identifier; CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-2].structDecl;}
        break;
      case 12: // stmt -> type, ID, '(', funcArgs, ')', '{', stmts, '}'
#line 54 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-5].functionDecl.Name = ValueStack[ValueStack.Depth-7].identifier; ValueStack[ValueStack.Depth-5].functionDecl.ReturnTypeName = ValueStack[ValueStack.Depth-8].identifier; ValueStack[ValueStack.Depth-5].functionDecl.Body = new Block(ValueStack[ValueStack.Depth-2].stmts); CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-5].functionDecl;}
        break;
      case 13: // exp -> NUM
#line 57 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new IntLiteral(ValueStack[ValueStack.Depth-1].intValue); }
        break;
      case 14: // exp -> TRUE
#line 58 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(true); }
        break;
      case 15: // exp -> FALSE
#line 59 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(false); }
        break;
      case 16: // exp -> STRUCT, ID, '{', structFieldValues, exp, '}'
#line 60 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); ValueStack[ValueStack.Depth-3].structValue.Name = ValueStack[ValueStack.Depth-5].identifier; CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-3].structValue;}
        break;
      case 17: // exp -> exp, '+', exp
#line 61 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Sum(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 18: // exp -> exp, '-', exp
#line 62 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Subtraction(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 19: // exp -> exp, '*', exp
#line 63 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Product(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 20: // exp -> exp, '/', exp
#line 64 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Division(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 21: // exp -> exp, '%', exp
#line 65 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Remainder(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 22: // exp -> '+', exp
#line 66 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= ValueStack[ValueStack.Depth-1].exp; }
        break;
      case 23: // exp -> '-', exp
#line 67 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Minus(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 24: // exp -> exp, '!'
#line 68 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Factorial(ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 25: // exp -> exp, '^', exp
#line 69 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Power(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 26: // exp -> MAX, '(', exp, ',', exp, ')'
#line 70 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Max(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 27: // exp -> MIN, '(', exp, ',', exp, ')'
#line 71 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Min(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 28: // exp -> '(', exp, ')'
#line 72 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-2].exp;}
        break;
      case 29: // exp -> ID
#line 73 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Id(ValueStack[ValueStack.Depth-1].identifier); }
        break;
      case 30: // exp -> '~', exp
#line 74 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Not(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 31: // exp -> exp, AND, exp
#line 75 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new And(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 32: // exp -> exp, OR, exp
#line 76 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Or(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 33: // exp -> exp, EQUAL, exp
#line 77 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Equal(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 34: // exp -> exp, LEQ, exp
#line 78 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessEqual(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 35: // exp -> exp, LESS_THAN, exp
#line 79 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessThan(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 36: // exp -> exp, DOT, ID
#line 80 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Dot(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].identifier); }
        break;
      case 37: // structFieldDecl -> /* empty */
#line 83 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structDecl = new StructDecl();}
        break;
      case 38: // structFieldDecl -> structFieldDecl, type, ID, ';'
#line 84 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].structDecl.AddField(ValueStack[ValueStack.Depth-2].identifier, ValueStack[ValueStack.Depth-3].identifier); CurrentSemanticValue.structDecl = ValueStack[ValueStack.Depth-4].structDecl;}
        break;
      case 39: // structFieldValues -> /* empty */
#line 87 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structValue = new StructValue();}
        break;
      case 40: // structFieldValues -> structFieldValues, exp, ','
#line 88 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); CurrentSemanticValue.structValue = ValueStack[ValueStack.Depth-3].structValue;}
        break;
      case 41: // funcArgs -> /* empty */
#line 91 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionDecl = new FunctionDecl();}
        break;
      case 42: // funcArgs -> type, ID
#line 92 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionDecl = new FunctionDecl(); CurrentSemanticValue.functionDecl.AddParam(ValueStack[ValueStack.Depth-1].identifier, ValueStack[ValueStack.Depth-2].identifier);}
        break;
      case 43: // funcArgs -> funcArgs, ',', type, ID
#line 93 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].functionDecl.AddParam(ValueStack[ValueStack.Depth-1].identifier, ValueStack[ValueStack.Depth-2].identifier); CurrentSemanticValue.functionDecl = ValueStack[ValueStack.Depth-4].functionDecl;}
        break;
      case 44: // type -> BOOL
#line 96 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = "bool";}
        break;
      case 45: // type -> INT
#line 97 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = "int";}
        break;
      case 46: // type -> VOID
#line 98 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = "void";}
        break;
      case 47: // type -> STRUCT, ID
#line 99 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = ValueStack[ValueStack.Depth-1].identifier;}
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

#line 103 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	public Parser(Scanner s) : base(s) {}
}
}
