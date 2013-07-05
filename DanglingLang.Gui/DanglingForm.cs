namespace DanglingLang.Gui
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Properties;

    public partial class DanglingForm : Form
    {
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
            File.WriteAllText(SourceFile, ScriptCodeTxt.Text);
            var compiler = new Process {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = Settings.Default.CompilerPath,
                    Arguments = Settings.Default.CompilerArgs,
                    CreateNoWindow = true
                }
            };
            try {
                compiler.Start();
            } catch {
                
            }
            CompilerOutputTxt.ReadOnly = false;
            CompilerOutputTxt.Text = compiler.StandardOutput.ReadToEnd();
            CompilerOutputTxt.ReadOnly = true;
            compiler.WaitForExit();
        }
    }
}