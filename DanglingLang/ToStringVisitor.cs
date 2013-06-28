namespace DanglingLang
{
    using System.Diagnostics;
    using System.Text;

    sealed class ToStringVisitor : ITreeNodeVisitor
    {
        readonly StringBuilder _sb = new StringBuilder();
        int _level = 1;

        public string Result
        {
            get { return _sb.ToString(); }
        }

        public void Visit(Sum sum)
        {
            sum.Left.Accept(this);
            _sb.Append("+");
            sum.Right.Accept(this);
        }

        public void Visit(Subtraction sub)
        {
            sub.Left.Accept(this);
            _sb.Append("-");
            sub.Right.Accept(this);
        }

        public void Visit(Product prod)
        {
            prod.Left.Accept(this);
            _sb.Append("*");
            prod.Right.Accept(this);
        }

        public void Visit(Division div)
        {
            div.Left.Accept(this);
            _sb.Append("/");
            div.Right.Accept(this);
        }

        public void Visit(Remainder rem)
        {
            rem.Left.Accept(this);
            _sb.Append("%");
            rem.Right.Accept(this);
        }

        public void Visit(And and)
        {
            and.Left.Accept(this);
            _sb.Append(" && ");
            and.Right.Accept(this);
        }

        public void Visit(Or or)
        {
            or.Left.Accept(this);
            _sb.Append(" || ");
            or.Right.Accept(this);
        }

        public void Visit(Not not)
        {
            _sb.Append("~");
            not.Operand.Accept(this);
        }

        public void Visit(Equal eq)
        {
            eq.Left.Accept(this);
            _sb.Append(" == ");
            if (eq.Type != null) {
                Debug.Assert(eq.Left.Type.Equals(eq.Right.Type));
                _sb.Append(eq.Left.Type.LlvmType).Append(' ');
            }
            eq.Right.Accept(this);
        }

        public void Visit(LessEqual leq)
        {
            leq.Left.Accept(this);
            _sb.Append(" <= ");
            leq.Right.Accept(this);
        }

        public void Visit(LessThan lt)
        {
            lt.Left.Accept(this);
            _sb.Append(" < ");
            lt.Right.Accept(this);
        }

        public void Visit(Max max)
        {
            _sb.Append("max(");
            max.Left.Accept(this);
            _sb.Append(", ");
            max.Right.Accept(this);
            _sb.Append(")");
        }

        public void Visit(Min min)
        {
            _sb.Append("min(");
            min.Left.Accept(this);
            _sb.Append(", ");
            min.Right.Accept(this);
            _sb.Append(")");
        }

        public void Visit(Power pow)
        {
            pow.Left.Accept(this);
            _sb.Append("^");
            pow.Right.Accept(this);
        }

        public void Visit(BoolLiteral bl)
        {
            _sb.Append(bl.Value);
        }

        public void Visit(IntLiteral il)
        {
            _sb.Append(il.Value);
        }

        public void Visit(Id id)
        {
            _sb.Append(id.Var == null ? id.Name : id.Var.Name);
        }

        public void Visit(Print print)
        {
            Indent();
            if (print.Exp.Type != null) {
                _sb.Append(print.Exp.Type.Equals(Type.Bool) ? "print_bool" : "print_int");
            } else {
                _sb.Append("print");
            }
            _sb.Append('(');
            print.Exp.Accept(this);
            _sb.Append(")\n");
        }

        public void Visit(EvalExp eval)
        {
            Indent();
            eval.Exp.Accept(this);
            _sb.Append("\n");
        }

        public void Visit(Assignment asg)
        {
            Indent();
            _sb.Append(asg.Var == null ? asg.VarName : asg.Var.Name);
            _sb.Append(" = ");
            asg.Exp.Accept(this);
            _sb.Append("\n");
        }

        public void Visit(If ifs)
        {
            Indent();
            _sb.Append("if (");
            ifs.Guard.Accept(this);
            _sb.Append(")\n");
            ++_level;
            ifs.Body.Accept(this);
            --_level;
        }

        public void Visit(While whiles)
        {
            Indent();
            _sb.Append("while (");
            whiles.Guard.Accept(this);
            _sb.Append(")\n");
            ++_level;
            whiles.Body.Accept(this);
            --_level;
        }

        public void Visit(Block block)
        {
            Indent();
            _sb.Append("{\n");
            ++_level;
            foreach (var stmt in block.Statements) {
                stmt.Accept(this);
            }
            --_level;
            Indent();
            _sb.Append("}\n");
        }

        public void Visit(Prog prog)
        {
            foreach (var stmt in prog.Statements) {
                stmt.Accept(this);
            }
            if (prog.Vars.Count != 0) {
                _sb.Append("--- LLVM Vars ---\n");
                foreach (var v in prog.Vars) {
                    _sb.Append(v.Name).Append(" : ").Append(v.Type.LlvmType).Append('\n');
                }
            }
        }

        void Indent()
        {
            const int nspaces = 4;
            var x = _level*nspaces;
            for (var a = 0; a < x; ++a) {
                _sb.Append(' ');
            }
        }
    }
}