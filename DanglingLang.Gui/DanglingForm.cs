namespace DanglingLang.Gui
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Properties;
    using ScintillaNET;
    using Thrower;

    public partial class DanglingForm : Form
    {
        const string NewLine = "\r\n";
        const string SourceFile = "GuiSourceFile.txt";

        readonly Scintilla _scriptCodeTxt;

        public DanglingForm()
        {
            InitializeComponent();

            _scriptCodeTxt = new Scintilla {
                AcceptsReturn = true,
                AcceptsTab = true,
                Dock = DockStyle.Fill,
                Name = "ScriptCodeTxt",
                TabIndex = 0,
                MatchBraces = true,
                Parent = groupBox1
            };
            _scriptCodeTxt.Margins[0].Width = 20;
            _scriptCodeTxt.ConfigurationManager.Language = "cs";
            _scriptCodeTxt.ConfigurationManager.Configure();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) {
                CompileBtn.PerformClick();
                return true;
            }
            if (keyData == Keys.F6) {
                ClearBtn.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void CompileBtn_Click(object sender, EventArgs e)
        {
            if (_scriptCodeTxt.Text.Length == 0) {
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
            File.WriteAllText(SourceFile, _scriptCodeTxt.Text + NewLine);
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