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
    }
}
