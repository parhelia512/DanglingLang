namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class MathTests : TestBase
    {
        [Test]
        public void Sum_VariousOps()
        {
            AddLine("print(5+5)");
            AddLine("print(5+5+5+1+1)");
            AddLine("x = 7");
            AddLine("print(x+7)");
            Execute(new[] {"10", "17", "14"});
        }
    }
}
