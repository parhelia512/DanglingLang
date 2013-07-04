namespace DanglingLang.Visitors
{
    using Thrower;

    sealed class ReturnCheckVisitor : ITreeNodeVisitor
    {
        bool _foundReturn;

        public void Visit(Sum sum)
        {
            // Nothing to do here...
        }

        public void Visit(Subtraction sub)
        {
            // Nothing to do here...
        }

        public void Visit(Product prod)
        {
            // Nothing to do here...
        }

        public void Visit(Division div)
        {
            // Nothing to do here...
        }

        public void Visit(Remainder rem)
        {
            // Nothing to do here...
        }

        public void Visit(Minus min)
        {
            // Nothing to do here...
        }

        public void Visit(Factorial fact)
        {
            // Nothing to do here...
        }

        public void Visit(And and)
        {
            // Nothing to do here...
        }

        public void Visit(Or or)
        {
            // Nothing to do here...
        }

        public void Visit(Not not)
        {
            // Nothing to do here...
        }

        public void Visit(Equal eq)
        {
            // Nothing to do here...
        }

        public void Visit(LessEqual leq)
        {
            // Nothing to do here...
        }

        public void Visit(LessThan lt)
        {
            // Nothing to do here...
        }

        public void Visit(Dot dot)
        {
            // Nothing to do here...
        }

        public void Visit(Max max)
        {
            // Nothing to do here...
        }

        public void Visit(Min min)
        {
            // Nothing to do here...
        }

        public void Visit(Power pow)
        {
            // Nothing to do here...
        }

        public void Visit(BoolLiteral bl)
        {
            // Nothing to do here...
        }

        public void Visit(IntLiteral il)
        {
            // Nothing to do here...
        }

        public void Visit(StructValue sv)
        {
            // Nothing to do here...
        }

        public void Visit(FunctionCall fc)
        {
            // Nothing to do here...
        }

        public void Visit(Id id)
        {
            // Nothing to do here...
        }

        public void Visit(Print print)
        {
            // Nothing to do here...
        }

        public void Visit(StructDecl structDecl)
        {
            // Nothing to do here...
        }

        public void Visit(FunctionDecl funcDecl)
        {
            var oldRet = _foundReturn;
            _foundReturn = false;
            funcDecl.Body.Accept(this);
            Raise<ReturnCheckException>.If(!_foundReturn && funcDecl.ReturnTypeName != "void");
            _foundReturn = oldRet;
        }

        public void Visit(Assignment asg)
        {
            // Nothing to do here...
        }

        public void Visit(If ifs)
        {
            var oldRet = _foundReturn;
            _foundReturn = false;
            ifs.Body.Accept(this);
            _foundReturn = oldRet;
        }

        public void Visit(While whiles)
        {
            var oldRet = _foundReturn;
            _foundReturn = false;
            whiles.Body.Accept(this);
            _foundReturn = oldRet;
        }

        public void Visit(Block block)
        {
            foreach (var s in block.Statements) {     
                Raise<ReturnCheckException>.If(_foundReturn, "There must be no stmts after return");
                s.Accept(this);
            }
        }

        public void Visit(EvalExp eval)
        {
            // Nothing to do here...
        }

        public void Visit(Return ret)
        {
            Raise<ReturnCheckException>.If(_foundReturn, "Two or more returns in same block");
            _foundReturn = true;
        }
    }
}
