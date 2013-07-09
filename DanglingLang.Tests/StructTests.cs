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
        public void NestedStructs()
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
        public void NestedStructs_WithExps()
        {
            AddLine("struct time {int h; int m; int s;}");
            AddLine("struct twoTime {struct time t1; struct time t2;}");
            AddLine("t1 = struct time {12*2, 3!, 4!}");
            AddLine("t2 = struct time {16^2, min(3,5), max(4,5!)}");
            AddLine("t3 = struct twoTime {t1, t2}");
            AddLine("print(t3.t1.h)");
            AddLine("print(t3.t1.m)");
            AddLine("print(t3.t1.s)");
            AddLine("print(t3.t2.h)");
            AddLine("print(t3.t2.m)");
            AddLine("print(t3.t2.s)");
            AddLine("t3 = struct twoTime {struct time {16^2, 22/2, 35-5}, struct time {12^2, 10^3, 15^2}}");
            AddLine("print(t3.t1.h)");
            AddLine("print(t3.t1.m)");
            AddLine("print(t3.t1.s)");
            AddLine("print(t3.t2.h)");
            AddLine("print(t3.t2.m)");
            AddLine("print(t3.t2.s)");
            Execute(new[] {"24", "6", "24", "256", "3", "120", "256", "11", "30", "144", "1000", "225"});
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

        [Test]
        public void Equality()
        {
            AddLine("load(TestLoad)");
            AddLine("t1 = struct time {1, 2, 3}");
            AddLine("t2 = struct time {1, 2, 3}");
            AddLine("print(t1 == t2)");
            AddLine("t2 = struct time {1, 2, 4}");
            AddLine("print(t1 == t2)");
            AddLine("print(t1 == struct time {1,2,3})");
            AddLine("print(t1 == struct time {1,2,4})");
            Execute(new[] {"True", "False", "True", "False"});
        }
        
        [Test]
        public void Equality_NestedStructs()
        {
            AddLine("load(TestLoad)");
            AddLine("t1 = struct time {1, 2, 3}");
            AddLine("t2 = struct time {1, 2, 3}");
            AddLine("d1 = struct datum {t1, true}");
            AddLine("d2 = struct datum {t2, true}");
            AddLine("print(d1 == d2)");
            AddLine("d2 = struct datum {t2, false}");
            AddLine("print(d1 == d2)");
            AddLine("t2 = struct time {1, 2, 4}");
            AddLine("d2 = struct datum {t2, true}");
            AddLine("print(d1 == d2)");
            AddLine("print(d1 == struct datum {struct time {1,2,3}, true})");
            AddLine("print(d1 == struct datum {struct time {1,2,3}, false})");
            AddLine("print(d1 == struct datum {struct time {1,2,4}, true})");
            Execute(new[] {"True", "False", "False", "True", "False", "False"});
        }

        [Test]
        public void BooleanFields()
        {
            AddLine("struct bools {bool f1; bool f2; bool f3;}");
            AddLine("f = struct bools {true, ~true, false || true}");
            AddLine("print(f.f1 || f.f2 || f.f3)");
            AddLine("print(f.f1 && f.f2 && f.f3)");
            AddLine("f = struct bools {~f.f1, ~f.f2, f.f1 || f.f2 || f.f3}");
            AddLine("print(f.f1)");
            AddLine("print(f.f2)");
            AddLine("print(f.f3)");
            Execute(new[] {"True", "False", "False", "True", "True"});
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void RedefineStruct()
        {
            AddLine("struct bools {bool f1; bool f2; bool f3;}");
            AddLine("struct bools {int f1; bool f2; struct bools f3;}");
            CheckCode();
        }

        [Test]
        [ExpectedException(typeof(TypeCheckException))]
        public void RecursiveStruct()
        {
            AddLine("struct bools {bool f1; bool f2; struct bools f3;}");
            CheckCode();
        }
    }
}