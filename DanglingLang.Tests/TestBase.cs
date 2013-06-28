namespace DanglingLang.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            _fileWriter = new StreamWriter(Input, append: false) {AutoFlush = true};
        }

        [TearDown]
        public void TearDown()
        {
            _fileWriter = null;
        }

        const string Output = "TestPino.exe";
        const string Input = "TestPino.txt";
        StreamWriter _fileWriter;

        protected void AddLine(string line)
        {
            _fileWriter.WriteLine(line);
        }

        protected void Execute(IEnumerable<string> lines)
        {
            CloseInput();
            Launcher.Compile(Input);
            var process = new Process {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    FileName = Output
                }
            };
            process.Start();
            var output = process.StandardOutput;
            foreach (var line in lines) {
                LineAssert(line, output.ReadLine(), process);
            }
            LineAssert("", output.ReadLine(), process);
            LineAssert("Press any key to exit...", output.ReadLine(), process);
            process.StandardInput.Write('Q');       
            // To avoid locked files...
            Thread.Sleep(750);
            process.WaitForExit();          
        }

        static void LineAssert(string line, string readLine, Process process)
        {
            if (line != readLine) {
                process.Kill();
                Assert.Fail("Expected \"{0}\", found \"{1}\".", line, readLine);
            }
        }

        void CloseInput()
        {
            _fileWriter.WriteLine();
            _fileWriter.Close();
        }
    }
}