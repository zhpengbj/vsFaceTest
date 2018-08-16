namespace FaceTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tb_Url = new System.Windows.Forms.TextBox();
            this.tb_Pass = new System.Windows.Forms.TextBox();
            this.tb_Path = new System.Windows.Forms.TextBox();
            this.receiveMsg = new System.Windows.Forms.RichTextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.cb_saveImageKey = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.tb_CallBackUrl = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_PersonId = new System.Windows.Forms.Label();
            this.lb_PersonName = new System.Windows.Forms.Label();
            this.tb_MachineCode = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.tb_AuthorizeCode = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.bt_GetApkVersion = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.tb_DownApkUrl = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.tb_time = new System.Windows.Forms.TextBox();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button14 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 442);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动web服务(回调服务)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 43);
            this.button2.TabIndex = 1;
            this.button2.Text = "设置照片路径 ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 85);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(168, 43);
            this.button3.TabIndex = 3;
            this.button3.Text = "设置Url ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 134);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(168, 43);
            this.button4.TabIndex = 4;
            this.button4.Text = "设置Pass";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 185);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(168, 43);
            this.button5.TabIndex = 5;
            this.button5.Text = "处理照片";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tb_Url
            // 
            this.tb_Url.Location = new System.Drawing.Point(190, 92);
            this.tb_Url.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(245, 28);
            this.tb_Url.TabIndex = 6;
            this.tb_Url.Text = "http://192.168.8.101:8090";
            // 
            // tb_Pass
            // 
            this.tb_Pass.Location = new System.Drawing.Point(190, 142);
            this.tb_Pass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_Pass.Name = "tb_Pass";
            this.tb_Pass.Size = new System.Drawing.Size(245, 28);
            this.tb_Pass.TabIndex = 7;
            this.tb_Pass.Text = "123";
            // 
            // tb_Path
            // 
            this.tb_Path.Location = new System.Drawing.Point(189, 22);
            this.tb_Path.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_Path.Name = "tb_Path";
            this.tb_Path.Size = new System.Drawing.Size(245, 28);
            this.tb_Path.TabIndex = 8;
            this.tb_Path.Text = "FacePicTest2";
            // 
            // receiveMsg
            // 
            this.receiveMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.receiveMsg.Location = new System.Drawing.Point(489, 0);
            this.receiveMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.receiveMsg.Name = "receiveMsg";
            this.receiveMsg.Size = new System.Drawing.Size(842, 881);
            this.receiveMsg.TabIndex = 9;
            this.receiveMsg.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(15, 240);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(168, 43);
            this.button6.TabIndex = 10;
            this.button6.Text = "同步";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(189, 240);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(168, 43);
            this.button7.TabIndex = 11;
            this.button7.Text = "清空日志";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // cb_saveImageKey
            // 
            this.cb_saveImageKey.AutoSize = true;
            this.cb_saveImageKey.Checked = true;
            this.cb_saveImageKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_saveImageKey.Location = new System.Drawing.Point(205, 197);
            this.cb_saveImageKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_saveImageKey.Name = "cb_saveImageKey";
            this.cb_saveImageKey.Size = new System.Drawing.Size(142, 22);
            this.cb_saveImageKey.TabIndex = 13;
            this.cb_saveImageKey.Text = "保存照片特征";
            this.cb_saveImageKey.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 626);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(227, 43);
            this.button8.TabIndex = 14;
            this.button8.Text = "设置回调URL";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // tb_CallBackUrl
            // 
            this.tb_CallBackUrl.Location = new System.Drawing.Point(15, 677);
            this.tb_CallBackUrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_CallBackUrl.Name = "tb_CallBackUrl";
            this.tb_CallBackUrl.Size = new System.Drawing.Size(450, 28);
            this.tb_CallBackUrl.TabIndex = 15;
            this.tb_CallBackUrl.Text = "http://192.168.8.100:8091/Handler.ashx";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(340, 475);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 121);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lb_PersonId
            // 
            this.lb_PersonId.AutoSize = true;
            this.lb_PersonId.Location = new System.Drawing.Point(363, 422);
            this.lb_PersonId.Name = "lb_PersonId";
            this.lb_PersonId.Size = new System.Drawing.Size(107, 18);
            this.lb_PersonId.TabIndex = 17;
            this.lb_PersonId.Text = "lb_PersonId";
            // 
            // lb_PersonName
            // 
            this.lb_PersonName.AutoSize = true;
            this.lb_PersonName.Location = new System.Drawing.Point(361, 453);
            this.lb_PersonName.Name = "lb_PersonName";
            this.lb_PersonName.Size = new System.Drawing.Size(125, 18);
            this.lb_PersonName.TabIndex = 18;
            this.lb_PersonName.Text = "lb_PersonName";
            // 
            // tb_MachineCode
            // 
            this.tb_MachineCode.Location = new System.Drawing.Point(190, 336);
            this.tb_MachineCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_MachineCode.Name = "tb_MachineCode";
            this.tb_MachineCode.Size = new System.Drawing.Size(245, 28);
            this.tb_MachineCode.TabIndex = 20;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(15, 328);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(168, 43);
            this.button9.TabIndex = 19;
            this.button9.Text = "得到机器码";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // tb_AuthorizeCode
            // 
            this.tb_AuthorizeCode.Location = new System.Drawing.Point(190, 386);
            this.tb_AuthorizeCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_AuthorizeCode.Name = "tb_AuthorizeCode";
            this.tb_AuthorizeCode.Size = new System.Drawing.Size(245, 28);
            this.tb_AuthorizeCode.TabIndex = 22;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(15, 379);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(168, 43);
            this.button10.TabIndex = 21;
            this.button10.Text = "授权";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // bt_GetApkVersion
            // 
            this.bt_GetApkVersion.Location = new System.Drawing.Point(15, 764);
            this.bt_GetApkVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_GetApkVersion.Name = "bt_GetApkVersion";
            this.bt_GetApkVersion.Size = new System.Drawing.Size(450, 28);
            this.bt_GetApkVersion.TabIndex = 24;
            this.bt_GetApkVersion.Text = "http://192.168.8.100:8091/GetUpdate.ashx";
            this.bt_GetApkVersion.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(15, 714);
            this.button11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(227, 43);
            this.button11.TabIndex = 23;
            this.button11.Text = "设置查看版本号URL";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // tb_DownApkUrl
            // 
            this.tb_DownApkUrl.Location = new System.Drawing.Point(15, 852);
            this.tb_DownApkUrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_DownApkUrl.Name = "tb_DownApkUrl";
            this.tb_DownApkUrl.Size = new System.Drawing.Size(450, 28);
            this.tb_DownApkUrl.TabIndex = 26;
            this.tb_DownApkUrl.Text = "http://192.168.8.100:8091/update.apk";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(15, 802);
            this.button12.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(227, 43);
            this.button12.TabIndex = 25;
            this.button12.Text = "设置APK下载URL";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // tb_time
            // 
            this.tb_time.Location = new System.Drawing.Point(189, 291);
            this.tb_time.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_time.Name = "tb_time";
            this.tb_time.Size = new System.Drawing.Size(245, 28);
            this.tb_time.TabIndex = 28;
            this.tb_time.Visible = false;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(14, 283);
            this.button13.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(168, 43);
            this.button13.TabIndex = 27;
            this.button13.Text = "设置时间";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Visible = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 588);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(450, 28);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "http://192.168.8.100:8091/VerifyHandler.ashx";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(15, 537);
            this.button14.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(227, 43);
            this.button14.TabIndex = 29;
            this.button14.Text = "设置验证回调URL";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 881);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.tb_time);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.tb_DownApkUrl);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.bt_GetApkVersion);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.tb_AuthorizeCode);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.tb_MachineCode);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.lb_PersonName);
            this.Controls.Add(this.lb_PersonId);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tb_CallBackUrl);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.cb_saveImageKey);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.receiveMsg);
            this.Controls.Add(this.tb_Path);
            this.Controls.Add(this.tb_Pass);
            this.Controls.Add(this.tb_Url);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "xFace";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tb_Url;
        private System.Windows.Forms.TextBox tb_Path;
        private System.Windows.Forms.TextBox tb_Pass;
        private System.Windows.Forms.RichTextBox receiveMsg;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox cb_saveImageKey;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox tb_CallBackUrl;
        private System.Windows.Forms.Label lb_PersonName;
        private System.Windows.Forms.Label lb_PersonId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tb_AuthorizeCode;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox tb_MachineCode;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox bt_GetApkVersion;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox tb_DownApkUrl;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox tb_time;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button14;
    }
}

