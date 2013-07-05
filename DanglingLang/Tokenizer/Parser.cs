// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.0
// Machine:  CELESTINO
// DateTime: 05/07/2013 14.53.41
// UserName: Alessio
// Input file <D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 04/07/2013 10.43.22>

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
    DOT=145,BOOL=146,INT=147,VOID=148,LOAD=149,RETURN=150,
    UMINUS=151};

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
	internal FunctionCall functionCall;
#line 10 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal Stmt stmt;
#line 11 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal List<Stmt> stmts;
#line 12 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
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
  // Verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 04/07/2013 10.43.22
#line 41 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	internal FunctionDecl Prog;
  // End verbatim content from D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y - 04/07/2013 10.43.22

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[59];
  private static State[] states = new State[133];
  private static string[] nonTerms = new string[] {
      "exp", "structFieldValues", "structFieldDecl", "funcParams", "funcArgs", 
      "stmt", "topstmt", "stmts", "topstmts", "prog", "type", "$accept", };

  static Parser() {
    states[0] = new State(-3,new int[]{-10,1,-9,3});
    states[1] = new State(new int[]{128,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{133,6,130,7,129,36,138,37,139,38,136,75,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106,146,83,147,84,148,121,149,129,128,-2},new int[]{-7,4,-6,5,-1,74,-11,108});
    states[4] = new State(-4);
    states[5] = new State(-5);
    states[6] = new State(-12);
    states[7] = new State(new int[]{61,8,40,66,43,-37,45,-37,42,-37,47,-37,37,-37,33,-37,94,-37,140,-37,141,-37,142,-37,144,-37,143,-37,145,-37,133,-37,130,-37,129,-37,138,-37,139,-37,136,-37,131,-37,132,-37,126,-37,123,-37,134,-37,135,-37,137,-37,150,-37,146,-37,147,-37,148,-37,149,-37,128,-37,125,-37});
    states[8] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,9});
    states[9] = new State(new int[]{133,10,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[10] = new State(-13);
    states[11] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,12});
    states[12] = new State(new int[]{43,-25,45,-25,42,15,47,17,37,19,33,21,94,22,140,-25,141,-25,142,-25,144,-25,143,-25,145,34,133,-25,130,-25,129,-25,138,-25,139,-25,136,-25,131,-25,132,-25,40,-25,126,-25,123,-25,134,-25,135,-25,137,-25,150,-25,146,-25,147,-25,148,-25,149,-25,128,-25,125,-25,44,-25,41,-25});
    states[13] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,14});
    states[14] = new State(new int[]{43,-26,45,-26,42,15,47,17,37,19,33,21,94,22,140,-26,141,-26,142,-26,144,-26,143,-26,145,34,133,-26,130,-26,129,-26,138,-26,139,-26,136,-26,131,-26,132,-26,40,-26,126,-26,123,-26,134,-26,135,-26,137,-26,150,-26,146,-26,147,-26,148,-26,149,-26,128,-26,125,-26,44,-26,41,-26});
    states[15] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,16});
    states[16] = new State(new int[]{43,-27,45,-27,42,-27,47,-27,37,-27,33,21,94,22,140,-27,141,-27,142,-27,144,-27,143,-27,145,34,133,-27,130,-27,129,-27,138,-27,139,-27,136,-27,131,-27,132,-27,40,-27,126,-27,123,-27,134,-27,135,-27,137,-27,150,-27,146,-27,147,-27,148,-27,149,-27,128,-27,125,-27,44,-27,41,-27});
    states[17] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,18});
    states[18] = new State(new int[]{43,-28,45,-28,42,-28,47,-28,37,-28,33,21,94,22,140,-28,141,-28,142,-28,144,-28,143,-28,145,34,133,-28,130,-28,129,-28,138,-28,139,-28,136,-28,131,-28,132,-28,40,-28,126,-28,123,-28,134,-28,135,-28,137,-28,150,-28,146,-28,147,-28,148,-28,149,-28,128,-28,125,-28,44,-28,41,-28});
    states[19] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,20});
    states[20] = new State(new int[]{43,-29,45,-29,42,-29,47,-29,37,-29,33,21,94,22,140,-29,141,-29,142,-29,144,-29,143,-29,145,34,133,-29,130,-29,129,-29,138,-29,139,-29,136,-29,131,-29,132,-29,40,-29,126,-29,123,-29,134,-29,135,-29,137,-29,150,-29,146,-29,147,-29,148,-29,149,-29,128,-29,125,-29,44,-29,41,-29});
    states[21] = new State(-32);
    states[22] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,23});
    states[23] = new State(new int[]{43,-33,45,-33,42,-33,47,-33,37,-33,33,-33,94,22,140,-33,141,-33,142,-33,144,-33,143,-33,145,34,133,-33,130,-33,129,-33,138,-33,139,-33,136,-33,131,-33,132,-33,40,-33,126,-33,123,-33,134,-33,135,-33,137,-33,150,-33,146,-33,147,-33,148,-33,149,-33,128,-33,125,-33,44,-33,41,-33});
    states[24] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,25});
    states[25] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,-39,141,-39,142,28,144,30,143,32,145,34,133,-39,130,-39,129,-39,138,-39,139,-39,136,-39,131,-39,132,-39,40,-39,126,-39,123,-39,134,-39,135,-39,137,-39,150,-39,146,-39,147,-39,148,-39,149,-39,128,-39,125,-39,44,-39,41,-39});
    states[26] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,27});
    states[27] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,-40,142,28,144,30,143,32,145,34,133,-40,130,-40,129,-40,138,-40,139,-40,136,-40,131,-40,132,-40,40,-40,126,-40,123,-40,134,-40,135,-40,137,-40,150,-40,146,-40,147,-40,148,-40,149,-40,128,-40,125,-40,44,-40,41,-40});
    states[28] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,29});
    states[29] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,-41,141,-41,142,-41,144,30,143,32,145,34,133,-41,130,-41,129,-41,138,-41,139,-41,136,-41,131,-41,132,-41,40,-41,126,-41,123,-41,134,-41,135,-41,137,-41,150,-41,146,-41,147,-41,148,-41,149,-41,128,-41,125,-41,44,-41,41,-41});
    states[30] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,31});
    states[31] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,-42,141,-42,142,-42,144,-42,143,-42,145,34,133,-42,130,-42,129,-42,138,-42,139,-42,136,-42,131,-42,132,-42,40,-42,126,-42,123,-42,134,-42,135,-42,137,-42,150,-42,146,-42,147,-42,148,-42,149,-42,128,-42,125,-42,44,-42,41,-42});
    states[32] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,33});
    states[33] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,-43,141,-43,142,-43,144,-43,143,-43,145,34,133,-43,130,-43,129,-43,138,-43,139,-43,136,-43,131,-43,132,-43,40,-43,126,-43,123,-43,134,-43,135,-43,137,-43,150,-43,146,-43,147,-43,148,-43,149,-43,128,-43,125,-43,44,-43,41,-43});
    states[34] = new State(new int[]{130,35});
    states[35] = new State(-44);
    states[36] = new State(-21);
    states[37] = new State(-22);
    states[38] = new State(-23);
    states[39] = new State(new int[]{130,40});
    states[40] = new State(new int[]{123,41});
    states[41] = new State(-48,new int[]{-2,42});
    states[42] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,43});
    states[43] = new State(new int[]{125,44,44,45,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[44] = new State(-24);
    states[45] = new State(-49);
    states[46] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,47});
    states[47] = new State(new int[]{43,-30,45,-30,42,-30,47,-30,37,-30,33,21,94,22,140,-30,141,-30,142,-30,144,-30,143,-30,145,34,133,-30,130,-30,129,-30,138,-30,139,-30,136,-30,131,-30,132,-30,40,-30,126,-30,123,-30,134,-30,135,-30,137,-30,150,-30,146,-30,147,-30,148,-30,149,-30,128,-30,125,-30,44,-30,41,-30});
    states[48] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,49});
    states[49] = new State(new int[]{43,-31,45,-31,42,-31,47,-31,37,-31,33,21,94,22,140,-31,141,-31,142,-31,144,-31,143,-31,145,34,133,-31,130,-31,129,-31,138,-31,139,-31,136,-31,131,-31,132,-31,40,-31,126,-31,123,-31,134,-31,135,-31,137,-31,150,-31,146,-31,147,-31,148,-31,149,-31,128,-31,125,-31,44,-31,41,-31});
    states[50] = new State(new int[]{40,51});
    states[51] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,52});
    states[52] = new State(new int[]{44,53,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[53] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,54});
    states[54] = new State(new int[]{41,55,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[55] = new State(-34);
    states[56] = new State(new int[]{40,57});
    states[57] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,58});
    states[58] = new State(new int[]{44,59,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[59] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,60});
    states[60] = new State(new int[]{41,61,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[61] = new State(-35);
    states[62] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,63});
    states[63] = new State(new int[]{41,64,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[64] = new State(-36);
    states[65] = new State(new int[]{40,66,133,-37,43,-37,45,-37,42,-37,47,-37,37,-37,33,-37,94,-37,140,-37,141,-37,142,-37,144,-37,143,-37,145,-37,130,-37,129,-37,138,-37,139,-37,136,-37,131,-37,132,-37,126,-37,123,-37,134,-37,135,-37,137,-37,150,-37,146,-37,147,-37,148,-37,149,-37,128,-37,125,-37,44,-37,41,-37});
    states[66] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71,41,-53,44,-53},new int[]{-5,67,-1,73});
    states[67] = new State(new int[]{41,68,44,69});
    states[68] = new State(-45);
    states[69] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,70});
    states[70] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34,41,-55,44,-55});
    states[71] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,72});
    states[72] = new State(new int[]{43,-38,45,-38,42,-38,47,-38,37,-38,33,-38,94,-38,140,-38,141,-38,142,-38,144,-38,143,-38,145,34,133,-38,130,-38,129,-38,138,-38,139,-38,136,-38,131,-38,132,-38,40,-38,126,-38,123,-38,134,-38,135,-38,137,-38,150,-38,146,-38,147,-38,148,-38,149,-38,128,-38,125,-38,44,-38,41,-38});
    states[73] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34,41,-54,44,-54});
    states[74] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34,133,-14,130,-14,129,-14,138,-14,139,-14,136,-14,131,-14,132,-14,40,-14,126,-14,123,-14,134,-14,135,-14,137,-14,150,-14,146,-14,147,-14,148,-14,149,-14,128,-14,125,-14});
    states[75] = new State(new int[]{130,76});
    states[76] = new State(new int[]{123,77,130,-58});
    states[77] = new State(new int[]{129,-48,138,-48,139,-48,136,-46,43,-48,45,-48,131,-48,132,-48,40,-48,130,-48,126,-48,125,-46,146,-46,147,-46},new int[]{-2,42,-3,78});
    states[78] = new State(new int[]{125,79,146,83,147,84,136,85},new int[]{-11,80});
    states[79] = new State(-6);
    states[80] = new State(new int[]{130,81});
    states[81] = new State(new int[]{59,82});
    states[82] = new State(-47);
    states[83] = new State(-56);
    states[84] = new State(-57);
    states[85] = new State(new int[]{130,86});
    states[86] = new State(-58);
    states[87] = new State(-10,new int[]{-8,88});
    states[88] = new State(new int[]{125,89,133,6,130,7,129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106},new int[]{-6,90,-1,74});
    states[89] = new State(-15);
    states[90] = new State(-11);
    states[91] = new State(new int[]{40,92});
    states[92] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,93});
    states[93] = new State(new int[]{41,94,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[94] = new State(new int[]{133,6,130,7,129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106},new int[]{-6,95,-1,74});
    states[95] = new State(-16);
    states[96] = new State(new int[]{40,97});
    states[97] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,98});
    states[98] = new State(new int[]{41,99,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[99] = new State(new int[]{133,6,130,7,129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106},new int[]{-6,100,-1,74});
    states[100] = new State(-17);
    states[101] = new State(new int[]{40,102});
    states[102] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71},new int[]{-1,103});
    states[103] = new State(new int[]{41,104,43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34});
    states[104] = new State(new int[]{133,105});
    states[105] = new State(-18);
    states[106] = new State(new int[]{129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,130,65,126,71,133,-19,123,-19,134,-19,135,-19,137,-19,150,-19,146,-19,147,-19,148,-19,149,-19,128,-19,125,-19},new int[]{-1,107});
    states[107] = new State(new int[]{43,11,45,13,42,15,47,17,37,19,33,21,94,22,140,24,141,26,142,28,144,30,143,32,145,34,133,-20,130,-20,129,-20,138,-20,139,-20,136,-20,131,-20,132,-20,40,-20,126,-20,123,-20,134,-20,135,-20,137,-20,150,-20,146,-20,147,-20,148,-20,149,-20,128,-20,125,-20});
    states[108] = new State(new int[]{130,109});
    states[109] = new State(new int[]{40,110});
    states[110] = new State(new int[]{146,83,147,84,136,85,41,-50,44,-50},new int[]{-4,111,-11,119});
    states[111] = new State(new int[]{41,112,44,116});
    states[112] = new State(new int[]{123,113});
    states[113] = new State(-10,new int[]{-8,114});
    states[114] = new State(new int[]{125,115,133,6,130,7,129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106},new int[]{-6,90,-1,74});
    states[115] = new State(-7);
    states[116] = new State(new int[]{146,83,147,84,136,85},new int[]{-11,117});
    states[117] = new State(new int[]{130,118});
    states[118] = new State(-52);
    states[119] = new State(new int[]{130,120});
    states[120] = new State(-51);
    states[121] = new State(new int[]{130,122});
    states[122] = new State(new int[]{40,123});
    states[123] = new State(new int[]{146,83,147,84,136,85,41,-50,44,-50},new int[]{-4,124,-11,119});
    states[124] = new State(new int[]{41,125,44,116});
    states[125] = new State(new int[]{123,126});
    states[126] = new State(-10,new int[]{-8,127});
    states[127] = new State(new int[]{125,128,133,6,130,7,129,36,138,37,139,38,136,39,43,46,45,48,131,50,132,56,40,62,126,71,123,87,134,91,135,96,137,101,150,106},new int[]{-6,90,-1,74});
    states[128] = new State(-8);
    states[129] = new State(new int[]{40,130});
    states[130] = new State(new int[]{130,131});
    states[131] = new State(new int[]{41,132});
    states[132] = new State(-9);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-12, new int[]{-10,128});
    rules[2] = new Rule(-10, new int[]{-9});
    rules[3] = new Rule(-9, new int[]{});
    rules[4] = new Rule(-9, new int[]{-9,-7});
    rules[5] = new Rule(-7, new int[]{-6});
    rules[6] = new Rule(-7, new int[]{136,130,123,-3,125});
    rules[7] = new Rule(-7, new int[]{-11,130,40,-4,41,123,-8,125});
    rules[8] = new Rule(-7, new int[]{148,130,40,-4,41,123,-8,125});
    rules[9] = new Rule(-7, new int[]{149,40,130,41});
    rules[10] = new Rule(-8, new int[]{});
    rules[11] = new Rule(-8, new int[]{-8,-6});
    rules[12] = new Rule(-6, new int[]{133});
    rules[13] = new Rule(-6, new int[]{130,61,-1,133});
    rules[14] = new Rule(-6, new int[]{-1});
    rules[15] = new Rule(-6, new int[]{123,-8,125});
    rules[16] = new Rule(-6, new int[]{134,40,-1,41,-6});
    rules[17] = new Rule(-6, new int[]{135,40,-1,41,-6});
    rules[18] = new Rule(-6, new int[]{137,40,-1,41,133});
    rules[19] = new Rule(-6, new int[]{150});
    rules[20] = new Rule(-6, new int[]{150,-1});
    rules[21] = new Rule(-1, new int[]{129});
    rules[22] = new Rule(-1, new int[]{138});
    rules[23] = new Rule(-1, new int[]{139});
    rules[24] = new Rule(-1, new int[]{136,130,123,-2,-1,125});
    rules[25] = new Rule(-1, new int[]{-1,43,-1});
    rules[26] = new Rule(-1, new int[]{-1,45,-1});
    rules[27] = new Rule(-1, new int[]{-1,42,-1});
    rules[28] = new Rule(-1, new int[]{-1,47,-1});
    rules[29] = new Rule(-1, new int[]{-1,37,-1});
    rules[30] = new Rule(-1, new int[]{43,-1});
    rules[31] = new Rule(-1, new int[]{45,-1});
    rules[32] = new Rule(-1, new int[]{-1,33});
    rules[33] = new Rule(-1, new int[]{-1,94,-1});
    rules[34] = new Rule(-1, new int[]{131,40,-1,44,-1,41});
    rules[35] = new Rule(-1, new int[]{132,40,-1,44,-1,41});
    rules[36] = new Rule(-1, new int[]{40,-1,41});
    rules[37] = new Rule(-1, new int[]{130});
    rules[38] = new Rule(-1, new int[]{126,-1});
    rules[39] = new Rule(-1, new int[]{-1,140,-1});
    rules[40] = new Rule(-1, new int[]{-1,141,-1});
    rules[41] = new Rule(-1, new int[]{-1,142,-1});
    rules[42] = new Rule(-1, new int[]{-1,144,-1});
    rules[43] = new Rule(-1, new int[]{-1,143,-1});
    rules[44] = new Rule(-1, new int[]{-1,145,130});
    rules[45] = new Rule(-1, new int[]{130,40,-5,41});
    rules[46] = new Rule(-3, new int[]{});
    rules[47] = new Rule(-3, new int[]{-3,-11,130,59});
    rules[48] = new Rule(-2, new int[]{});
    rules[49] = new Rule(-2, new int[]{-2,-1,44});
    rules[50] = new Rule(-4, new int[]{});
    rules[51] = new Rule(-4, new int[]{-11,130});
    rules[52] = new Rule(-4, new int[]{-4,44,-11,130});
    rules[53] = new Rule(-5, new int[]{});
    rules[54] = new Rule(-5, new int[]{-1});
    rules[55] = new Rule(-5, new int[]{-5,44,-1});
    rules[56] = new Rule(-11, new int[]{146});
    rules[57] = new Rule(-11, new int[]{147});
    rules[58] = new Rule(-11, new int[]{136,130});
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
      case 2: // prog -> topstmts
#line 45 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{Prog = new FunctionDecl(); Prog.Name = "$Main"; Prog.ReturnTypeName = "void"; Prog.Body = new Block(ValueStack[ValueStack.Depth-1].stmts);}
        break;
      case 3: // topstmts -> /* empty */
#line 48 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmts = new List<Stmt>();}
        break;
      case 4: // topstmts -> topstmts, topstmt
#line 49 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{if (ValueStack[ValueStack.Depth-1].stmt != null) ValueStack[ValueStack.Depth-2].stmts.Add(ValueStack[ValueStack.Depth-1].stmt); CurrentSemanticValue.stmts = ValueStack[ValueStack.Depth-2].stmts;}
        break;
      case 5: // topstmt -> stmt
#line 52 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-1].stmt;}
        break;
      case 6: // topstmt -> STRUCT, ID, '{', structFieldDecl, '}'
#line 53 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-2].structDecl.Name = ValueStack[ValueStack.Depth-4].identifier; CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-2].structDecl;}
        break;
      case 7: // topstmt -> type, ID, '(', funcParams, ')', '{', stmts, '}'
#line 54 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-5].functionDecl.Name = ValueStack[ValueStack.Depth-7].identifier; ValueStack[ValueStack.Depth-5].functionDecl.ReturnTypeName = ValueStack[ValueStack.Depth-8].identifier; ValueStack[ValueStack.Depth-5].functionDecl.Body = new Block(ValueStack[ValueStack.Depth-2].stmts); CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-5].functionDecl;}
        break;
      case 8: // topstmt -> VOID, ID, '(', funcParams, ')', '{', stmts, '}'
#line 55 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-5].functionDecl.Name = ValueStack[ValueStack.Depth-7].identifier; ValueStack[ValueStack.Depth-5].functionDecl.ReturnTypeName = "void"; ValueStack[ValueStack.Depth-5].functionDecl.Body = new Block(ValueStack[ValueStack.Depth-2].stmts); CurrentSemanticValue.stmt = ValueStack[ValueStack.Depth-5].functionDecl;}
        break;
      case 9: // topstmt -> LOAD, '(', ID, ')'
#line 56 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new LoadStmt(ValueStack[ValueStack.Depth-2].identifier);}
        break;
      case 10: // stmts -> /* empty */
#line 59 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmts = new List<Stmt>();}
        break;
      case 11: // stmts -> stmts, stmt
#line 60 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{if (ValueStack[ValueStack.Depth-1].stmt != null) ValueStack[ValueStack.Depth-2].stmts.Add(ValueStack[ValueStack.Depth-1].stmt); CurrentSemanticValue.stmts = ValueStack[ValueStack.Depth-2].stmts;}
        break;
      case 12: // stmt -> NEWLINE
#line 63 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = null;}
        break;
      case 13: // stmt -> ID, '=', exp, NEWLINE
#line 64 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Assignment(ValueStack[ValueStack.Depth-4].identifier, ValueStack[ValueStack.Depth-2].exp);}
        break;
      case 14: // stmt -> exp
#line 65 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new EvalExp(ValueStack[ValueStack.Depth-1].exp);}
        break;
      case 15: // stmt -> '{', stmts, '}'
#line 66 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Block(ValueStack[ValueStack.Depth-2].stmts);}
        break;
      case 16: // stmt -> IF, '(', exp, ')', stmt
#line 67 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new If(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>()));}
        break;
      case 17: // stmt -> WHILE, '(', exp, ')', stmt
#line 68 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new While(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].stmt ?? new Block(new List<Stmt>()));}
        break;
      case 18: // stmt -> PRINT, '(', exp, ')', NEWLINE
#line 69 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Print(ValueStack[ValueStack.Depth-3].exp);}
        break;
      case 19: // stmt -> RETURN
#line 70 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Return();}
        break;
      case 20: // stmt -> RETURN, exp
#line 71 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.stmt = new Return(ValueStack[ValueStack.Depth-1].exp);}
        break;
      case 21: // exp -> NUM
#line 74 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new IntLiteral(ValueStack[ValueStack.Depth-1].intValue); }
        break;
      case 22: // exp -> TRUE
#line 75 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(true); }
        break;
      case 23: // exp -> FALSE
#line 76 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new BoolLiteral(false); }
        break;
      case 24: // exp -> STRUCT, ID, '{', structFieldValues, exp, '}'
#line 77 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); ValueStack[ValueStack.Depth-3].structValue.Name = ValueStack[ValueStack.Depth-5].identifier; CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-3].structValue;}
        break;
      case 25: // exp -> exp, '+', exp
#line 78 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Sum(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 26: // exp -> exp, '-', exp
#line 79 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Subtraction(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 27: // exp -> exp, '*', exp
#line 80 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Product(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 28: // exp -> exp, '/', exp
#line 81 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Division(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 29: // exp -> exp, '%', exp
#line 82 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Remainder(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 30: // exp -> '+', exp
#line 83 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= ValueStack[ValueStack.Depth-1].exp; }
        break;
      case 31: // exp -> '-', exp
#line 84 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Minus(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 32: // exp -> exp, '!'
#line 85 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp= new Factorial(ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 33: // exp -> exp, '^', exp
#line 86 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Power(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 34: // exp -> MAX, '(', exp, ',', exp, ')'
#line 87 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Max(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 35: // exp -> MIN, '(', exp, ',', exp, ')'
#line 88 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Min(ValueStack[ValueStack.Depth-4].exp, ValueStack[ValueStack.Depth-2].exp); }
        break;
      case 36: // exp -> '(', exp, ')'
#line 89 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-2].exp;}
        break;
      case 37: // exp -> ID
#line 90 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Id(ValueStack[ValueStack.Depth-1].identifier); }
        break;
      case 38: // exp -> '~', exp
#line 91 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Not(ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 39: // exp -> exp, AND, exp
#line 92 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new And(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 40: // exp -> exp, OR, exp
#line 93 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Or(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 41: // exp -> exp, EQUAL, exp
#line 94 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new Equal(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 42: // exp -> exp, LEQ, exp
#line 95 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessEqual(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 43: // exp -> exp, LESS_THAN, exp
#line 96 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ CurrentSemanticValue.exp = new LessThan(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].exp); }
        break;
      case 44: // exp -> exp, DOT, ID
#line 97 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.exp = new Dot(ValueStack[ValueStack.Depth-3].exp, ValueStack[ValueStack.Depth-1].identifier);}
        break;
      case 45: // exp -> ID, '(', funcArgs, ')'
#line 98 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-2].functionCall.FunctionName = ValueStack[ValueStack.Depth-4].identifier; CurrentSemanticValue.exp = ValueStack[ValueStack.Depth-2].functionCall;}
        break;
      case 46: // structFieldDecl -> /* empty */
#line 101 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structDecl = new StructDecl();}
        break;
      case 47: // structFieldDecl -> structFieldDecl, type, ID, ';'
#line 102 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].structDecl.AddField(ValueStack[ValueStack.Depth-2].identifier, ValueStack[ValueStack.Depth-3].identifier); CurrentSemanticValue.structDecl = ValueStack[ValueStack.Depth-4].structDecl;}
        break;
      case 48: // structFieldValues -> /* empty */
#line 105 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.structValue = new StructValue();}
        break;
      case 49: // structFieldValues -> structFieldValues, exp, ','
#line 106 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].structValue.AddValue(ValueStack[ValueStack.Depth-2].exp); CurrentSemanticValue.structValue = ValueStack[ValueStack.Depth-3].structValue;}
        break;
      case 50: // funcParams -> /* empty */
#line 109 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionDecl = new FunctionDecl();}
        break;
      case 51: // funcParams -> type, ID
#line 110 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionDecl = new FunctionDecl(); CurrentSemanticValue.functionDecl.AddParam(ValueStack[ValueStack.Depth-1].identifier, ValueStack[ValueStack.Depth-2].identifier);}
        break;
      case 52: // funcParams -> funcParams, ',', type, ID
#line 111 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-4].functionDecl.AddParam(ValueStack[ValueStack.Depth-1].identifier, ValueStack[ValueStack.Depth-2].identifier); CurrentSemanticValue.functionDecl = ValueStack[ValueStack.Depth-4].functionDecl;}
        break;
      case 53: // funcArgs -> /* empty */
#line 114 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionCall = new FunctionCall();}
        break;
      case 54: // funcArgs -> exp
#line 115 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.functionCall = new FunctionCall(); CurrentSemanticValue.functionCall.AddArgument(ValueStack[ValueStack.Depth-1].exp);}
        break;
      case 55: // funcArgs -> funcArgs, ',', exp
#line 116 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{ValueStack[ValueStack.Depth-3].functionCall.AddArgument(ValueStack[ValueStack.Depth-1].exp); CurrentSemanticValue.functionCall = ValueStack[ValueStack.Depth-3].functionCall;}
        break;
      case 56: // type -> BOOL
#line 119 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = "bool";}
        break;
      case 57: // type -> INT
#line 120 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
{CurrentSemanticValue.identifier = "int";}
        break;
      case 58: // type -> STRUCT, ID
#line 121 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
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

#line 125 "D:\Dropbox\Progetti\C#\DanglingLang\DanglingLang\Tokenizer/DanglingLang.y"
	public Parser(Scanner s) : base(s) {}
}
}
