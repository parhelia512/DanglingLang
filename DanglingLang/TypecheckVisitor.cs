namespace DanglingLang
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Thrower;

    [Serializable]
    public sealed class TypeCheckingException : Exception
    {
        public TypeCheckingException() {}
        public TypeCheckingException(string message) : base(message) {}
        public TypeCheckingException(string message, Exception inner) : base(message, inner) {}
    }

    sealed class Type
    {
        internal static readonly Type Bool = new Type("i1");
        internal static readonly Type Int = new Type("i32");

        Type(string llvmType)
        {
            LlvmType = llvmType;
        }

        public string LlvmType { get; private set; }
    }

    sealed class Variable
    {
        internal readonly string Name;
        internal readonly Type Type;

        public Variable(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }

    interface IStaticEnv
    {
        Variable GetVariable(string name);
        bool TryGetVariable(string name, out Variable t);
        void SetVariable(string name, Variable v);
    }

    sealed class StaticEnv : IStaticEnv
    {
        readonly Dictionary<string, Variable> _locals = new Dictionary<string, Variable>();
        readonly IStaticEnv _parent;

        public StaticEnv(IStaticEnv parent)
        {
            Raise<ArgumentNullException>.IfIsNull(parent);
            _parent = parent;
        }

        public Variable GetVariable(string name)
        {
            Variable v;
            if (_locals.TryGetValue(name, out v)) {
                return v;
            }
            return _parent.GetVariable(name);
        }

        public bool TryGetVariable(string name, out Variable t)
        {
            if (_locals.TryGetValue(name, out t)) {
                return true;
            }
            return _parent.TryGetVariable(name, out t);
        }

        public void SetVariable(string name, Variable v)
        {
            Debug.Assert(!_locals.ContainsKey(name));
            _locals[name] = v;
        }
    }

    sealed class OutmostStaticEnv : IStaticEnv
    {
        public Variable GetVariable(string name)
        {
            throw new TypeCheckingException(string.Format("Undefined variable {0}", name));
        }

        public bool TryGetVariable(string name, out Variable t)
        {
            t = null;
            return false;
        }

        public void SetVariable(string name, Variable v)
        {
            throw new TypeCheckingException("Internal compiler error");
        }
    }

    sealed class TypecheckVisitor : ITreeNodeVisitor
    {
        Prog _prog;
        Type _result;
        IStaticEnv _staticEnv = new StaticEnv(new OutmostStaticEnv());

        public void Visit(Sum sum)
        {
            ResultIsIntAndBothOperandsMustBeInt(sum);
        }

        public void Visit(Subtraction sub)
        {
            ResultIsIntAndBothOperandsMustBeInt(sub);
        }

        public void Visit(Product prod)
        {
            ResultIsIntAndBothOperandsMustBeInt(prod);
        }

        public void Visit(Division div)
        {
            ResultIsIntAndBothOperandsMustBeInt(div);
        }

        public void Visit(Remainder rem)
        {
            ResultIsIntAndBothOperandsMustBeInt(rem);
        }

        public void Visit(Minus min)
        {
            min.Operand.Accept(this);
            min.Type = MustBeInt(string.Format("The operand of {0} must be integer", min));
        }

        public void Visit(Factorial fact)
        {
            fact.Operand.Accept(this);
            fact.Type = MustBeInt(string.Format("The operand of {0} must be integer", fact));
        }

        public void Visit(And and)
        {
            ResultIsBoolAndBothOperandsMustBeBool(and);
        }

        public void Visit(Or or)
        {
            ResultIsBoolAndBothOperandsMustBeBool(or);
        }

        public void Visit(Not not)
        {
            not.Operand.Accept(this);
            not.Type = MustBeBool(string.Format("The operand of {0} must be bool", not));
        }

        public void Visit(Equal eq)
        {
            eq.Left.Accept(this);
            eq.Right.Accept(this);
            if (!eq.Left.Type.Equals(eq.Right.Type)) {
                throw new TypeCheckingException("Both operands of == must have the same type");
            }
            eq.Type = _result = Type.Bool;
        }

        public void Visit(LessEqual leq)
        {
            ResultIsBoolAndBothOperandsMustBeInt(leq);
        }

        public void Visit(LessThan lt)
        {
            ResultIsBoolAndBothOperandsMustBeInt(lt);
        }

        public void Visit(Max max)
        {
            ResultIsIntAndBothOperandsMustBeInt(max);
        }

        public void Visit(Min min)
        {
            ResultIsIntAndBothOperandsMustBeInt(min);
        }

        public void Visit(Power pow)
        {
            ResultIsIntAndBothOperandsMustBeInt(pow);
        }

        public void Visit(BoolLiteral bl)
        {
            _result = bl.Type = Type.Bool;
        }

        public void Visit(IntLiteral il)
        {
            _result = il.Type = Type.Int;
        }

        public void Visit(Id id)
        {
            var v = _staticEnv.GetVariable(id.Name);
            id.Var = v;
            id.Type = _result = v.Type;
        }

        public void Visit(Print print)
        {
            print.Exp.Accept(this);
        }

        public void Visit(EvalExp eval)
        {
            eval.Exp.Accept(this);
        }

        public void Visit(Assignment asg)
        {
            asg.Exp.Accept(this);
            var varName = asg.VarName;
            Variable variable;
            if (_staticEnv.TryGetVariable(varName, out variable)) {
                if (!variable.Type.Equals(_result)) {
                    throw new TypeCheckingException(string.Format(
                        "Cannot re-assign {0} with a value of different type", varName));
                }
            } else {
                variable = CreateVar(varName);
                _staticEnv.SetVariable(varName, variable);
            }
            asg.Var = variable;
        }

        public void Visit(If ifs)
        {
            ifs.Guard.Accept(this);
            MustBeBool("The if guard");
            ifs.Body.Accept(this);
        }

        public void Visit(While whiles)
        {
            whiles.Guard.Accept(this);
            MustBeBool("The while guard");
            whiles.Body.Accept(this);
        }

        public void Visit(Block block)
        {
            var previousStaticEnv = _staticEnv;
            _staticEnv = new StaticEnv(previousStaticEnv);
            try {
                foreach (var stmt in block.Statements) {
                    stmt.Accept(this);
                }
            } finally {
                _staticEnv = previousStaticEnv;
            }
        }

        public void Visit(Prog prog)
        {
            _prog = prog;
            foreach (var stmt in prog.Statements) {
                stmt.Accept(this);
            }
        }

        Type MustBe(Type t, string msg)
        {
            if (!_result.Equals(t)) {
                throw new TypeCheckingException(msg);
            }
            return t;
        }

        Type MustBeInt(string msg)
        {
            return MustBe(Type.Int, msg);
        }

        Type MustBeBool(string msg)
        {
            return MustBe(Type.Bool, msg);
        }

        void ResultIsIntAndBothOperandsMustBeInt(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeInt("The type of the left operand of " + binaryOp + " must be integer");
            binaryOp.Right.Accept(this);
            MustBeInt("The type of the right operand of " + binaryOp + " must be integer");
            binaryOp.Type = _result = Type.Int;
        }

        void ResultIsBoolAndBothOperandsMustBeBool(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeBool("The type of the left operand of " + binaryOp + " must be bool");
            binaryOp.Right.Accept(this);
            MustBeBool("The type of the right operand of " + binaryOp + " must be bool");
            binaryOp.Type = _result = Type.Bool;
        }

        void ResultIsBoolAndBothOperandsMustBeInt(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeInt("The type of the left operand of " + binaryOp + " must be integer");
            binaryOp.Right.Accept(this);
            MustBeInt("The type of the right operand of " + binaryOp + " must be integer");
            binaryOp.Type = _result = Type.Bool;
        }

        Variable CreateVar(string varName)
        {
            var sameName = _prog.Vars.Count(v => v.Name == varName);
            var llvmName = sameName == 0 ? string.Format("%{0}", varName) : string.Format("%{0}.{1}", varName, sameName);
            var llvmVar = new Variable(varName, _result);
            _prog.Vars.Add(llvmVar);
            return llvmVar;
        }
    }
}