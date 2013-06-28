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
    }
}