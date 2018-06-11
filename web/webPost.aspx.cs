using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using System.IO;

    public partial class webPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResultInfo result = new ResultInfo();
            result.msg = "test";
            ReturnPost(result);
        }
        public void ReturnPost(object oReturn)
        {
            string sJsonReturn = JsonConvert.SerializeObject(oReturn);
            byte[] bReturn = System.Text.Encoding.UTF8.GetBytes(sJsonReturn);
            Stream webStream = Response.OutputStream;

            webStream.Write(bReturn, 0, bReturn.Length);

            webStream.Close();
        }
    }

