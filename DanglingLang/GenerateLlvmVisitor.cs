namespace DanglingLang
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    class GenerateLlvmVisitor : ITreeNodeVisitor
    {
        readonly StringBuilder _sb = new StringBuilder();
        int _labelCounter;
        int _regCounter;
        string _regName;

        public string Result
        {
            get { return _sb.ToString(); }
        }

        public void Visit(Sum sum)
        {
            VisitBinaryOp(sum, "\t{0} = add i32 {1}, {2}\n");
        }

        public void Visit(Subtraction sub)
        {
            VisitBinaryOp(sub, "\t{0} = sub i32 {1}, {2}\n");
        }

        public void Visit(Product prod)
        {
            VisitBinaryOp(prod, "\t{0} = mul i32 {1}, {2}\n");
        }

        public void Visit(Division div)
        {
            VisitBinaryOp(div, "\t{0} = sdiv i32 {1}, {2}\n");
        }

        public void Visit(Remainder rem)
        {
            VisitBinaryOp(rem, "\t{0} = srem i32 {1}, {2}\n");
        }

        public void Visit(And and)
        {
            and.Left.Accept(this);
            var l = _regName;
            var lLeftEnd = GenerateLabel();
            var lRightBegin = GenerateLabel();
            var lRightEnd = GenerateLabel();
            EmitLabel(lLeftEnd, true);
            _sb.AppendFormat("\tbr i1 {0}, label %{1}, label %{2}\n", l, lRightBegin, lRightEnd);
            EmitLabel(lRightBegin);
            and.Right.Accept(this);
            EmitLabel(lRightEnd, true);
            var r = _regName;
            GenerateRegName();
            _sb.AppendFormat("\t{0} = phi i1 [0, %{1}], [{2}, %{3}]\n", _regName, lLeftEnd, r, lRightBegin);
        }

        public void Visit(Or or)
        {
            or.Left.Accept(this);
            var l = _regName;
            var lLeftEnd = GenerateLabel();
            var lRightBegin = GenerateLabel();
            var lRightEnd = GenerateLabel();
            EmitLabel(lLeftEnd, true);
            _sb.AppendFormat("\tbr i1 {0}, label %{1}, label %{2}\n", l, lRightEnd, lRightBegin);
            EmitLabel(lRightBegin);
            or.Right.Accept(this);
            EmitLabel(lRightEnd, true);
            var r = _regName;
            GenerateRegName();
            _sb.AppendFormat("\t{0} = phi i1 [0, %{1}], [{2}, %{3}]\n", _regName, lLeftEnd, r, lRightBegin);
        }

        public void Visit(Not not)
        {
            not.Operand.Accept(this);
            var opReg = _regName;
            GenerateRegName();
            _sb.AppendFormat("\t{0} = xor i1 1, {1}\n", _regName, opReg);
        }

        public void Visit(Equal eq)
        {
            eq.Left.Accept(this);
            var l = _regName;
            eq.Right.Accept(this);
            var r = _regName;
            GenerateRegName();
            _sb.AppendFormat("\t{0} = icmp eq {1} {2}, {3}\n", _regName, eq.Left.Type.LlvmType, l, r);
        }

        public void Visit(LessEqual leq)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(LessThan lt)
        {
            VisitBinaryOp(lt, "\t{0} = icmp slt i32 {1}, {2}\n");
        }

        public void Visit(Max max)
        {
            VisitBinaryOp(max, "\t{0} = call i32 (i32, i32)* @max(i32 {1}, i32 {2})\n");
        }

        public void Visit(Min min)
        {
            VisitBinaryOp(min, "\t{0} = call i32 (i32, i32)* @min(i32 {1}, i32 {2})\n");
        }

        public void Visit(Power pow)
        {
            VisitBinaryOp(pow, "\t{0} = call i32 (i32, i32)* @pow(i32 {1}, i32 {2})\n");
        }

        public void Visit(BoolLiteral bl)
        {
            _regName = bl.Value.ToString().ToLower();
        }

        public void Visit(IntLiteral il)
        {
            _regName = il.Value.ToString(CultureInfo.InvariantCulture);
        }

        public void Visit(Id id)
        {
            GenerateRegName();
            _sb.AppendFormat("\t{0} = load {1}* {2}\n", _regName, id.Var.Type.LlvmType, id.Var.Name);
        }

        public void Visit(Print print)
        {
            _sb.Append("\t; ").Append(print);
            print.Exp.Accept(this);
            var funcName = print.Exp.Type.Equals(Type.Bool) ? "print_bool" : "print_int";
            _sb.AppendFormat("\tcall void ({0})* @{1}({0} {2})\n", print.Exp.Type.LlvmType, funcName, _regName);
        }

        public void Visit(EvalExp eval)
        {
            _sb.Append("\t; ").Append(eval);
            eval.Exp.Accept(this);
        }

        public void Visit(Assignment asg)
        {
            _sb.Append("\t; ").Append(asg);
            asg.Exp.Accept(this);
            Debug.Assert(asg.Exp.Type.Equals(asg.Var.Type));
            _sb.AppendFormat("\tstore {0} {1}, {2}* {3}\n", asg.Exp.Type.LlvmType, _regName, asg.Var.Type.LlvmType,
                             asg.Var.Name);
        }

        public void Visit(If ifs)
        {
            _sb.Append("\t; BEGIN IF, guard=").AppendLine(ifs.Guard.ToString());
            ifs.Guard.Accept(this);
            var lBodyBegin = GenerateLabel();
            var lBodyEnd = GenerateLabel();
            _sb.AppendFormat("\tbr i1 {0}, label %{1}, label %{2}\n", _regName, lBodyBegin, lBodyEnd);
            EmitLabel(lBodyBegin);
            ifs.Body.Accept(this);
            EmitLabel(lBodyEnd, true);
            _sb.Append("\t; END IF, guard=").AppendLine(ifs.Guard.ToString());
        }

        public void Visit(While whiles)
        {
            _sb.Append("\t; BEGIN WHILE, guard=").AppendLine(whiles.Guard.ToString());
            whiles.Guard.Accept(this);
            var lBodyBegin = GenerateLabel();
            var lBodyEnd = GenerateLabel();
            _sb.AppendFormat("\tbr i1 {0}, label %{1}, label %{2}\n", _regName, lBodyBegin, lBodyEnd);
            EmitLabel(lBodyBegin);
            whiles.Body.Accept(this);
            whiles.Guard.Accept(this);
            _sb.AppendFormat("\tbr i1 {0}, label %{1}, label %{2}\n", _regName, lBodyBegin, lBodyEnd);
            EmitLabel(lBodyEnd, true);
            _sb.Append("\t; END WHILE, guard=").AppendLine(whiles.Guard.ToString());
        }

        public void Visit(Block block)
        {
            foreach (var stmt in block.Statements) {
                stmt.Accept(this);
            }
        }

        public void Visit(Prog prog)
        {
            _sb.Append("target triple = \"i686-w64-mingw32\"\n\ndefine i32 @main() {\n");
            foreach (var v in prog.Vars) {
                _sb.AppendFormat("\t{0} = alloca {1}\n", v.Name, v.Type.LlvmType);
            }
            foreach (var stmt in prog.Statements) {
                stmt.Accept(this);
            }
            _sb.Append("\tret i32 0\n}\n");
#if true
            _sb.Append(@"
@fmt_int  = private unnamed_addr constant [4 x i8] c""%d\0A\00""
@true_str = private unnamed_addr constant [5 x i8] c""true\00""
@false_str= private unnamed_addr constant [6 x i8] c""false\00""

define void @print_int(i32 %x) {
  tail call i32 (i8*, ...)* @printf(i8* getelementptr inbounds ([4 x i8]* @fmt_int, i32 0, i32 0), i32 %x)
  ret void
}

define void @print_bool(i1 %b) {
  %1 = icmp ne i1 %b, 0
  %2 = select i1 %1, i8* getelementptr inbounds ([5 x i8]* @true_str, i32 0, i32 0), i8* getelementptr inbounds ([6 x i8]* @false_str, i32 0, i32 0)
  tail call i32 @puts(i8* %2)
  ret void
}

declare i32 @printf(i8*, ...) 
declare i32 @puts(i8*) 

define i32 @max(i32 %a, i32 %b) {
  %1 = icmp sgt i32 %a, %b
  %2 = select i1 %1, i32 %a, i32 %b
  ret i32 %2
}

define i32 @min(i32 %a, i32 %b) {
  %1 = icmp slt i32 %a, %b
  %2 = select i1 %1, i32 %a, i32 %b
  ret i32 %2
}
            ");
#endif
        }

        void GenerateRegName()
        {
            _regName = string.Format("%r{0}", _regCounter++);
        }

        string GenerateLabel()
        {
            return string.Format("l{0}", _labelCounter++);
        }

        void VisitBinaryOp(BinaryOp op, string fmtString)
        {
            op.Left.Accept(this);
            var l = _regName;
            op.Right.Accept(this);
            var r = _regName;
            GenerateRegName();
            _sb.AppendFormat(fmtString, _regName, l, r);
        }

        void VisitUnaryOp(UnaryOperator op, string fmtString)
        {
            op.Operand.Accept(this);
            var opReg = _regName;
            GenerateRegName();
            _sb.AppendFormat(fmtString, _regName, opReg);
        }

        void EmitLabel(string label, bool andJumpToIt = false)
        {
            if (andJumpToIt) {
                _sb.AppendFormat("\tbr label %{0}\n", label);
            }
            _sb.AppendFormat("{0}:\n", label);
        }
    }
}