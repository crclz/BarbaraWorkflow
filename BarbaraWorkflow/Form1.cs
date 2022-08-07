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

            hintStatus = HintStatus.CreateFromText(File.ReadAllText(@"C:\Users\ThePlayer\Desktop\repos\BarbaraWorkflow\routes\ •“≈ŒÔ-±À“Ù–«€Û-Aœﬂ-BV1q44y137NC.txt"));
            mainLabel.Text = hintStatus.GetCurrentHint();

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
                topmostButton.Hide();
                BackColor = Color.Black;
                TransparencyKey = Color.Black;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.None;

                mainLabel.ForeColor = Color.LightYellow;
            }
            else
            {

                topmostButton.Show();
                BackColor = SystemColors.Control;
                TransparencyKey = Color.Empty;
                ControlBox = true;
                FormBorderStyle = FormBorderStyle.Sizable;

                mainLabel.ForeColor = Color.Black;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}