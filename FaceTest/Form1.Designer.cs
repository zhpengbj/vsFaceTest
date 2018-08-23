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
            this.receiveMsg = new System.Windows.Forms.RichTextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.cb_saveImageKey = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_PersonId = new System.Windows.Forms.Label();
            this.lb_PersonName = new System.Windows.Forms.Label();
            this.tb_MachineCode = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.tb_AuthorizeCode = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.tb_time = new System.Windows.Forms.TextBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.lb_Path = new System.Windows.Forms.Label();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_PersonAddOrUpdate_PersonName = new System.Windows.Forms.TextBox();
            this.tb_PersonAddOrUpdate_PersonId = new System.Windows.Forms.TextBox();
            this.tb_PersonDelete_PersonId = new System.Windows.Forms.TextBox();
            this.tb_DeletePassTimeName = new System.Windows.Forms.TextBox();
            this.tb_HeartBeatUrl = new System.Windows.Forms.TextBox();
            this.tb_SetPassTime_PassTimeName = new System.Windows.Forms.TextBox();
            this.tb_SetPassTime_UserId = new System.Windows.Forms.TextBox();
            this.tb_CallBackVerifyUrl = new System.Windows.Forms.TextBox();
            this.tb_DownApkUrl = new System.Windows.Forms.TextBox();
            this.bt_GetApkVersion = new System.Windows.Forms.TextBox();
            this.tb_CallBackUrl = new System.Windows.Forms.TextBox();
            this.tb_Path = new System.Windows.Forms.TextBox();
            this.tb_Pass = new System.Windows.Forms.TextBox();
            this.tb_Url = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_PersonFind_PersonId = new System.Windows.Forms.TextBox();
            this.button21 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(381, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动web服务(回调服务)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 36);
            this.button2.TabIndex = 1;
            this.button2.Text = "设置照片路径 ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 36);
            this.button3.TabIndex = 3;
            this.button3.Text = "设置人脸接口Url ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 53);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 36);
            this.button4.TabIndex = 4;
            this.button4.Text = "设置Pass";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 176);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 36);
            this.button5.TabIndex = 5;
            this.button5.Text = "处理照片";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // receiveMsg
            // 
            this.receiveMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.receiveMsg.Location = new System.Drawing.Point(726, 0);
            this.receiveMsg.Margin = new System.Windows.Forms.Padding(4);
            this.receiveMsg.Name = "receiveMsg";
            this.receiveMsg.Size = new System.Drawing.Size(778, 766);
            this.receiveMsg.TabIndex = 9;
            this.receiveMsg.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 218);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 36);
            this.button6.TabIndex = 10;
            this.button6.Text = "同步照片";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(381, 718);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(202, 36);
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
            this.cb_saveImageKey.Location = new System.Drawing.Point(168, 183);
            this.cb_saveImageKey.Name = "cb_saveImageKey";
            this.cb_saveImageKey.Size = new System.Drawing.Size(119, 19);
            this.cb_saveImageKey.TabIndex = 13;
            this.cb_saveImageKey.Text = "保存照片特征";
            this.cb_saveImageKey.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Location = new System.Drawing.Point(381, 166);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(202, 36);
            this.button8.TabIndex = 14;
            this.button8.Text = "设置识别回调URL";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(381, 517);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lb_PersonId
            // 
            this.lb_PersonId.AutoSize = true;
            this.lb_PersonId.Location = new System.Drawing.Point(378, 468);
            this.lb_PersonId.Name = "lb_PersonId";
            this.lb_PersonId.Size = new System.Drawing.Size(95, 15);
            this.lb_PersonId.TabIndex = 17;
            this.lb_PersonId.Text = "lb_PersonId";
            // 
            // lb_PersonName
            // 
            this.lb_PersonName.AutoSize = true;
            this.lb_PersonName.Location = new System.Drawing.Point(527, 468);
            this.lb_PersonName.Name = "lb_PersonName";
            this.lb_PersonName.Size = new System.Drawing.Size(111, 15);
            this.lb_PersonName.TabIndex = 18;
            this.lb_PersonName.Text = "lb_PersonName";
            // 
            // tb_MachineCode
            // 
            this.tb_MachineCode.Location = new System.Drawing.Point(537, 645);
            this.tb_MachineCode.Name = "tb_MachineCode";
            this.tb_MachineCode.Size = new System.Drawing.Size(182, 25);
            this.tb_MachineCode.TabIndex = 20;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(381, 638);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(149, 36);
            this.button9.TabIndex = 19;
            this.button9.Text = "得到机器码";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // tb_AuthorizeCode
            // 
            this.tb_AuthorizeCode.Location = new System.Drawing.Point(537, 687);
            this.tb_AuthorizeCode.Name = "tb_AuthorizeCode";
            this.tb_AuthorizeCode.Size = new System.Drawing.Size(182, 25);
            this.tb_AuthorizeCode.TabIndex = 22;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(381, 681);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(149, 36);
            this.button10.TabIndex = 21;
            this.button10.Text = "授权";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button11.Location = new System.Drawing.Point(381, 239);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(202, 36);
            this.button11.TabIndex = 23;
            this.button11.Text = "设置查看版本号URL";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button12.Location = new System.Drawing.Point(381, 312);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(202, 36);
            this.button12.TabIndex = 25;
            this.button12.Text = "设置APK下载URL";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // tb_time
            // 
            this.tb_time.Location = new System.Drawing.Point(536, 54);
            this.tb_time.Name = "tb_time";
            this.tb_time.Size = new System.Drawing.Size(183, 25);
            this.tb_time.TabIndex = 28;
            this.tb_time.Visible = false;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(381, 48);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(149, 36);
            this.button13.TabIndex = 27;
            this.button13.Text = "设置时间";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Visible = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button14.Location = new System.Drawing.Point(381, 91);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(202, 36);
            this.button14.TabIndex = 29;
            this.button14.Text = "设置验证回调URL";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(12, 281);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(149, 36);
            this.button15.TabIndex = 31;
            this.button15.Text = "设置时段权限";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(11, 363);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(149, 36);
            this.button16.TabIndex = 32;
            this.button16.Text = "设置人员时段";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button17.Location = new System.Drawing.Point(381, 385);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(202, 36);
            this.button17.TabIndex = 35;
            this.button17.Text = "设置心跳包URL";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "人员ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 410);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "时段名称";
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(12, 323);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(149, 36);
            this.button18.TabIndex = 39;
            this.button18.Text = "删除时段权限";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // lb_Path
            // 
            this.lb_Path.AutoSize = true;
            this.lb_Path.Location = new System.Drawing.Point(378, 499);
            this.lb_Path.Name = "lb_Path";
            this.lb_Path.Size = new System.Drawing.Size(55, 15);
            this.lb_Path.TabIndex = 41;
            this.lb_Path.Text = "label4";
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(9, 468);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(149, 36);
            this.button19.TabIndex = 42;
            this.button19.Text = "新增或更新人员";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(9, 542);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(149, 36);
            this.button20.TabIndex = 43;
            this.button20.Text = "删除人员";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 555);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "人员Ids";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 49;
            this.label5.Text = "姓名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 514);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 48;
            this.label6.Text = "人员ID";
            // 
            // tb_PersonAddOrUpdate_PersonName
            // 
            this.tb_PersonAddOrUpdate_PersonName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_PersonAddOrUpdate_PersonName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_PersonAddOrUpdate_PersonName.Location = new System.Drawing.Point(232, 509);
            this.tb_PersonAddOrUpdate_PersonName.Name = "tb_PersonAddOrUpdate_PersonName";
            this.tb_PersonAddOrUpdate_PersonName.Size = new System.Drawing.Size(137, 25);
            this.tb_PersonAddOrUpdate_PersonName.TabIndex = 47;
            this.tb_PersonAddOrUpdate_PersonName.Text = global::FaceTest.Properties.Settings.Default.tb_PersonAddOrUpdate_PersonName;
            // 
            // tb_PersonAddOrUpdate_PersonId
            // 
            this.tb_PersonAddOrUpdate_PersonId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_PersonAddOrUpdate_PersonId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_PersonAddOrUpdate_PersonId.Location = new System.Drawing.Point(67, 509);
            this.tb_PersonAddOrUpdate_PersonId.Name = "tb_PersonAddOrUpdate_PersonId";
            this.tb_PersonAddOrUpdate_PersonId.Size = new System.Drawing.Size(91, 25);
            this.tb_PersonAddOrUpdate_PersonId.TabIndex = 46;
            this.tb_PersonAddOrUpdate_PersonId.Text = global::FaceTest.Properties.Settings.Default.tb_PersonAddOrUpdate_PersonId;
            // 
            // tb_PersonDelete_PersonId
            // 
            this.tb_PersonDelete_PersonId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_PersonDelete_PersonId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_PersonDelete_PersonId.Location = new System.Drawing.Point(232, 550);
            this.tb_PersonDelete_PersonId.Name = "tb_PersonDelete_PersonId";
            this.tb_PersonDelete_PersonId.Size = new System.Drawing.Size(91, 25);
            this.tb_PersonDelete_PersonId.TabIndex = 44;
            this.tb_PersonDelete_PersonId.Text = global::FaceTest.Properties.Settings.Default.tb_PersonDelete_PersonId;
            // 
            // tb_DeletePassTimeName
            // 
            this.tb_DeletePassTimeName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_DeletePassTimeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_DeletePassTimeName.Location = new System.Drawing.Point(168, 331);
            this.tb_DeletePassTimeName.Name = "tb_DeletePassTimeName";
            this.tb_DeletePassTimeName.Size = new System.Drawing.Size(206, 25);
            this.tb_DeletePassTimeName.TabIndex = 40;
            this.tb_DeletePassTimeName.Text = global::FaceTest.Properties.Settings.Default.tb_DeletePassTimeName;
            // 
            // tb_HeartBeatUrl
            // 
            this.tb_HeartBeatUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_HeartBeatUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_HeartBeatUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_HeartBeatUrl.Location = new System.Drawing.Point(381, 427);
            this.tb_HeartBeatUrl.Name = "tb_HeartBeatUrl";
            this.tb_HeartBeatUrl.Size = new System.Drawing.Size(338, 25);
            this.tb_HeartBeatUrl.TabIndex = 36;
            this.tb_HeartBeatUrl.Text = global::FaceTest.Properties.Settings.Default.tb_HeartBeatUrl;
            // 
            // tb_SetPassTime_PassTimeName
            // 
            this.tb_SetPassTime_PassTimeName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_SetPassTime_PassTimeName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_SetPassTime_PassTimeName.Location = new System.Drawing.Point(236, 405);
            this.tb_SetPassTime_PassTimeName.Name = "tb_SetPassTime_PassTimeName";
            this.tb_SetPassTime_PassTimeName.Size = new System.Drawing.Size(137, 25);
            this.tb_SetPassTime_PassTimeName.TabIndex = 34;
            this.tb_SetPassTime_PassTimeName.Text = global::FaceTest.Properties.Settings.Default.tb_SetPassTime_PassTimeName;
            // 
            // tb_SetPassTime_UserId
            // 
            this.tb_SetPassTime_UserId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_SetPassTime_UserId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_SetPassTime_UserId.Location = new System.Drawing.Point(70, 405);
            this.tb_SetPassTime_UserId.Name = "tb_SetPassTime_UserId";
            this.tb_SetPassTime_UserId.Size = new System.Drawing.Size(91, 25);
            this.tb_SetPassTime_UserId.TabIndex = 33;
            this.tb_SetPassTime_UserId.Text = global::FaceTest.Properties.Settings.Default.tb_SetPassTime_UserId;
            // 
            // tb_CallBackVerifyUrl
            // 
            this.tb_CallBackVerifyUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_CallBackVerifyUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_CallBackVerifyUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_CallBackVerifyUrl.Location = new System.Drawing.Point(381, 134);
            this.tb_CallBackVerifyUrl.Name = "tb_CallBackVerifyUrl";
            this.tb_CallBackVerifyUrl.Size = new System.Drawing.Size(338, 25);
            this.tb_CallBackVerifyUrl.TabIndex = 30;
            this.tb_CallBackVerifyUrl.Text = global::FaceTest.Properties.Settings.Default.tb_CallBackVerifyUrl;
            // 
            // tb_DownApkUrl
            // 
            this.tb_DownApkUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_DownApkUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_DownApkUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_DownApkUrl.Location = new System.Drawing.Point(381, 354);
            this.tb_DownApkUrl.Name = "tb_DownApkUrl";
            this.tb_DownApkUrl.Size = new System.Drawing.Size(338, 25);
            this.tb_DownApkUrl.TabIndex = 26;
            this.tb_DownApkUrl.Text = global::FaceTest.Properties.Settings.Default.tb_DownApkUrl;
            // 
            // bt_GetApkVersion
            // 
            this.bt_GetApkVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "bt_GetApkVersion", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.bt_GetApkVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_GetApkVersion.Location = new System.Drawing.Point(381, 281);
            this.bt_GetApkVersion.Name = "bt_GetApkVersion";
            this.bt_GetApkVersion.Size = new System.Drawing.Size(338, 25);
            this.bt_GetApkVersion.TabIndex = 24;
            this.bt_GetApkVersion.Text = global::FaceTest.Properties.Settings.Default.bt_GetApkVersion;
            this.bt_GetApkVersion.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tb_CallBackUrl
            // 
            this.tb_CallBackUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_CallBackUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_CallBackUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_CallBackUrl.Location = new System.Drawing.Point(381, 208);
            this.tb_CallBackUrl.Name = "tb_CallBackUrl";
            this.tb_CallBackUrl.Size = new System.Drawing.Size(338, 25);
            this.tb_CallBackUrl.TabIndex = 15;
            this.tb_CallBackUrl.Text = global::FaceTest.Properties.Settings.Default.tb_CallBackUrl;
            // 
            // tb_Path
            // 
            this.tb_Path.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_Path", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_Path.Location = new System.Drawing.Point(168, 123);
            this.tb_Path.Name = "tb_Path";
            this.tb_Path.Size = new System.Drawing.Size(206, 25);
            this.tb_Path.TabIndex = 8;
            this.tb_Path.Text = global::FaceTest.Properties.Settings.Default.tb_Path;
            // 
            // tb_Pass
            // 
            this.tb_Pass.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_Pass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_Pass.Location = new System.Drawing.Point(167, 59);
            this.tb_Pass.Name = "tb_Pass";
            this.tb_Pass.Size = new System.Drawing.Size(206, 25);
            this.tb_Pass.TabIndex = 7;
            this.tb_Pass.Text = global::FaceTest.Properties.Settings.Default.tb_Pass;
            // 
            // tb_Url
            // 
            this.tb_Url.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_Url.Location = new System.Drawing.Point(167, 20);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(206, 25);
            this.tb_Url.TabIndex = 6;
            this.tb_Url.Text = global::FaceTest.Properties.Settings.Default.tb_Url;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 597);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 52;
            this.label7.Text = "人员ID";
            // 
            // tb_PersonFind_PersonId
            // 
            this.tb_PersonFind_PersonId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceTest.Properties.Settings.Default, "tb_PersonFind_PersonId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tb_PersonFind_PersonId.Location = new System.Drawing.Point(232, 592);
            this.tb_PersonFind_PersonId.Name = "tb_PersonFind_PersonId";
            this.tb_PersonFind_PersonId.Size = new System.Drawing.Size(91, 25);
            this.tb_PersonFind_PersonId.TabIndex = 51;
            this.tb_PersonFind_PersonId.Text = global::FaceTest.Properties.Settings.Default.tb_PersonFind_PersonId;
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(9, 584);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(149, 36);
            this.button21.TabIndex = 50;
            this.button21.Text = "查找人员";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 766);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_PersonFind_PersonId);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_PersonAddOrUpdate_PersonName);
            this.Controls.Add(this.tb_PersonAddOrUpdate_PersonId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_PersonDelete_PersonId);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.lb_Path);
            this.Controls.Add(this.tb_DeletePassTimeName);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_HeartBeatUrl);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.tb_SetPassTime_PassTimeName);
            this.Controls.Add(this.tb_SetPassTime_UserId);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.tb_CallBackVerifyUrl);
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
            this.Name = "Form1";
            this.Text = "xFace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        private System.Windows.Forms.TextBox tb_CallBackVerifyUrl;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TextBox tb_SetPassTime_UserId;
        private System.Windows.Forms.TextBox tb_SetPassTime_PassTimeName;
        private System.Windows.Forms.TextBox tb_HeartBeatUrl;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.TextBox tb_DeletePassTimeName;
        private System.Windows.Forms.Label lb_Path;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_PersonDelete_PersonId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_PersonAddOrUpdate_PersonName;
        private System.Windows.Forms.TextBox tb_PersonAddOrUpdate_PersonId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_PersonFind_PersonId;
        private System.Windows.Forms.Button button21;
    }
}

