namespace DanglingLang.Visitors
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    sealed class CecilVisitor : ITreeNodeVisitor
    {
        readonly AssemblyDefinition _assembly;
        readonly ModuleDefinition _module;
        readonly MethodReference _objCtor;
        readonly MethodReference _fact;
        readonly MethodReference _max;
        readonly MethodReference _min;
        readonly MethodReference _pow;
        readonly MethodReference _printBool;
        readonly MethodReference _printInt;
        readonly MethodDefinition _main;
        readonly TypeDefinition _userFunctions;
        MethodBody _body;
        ICollection<Instruction> _instructions;

        public CecilVisitor(AssemblyDefinition assembly, ModuleDefinition module)
        {
            _assembly = assembly;
            _module = module;

            var program = _module.Types.First(t => t.Name == "Program");
            _main = program.Methods.First(m => m.Name == "Main");

            _body = _main.Body;
            _instructions = _body.Instructions;
            _instructions.Clear();

            _objCtor = _module.Import(typeof(object).GetConstructor(new System.Type[0]));

            var systemFunctions = _module.Types.First(t => t.Name == "SystemFunctions");
            // Math methods
            _fact = systemFunctions.Methods.First(m => m.Name == "Fact");
            _max = systemFunctions.Methods.First(m => m.Name == "Max");
            _min = systemFunctions.Methods.First(m => m.Name == "Min");
            _pow = systemFunctions.Methods.First(m => m.Name == "Pow");
            // Console methods
            _printBool = systemFunctions.Methods.First(m => m.Name == "PrintBool");
            _printInt = systemFunctions.Methods.First(m => m.Name == "PrintInt");
            
            _userFunctions = _module.Types.First(t => t.Name == "UserFunctions");
        }

        public void Write(string outputPrefix)
        {
            _assembly.Name.Name = outputPrefix;
            _module.Name = _module.Name.Replace("DanglingLang.Runner", outputPrefix);
            // Changes the namespace root of all the main module types.
            foreach(var typeDef in _module.Types) {
                typeDef.Namespace = typeDef.Namespace.Replace("DanglingLang.Runner", outputPrefix);
            }

            _assembly.Write(outputPrefix + ".exe");
        }

        public void Visit(Sum sum)
        {
            Visit(sum, OpCodes.Add_Ovf); // With overflow check
        }

        public void Visit(Subtraction sub)
        {
            Visit(sub, OpCodes.Sub_Ovf); // With overflow check
        }

        public void Visit(Product prod)
        {
            Visit(prod, OpCodes.Mul_Ovf); // With overflow check
        }

        public void Visit(Division div)
        {
            Visit(div, OpCodes.Div);
        }

        public void Visit(Remainder rem)
        {
            Visit(rem, OpCodes.Rem);
        }

        public void Visit(Minus min)
        {
            _instructions.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            min.Operand.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Sub_Ovf));
        }

        public void Visit(Factorial fact)
        {
            fact.Operand.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Call, _fact));
        }

        public void Visit(And and)
        {
            // "And" operator must be short circuited.
            and.Left.Accept(this);
            var br = Instruction.Create(OpCodes.Ldc_I4_0);
            _instructions.Add(Instruction.Create(OpCodes.Brfalse_S, br));
            and.Right.Accept(this);
            var end = Instruction.Create(OpCodes.Nop);
            _instructions.Add(Instruction.Create(OpCodes.Br, end));
            _instructions.Add(br);
            _instructions.Add(end);
        }

        public void Visit(Or or)
        {     
            // "Or" operator must be short circuited.
            or.Left.Accept(this);
            var br = Instruction.Create(OpCodes.Ldc_I4_1);
            _instructions.Add(Instruction.Create(OpCodes.Brtrue_S, br));
            or.Right.Accept(this);
            var end = Instruction.Create(OpCodes.Nop);
            _instructions.Add(Instruction.Create(OpCodes.Br, end));
            _instructions.Add(br);
            _instructions.Add(end);
        }

        public void Visit(Not not)
        {
            _instructions.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            not.Operand.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Sub));
        }

        public void Visit(Equal eq)
        {
            var leftType = eq.Left.Type as StructType;
            if (leftType != null) {
                eq.Left.Accept(this);
                eq.Right.Accept(this);
                _instructions.Add(Instruction.Create(OpCodes.Callvirt, leftType.TypeEquals));
            } else {
                Visit(eq, OpCodes.Ceq);
            }
        }

        public void Visit(LessEqual leq)
        {
            leq.Left.Accept(this);
            leq.Right.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Cgt));
            _instructions.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            _instructions.Add(Instruction.Create(OpCodes.Ceq));
        }

        public void Visit(LessThan lt)
        {
            Visit(lt, OpCodes.Clt);
        }

        public void Visit(Dot dot)
        {
            dot.Left.Accept(this);
            var st = dot.Left.Type as StructType;
            Debug.Assert(st != null); // To keep ReSharper quiet :)
            _instructions.Add(Instruction.Create(OpCodes.Ldfld, st.GetField(dot.Right).Reference));
        }

        public void Visit(Max max)
        {
            Visit(max, _max);
        }

        public void Visit(Min min)
        {
            Visit(min, _min);
        }

        public void Visit(Power pow)
        {
            Visit(pow, _pow);
        }

        public void Visit(BoolLiteral bl)
        {
            var opCode = bl.Value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0;
            _instructions.Add(Instruction.Create(opCode));
        }

        public void Visit(IntLiteral il)
        {
            _instructions.Add(Instruction.Create(OpCodes.Ldc_I4, il.Value));
        }

        public void Visit(StructValue sv)
        {
            var st = sv.Type as StructType;
            Debug.Assert(st != null);
            foreach (var v in sv.Values) {
                v.Accept(this);
            }
            _instructions.Add(Instruction.Create(OpCodes.Newobj, st.Ctor));
        }

        public void Visit(FunctionCall fc)
        {
            foreach (var a in fc.Arguments) {
                a.Accept(this);
            }
            _instructions.Add(Instruction.Create(OpCodes.Call, fc.Function.Reference));
        }

        public void Visit(Id id)
        {
            if (id.Var.IsParam) {
                var paramInfo = id.Var.Info as FunctionDecl.ParamInfo;
                Debug.Assert(paramInfo != null);
                _instructions.Add(Instruction.Create(OpCodes.Ldarg, paramInfo.Reference));
            } else {
                var varInfo = id.Var.Info as FunctionDecl.VarInfo;
                Debug.Assert(varInfo != null);
                _instructions.Add(Instruction.Create(OpCodes.Ldloc, varInfo.Reference));
            }
        }

        public void Visit(Print print)
        {
            print.Exp.Accept(this);
            var printFn = print.Exp.Type.Name == "int" ? _printInt : _printBool;
            _instructions.Add(Instruction.Create(OpCodes.Call, printFn));
        }

        public void Visit(StructDecl structDecl)
        {
            // Each field is added to the struct type...
            const FieldAttributes fieldAttr = FieldAttributes.Public;
            var typeDef = structDecl.Type.Reference as TypeDefinition;
            Debug.Assert(typeDef != null);
            foreach (var f in structDecl.Type.Fields) {
                var fieldDef = new FieldDefinition(f.Name, fieldAttr, f.Type.Reference);
                typeDef.Fields.Add(fieldDef);
                f.Reference = fieldDef;
            }
            
            // We add a proper constructor to the new type. We first create
            // a parameter for each field in the struct.
            var structFields = structDecl.Type.Fields;
            var parameters = new ParameterDefinition[structFields.Count];
            for (var i = 0; i < structFields.Count; ++i) {             
                var pName = "_" + structFields[i].Name;
                const ParameterAttributes pAttr = ParameterAttributes.None;
                var pType = structFields[i].Type.Reference;
                parameters[i] = new ParameterDefinition(pName, pAttr, pType);
            }
            // Then, we create the constructor itself...
            const MethodAttributes ctorAttr =
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;
            var ctor = new MethodDefinition(".ctor", ctorAttr, _module.TypeSystem.Void);
            // And we add the parameters we created before.
            foreach (var p in parameters) {
                ctor.Parameters.Add(p);
            }
            // Then, we build a constructor so that each field receives
            // its new value from the corresponding parameter.
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0)); // This
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Call, _objCtor));
            for (var i = 0; i < parameters.Length; ++i) {
                ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0)); // This
                ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg, parameters[i]));
                ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Stfld, structFields[i].Reference));
            }
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
            // Constructor is added to the new type.
            typeDef.Methods.Add(ctor);
            structDecl.Type.Ctor = ctor;  

            // After that, we have to follow a similar procedure
            // to declare a proper "Equals" method for the new type.
            // As before, we first create the parameter.
            var eqParam = new ParameterDefinition("other", ParameterAttributes.None, typeDef);
            // Then, we create the method itself...
            const MethodAttributes eqAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
            var equals = new MethodDefinition("MyEquals", eqAttr, _module.TypeSystem.Boolean);
            // And we add the parameters we created before.
            equals.Parameters.Add(eqParam);
            // Then, we build a method so that each field is compared
            // against value from the corresponding other field.
            var nop = Instruction.Create(OpCodes.Nop);
            var ret = Instruction.Create(OpCodes.Ret);
            foreach (var f in structFields) {
                equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0)); // This
                equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldfld, f.Reference));
                equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_1)); // Other
                equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldfld, f.Reference));
                if (f.Type is StructType) {
                    var fieldEq = (f.Type as StructType).TypeEquals;
                    equals.Body.Instructions.Add(Instruction.Create(OpCodes.Call, fieldEq));
                } else {
                    equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ceq));
                }
                equals.Body.Instructions.Add(Instruction.Create(OpCodes.Brfalse, nop));
            }
            equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4_1));
            equals.Body.Instructions.Add(Instruction.Create(OpCodes.Br, ret));
            equals.Body.Instructions.Add(nop);
            equals.Body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4_0));
            equals.Body.Instructions.Add(ret);
            // Equals is added to the new type.
            typeDef.Methods.Add(equals);
            structDecl.Type.TypeEquals = equals; 
       
            // New type is then added to the assembly.
            _module.Types.Add(typeDef);
        }

        public void Visit(FunctionDecl funcDecl)
        {
            const MethodAttributes funcAttr =
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig;

            MethodDefinition func;
            if (funcDecl.Name == "$Main") {
                func = _main;
            } else {
                func = new MethodDefinition(funcDecl.Name, funcAttr, funcDecl.ReturnType.Reference);
                _userFunctions.Methods.Add(func);
                funcDecl.Reference = func;
            }

            foreach (var p in funcDecl.Params) {
                const ParameterAttributes paramAttr = ParameterAttributes.None;
                var paramDef = new ParameterDefinition(p.Name, paramAttr, p.Type.Reference);
                func.Parameters.Add(paramDef);
                p.Reference = paramDef;
            }

            var oldBody = _body;
            var oldInstructions = _instructions;

            _body = func.Body;
            _instructions = _body.Instructions;
         
            foreach (var @var in funcDecl.Variables) {
                var varDef = new VariableDefinition(@var.Name, @var.Type.Reference);
                _body.Variables.Add(varDef);
                @var.Reference = varDef;
            }

            funcDecl.Body.Accept(this);
            if (funcDecl.RequiresExplicitReturn) {
                _instructions.Add(Instruction.Create(OpCodes.Ret));
            }

            _body = oldBody;
            _instructions = oldInstructions;
        }

        public void Visit(Assignment asg)
        {
            asg.Exp.Accept(this);
            if (asg.Var.IsParam) {
                var paramInfo = asg.Var.Info as FunctionDecl.ParamInfo;
                Debug.Assert(paramInfo != null);
                _instructions.Add(Instruction.Create(OpCodes.Starg, paramInfo.Reference));
            } else {
                var varInfo = asg.Var.Info as FunctionDecl.VarInfo;
                Debug.Assert(varInfo != null);
                _instructions.Add(Instruction.Create(OpCodes.Stloc, varInfo.Reference));
            }
        }

        public void Visit(If @if)
        {
            @if.Guard.Accept(this);
            var end = Instruction.Create(OpCodes.Nop);
            _instructions.Add(Instruction.Create(OpCodes.Brfalse, end));
            @if.Body.Accept(this);
            _instructions.Add(end);
        }

        public void Visit(While @while)
        {
            var guard = Instruction.Create(OpCodes.Nop);
            _instructions.Add(guard);
            @while.Guard.Accept(this);
            var end = Instruction.Create(OpCodes.Nop);
            _instructions.Add(Instruction.Create(OpCodes.Brfalse, end));
            @while.Body.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Br, guard));
            _instructions.Add(end);
        }

        public void Visit(Block block)
        {
            foreach (var stmt in block.Statements) {
                stmt.Accept(this);
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
            }
            _instructions.Add(Instruction.Create(OpCodes.Ret));
        }

        public void Visit(LoadStmt load)
        {
            // Nothing to do here...
        }

        void Visit(BinaryOp binaryOp, OpCode opCode)
        {
            binaryOp.Left.Accept(this);
            binaryOp.Right.Accept(this);
            _instructions.Add(Instruction.Create(opCode));
        }

        void Visit(BinaryOp binaryOp, MethodReference method)
        {
            binaryOp.Left.Accept(this);
            binaryOp.Right.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Call, method));
        }
    }
}