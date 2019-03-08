namespace FaceTest
{
    partial class FrmServer
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
            this.labPersonId = new System.Windows.Forms.Label();
            this.labStaff = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picFacePic = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.receiveMsg = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picFacePic)).BeginInit();
            this.SuspendLayout();
            // 
            // labPersonId
            // 
            this.labPersonId.AutoSize = true;
            this.labPersonId.Location = new System.Drawing.Point(175, 49);
            this.labPersonId.Name = "labPersonId";
            this.labPersonId.Size = new System.Drawing.Size(55, 15);
            this.labPersonId.TabIndex = 0;
            this.labPersonId.Text = "label1";
            // 
            // labStaff
            // 
            this.labStaff.AutoSize = true;
            this.labStaff.Location = new System.Drawing.Point(175, 97);
            this.labStaff.Name = "labStaff";
            this.labStaff.Size = new System.Drawing.Size(55, 15);
            this.labStaff.TabIndex = 1;
            this.labStaff.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // picFacePic
            // 
            this.picFacePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFacePic.Location = new System.Drawing.Point(287, 56);
            this.picFacePic.Name = "picFacePic";
            this.picFacePic.Size = new System.Drawing.Size(105, 98);
            this.picFacePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFacePic.TabIndex = 55;
            this.picFacePic.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(60, 218);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 29);
            this.btnStart.TabIndex = 56;
            this.btnStart.Text = "button1";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // receiveMsg
            // 
            this.receiveMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.receiveMsg.Location = new System.Drawing.Point(409, 0);
            this.receiveMsg.Margin = new System.Windows.Forms.Padding(4);
            this.receiveMsg.Name = "receiveMsg";
            this.receiveMsg.Size = new System.Drawing.Size(853, 652);
            this.receiveMsg.TabIndex = 57;
            this.receiveMsg.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 58;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 652);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.receiveMsg);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.picFacePic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labStaff);
            this.Controls.Add(this.labPersonId);
            this.Name = "FrmServer";
            this.Text = "FrmServer";
            this.Load += new System.EventHandler(this.FrmServer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFacePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPersonId;
        private System.Windows.Forms.Label labStaff;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picFacePic;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox receiveMsg;
        private System.Windows.Forms.Button button1;
    }
}