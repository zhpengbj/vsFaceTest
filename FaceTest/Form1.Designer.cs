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
            this.button8 = new System.Windows.Forms.Button();
            this.cb_saveImageKey = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 73);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(27, 23);
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
            this.label1.Location = new System.Drawing.Point(28, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 78);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 36);
            this.button3.TabIndex = 3;
            this.button3.Text = "设置Url ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 132);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 36);
            this.button4.TabIndex = 4;
            this.button4.Text = "设置Pass";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(27, 199);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 36);
            this.button5.TabIndex = 5;
            this.button5.Text = "处理照片";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tb_Url
            // 
            this.tb_Url.Location = new System.Drawing.Point(196, 84);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(218, 25);
            this.tb_Url.TabIndex = 6;
            this.tb_Url.Text = "http://192.168.1.107:8090";
            // 
            // tb_Pass
            // 
            this.tb_Pass.Location = new System.Drawing.Point(196, 138);
            this.tb_Pass.Name = "tb_Pass";
            this.tb_Pass.Size = new System.Drawing.Size(218, 25);
            this.tb_Pass.TabIndex = 7;
            this.tb_Pass.Text = "123";
            // 
            // tb_Path
            // 
            this.tb_Path.Location = new System.Drawing.Point(196, 29);
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
            this.receiveMsg.Size = new System.Drawing.Size(749, 601);
            this.receiveMsg.TabIndex = 9;
            this.receiveMsg.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(27, 255);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 36);
            this.button6.TabIndex = 10;
            this.button6.Text = "同步";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(265, 414);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(149, 36);
            this.button7.TabIndex = 11;
            this.button7.Text = "清空日志";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(196, 255);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(149, 36);
            this.button8.TabIndex = 12;
            this.button8.Text = "同步";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // cb_saveImageKey
            // 
            this.cb_saveImageKey.AutoSize = true;
            this.cb_saveImageKey.Checked = true;
            this.cb_saveImageKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_saveImageKey.Location = new System.Drawing.Point(196, 209);
            this.cb_saveImageKey.Name = "cb_saveImageKey";
            this.cb_saveImageKey.Size = new System.Drawing.Size(119, 19);
            this.cb_saveImageKey.TabIndex = 13;
            this.cb_saveImageKey.Text = "保存照片特征";
            this.cb_saveImageKey.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 601);
            this.Controls.Add(this.cb_saveImageKey);
            this.Controls.Add(this.button8);
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
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
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
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox cb_saveImageKey;
    }
}

