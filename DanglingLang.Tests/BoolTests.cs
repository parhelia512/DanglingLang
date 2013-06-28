namespace DanglingLang.Tests
{
    using NUnit.Framework;

    public sealed class BoolTests : TestBase
    {
        [Test]
        public void TrueFalse()
        {
            AddLine("print(false)");
            AddLine("print(true)");
            AddLine("print(~false)");
            AddLine("print(~true)");
            Execute(new[] {"False", "True", "True", "False"});
        }

        [Test]
        public void TrueAndFalse()
        {
            AddLine("print(false && false)");
            AddLine("print(true && true)");
            AddLine("print((~false) && false)");
            AddLine("print((~true) && true)");
            Execute(new[] {"False", "True", "False", "False"});
        }

        [Test]
        public void TrueOrFalse()
        {
            AddLine("print(false || false)");
            AddLine("print(true || true)");
            AddLine("print((~false) || false)");
            AddLine("print((~true) || true)");
            AddLine("print((~true) || (~true))");
            Execute(new[] {"False", "True", "True", "True", "False"});
        }

        [Test]
        public void NumberEquality()
        {
            AddLine("print(1 == 1)");
            AddLine("print(0 == 1)");
            AddLine("print((5-1) == (1*4))");
            AddLine("print((5-5) == (4-3))");
            Execute(new[] {"True", "False", "True", "False"});
        }

        [Test]
        public void NumberComparison1()
        {
            AddLine("print(1 < 1)");
            AddLine("print(1 <= 1)");
            AddLine("print(0 <= 1)");
            AddLine("print((5-1) < (1*4))");
            AddLine("print((5-1) <= (1*4))");
            AddLine("print((5-5) <= (4-3))");
            Execute(new[] {"False", "True", "True", "False", "True", "True"});
        }

        [Test]
        public void NumberComparison2()
        {
            AddLine("i = 1");
            AddLine("j = 1");
            AddLine("print(i < j)");
            AddLine("print(i <= j)");
            AddLine("i = 0");
            AddLine("print(i <= j)");
            Execute(new[] {"False", "True", "True"});
        }
    }
}