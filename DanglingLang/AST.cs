namespace DanglingLang
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Thrower;

    abstract class TreeNode
    {
        public abstract void Accept(ITreeNodeVisitor visitor);

        public override string ToString()
        {
            var pv = new ToStringVisitor();
            Accept(pv);
            return pv.Result;
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
        void Visit(Id id);
        void Visit(Print print);
        void Visit(StructDecl structDecl);
        void Visit(EvalExp eval);
        void Visit(Assignment asg);
        void Visit(If ifs);
        void Visit(While whiles);
        void Visit(Block block);
        void Visit(Prog prog);
    }

    abstract class Exp : TreeNode
    {
        internal Type Type { get; set; } // Initialized by TypecheckVisitor (and used by CecilVisitor)
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
        readonly string _name;

        public Id(string name)
        {
            _name = name;
        }

        internal Variable Var { get; set; }

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
        public Variable Temp; // Assigned by type checker.

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

    class Print : Stmt
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
        readonly LinkedList<Tuple<string, string>> _fields = new LinkedList<Tuple<string, string>>();
        public StructType Type;

        public IEnumerable<Tuple<string, string>> Fields
        {
            get { return _fields; }
        } 

        public string Name { get; set; }

        public void AddField(string name, string type)
        {
            Raise<ArgumentException>.If(_fields.Any(f => f.Item1 == name && f.Item2 == type));
            _fields.AddLast(Tuple.Create(name, type));
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class EvalExp : Stmt
    {
        readonly Exp _exp;

        public EvalExp(Exp exp)
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

    class Prog : TreeNode
    {
        internal readonly List<Variable> Variables = new List<Variable>();
        // Initialized by TypecheckVisitor (and used by GenerateLlvmVisitor)

        readonly List<Stmt> _statements;

        public Prog(List<Stmt> statements)
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

    class Block : Stmt
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

    sealed class Assignment : Stmt
    {
        readonly Exp _exp;
        readonly string _varName;

        public Assignment(string varName, Exp exp)
        {
            _varName = varName;
            _exp = exp;
        }

        internal Variable Var { get; set; }

        public string VarName
        {
            get { return _varName; }
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
}