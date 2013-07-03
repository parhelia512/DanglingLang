namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class FunctionTests : TestBase
    {
        [Test]
        public void SimpleProcedure()
        {
            AddLine("void printFive() {");
            AddLine("    print(5)      ");
            AddLine("}                 ");
            AddLine("printFive()      ");
            Execute(new[] {"5"});
        }
        
        [Test]
        [ExpectedException(typeof(TypeCheckingException))]
        public void SimpleProcedure_WrongReturnType()
        {
            AddLine("void printFive() {");
            AddLine("    print(5)      ");
            AddLine("    return 5      ");
            AddLine("}                 ");
            AddLine("printFive()      ");
            TypeCheck();
        }

        [Test]
        public void SimpleFunction()
        {
            AddLine("int returnFive() {   ");
            AddLine("    return 5         ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            Execute(new[] {"5", "5", "10"});
        }

        [Test]
        [ExpectedException(typeof(TypeCheckingException))]
        public void SimpleFunction_WrongReturnType()
        {
            AddLine("int returnFive() {");
            AddLine("    return true   ");
            AddLine("}                 ");
            AddLine("x = returnFive()  ");
            TypeCheck();
        }

        

        [Test]
        [ExpectedException(typeof(ParsingException))]
        public void NestedFunctions()
        {
            AddLine("int returnFive() {");
            AddLine("    int wrong() { ");
            AddLine("        return 1  ");
            AddLine("    }             ");
            AddLine("    return wrong()");
            AddLine("}                 ");
            AddLine("x = returnFive()  ");
            TypeCheck();
        }

        void main()
        {
            var x = printFive();
            System.Console.Write(x);
        }

        int printFive()
        {
            return 5;
        }
    }
}
