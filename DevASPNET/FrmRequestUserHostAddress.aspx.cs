using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevASPNET
{
    public partial class FrmRequestUserHostAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 웹폼에서 현재 접속자의 IP 주소 얻기
            this.Label1.Text = Request.UserHostAddress; // 추천
            this.Label2.Text =
                Request.ServerVariables["REMOTE_HOST"];
            this.Label3.Text =
                Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}