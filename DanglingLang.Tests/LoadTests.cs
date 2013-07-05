namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class LoadTests : TestBase
    {
        [Test]
        public void SimpleProcedureCall()
        {
            AddLine("load(TestLoad)");
            AddLine("t = struct time {14, 08, 23}");
            AddLine("printTime(t)");
            Execute(new[] {"14", "8", "23"});
        }
        
        [Test]
        public void SimpleFunctionCall()
        {
            AddLine("load(TestLoad)");
            AddLine("t = createTime(14, 08, 23)");
            AddLine("printTime(t)");
            Execute(new[] {"14", "8", "23"});
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void RedefineLoadedType()
        {
            AddLine("load(TestLoad)");
            AddLine("struct time {int i; bool b;}");
            AddLine("t = struct time {1, true}");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void RedefineLoadedFunction()
        {
            AddLine("load(TestLoad)           ");
            AddLine("void printTime(int sec) {");
            AddLine("    print(sec)           ");
            AddLine("}                        ");
            AddLine("printTime(3600)          ");
            CheckCode();
        }
    }
}
