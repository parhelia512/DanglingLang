namespace DanglingLang
{
    using System.Collections.Generic;

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
        void Visit(And and);
        void Visit(Or or);
        void Visit(Not not);
        void Visit(Equal eq);
        void Visit(LessEqual leq);
        void Visit(LessThan lt);
        void Visit(Max max);
        void Visit(Min min);
        void Visit(Power pow);
        void Visit(BoolLiteral bl);
        void Visit(IntLiteral il);
        void Visit(Id id);
        void Visit(Print print);
        void Visit(EvalExp eval);
        void Visit(Assignment asg);
        void Visit(If ifs);
        void Visit(While whiles);
        void Visit(Block block);
        void Visit(Prog prog);
    }

    abstract class Exp : TreeNode
    {
        internal Type Type { get; set; } // Initialized by TypecheckVisitor (and used by GenerateLlvmVisitor)
    }

    abstract class Stmt : TreeNode {}

    abstract class BinaryOp : Exp
    {
        readonly Exp _left;
        readonly Exp _right;

        protected BinaryOp(Exp left, Exp right)
        {
            _left = left;
            _right = right;
        }

        public Exp Left
        {
            get { return _left; }
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

    abstract class UnaryOperator : Exp
    {
        readonly Exp _operand;

        public UnaryOperator(Exp operand)
        {
            _operand = operand;
        }

        public Exp Operand
        {
            get { return _operand; }
        }
    }

    class Not : UnaryOperator
    {
        public Not(Exp operand) : base(operand) {}

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Id : Exp
    {
        readonly string _name;

        public Id(string name)
        {
            _name = name;
        }

        internal LlvmVar Var { get; set; }

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
        internal readonly List<LlvmVar> Vars = new List<LlvmVar>();
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

    class Assignment : Stmt
    {
        readonly Exp _exp;
        readonly string _varName;

        public Assignment(string varName, Exp exp)
        {
            _varName = varName;
            _exp = exp;
        }

        internal LlvmVar Var { get; set; }

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

    class If : Stmt
    {
        readonly Stmt _body;
        readonly Exp _guard;

        public If(Exp guard, Stmt body)
        {
            _guard = guard;
            _body = body;
        }

        public Exp Guard
        {
            get { return _guard; }
        }

        public Stmt Body
        {
            get { return _body; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    sealed class While : Stmt
    {
        readonly Stmt _body;
        readonly Exp _guard;

        public While(Exp guard, Stmt body)
        {
            _guard = guard;
            _body = body;
        }

        public Exp Guard
        {
            get { return _guard; }
        }

        public Stmt Body
        {
            get { return _body; }
        }

        public override void Accept(ITreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}