using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevCaching
{
    public partial class FrmCaching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 현재 시간 출력: 매번 바로 출력
            lblTimeWebForms.Text = DateTime.Now.ToString();
        }
    }
}