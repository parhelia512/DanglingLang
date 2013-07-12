namespace DanglingLang
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Thrower;
    using Visitors;
    using Type = Visitors.Type;

    abstract class TreeNode
    {
        public abstract void Accept(ITreeNodeVisitor visitor);

        public override sealed string ToString()
        {
            var tsv = new ToStringVisitor();
            Accept(tsv);
            return tsv.Result;
        }
    }

    interface ITreeNodeVisitor
    {
        void Visit(Sum sum);
        void Visit(Subtraction sub);
        void Visit(Product prod);
        void Visit(Division div);
        void Visit(Remainder rem);
        void Visit(Minus min);
        void Visit(Factorial fact);
        void Visit(And and);
        void Visit(Or or);
        void Visit(Not not);
        void Visit(Equal eq);
        void Visit(LessEqual leq);
        void Visit(LessThan lt);
        void Visit(Dot dot);
        void Visit(Max max);
        void Visit(Min min);
        void Visit(Power pow);
        void Visit(BoolLiteral bl);
        void Visit(IntLiteral il);
        void Visit(StructValue sv);
        void Visit(FunctionCall fc);
        void Visit(Id id);
        void Visit(Print print);
        void Visit(StructDecl structDecl);
        void Visit(FunctionDecl funcDecl);
        void Visit(Assignment asg);
        void Visit(If ifs);
        void Visit(While whiles);
        void Visit(Block block);
        void Visit(EvalExp eval);
        void Visit(Return ret);
        void Visit(LoadStmt load);
    }

    abstract class Exp : TreeNode
    {
        public Type Type; // Initialized by TypecheckVisitor (and used by CecilVisitor)
    }

    abstract class Stmt : TreeNode {}

    abstract class BinaryOp : Exp
    {
        public readonly Exp Left;
        readonly Exp _right;

        protected BinaryOp(Exp left, Exp right)
        {
            Left = left;
            _right = right;
        }

        public Exp Right
        {
            get { return _right; }
        }
    }

    class Sum : BinaryOp
    {
        public Sum(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Subtraction : BinaryOp
    {
        public Subtraction(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Product : BinaryOp
    {
        public Product(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Division : BinaryOp
    {
        public Division(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Remainder : BinaryOp
    {
        public Remainder(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Power : BinaryOp
    {
        public Power(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Max : BinaryOp
    {
        public Max(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Min : BinaryOp
    {
        public Min(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class And : BinaryOp
    {
        public And(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Or : BinaryOp
    {
        public Or(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Equal : BinaryOp
    {
        public Equal(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class LessEqual : BinaryOp
    {
        public LessEqual(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class LessThan : BinaryOp
    {
        public LessThan(Exp left, Exp right) : base(left, right) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Dot : Exp
    {
        public readonly Exp Left;
        public readonly string Right;

        public Dot(Exp left, string right)
        {
            Left = left;
            Right = right;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    abstract class UnaryOperator : Exp
    {
        internal readonly Exp Operand;

        protected UnaryOperator(Exp operand)
        {
            Operand = operand;
        }
    }

    sealed class Minus : UnaryOperator
    {
        public Minus(Exp operand) : base(operand) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Factorial : UnaryOperator
    {
        public Factorial(Exp operand) : base(operand) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Not : UnaryOperator
    {
        public Not(Exp operand) : base(operand) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Id : Exp
    {
        public StaticEnvBase.VarInfo Var;
        readonly string _name;

        public Id(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class BoolLiteral : Exp
    {
        readonly bool _b;

        public BoolLiteral(bool b)
        {
            _b = b;
        }

        public bool Value
        {
            get { return _b; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class IntLiteral : Exp
    {
        readonly int _i;

        public IntLiteral(int i)
        {
            _i = i;
        }

        public int Value
        {
            get { return _i; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class StructValue : Exp
    {
        readonly IList<Exp> _values = new List<Exp>();
        public string Name;
        public FunctionDecl.VarInfo Temp; // Assigned by type checker.

        public ReadOnlyCollection<Exp> Values
        {
            get { return new ReadOnlyCollection<Exp>(_values); }
        }

        public void AddValue(Exp value)
        {
            _values.Add(value);
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class FunctionCall : Exp
    {
        readonly IList<Exp> _args = new List<Exp>(); 
        public string FunctionName;
        public FunctionDecl Function;

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ReadOnlyCollection<Exp> Arguments
        {
            get { return new ReadOnlyCollection<Exp>(_args); }
        }

        public void AddArgument(Exp arg)
        {
            _args.Add(arg);
        }
    }

    sealed class Print : Stmt
    {
        readonly Exp _exp;

        public Print(Exp exp)
        {
            _exp = exp;
        }

        public Exp Exp
        {
            get { return _exp; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class StructDecl : Stmt
    {
        readonly IList<Tuple<string, string>> _fields = new List<Tuple<string, string>>();
        public StructType Type;

        public ReadOnlyCollection<Tuple<string, string>> Fields
        {
            get { return new ReadOnlyCollection<Tuple<string, string>>(_fields); }
        } 

        public string Name { get; set; }

        public void AddField(string name, string type)
        {
            Raise<ArgumentException>.If(_fields.Any(f => f.Item1 == name));
            _fields.Add(Tuple.Create(name, type));
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class FunctionDecl : Stmt
    {
        readonly IList<ParamInfo> _params = new List<ParamInfo>();
        readonly IList<VarInfo> _variables = new List<VarInfo>();
        public string Name;
        public string ReturnTypeName;
        public Type ReturnType;
        public Block Body;
        public MethodReference Reference;
        public bool RequiresExplicitReturn;

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ReadOnlyCollection<ParamInfo> Params
        {
            get { return new ReadOnlyCollection<ParamInfo>(_params); }
        }

        public ReadOnlyCollection<VarInfo> Variables
        {
            get { return new ReadOnlyCollection<VarInfo>(_variables); }
        }

        public ParamInfo AddParam(string name, string typeName)
        {
            Raise<ArgumentException>.If(_params.Any(p => p.Name == name));
            var paramInfo = new ParamInfo(name, typeName);
            _params.Add(paramInfo);
            return paramInfo;
        }

        public VarInfo AddVariable(string name, Type type)
        {
            Raise<ArgumentException>.If(_variables.Any(v => v.Name == name));
            var varInfo = new VarInfo(name, type);
            _variables.Add(varInfo);
            return varInfo;
        }

        public sealed class ParamInfo
        {
            public readonly string Name;
            public readonly string TypeName;
            public Type Type;
            public ParameterDefinition Reference;

            public ParamInfo(string name, string typeName)
            {
                Name = name;
                TypeName = typeName;
            }
        }

        public sealed class VarInfo
        {
            public readonly string Name;
            public readonly Type Type;
            public VariableDefinition Reference;

            public VarInfo(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }
    }

    sealed class Block : Stmt
    {
        readonly List<Stmt> _statements;

        public Block(List<Stmt> statements)
        {
            _statements = statements;
        }

        public IEnumerable<Stmt> Statements
        {
            get { return _statements; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class EvalExp : Stmt
    {
        public readonly Exp Exp;

        public EvalExp(Exp exp)
        {
            Exp = exp;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Assignment : Stmt
    {
        public StaticEnvBase.VarInfo Var;
        public readonly Exp Exp;
        public readonly string VarName;
        public readonly Exp LoadExp;

        public Assignment(string varName, Exp exp, Exp loadExp)
        {
            VarName = varName;
            Exp = exp;
            LoadExp = loadExp;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class If : Stmt
    {
        internal readonly Stmt Body;
        internal readonly Exp Guard;

        public If(Exp guard, Stmt body)
        {
            Guard = guard;
            Body = body;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class While : Stmt
    {
        internal readonly Stmt Body;
        internal readonly Exp Guard;

        public While(Exp guard, Stmt body)
        {
            Guard = guard;
            Body = body;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class Return : Stmt
    {
        public readonly Exp ReturnExp;

        public Return(Exp retExp = null)
        {
            ReturnExp = retExp;
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class LoadStmt : Stmt
    {
        public readonly string Assembly;

        public LoadStmt(string assembly)
        {
            Debug.Assert(!string.IsNullOrEmpty(assembly));
            Assembly = assembly + ".exe";
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}