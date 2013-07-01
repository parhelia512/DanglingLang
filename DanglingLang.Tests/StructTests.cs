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
            AddLine("print(t1.h)");
            AddLine("print(t1.m)");
            AddLine("print(t1.s)");
            AddLine("t1 = struct time {3, 6, 9}");
            AddLine("print(t1.h)");
            AddLine("print(t1.m)");
            AddLine("print(t1.s)");
            AddLine("t2 = t1");
            AddLine("print(t2.h)");
            AddLine("print(t2.m)");
            AddLine("print(t2.s)");
            AddLine("x = t1.h");
            AddLine("print(x)");
            Execute(new[] {"12", "10", "15", "3", "6", "9", "3", "6", "9", "3"});
        }

        [Test]
        public void SimpleStruct_WithExps()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("t1 = struct time {12*2, 10-5, 15^2}");
            AddLine("print(t1.h)");
            AddLine("print(t1.m)");
            AddLine("print(t1.s)");
            AddLine("t1 = struct time {3+t1.h, 6*t1.m, 9/t1.s}");
            AddLine("print(t1.h)");
            AddLine("print(t1.m)");
            AddLine("print(t1.s)");
            AddLine("t2 = t1");
            AddLine("print(t2.h)");
            AddLine("print(t2.m)");
            AddLine("print(t2.s)");
            AddLine("x = t1.h");
            AddLine("print(x)");
            Execute(new[] {"24", "5", "225", "27", "30", "0", "27", "30", "0", "27"});
        }

        [Test]
        public void NestedStruct()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("struct twoTime {struct time t1; struct time t2;}");
            AddLine("t1 = struct time {12, 10, 15}");
            AddLine("t2 = struct time {16, 22, 35}");
            AddLine("t3 = struct twoTime {t1, t2}");
            AddLine("print(t3.t1.h)");
            AddLine("print(t3.t1.m)");
            AddLine("print(t3.t1.s)");
            AddLine("print(t3.t2.h)");
            AddLine("print(t3.t2.m)");
            AddLine("print(t3.t2.s)");
            AddLine("t3 = struct twoTime {struct time {16, 22, 35}, struct time {12, 10, 15}}");
            AddLine("print(t3.t1.h)");
            AddLine("print(t3.t1.m)");
            AddLine("print(t3.t1.s)");
            AddLine("print(t3.t2.h)");
            AddLine("print(t3.t2.m)");
            AddLine("print(t3.t2.s)");
            Execute(new[] {"12", "10", "15", "16", "22", "35", "16", "22", "35", "12", "10", "15"});
        }

        [Test]
        public void SlidesExample()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("t1 = struct time {12, 10, 15}");
            AddLine("print(t1.s)");
            AddLine("struct date {int y; int m; int d; struct time t; bool f;}");
            AddLine("d1 = struct date {2013, 04, 27, struct time{1, 2, 3}, false}");
            AddLine("print(d1.t.s)");
            AddLine("print(d1.f)");
            AddLine("t1 = d1.t");
            AddLine("print(t1.s)");
            AddLine("t1 = struct time {12, 59, 59}");
            AddLine("print(t1.h)");
            Execute(new[] {"15", "3", "False", "3", "12"});
        }
    }
}