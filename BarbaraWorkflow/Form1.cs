using BarbaraWorkflow.Domain.Models;
using BarbaraWorkflow.Infra;

namespace BarbaraWorkflow
{
    public partial class Form1 : Form
    {
        private bool IsOptimized { get; set; } = false;

        KeyboardHook OptimizeHotkey { get; set; }

        KeyboardHook ForwardHotkey { get; set; }
        KeyboardHook BackwardHotkey { get; set; }

        HintStatus hintStatus { get; set; }

        public Form1()
        {
            InitializeComponent();

            OptimizeHotkey = new KeyboardHook();
            OptimizeHotkey.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.F12);
            OptimizeHotkey.KeyPressed += OptimizeHotkey_KeyPressed;


            ForwardHotkey = new KeyboardHook();
            ForwardHotkey.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.Right);
            ForwardHotkey.KeyPressed += ForwardHotkey_KeyPressed;

            BackwardHotkey = new KeyboardHook();
            BackwardHotkey.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.Left);
            BackwardHotkey.KeyPressed += BackwardHotkey_KeyPressed;



        }

        private void BackwardHotkey_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            hintStatus.TryBackward();
            mainLabel.Text = hintStatus.GetCurrentHint();
        }

        private void ForwardHotkey_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            hintStatus.TryForward();
            mainLabel.Text = hintStatus.GetCurrentHint();
        }

        private void OptimizeHotkey_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            ToggleOptimization();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTopmost(true);
        }

        private void SetTopmost(bool topMost)
        {
            TopMost = topMost;
            topmostButton.Text = $"÷√∂•: {Convert.ToInt32(TopMost)}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetTopmost(!TopMost);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToggleOptimization();
        }

        private void ToggleOptimization()
        {
            IsOptimized = !IsOptimized;

            if (IsOptimized)
            {
                panel1.Hide();
                BackColor = Color.Black;
                TransparencyKey = Color.Black;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.None;

                mainLabel.ForeColor = Color.LightYellow;
            }
            else
            {

                panel1.Show();
                BackColor = SystemColors.Control;
                TransparencyKey = Color.Empty;
                ControlBox = true;
                FormBorderStyle = FormBorderStyle.Sizable;

                mainLabel.ForeColor = Color.Black;
            }
        }


        public void LoadTxtFile(string filename)
        {
            string text;
            try
            {
                text = File.ReadAllText(filename);
            }
            catch (Exception e)
            {
                text = e.Message;
            }

            hintStatus = HintStatus.CreateFromText(text);
            mainLabel.Text = hintStatus.GetCurrentHint();
        }

        private void loadtxtButton_Click(object sender, EventArgs e)
        {
            loadtxtDialog.ShowDialog();
        }

        private void loadtxtDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (loadtxtDialog.FileName == "")
            {
                return;
            }

            LoadTxtFile(loadtxtDialog.FileName);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            var file = files.FirstOrDefault();

            if (!string.IsNullOrEmpty(file))
            {
                LoadTxtFile(file);
            }
        }
    }
}