namespace DanglingLang.Gui
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Properties;
    using Thrower;

    public partial class DanglingForm : Form
    {
        const string NewLine = "\r\n";
        const string SourceFile = "GuiSourceFile.txt";

        public DanglingForm()
        {
            InitializeComponent();
        }

        void CompileBtn_Click(object sender, EventArgs e)
        {
            if (ScriptCodeTxt.Text.Length == 0) {
                return; // Nothing to do...
            }
            try {
                CompilerOutputTxt.Clear();
                ScriptOutputTxt.Clear();
                RunCompiler();
                RunExecutable();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        void ClearBtn_Click(object sender, EventArgs e)
        {
            CompilerOutputTxt.Clear();
            ScriptOutputTxt.Clear();
        }

        void RunCompiler()
        {
            File.WriteAllText(SourceFile, ScriptCodeTxt.Text + NewLine);
            var compiler = new Process {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = Settings.Default.CompilerPath,
                    Arguments = Settings.Default.CompilerArgs,
                    CreateNoWindow = true
                }
            };
            compiler.Start();
            CompilerOutputTxt.ReadOnly = false;
            while (!compiler.StandardOutput.EndOfStream) {
                CompilerOutputTxt.Text += compiler.StandardOutput.ReadLine() + NewLine;
            }      
            CompilerOutputTxt.ReadOnly = true;
            compiler.WaitForExit();
            Raise<Exception>.IfAreEqual(compiler.ExitCode, 1, "Compilation failed :(");
        }

        void RunExecutable()
        {
            var exec = new Process {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = Settings.Default.ExecutablePath,
                    Arguments = Settings.Default.ExecutableArgs,
                    CreateNoWindow = true
                }
            };
            exec.Start();
            ScriptOutputTxt.ReadOnly = false;
            while (!exec.StandardOutput.EndOfStream) {
                ScriptOutputTxt.Text += exec.StandardOutput.ReadLine() + NewLine;
            } 
            ScriptOutputTxt.ReadOnly = true;
            exec.WaitForExit();
            Raise<Exception>.IfAreEqual(exec.ExitCode, 1, "Execution failed :(");
        }
    }
}