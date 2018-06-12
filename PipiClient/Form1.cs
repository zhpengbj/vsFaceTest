using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Pipes;
using System.IO;
using System.Security.Principal;

namespace PipiClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "FaceTestPip", PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
        StreamWriter sw = null;

        private void button2_Click(object sender, EventArgs e)
        {
            //NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "FaceTestPip", PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
            //StreamWriter sw = new StreamWriter(pipeClient);
            ////连接pipi，准备发送消息
            //pipeClient.Connect();
            //sw.AutoFlush = true;
            sw.WriteLine(DateTime.Now.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pipeClient.Connect();
            sw = new StreamWriter(pipeClient);
            sw.AutoFlush = true;

        }
    }
}
