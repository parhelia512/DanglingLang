namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class StructTests : TestBase
    {
        [Test]
        public void SimpleStruct()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("t1 = struct time {12, 10, 15}");
            AddLine("print(21)");
            AddLine("print(42)");
            Execute(new[] {"21", "42"});
        }

        [Test]
        public void NestedStruct()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("struct twoTime {struct time t1; struct time t2;}");
            AddLine("t1 = struct time {12, 10, 15}");
            AddLine("t2 = struct time {16, 22, 35}");
            AddLine("t3 = struct twoTime {t1, t2}");
            AddLine("t3 = struct twoTime {struct time {16, 22, 35}, struct time {12, 10, 15}}");
            AddLine("print(21)");
            AddLine("print(42)");
            Execute(new[] {"21", "42"});
        }

        public A Boh()
        {
            var a = new A();
            a.x = 5;
            a.y = 10;
            return a;
        }

        public sealed class A
        {
            public int x;
            public int y;
        }
    }
}