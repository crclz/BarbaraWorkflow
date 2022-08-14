using BarbaraWorkflow.App.Services;
using BarbaraWorkflow.Domain.Models;
using BarbaraWorkflow.Infra;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BarbaraWorkflow
{
    public partial class Form1 : Form
    {
        private readonly MyConfigService configService;


        public Form1(MyConfigService configService)
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
            this.configService = configService;
        }


        private bool IsOptimized { get; set; } = false;

        KeyboardHook OptimizeHotkey { get; set; }

        KeyboardHook ForwardHotkey { get; set; }
        KeyboardHook BackwardHotkey { get; set; }

        HintStatus hintStatus { get; set; }

        private Subject<int> formDestroySS = new Subject<int>();

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTopmost(true);

            ObserveThings();
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



        private void SetTopmost(bool topMost)
        {
            TopMost = topMost;
            topmostButton.Text = $"ÖÃ¶¥: {Convert.ToInt32(TopMost)}";
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

        private void ObserveThings()
        {
            //configService.FontSizeSS.TakeUntil(formDestroySS).ObserveOn(SynchronizationContext.Current!)
            //    .Subscribe(p => mainLabel.Font = new Font(mainLabel.Font.FontFamily, p, GraphicsUnit.Pixel));

            configService.MainLabelStyleSS
                .TakeUntil(formDestroySS).ObserveOn(SynchronizationContext.Current!)
                .Subscribe(p =>
                {
                    var fontStyle = FontStyle.Regular;
                    if (p.IsBold)
                    {
                        fontStyle |= FontStyle.Bold;
                    }
                    var font = new Font(p.FontFamily, p.FontSize, fontStyle, GraphicsUnit.Pixel);
                    mainLabel.Font = font;
                });

            configService.SettingMessageSS.TakeUntil(formDestroySS)
                .DistinctUntilChanged().ObserveOn(SynchronizationContext.Current!)
                .Subscribe(p => settingMessageLabel.Text = p);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            formDestroySS.OnNext(1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", new string[] { configService.GetSettingFile().FullName });
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"/select,""{configService.GetSettingFile().FullName}""");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            configService.GetSettingFile().Delete();
        }
    }
}