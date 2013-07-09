namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class FunctionTests : TestBase
    {
        [Test]
        [ExpectedException(typeof(ParseException))]
        public void NestedFunctions()
        {
            AddLine("int returnFive() {");
            AddLine("    int wrong() { ");
            AddLine("        return 1  ");
            AddLine("    }             ");
            AddLine("    return wrong()");
            AddLine("}                 ");
            AddLine("x = returnFive()  ");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(ParseException))]
        public void NestedProcedures()
        {
            AddLine("void printFive() {");
            AddLine("    void wrong() {");
            AddLine("        print(1)  ");
            AddLine("    }             ");
            AddLine("    wrong()       ");
            AddLine("}                 ");
            AddLine("printFive()       ");
            CheckCode();
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
        [ExpectedException(typeof(ReturnCheckException))]
        public void Main_TwoReturnStatements()
        {          
            AddLine("x = 10");
            AddLine("return");
            AddLine("print(x)");
            AddLine("return");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_NoReturn()
        {
            AddLine("int returnFive() {   ");
            AddLine("    x = 5 + 5        ");
            AddLine("    print(x)         ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            CheckCode();
        }

        [Test]
        public void SimpleFunction_ReturnInsideBlock()
        {
            AddLine("int returnFive() {   ");
            AddLine("    {return 5}       ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            Execute(new[] {"5", "5", "10"});
        }

        [Test]
        public void SimpleFunction_ReturnInsideIf()
        {
            AddLine("bool isFive(int x) { ");
            AddLine("    if (x == 5) {    ");
            AddLine("        return true  ");
            AddLine("    }                ");
            AddLine("    return false     ");
            AddLine("}                    ");
            AddLine("x = isFive(5)        ");
            AddLine("print(isFive(7))     ");
            AddLine("print(x)             ");
            AddLine("print(x && isFive(1))");
            Execute(new[] {"False", "True", "False"});
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_ReturnOnlyInsideIf()
        {
            AddLine("bool isFive(int x) { ");
            AddLine("    if (x == 5) {    ");
            AddLine("        return true  ");
            AddLine("    }                ");
            AddLine("    if (x == 5) {    ");
            AddLine("        return false ");
            AddLine("    }                ");
            AddLine("}                    ");
            AddLine("x = isFive(5)        ");
            AddLine("print(isFive(7))     ");
            AddLine("print(x)             ");
            AddLine("print(x && isFive(1))");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_StatementsAfterReturn()
        {
            AddLine("int returnFive() {   ");
            AddLine("    return 5         ");
            AddLine("    print(5)         ");
            AddLine("    x = 10           ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            Execute(new[] {"5", "5", "10"});
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_StatementsAfterReturn_ReturnInsideIf()
        {
            AddLine("bool isFive(int x) { ");
            AddLine("    if (x == 5) {    ");
            AddLine("        return true  ");
            AddLine("        print(5)     ");
            AddLine("        print(10)    ");
            AddLine("    }                ");
            AddLine("    return false     ");
            AddLine("}                    ");
            AddLine("x = isFive(5)        ");
            AddLine("print(isFive(7))     ");
            AddLine("print(x)             ");
            AddLine("print(x && isFive(1))");
            Execute(new[] {"False", "True", "False"});
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_TwoReturnStatements()
        {
            AddLine("int returnFive() {   ");
            AddLine("    return 5         ");
            AddLine("    return 10        ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(ReturnCheckException))]
        public void SimpleFunction_TwoReturnStatements_InsideBlock()
        {
            AddLine("int returnFive() {   ");
            AddLine("    return 5         ");
            AddLine("    {return 10}      ");
            AddLine("}                    ");
            AddLine("x = returnFive()     ");
            AddLine("print(returnFive())  ");
            AddLine("print(x)             ");
            AddLine("print(x+returnFive())");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void SimpleFunction_WrongReturnType()
        {
            AddLine("int returnFive() {");
            AddLine("    return true   ");
            AddLine("}                 ");
            AddLine("x = returnFive()  ");
            CheckCode();
        }

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
        [ExpectedException(typeof(TypeCheckException))]
        public void SimpleProcedure_WrongReturnType()
        {
            AddLine("void printFive() {");
            AddLine("    print(5)      ");
            AddLine("    return 5      ");
            AddLine("}                 ");
            AddLine("printFive()      ");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void RedefineFunction()
        {
            AddLine("int returnFive() {     ");
            AddLine("    return 5           ");
            AddLine("}                      ");
            AddLine("int returnFive(int x) {");
            AddLine("    return x*5         ");
            AddLine("}                      ");
            CheckCode();
        }
    }
}