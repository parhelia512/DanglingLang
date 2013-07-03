namespace DanglingLang
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Mono.Cecil;
    using Thrower;

    [Serializable]
    public sealed class TypeCheckingException : Exception
    {
        public TypeCheckingException() {}
        public TypeCheckingException(string message) : base(message) {}
        public TypeCheckingException(string message, Exception inner) : base(message, inner) {}
    }

    class Type
    {
        public readonly string Name;
        public readonly TypeReference Reference;
        public MethodReference Ctor;

        public Type(string name, TypeReference reference)
        {
            Debug.Assert(name == name.ToLower() && reference != null);
            Name = name;
            Reference = reference;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    sealed class StructType : Type
    {
        readonly IList<FieldInfo> _fields = new List<FieldInfo>();

        public StructType(string name, TypeReference reference) : base(name, reference)
        {
        }

        public ReadOnlyCollection<FieldInfo> Fields
        {
            get { return new ReadOnlyCollection<FieldInfo>(_fields); }
        }

        public void AddField(string name, Type type)
        {
            Raise<TypeCheckingException>.If(_fields.Any(f => f.Name == name));
            _fields.Add(new FieldInfo(name, type));
        }

        public FieldInfo GetField(string name)
        {
            var field = _fields.FirstOrDefault(f => f.Name == name);
            Raise<TypeCheckingException>.IfIsNull(field);
            return field;
        }

        public override string ToString()
        {
            return "struct " + Name;
        }

        public sealed class FieldInfo
        {
            public readonly string Name;
            public readonly Type Type;
            public FieldReference Reference;

            public FieldInfo(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }
    }

    abstract class StaticEnvBase
    {
        public abstract VarInfo GetVariable(string name);
        public abstract bool TryGetVariable(string name, out VarInfo v);
        public abstract void SetVariable(string name, VarInfo v);

        public sealed class VarInfo
        {
            public readonly string Name;
            public readonly Type Type;
            public readonly bool IsParam;
            public readonly object Info;

            public VarInfo(string name, Type type, bool isParam, object info)
            {
                Name = name;
                Type = type;
                IsParam = isParam;
                Debug.Assert(info is FunctionDecl.ParamInfo || info is FunctionDecl.VarInfo);
                Info = info;
            }
        }
    }

    sealed class StaticEnv : StaticEnvBase
    {
        readonly Dictionary<string, VarInfo> _locals = new Dictionary<string, VarInfo>();
        readonly StaticEnvBase _parent;

        public StaticEnv(StaticEnvBase parent)
        {
            Debug.Assert(parent != null);
            _parent = parent;
        }

        public override VarInfo GetVariable(string name)
        {
            VarInfo v;
            return _locals.TryGetValue(name, out v) ? v : _parent.GetVariable(name);
        }

        public override bool TryGetVariable(string name, out VarInfo v)
        {
            return _locals.TryGetValue(name, out v) || _parent.TryGetVariable(name, out v);
        }

        public override void SetVariable(string name, VarInfo v)
        {
            Debug.Assert(!_locals.ContainsKey(name));
            _locals[name] = v;
        }
    }

    sealed class OutmostStaticEnv : StaticEnvBase
    {
        public override VarInfo GetVariable(string name)
        {
            throw new TypeCheckingException(string.Format("Undefined variable {0}", name));
        }

        public override bool TryGetVariable(string name, out VarInfo v)
        {
            v = null;
            return false;
        }

        public override void SetVariable(string name, VarInfo v)
        {
            throw new TypeCheckingException("Internal compiler error");
        }
    }

    sealed class TypecheckVisitor : ITreeNodeVisitor
    {
        readonly IDictionary<string, Type> _types = new Dictionary<string, Type>();
        readonly IDictionary<string, StructType> _structTypes = new Dictionary<string, StructType>();
        readonly IDictionary<string, FunctionDecl> _funcDecls = new Dictionary<string, FunctionDecl>();
        readonly Type _boolType;
        readonly Type _intType;
        readonly Type _voidType;
        int _tempCounter;
        public readonly AssemblyDefinition Assembly;
        public readonly ModuleDefinition Module;
            
        FunctionDecl _currFunc;
        Type _result;
        StaticEnvBase _staticEnv = new StaticEnv(new OutmostStaticEnv());

        public TypecheckVisitor()
        {
            Assembly = AssemblyDefinition.ReadAssembly("DanglingLang.Runner.exe");
            Module = Assembly.MainModule;
            
            _boolType = AddType("bool", Module.TypeSystem.Boolean);
            _intType = AddType("int", Module.TypeSystem.Int32);
            _voidType = AddType("void", Module.TypeSystem.Void);
        }

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
            eq.Type = _result = _boolType;
        }

        public void Visit(LessEqual leq)
        {
            ResultIsBoolAndBothOperandsMustBeInt(leq);
        }

        public void Visit(LessThan lt)
        {
            ResultIsBoolAndBothOperandsMustBeInt(lt);
        }

        public void Visit(Dot dot)
        {
            dot.Left.Accept(this);
            var st = dot.Left.Type as StructType;
            Raise<TypeCheckingException>.IfIsNull(st);
            Debug.Assert(st != null); // To keep ReSharper quiet :)
            dot.Type = _result = st.GetField(dot.Right).Type;
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
            _result = bl.Type = _boolType;
        }

        public void Visit(IntLiteral il)
        {
            _result = il.Type = _intType;
        }

        public void Visit(StructValue sv)
        {
            var st = GetStructType(sv.Name);
            Raise<TypeCheckingException>.IfAreNotEqual(st.Fields.Count, sv.Values.Count);
            for (var i = 0; i < st.Fields.Count; ++i) {
                sv.Values[i].Accept(this);
                Raise<TypeCheckingException>.IfAreNotSame(st.Fields[i].Type, sv.Values[i].Type);
            }  
            sv.Type = _result = st;
            sv.Temp = _currFunc.AddVariable("$" + _tempCounter++, st);
        }

        public void Visit(FunctionCall fc)
        {
            Raise<TypeCheckingException>.If(!_funcDecls.ContainsKey(fc.FunctionName));
            var fd = _funcDecls[fc.FunctionName];
            for (var i = 0; i < fd.Params.Count; ++i) {
                fc.Arguments[i].Accept(this);
                Raise<TypeCheckingException>.IfAreNotSame(fc.Arguments[i].Type, fd.Params[i].Type);
            }
            fc.Function = fd;
            fc.Type = fd.ReturnType;
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
            Raise<TypeCheckingException>.If(print.Exp.Type != _intType && print.Exp.Type != _boolType);
        }

        public void Visit(StructDecl structDecl)
        {
            structDecl.Type = AddStructType(structDecl);
        }

        public void Visit(FunctionDecl funcDecl)
        {
            Raise<TypeCheckingException>.If(_funcDecls.ContainsKey(funcDecl.Name));
            funcDecl.ReturnType = GetType(funcDecl.ReturnTypeName);
            var previousStaticEnv = _staticEnv;
            _staticEnv = new OutmostStaticEnv();
            foreach (var p in funcDecl.Params) {
                p.Type = GetType(p.TypeName);
                Raise<TypeCheckingException>.IfAreSame(p.Type, _voidType);
                var vInfo = new StaticEnvBase.VarInfo(p.Name, p.Type, true, p);
                _staticEnv.SetVariable(p.Name, vInfo);
            }
            var prevFunc = _currFunc;
            _currFunc = funcDecl;
            funcDecl.Body.Accept(this);
            _currFunc = prevFunc;
            _staticEnv = previousStaticEnv;
            _funcDecls.Add(funcDecl.Name, funcDecl);
        }

        public void Visit(Assignment asg)
        {
            asg.Exp.Accept(this);
            var varName = asg.VarName;
            StaticEnvBase.VarInfo varInfo;
            if (_staticEnv.TryGetVariable(varName, out varInfo)) {
                if (!varInfo.Type.Equals(_result)) {
                    throw new TypeCheckingException(string.Format(
                        "Cannot re-assign {0} with a value of different type", varName));
                }
            } else {
                var info = _currFunc.AddVariable(varName, asg.Exp.Type);
                varInfo = new StaticEnvBase.VarInfo(varName, asg.Exp.Type, false, info);
                _staticEnv.SetVariable(varName, varInfo);
            }
            asg.Var = varInfo;
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

        public void Visit(EvalExp eval)
        {
            eval.Exp.Accept(this);
        }

        public void Visit(Return ret)
        {
            if (ret.ReturnExp != null) {
                ret.ReturnExp.Accept(this);
                Raise<TypeCheckingException>.IfAreNotSame(ret.ReturnExp.Type, _currFunc.ReturnType);
            } else {
                Raise<TypeCheckingException>.IfAreNotSame(_voidType, _currFunc.ReturnType);
            }
        }

        Type AddType(string name, TypeReference reference)
        {
            name = name.ToLower();
            Raise<TypeCheckingException>.If(_types.ContainsKey(name), "Type already existing.");
            var type = new Type(name, reference);
            _types.Add(name, type);
            return type;
        }

        StructType AddStructType(StructDecl decl)
        {
            var name = decl.Name.ToLower();
            Raise<ArgumentException>.If(_types.ContainsKey(name));
            
            const string nmsp = "DanglingLang.Runner";
            const TypeAttributes typeAttr = TypeAttributes.Class | TypeAttributes.Sealed;
            var typeDef = new TypeDefinition(nmsp, name, typeAttr) {BaseType = Module.Import(typeof(object))};

            var type = new StructType(name, typeDef);
            foreach (var f in decl.Fields) {
                var fieldType = GetType(f.Item2);
                Raise<TypeCheckingException>.IfAreEqual("void", fieldType.Name);
                type.AddField(f.Item1, fieldType);
            }
            
            _types.Add(name, type);
            _structTypes.Add(name, type);
            return type;
        }

        Type GetType(string name)
        {
            name = name.ToLower();
            Raise<ArgumentException>.If(!_types.ContainsKey(name), "Type does not exist.");
            return _types[name];
        }

        StructType GetStructType(string name)
        {
            name = name.ToLower();
            Raise<ArgumentException>.If(!_structTypes.ContainsKey(name), "Type does not exist.");
            return _structTypes[name];
        }

        Type MustBe(Type t, string msg)
        {
            Raise<TypeCheckingException>.IfAreNotSame(_result, t, msg);
            return t;
        }

        Type MustBeInt(string msg)
        {
            return MustBe(_intType, msg);
        }

        Type MustBeBool(string msg)
        {
            return MustBe(_boolType, msg);
        }

        void ResultIsIntAndBothOperandsMustBeInt(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeInt("The type of the left operand of " + binaryOp + " must be integer");
            binaryOp.Right.Accept(this);
            MustBeInt("The type of the right operand of " + binaryOp + " must be integer");
            binaryOp.Type = _result = _intType;
        }

        void ResultIsBoolAndBothOperandsMustBeBool(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeBool("The type of the left operand of " + binaryOp + " must be bool");
            binaryOp.Right.Accept(this);
            MustBeBool("The type of the right operand of " + binaryOp + " must be bool");
            binaryOp.Type = _result = _boolType;
        }

        void ResultIsBoolAndBothOperandsMustBeInt(BinaryOp binaryOp)
        {
            binaryOp.Left.Accept(this);
            MustBeInt("The type of the left operand of " + binaryOp + " must be integer");
            binaryOp.Right.Accept(this);
            MustBeInt("The type of the right operand of " + binaryOp + " must be integer");
            binaryOp.Type = _result = _boolType;
        }
    }
}