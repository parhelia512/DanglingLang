namespace DanglingLang.Visitors
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

        public void Visit(Minus min)
        {
            _sb.Append("-");
            min.Operand.Accept(this);
        }

        public void Visit(Factorial fact)
        {
            fact.Operand.Accept(this);
            _sb.Append("!");
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
            Debug.Assert(eq.Left.Type.Equals(eq.Right.Type));
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

        public void Visit(Dot dot)
        {
            dot.Left.Accept(this);
            _sb.AppendFormat(".{0}", dot.Right);
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

        public void Visit(StructValue sv)
        {
            _sb.AppendFormat("struct {0} {{", sv.Name);
            int i = 0;
            for (; i < sv.Values.Count - 1; ++i) {
                sv.Values[i].Accept(this);
                _sb.Append(", ");
            }
            sv.Values[i].Accept(this);
            _sb.Append("}");
        }

        public void Visit(FunctionCall fc)
        {
            _sb.AppendFormat("{0}(", fc.FunctionName);
            if (fc.Arguments.Count > 0) {
                var i = 0;
                for (; i < fc.Arguments.Count - 1; ++i) {
                    fc.Arguments[i].Accept(this);
                    _sb.Append(", ");
                }
                fc.Arguments[i].Accept(this);
            }
            _sb.Append(")");
        }

        public void Visit(Id id)
        {
            _sb.Append(id.Var == null ? id.Name : id.Var.Name);
        }

        public void Visit(Print print)
        {
            Indent();
            _sb.Append("print(");
            print.Exp.Accept(this);
            _sb.Append(")\r\n");
        }

        public void Visit(StructDecl structDecl)
        {
            var type = structDecl.Type;
            Indent();
            _sb.Append("struct ");
            _sb.Append(type.Name);
            _sb.Append(" {");
            int i = 0;
            for (; i < type.Fields.Count - 1; ++i) {
                _sb.Append(type.Fields[i].Type);
                _sb.Append(" ");
                _sb.Append(type.Fields[i].Name);
                _sb.Append("; ");
            }
            _sb.Append(type.Fields[i].Type);
            _sb.Append(" ");
            _sb.Append(type.Fields[i].Name);
            _sb.Append(";}\r\n");
        }

        public void Visit(FunctionDecl funcDecl)
        {
            Indent();
            _sb.AppendFormat("{0} {1}(", funcDecl.ReturnType, funcDecl.Name);
            if (funcDecl.Params.Count > 0) {
                var i = 0;
                FunctionDecl.ParamInfo p;
                for (; i < funcDecl.Params.Count - 1; ++i) {
                    p = funcDecl.Params[i];
                    _sb.AppendFormat("{0} {1},", p.Type, p.Name);
                }
                p = funcDecl.Params[i];
                _sb.AppendFormat("{0} {1}", p.Type, p.Name);
            } 
            _sb.Append(")\r\n");   
            funcDecl.Body.Accept(this);
        }

        public void Visit(Assignment asg)
        {
            Indent();
            if (asg.LoadExp != null) {
                asg.LoadExp.Accept(this);
                _sb.Append(".");
            }
            _sb.Append(asg.Var == null ? asg.VarName : asg.Var.Name);
            _sb.Append(" = ");
            asg.Exp.Accept(this);
            _sb.Append("\r\n");
        }

        public void Visit(If ifs)
        {
            Indent();
            _sb.Append("if (");
            ifs.Guard.Accept(this);
            _sb.Append(")\r\n");
            ++_level;
            ifs.Body.Accept(this);
            --_level;
        }

        public void Visit(While whiles)
        {
            Indent();
            _sb.Append("while (");
            whiles.Guard.Accept(this);
            _sb.Append(")\r\n");
            ++_level;
            whiles.Body.Accept(this);
            --_level;
        }

        public void Visit(Block block)
        {
            Indent();
            _sb.Append("{\r\n");
            ++_level;
            foreach (var stmt in block.Statements) {
                stmt.Accept(this);
            }
            --_level;
            Indent();
            _sb.Append("}\r\n");
        }

        public void Visit(EvalExp eval)
        {
            Indent();
            eval.Exp.Accept(this);
            _sb.Append("\r\n");
        }

        public void Visit(Return ret)
        {
            Indent();
            _sb.Append("return ");
            if (ret.ReturnExp != null) {
                ret.ReturnExp.Accept(this);
            }
            _sb.Append("\r\n");
        }

        public void Visit(LoadStmt load)
        {
            Indent();
            _sb.AppendFormat("load({0})\r\n", load.Assembly);
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