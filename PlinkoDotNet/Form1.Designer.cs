namespace PlinkoDotNet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.InputForm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPoker = new System.Windows.Forms.Button();
            this.poker1 = new System.Windows.Forms.PictureBox();
            this.poker2 = new System.Windows.Forms.PictureBox();
            this.poker3 = new System.Windows.Forms.PictureBox();
            this.poker4 = new System.Windows.Forms.PictureBox();
            this.poker5 = new System.Windows.Forms.PictureBox();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.GameZone = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.poker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker5)).BeginInit();
            this.GamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameZone)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(653, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Plinko!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InputForm
            // 
            this.InputForm.Location = new System.Drawing.Point(638, 52);
            this.InputForm.Name = "InputForm";
            this.InputForm.Size = new System.Drawing.Size(100, 20);
            this.InputForm.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(676, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mise";
            // 
            // MessageBox
            // 
            this.MessageBox.AcceptsReturn = true;
            this.MessageBox.AcceptsTab = true;
            this.MessageBox.Location = new System.Drawing.Point(596, 175);
            this.MessageBox.Multiline = true;
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ReadOnly = true;
            this.MessageBox.Size = new System.Drawing.Size(180, 343);
            this.MessageBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(659, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Messages";
            // 
            // btnPoker
            // 
            this.btnPoker.Location = new System.Drawing.Point(653, 119);
            this.btnPoker.Name = "btnPoker";
            this.btnPoker.Size = new System.Drawing.Size(75, 23);
            this.btnPoker.TabIndex = 6;
            this.btnPoker.Text = "Poker";
            this.btnPoker.UseVisualStyleBackColor = true;
            this.btnPoker.Click += new System.EventHandler(this.btnPoker_Click);
            // 
            // poker1
            // 
            this.poker1.Location = new System.Drawing.Point(27, 64);
            this.poker1.Name = "poker1";
            this.poker1.Size = new System.Drawing.Size(130, 180);
            this.poker1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.poker1.TabIndex = 0;
            this.poker1.TabStop = false;
            this.poker1.Visible = false;
            // 
            // poker2
            // 
            this.poker2.Location = new System.Drawing.Point(178, 64);
            this.poker2.Name = "poker2";
            this.poker2.Size = new System.Drawing.Size(130, 180);
            this.poker2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.poker2.TabIndex = 1;
            this.poker2.TabStop = false;
            this.poker2.Visible = false;
            // 
            // poker3
            // 
            this.poker3.Location = new System.Drawing.Point(333, 64);
            this.poker3.Name = "poker3";
            this.poker3.Size = new System.Drawing.Size(130, 180);
            this.poker3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.poker3.TabIndex = 2;
            this.poker3.TabStop = false;
            this.poker3.Visible = false;
            // 
            // poker4
            // 
            this.poker4.Location = new System.Drawing.Point(84, 292);
            this.poker4.Name = "poker4";
            this.poker4.Size = new System.Drawing.Size(130, 180);
            this.poker4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.poker4.TabIndex = 3;
            this.poker4.TabStop = false;
            this.poker4.Visible = false;
            // 
            // poker5
            // 
            this.poker5.Location = new System.Drawing.Point(274, 292);
            this.poker5.Name = "poker5";
            this.poker5.Size = new System.Drawing.Size(130, 180);
            this.poker5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.poker5.TabIndex = 4;
            this.poker5.TabStop = false;
            this.poker5.Visible = false;
            // 
            // GamePanel
            // 
            this.GamePanel.BackColor = System.Drawing.Color.Lime;
            this.GamePanel.Controls.Add(this.GameZone);
            this.GamePanel.Controls.Add(this.poker5);
            this.GamePanel.Controls.Add(this.poker4);
            this.GamePanel.Controls.Add(this.poker3);
            this.GamePanel.Controls.Add(this.poker2);
            this.GamePanel.Controls.Add(this.poker1);
            this.GamePanel.Location = new System.Drawing.Point(12, 12);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(538, 506);
            this.GamePanel.TabIndex = 0;
            // 
            // GameZone
            // 
            this.GameZone.Location = new System.Drawing.Point(0, 0);
            this.GameZone.Name = "GameZone";
            this.GameZone.Size = new System.Drawing.Size(538, 506);
            this.GameZone.TabIndex = 5;
            this.GameZone.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(810, 549);
            this.Controls.Add(this.btnPoker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputForm);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GamePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.poker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poker5)).EndInit();
            this.GamePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GameZone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox InputForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPoker;
        private System.Windows.Forms.PictureBox poker1;
        private System.Windows.Forms.PictureBox poker2;
        private System.Windows.Forms.PictureBox poker3;
        private System.Windows.Forms.PictureBox poker4;
        private System.Windows.Forms.PictureBox poker5;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.PictureBox GameZone;
    }
}

