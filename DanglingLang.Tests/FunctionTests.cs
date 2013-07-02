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
            AddLine("print(5)          ");
            Execute(new[] {"5"});
        }
    }
}
