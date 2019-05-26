namespace FaceTest
{
    partial class frmDeviceTest
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
            this.button32 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.receiveMsg = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.receiveMsg2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 75);
            this.button1.TabIndex = 12;
            this.button1.Text = "广播";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button32
            // 
            this.button32.Location = new System.Drawing.Point(47, 491);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(371, 56);
            this.button32.TabIndex = 79;
            this.button32.Text = "初始化设备";
            this.button32.UseVisualStyleBackColor = true;
            this.button32.Click += new System.EventHandler(this.button32_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(460, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 668);
            this.tabControl1.TabIndex = 80;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.receiveMsg);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(615, 639);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "简要";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // receiveMsg
            // 
            this.receiveMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveMsg.Location = new System.Drawing.Point(3, 3);
            this.receiveMsg.Margin = new System.Windows.Forms.Padding(4);
            this.receiveMsg.Name = "receiveMsg";
            this.receiveMsg.Size = new System.Drawing.Size(609, 633);
            this.receiveMsg.TabIndex = 10;
            this.receiveMsg.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.receiveMsg2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(615, 639);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "详细";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // receiveMsg2
            // 
            this.receiveMsg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveMsg2.Location = new System.Drawing.Point(3, 3);
            this.receiveMsg2.Margin = new System.Windows.Forms.Padding(4);
            this.receiveMsg2.Name = "receiveMsg2";
            this.receiveMsg2.Size = new System.Drawing.Size(609, 633);
            this.receiveMsg2.TabIndex = 11;
            this.receiveMsg2.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(47, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 52);
            this.button2.TabIndex = 81;
            this.button2.Tag = "1";
            this.button2.Text = "补光灯";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.changeColor);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(241, 121);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 52);
            this.button3.TabIndex = 82;
            this.button3.Tag = "2";
            this.button3.Text = "风扇";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.changeColor);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(47, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(177, 52);
            this.button4.TabIndex = 83;
            this.button4.Tag = "3";
            this.button4.Text = "屏幕";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.changeColor);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(241, 190);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(177, 52);
            this.button5.TabIndex = 84;
            this.button5.Tag = "4";
            this.button5.Text = "网口";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.changeColor);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(47, 265);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(177, 52);
            this.button6.TabIndex = 85;
            this.button6.Tag = "5";
            this.button6.Text = "摄像头";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.changeColor);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(241, 265);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(177, 52);
            this.button7.TabIndex = 86;
            this.button7.Tag = "6";
            this.button7.Text = "提示灯";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.changeColor);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(47, 340);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(177, 52);
            this.button8.TabIndex = 87;
            this.button8.Tag = "7";
            this.button8.Text = "语音";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.changeColor);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(241, 340);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(177, 52);
            this.button9.TabIndex = 88;
            this.button9.Tag = "8";
            this.button9.Text = "开关量";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.changeColor);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(47, 419);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(177, 52);
            this.button10.TabIndex = 89;
            this.button10.Tag = "9";
            this.button10.Text = "韦根";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.changeColor);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(47, 574);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(371, 56);
            this.button11.TabIndex = 90;
            this.button11.Text = "保存结果";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(241, 419);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(177, 52);
            this.button12.TabIndex = 91;
            this.button12.Tag = "10";
            this.button12.Text = "电池";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.changeColor);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(241, 13);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(177, 75);
            this.button13.TabIndex = 92;
            this.button13.Text = "设置参数、同步照片";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // frmDeviceTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 668);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button32);
            this.Controls.Add(this.button1);
            this.Name = "frmDeviceTest";
            this.Text = "frmDeviceTest";
            this.Load += new System.EventHandler(this.frmDeviceTest_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox receiveMsg;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox receiveMsg2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
    }
}