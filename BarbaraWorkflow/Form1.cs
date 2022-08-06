using BarbaraWorkflow.Infra;

namespace BarbaraWorkflow
{
    public partial class Form1 : Form
    {
        private bool IsOptimized { get; set; } = false;

        KeyboardHook OptimizeHook { get; set; }

        public Form1()
        {
            InitializeComponent();
            OptimizeHook = new KeyboardHook();
            OptimizeHook.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.F12);
            OptimizeHook.KeyPressed += OptimizeHook_KeyPressed;
        }

        private void OptimizeHook_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            ToggleOptimization();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            topmostButton.Text = $"÷√∂•: {Convert.ToInt32(TopMost)}";
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
                BackColor = Color.LimeGreen;
                TransparencyKey = Color.LimeGreen;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                topmostButton.Show();
                BackColor = SystemColors.Control;
                TransparencyKey = Color.Empty;
                ControlBox = true;
                FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}