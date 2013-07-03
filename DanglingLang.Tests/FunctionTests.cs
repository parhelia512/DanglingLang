namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class FunctionTests : TestBase
    {
        [Test]
        public void SimpleProcedure1()
        {
            AddLine("void printFive() {");
            AddLine("    print(5)      ");
            AddLine("}                 ");
            AddLine("printFive()      ");
            Execute(new[] {"5"});
        }

        [Test]
        public void SimpleProcedure2()
        {
            AddLine("void myPrint(int x) {");
            AddLine("    print(x)        ");
            AddLine("}                   ");
            AddLine("myPrint(5)          ");
            AddLine("x = 10              ");
            AddLine("myPrint(x)          ");
            Execute(new[] {"5", "10"});
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
        public void RecursiveFunction()
        {
            AddLine("int recSum(int x) {       ");
            AddLine("    if (x == 0) return 0  ");
            AddLine("    return x + recSum(x-1)");
            AddLine("}                         ");
            AddLine("x = recSum(3)             ");
            AddLine("print(recSum(0))          ");
            AddLine("print(x)                  ");
            AddLine("print(x+recSum(1))        ");
            Execute(new[] {"0", "6", "7"});
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

        [Test]
        [ExpectedException(typeof(ParsingException))]
        public void NestedProcedures()
        {
            AddLine("void printFive() {");
            AddLine("    void wrong() {");
            AddLine("        print(1)  ");
            AddLine("    }             ");
            AddLine("    wrong()       ");
            AddLine("}                 ");
            AddLine("printFive()       ");
            TypeCheck();
        }
    }
}
