namespace DanglingLang
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Thrower;
    using Tokenizer;
    using Visitors;

    public static class Launcher
    {
        static int Main(string[] args)
        {
            Raise<InvalidOperationException>.If(args.Length == 0 || args.Length > 2,  "Wrong argument count!");
            Raise<InvalidOperationException>.If(args.Length == 2 && args[0] != "-e",  "Wrong flag!");
            try {
                if (args.Length == 2) {
                    var output = Compile(args[1]);
                    Execute(output);
                } else {
                    Compile(args[0]);
                }
            } catch (Exception ex) {
                Console.WriteLine();
                Console.WriteLine("ERROR: {0}", ex.Message);
                return 1;
            }
            return 0;
        }

        public static string Compile(string input)
        {
            Console.Write("# Parsing file {0}: ", input);
            FunctionDecl main;
            using (var file = new FileStream(input, FileMode.Open)) {
                var scanner = new Scanner(file);
                var parser = new Parser(scanner);
                if (!parser.Parse()) {
                    Console.WriteLine("Not OK :(");
                    throw new ArgumentException("Input is not valid...");
                }
                Console.WriteLine("OK");
                main = parser.Prog;
            }

            Console.Write("# Type checking file {0}: ", input);
            var tcv = new TypecheckVisitor();
            main.Accept(tcv);
            Console.WriteLine("OK");

            Console.Write("# Return checking file {0}: ", input);
            var rcv = new ReturnCheckVisitor();
            main.Accept(rcv);
            Console.WriteLine("OK");

            Console.WriteLine("# Contents of file {0}:", input);
            var toStringVisitor = new ToStringVisitor();
            main.Accept(toStringVisitor);
            Console.Write(toStringVisitor.Result);

            var prefix = input.Substring(0, input.LastIndexOf('.'));
            Console.Write("# Compiling file {0} into {1}: ", input, prefix + ".exe");
            var cecilVisitor = new CecilVisitor(tcv.Assembly, tcv.Module);
            main.Accept(cecilVisitor);
            cecilVisitor.Write(prefix);
            Console.WriteLine("OK");

            Console.Out.Flush();
            return prefix + ".exe";
        }

        public static void TypeCheck(string input)
        {
            FunctionDecl main;
            using (var file = new FileStream(input, FileMode.Open)) {
                var scanner = new Scanner(file);
                var parser = new Parser(scanner);
                Raise<ParseException>.IfNot(parser.Parse());
                main = parser.Prog;
            }
            main.Accept(new TypecheckVisitor());
            main.Accept(new ReturnCheckVisitor());
        }

        static void Execute(string output)
        {
            Console.WriteLine("# Running file {0}...", output);
            var process = Process.Start(output);
            Debug.Assert(process != null); // To keep ReSharper quiet :)
            process.WaitForExit();
        }
    }

// ReSharper disable UnusedMember.Global

    [Serializable]
    public sealed class ParseException : Exception
    {
        public ParseException() {}
        public ParseException(string message) : base(message) {}
        public ParseException(string message, Exception inner) : base(message, inner) {}
    }

    [Serializable]
    public sealed class ReturnCheckException : Exception
    {
        public ReturnCheckException() {}
        public ReturnCheckException(string message) : base(message) {}
        public ReturnCheckException(string message, Exception inner) : base(message, inner) {}
    }

    [Serializable]
    public sealed class TypeCheckException : Exception
    {
        public TypeCheckException() {}
        public TypeCheckException(string message) : base(message) {}
        public TypeCheckException(string message, Exception inner) : base(message, inner) {}
    }

// ReSharper restore UnusedMember.Global
}