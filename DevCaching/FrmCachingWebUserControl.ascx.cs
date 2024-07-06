using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevCaching
{
    public partial class FrmCachingWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 현재 시간 출력: 5초에 한 번씩만 출력
            lblTimeWebUserControl.Text = DateTime.Now.ToString();
        }
    }
}