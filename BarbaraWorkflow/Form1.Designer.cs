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
            this.SuspendLayout();
            // 
            // topmostButton
            // 
            this.topmostButton.Location = new System.Drawing.Point(7, 216);
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
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "alt+F12";
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mainLabel.Location = new System.Drawing.Point(7, 22);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(614, 66);
            this.mainLabel.TabIndex = 3;
            this.mainLabel.Text = "L蛇肠之路T 右下机关\r\nTR狭间之街B2 左 机关 左 机关 左 牛头";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 439);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.topmostButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button topmostButton;
        private Label label1;
        private Label mainLabel;
    }
}