namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class WhileTests : TestBase
    {
        [Test]
        public void FalseCond()
        {
            AddLine("while (false) {");
            AddLine("print(21)");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"42"});
        }

        [Test]
        public void TrueCond_OneTime()
        {
            AddLine("i = 1");
            AddLine("while (i == 1) {");
            AddLine("print(21)");
            AddLine("i = 0");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"21", "42"});
        }

        [Test]
        public void TrueCond_FiveTimes()
        {
            AddLine("i = 0");
            AddLine("while (i < 5) {");
            AddLine("print(i)");
            AddLine("i = i + 1");
            AddLine("}");
            AddLine("print(42)");
            Execute(new[] {"0", "1", "2", "3", "4", "42"});
        }
    }
}