﻿namespace DanglingLang
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Thrower;
    using Tokenizer;

    public static class Launcher
    {
        static void Main(string[] args)
        {
            Raise<InvalidOperationException>.IfAreNotEqual(args.Length, 1, "There should be only one argument.");
            var output = Compile(args[0]);
            Execute(output);
        }

        public static string Compile(string input)
        {         
            Console.Write("# Parsing file {0}: ", input);
            var file = new FileStream(input, FileMode.Open);
            var scanner = new Scanner(file);
            var parser = new Parser(scanner);
            parser.Parse();
            file.Close();
            Console.WriteLine("OK");

            Console.Write("# Type checking file {0}: ", input);
            parser.Prog.Accept(new TypecheckVisitor());
            Console.WriteLine("OK");

            Console.WriteLine("# Contents of file {0}:", input);
            var toStringVisitor = new ToStringVisitor();
            parser.Prog.Accept(toStringVisitor);
            Console.Write(toStringVisitor.Result);

            var output = input.Substring(0, input.LastIndexOf('.')) + ".exe";
            Console.Write("# Compiling file {0} into {1}: ", input, output);
            var cecilVisitor = new CecilVisitor();
            parser.Prog.Accept(cecilVisitor);
            cecilVisitor.Write(output);
            Console.WriteLine("OK");

            return output;
        }

        static void Execute(string output)
        {
            Console.WriteLine("# Running file {0}...", output);
            var process = Process.Start(output);
            Debug.Assert(process != null); // To keep ReSharper quiet :)
            process.WaitForExit();
        }
    }
}