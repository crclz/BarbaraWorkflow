using BarbaraWorkflow.Infra;

namespace BarbaraWorkflow
{
    public partial class Form1 : Form
    {
        private bool IsOptimized { get; set; } = false;

        KeyboardHook OptimizeHook { get; set; }

        KeyboardHook AltWHook { get; set; }
        KeyboardHook AltShiftWHook { get; set; }

        public Form1()
        {
            InitializeComponent();

            OptimizeHook = new KeyboardHook();
            OptimizeHook.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.F12);
            OptimizeHook.KeyPressed += OptimizeHook_KeyPressed;


            AltWHook = new KeyboardHook();
            AltWHook.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.Right);
            AltWHook.KeyPressed += AltWHook_KeyPressed;

            AltShiftWHook = new KeyboardHook();
            AltShiftWHook.RegisterHotKey(Infra.ModifierKeys.Alt, Keys.Left);
            AltShiftWHook.KeyPressed += AltShiftWHook_KeyPressed;
        }

        private void AltShiftWHook_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            mainLabel.Text += "S";

        }

        private void AltWHook_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            mainLabel.Text += "W";
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
            topmostButton.Text = $"�ö�: {Convert.ToInt32(TopMost)}";
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
            }
            else
            {
                mainLabel.ForeColor = Color.LightYellow;

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