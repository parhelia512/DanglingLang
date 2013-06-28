namespace DanglingLang
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    sealed class CecilVisitor : ITreeNodeVisitor
    {
        readonly IDictionary<string, VariableDefinition> _varDefs = new Dictionary<string, VariableDefinition>(); 
        readonly AssemblyDefinition _assembly;
        readonly ModuleDefinition _module;
        readonly MethodReference _fact;
        readonly MethodReference _max;
        readonly MethodReference _min;
        readonly MethodReference _pow;
        readonly MethodReference _pause;
        readonly MethodReference _printBool;
        readonly MethodReference _printInt;
        readonly TypeReference _intType;
        MethodBody _body;
        ICollection<Instruction> _instructions;

        public CecilVisitor()
        {
            _assembly = AssemblyDefinition.ReadAssembly("DanglingLang.Runner.exe");
            _module = _assembly.MainModule;

            _intType = _module.Import(typeof(int));

            var program = _module.Types.First(t => t.Name == "Program");
            var main = program.Methods.First(m => m.Name == "Main");

            _body = main.Body;
            _instructions = _body.Instructions;
            _instructions.Clear();

            var systemFunctions = _module.Types.First(t => t.Name == "SystemFunctions");
            // Math methods
            _fact = systemFunctions.Methods.First(m => m.Name == "Fact");
            _max = systemFunctions.Methods.First(m => m.Name == "Max");
            _min = systemFunctions.Methods.First(m => m.Name == "Min");
            _pow = systemFunctions.Methods.First(m => m.Name == "Pow");
            // Console methods
            _pause = systemFunctions.Methods.First(m => m.Name == "Pause");
            _printBool = systemFunctions.Methods.First(m => m.Name == "PrintBool");
            _printInt = systemFunctions.Methods.First(m => m.Name == "PrintInt");     
        }

        public void Write(string outputName)
        {
            _instructions.Add(Instruction.Create(OpCodes.Call, _pause));
            _instructions.Add(Instruction.Create(OpCodes.Ret));
            _assembly.Write(outputName);
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
            _instructions.Add(Instruction.Create(OpCodes.Brfalse, br));
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
            _instructions.Add(Instruction.Create(OpCodes.Brtrue, br));
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
            Visit(eq, OpCodes.Ceq);
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

        public void Visit(Id id)
        {
            _instructions.Add(Instruction.Create(OpCodes.Ldloc, _varDefs[id.Name]));
        }

        public void Visit(Print print)
        {
            print.Exp.Accept(this);
            var printFn = print.Exp.Type == Type.Int ? _printInt : _printBool;
            _instructions.Add(Instruction.Create(OpCodes.Call, printFn));
        }

        public void Visit(EvalExp eval)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Assignment asg)
        {
            asg.Exp.Accept(this);
            _instructions.Add(Instruction.Create(OpCodes.Stloc, _varDefs[asg.VarName]));
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

        public void Visit(Prog prog)
        {
            foreach (var @var in prog.Vars) {
                var varDef = new VariableDefinition(@var.Name, _intType);
                _varDefs.Add(@var.Name, varDef);
                _body.Variables.Add(varDef);
            }
            foreach (var stmt in prog.Statements) {
                stmt.Accept(this);
            }
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