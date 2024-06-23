using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevStandardControl
{
    public partial class FrmlMultiView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // [1] 첫번째(인덱스:0) 뷰컨트롤을 활성화
                ctlMultiView1.ActiveViewIndex = 0;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // [2] 두번째 뷰 컨트롤 활성화
            this.ctlMultiView1.ActiveViewIndex = 1;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // [3] 첫번째 뷰 컨트롤 활성화
            this.ctlMultiView1.ActiveViewIndex = 0;
        }
    }
}