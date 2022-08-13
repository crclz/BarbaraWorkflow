namespace BarbaraWorkflow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topmostButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mainLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.loadtxtButton = new System.Windows.Forms.Button();
            this.loadtxtDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topmostButton
            // 
            this.topmostButton.Location = new System.Drawing.Point(3, 3);
            this.topmostButton.Name = "topmostButton";
            this.topmostButton.Size = new System.Drawing.Size(75, 23);
            this.topmostButton.TabIndex = 0;
            this.topmostButton.Text = "置顶";
            this.topmostButton.UseVisualStyleBackColor = true;
            this.topmostButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "alt+F12; alt+方向键";
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("华文中宋", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mainLabel.Location = new System.Drawing.Point(7, 28);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(66, 31);
            this.mainLabel.TabIndex = 3;
            this.mainLabel.Text = "test";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.loadtxtButton);
            this.panel1.Controls.Add(this.topmostButton);
            this.panel1.Location = new System.Drawing.Point(12, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 100);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "支持拖放";
            // 
            // loadtxtButton
            // 
            this.loadtxtButton.Location = new System.Drawing.Point(3, 32);
            this.loadtxtButton.Name = "loadtxtButton";
            this.loadtxtButton.Size = new System.Drawing.Size(75, 23);
            this.loadtxtButton.TabIndex = 5;
            this.loadtxtButton.Text = "打开文件";
            this.loadtxtButton.UseVisualStyleBackColor = true;
            this.loadtxtButton.Click += new System.EventHandler(this.loadtxtButton_Click);
            // 
            // loadtxtDialog
            // 
            this.loadtxtDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.loadtxtDialog_FileOk);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 439);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button topmostButton;
        private Label label1;
        private Label mainLabel;
        private Panel panel1;
        private OpenFileDialog loadtxtDialog;
        private Button loadtxtButton;
        private Label label2;
    }
}