namespace DanglingLang.Runner
{
    using System;
    using Thrower;

    static class SystemFunctions
    {
        // Math methods

        public static int Fact(int n)
        {  
            Raise<ArgumentOutOfRangeException>.If(n < 0);
            var result = n;
            for (; n > 2; result *= --n) {}
            return result > 1 ? result : 1;
        }

        public static int Max(int x, int y)
        {
            return (x > y) ? x : y;
        }

        public static int Min(int x, int y)
        {
            return (x < y) ? x : y;
        }

        public static int Pow(int b, int e)
        {
            Raise<ArgumentOutOfRangeException>.If(e < 0); 
            return (int) Math.Pow(b, e);
        }

        // Console methods

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        public static void PrintBool(bool b)
        {
            Console.WriteLine(b);
        }

        public static void PrintInt(int i)
        {
            Console.WriteLine(i);
        }

        public static void PrintString(string s)
        {
            Console.WriteLine(s);
        }
    }
}