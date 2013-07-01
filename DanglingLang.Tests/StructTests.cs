namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class StructTests : TestBase
    {
        [Test]
        public void SimpleStruct()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("print(21)");
            AddLine("print(42)");
            Execute(new[] {"21", "42"});
        }

        [Test]
        public void NestedStruct()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("struct twoTime {struct time t1; struct time t2;}");
            AddLine("print(21)");
            AddLine("print(42)");
            Execute(new[] {"21", "42"});
        }
    }
}