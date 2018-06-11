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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
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
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "开启回调服务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 36);
            this.button2.TabIndex = 1;
            this.button2.Text = "设置照片路径 ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 36);
            this.button3.TabIndex = 3;
            this.button3.Text = "设置Url ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 36);
            this.button4.TabIndex = 4;
            this.button4.Text = "设置Pass";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 272);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 36);
            this.button5.TabIndex = 5;
            this.button5.Text = "处理照片";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tb_Url
            // 
            this.tb_Url.Location = new System.Drawing.Point(169, 77);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(218, 25);
            this.tb_Url.TabIndex = 6;
            this.tb_Url.Text = "http://192.168.1.107:8090";
            // 
            // tb_Pass
            // 
            this.tb_Pass.Location = new System.Drawing.Point(169, 118);
            this.tb_Pass.Name = "tb_Pass";
            this.tb_Pass.Size = new System.Drawing.Size(218, 25);
            this.tb_Pass.TabIndex = 7;
            this.tb_Pass.Text = "123";
            // 
            // tb_Path
            // 
            this.tb_Path.Location = new System.Drawing.Point(168, 18);
            this.tb_Path.Name = "tb_Path";
            this.tb_Path.Size = new System.Drawing.Size(218, 25);
            this.tb_Path.TabIndex = 8;
            this.tb_Path.Text = "FacePicTest";
            // 
            // receiveMsg
            // 
            this.receiveMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.receiveMsg.Location = new System.Drawing.Point(434, 0);
            this.receiveMsg.Margin = new System.Windows.Forms.Padding(4);
            this.receiveMsg.Name = "receiveMsg";
            this.receiveMsg.Size = new System.Drawing.Size(749, 661);
            this.receiveMsg.TabIndex = 9;
            this.receiveMsg.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(13, 318);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 36);
            this.button6.TabIndex = 10;
            this.button6.Text = "同步";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(168, 318);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(149, 36);
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
            this.cb_saveImageKey.Location = new System.Drawing.Point(182, 282);
            this.cb_saveImageKey.Name = "cb_saveImageKey";
            this.cb_saveImageKey.Size = new System.Drawing.Size(119, 19);
            this.cb_saveImageKey.TabIndex = 13;
            this.cb_saveImageKey.Text = "保存照片特征";
            this.cb_saveImageKey.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(27, 522);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(149, 36);
            this.button8.TabIndex = 14;
            this.button8.Text = "设置回调函数";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // tb_CallBackUrl
            // 
            this.tb_CallBackUrl.Location = new System.Drawing.Point(27, 564);
            this.tb_CallBackUrl.Name = "tb_CallBackUrl";
            this.tb_CallBackUrl.Size = new System.Drawing.Size(400, 25);
            this.tb_CallBackUrl.TabIndex = 15;
            this.tb_CallBackUrl.Text = "http://192.168.1.117:8733/FaceTest/Service1/PostTest";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(182, 415);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 143);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lb_PersonId
            // 
            this.lb_PersonId.AutoSize = true;
            this.lb_PersonId.Location = new System.Drawing.Point(321, 415);
            this.lb_PersonId.Name = "lb_PersonId";
            this.lb_PersonId.Size = new System.Drawing.Size(95, 15);
            this.lb_PersonId.TabIndex = 17;
            this.lb_PersonId.Text = "lb_PersonId";
            // 
            // lb_PersonName
            // 
            this.lb_PersonName.AutoSize = true;
            this.lb_PersonName.Location = new System.Drawing.Point(319, 441);
            this.lb_PersonName.Name = "lb_PersonName";
            this.lb_PersonName.Size = new System.Drawing.Size(111, 15);
            this.lb_PersonName.TabIndex = 18;
            this.lb_PersonName.Text = "lb_PersonName";
            // 
            // tb_MachineCode
            // 
            this.tb_MachineCode.Location = new System.Drawing.Point(168, 160);
            this.tb_MachineCode.Name = "tb_MachineCode";
            this.tb_MachineCode.Size = new System.Drawing.Size(218, 25);
            this.tb_MachineCode.TabIndex = 20;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(12, 154);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(149, 36);
            this.button9.TabIndex = 19;
            this.button9.Text = "得到机器码";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // tb_AuthorizeCode
            // 
            this.tb_AuthorizeCode.Location = new System.Drawing.Point(168, 202);
            this.tb_AuthorizeCode.Name = "tb_AuthorizeCode";
            this.tb_AuthorizeCode.Size = new System.Drawing.Size(218, 25);
            this.tb_AuthorizeCode.TabIndex = 22;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 196);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(149, 36);
            this.button10.TabIndex = 21;
            this.button10.Text = "授权";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 661);
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
            this.Name = "Form1";
            this.Text = "xFace";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
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
    }
}

