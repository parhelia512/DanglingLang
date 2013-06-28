namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class MathTests : TestBase
    {
        [Test]
        public void Max_VariousOps()
        {
            AddLine("print(max(5,5))");
            AddLine("print(max(1,3))");
            AddLine("print(max(10,3))");
            AddLine("x = 7");
            AddLine("print(max(x,7))");
            AddLine("print(max(x,14))");
            AddLine("print(max(8,x))");
            Execute(new[] {"5", "3", "10", "7", "14", "8"});
        }

        [Test]
        public void Min_VariousOps()
        {
            AddLine("print(min(5,5))");
            AddLine("print(min(1,3))");
            AddLine("print(min(10,3))");
            AddLine("x = 7");
            AddLine("print(min(x,7))");
            AddLine("print(min(x,14))");
            AddLine("print(min(8,x))");
            Execute(new[] {"5", "1", "3", "7", "7", "7"});
        }

        [Test]
        public void Sum_VariousOps()
        {
            AddLine("print(5+5)");
            AddLine("print(5+5+5+1+1)");
            AddLine("x = 7");
            AddLine("print(x+7)");
            Execute(new[] {"10", "17", "14"});
        }

        [Test]
        public void Fact_VariousOps()
        {
            AddLine("print(3!)");
            AddLine("print(0!)");
            AddLine("print(1!)");
            AddLine("print(5!)");
            AddLine("print((5^1)!)");
            AddLine("x = 3!");
            AddLine("x = x!");
            AddLine("print(x)");
            Execute(new[] {"6", "1", "1", "120", "120", "720"});
        }

        [Test]
        public void Pow_VariousOps()
        {
            AddLine("print(5^2)");
            AddLine("print((5^1)^(3^1))");
            AddLine("x = (-3)^3");
            AddLine("print(x^1)");
            Execute(new[] {"25", "125", "-27"});
        }

        [Test]
        public void UnaryMinus()
        {
            AddLine("print(-5--5)");
            AddLine("print(-5+5+5+1+(-1))");
            AddLine("x = -7");
            AddLine("print(-x+7)");
            Execute(new[] {"0", "5", "14"});
        }

        [Test]
        public void UnaryPlus()
        {
            AddLine("print(+5++5)");
            AddLine("print(+5+5+5+1+1)");
            AddLine("x = +7");
            AddLine("print(+x++7)");
            Execute(new[] {"10", "17", "14"});
        }
    }
}
