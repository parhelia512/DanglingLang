namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class IfTests : TestBase
    {
        [Test]
        public void FalseCond()
        {
            //var r = new System.Random();
            //var r1 = r.Next(5, 10);
            //var r2 = r.Next(15, 20);
            //var i = r1 <= r2;
            //AddLine(i.ToString());
            AddLine("if (false) {");
            AddLine("print(21)");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"42"});
        }

        [Test]
        public void FalseAndCond()
        {
            AddLine("if (false && false) {");
            AddLine("print(21)");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"42"});
        }

        [Test]
        public void FalseCond2()
        {
            AddLine("if (~true) {");
            AddLine("print(21)");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"42"});
        }

        [Test]
        public void TrueCond()
        {
            AddLine("if (true) {");
            AddLine("print(21)");
            AddLine("}");
            Execute(new[] {"21"});
        }

        [Test]
        public void TrueCond2()
        {
            AddLine("if (~false) {");
            AddLine("print(21)");
            AddLine("}");
            Execute(new[] {"21"});
        }

        [Test]
        public void TrueAndCond()
        {
            AddLine("if (true && true) {");
            AddLine("print(21)");
            AddLine("}");
            Execute(new[] {"21"});
        }
    }
}