using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevStateManagement
{
    public partial class FrmStateManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 처음 로드 할때만 출력
            if (!IsPostBack)
            {
                // 애플리케이션 변수와 세션 변수는 주로 Global.asax에서 선언
                this.txtApplication.Text = Application["Now"].ToString();
                this.txtSession.Text = Session["Now"].ToString();

                // 지정된 캐시, 쿠키, 뷰상태가 있다면 출력
                if (Cache["Now"] != null) 
                {
                    this.txtCache.Text = Cache["Now"].ToString();
                }
                if (Request.Cookies["Now"] != null) 
                {
                    this.txtCookies.Text = Server.UrlDecode(Request.Cookies["Now"].Value);
                }
                if (ViewState["Now"] != null) 
                {
                    this.txtViewState.Text = ViewState["Now"].ToString();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 각각의 상태 변수에 데이터 저장
            Application["Now"] = this.txtApplication.Text;
            Session["Now"] = this.txtSession.Text;
            Cache["Now"] = this.txtCache.Text;
            Response.Cookies["Now"].Value = Server.UrlEncode(txtCookies.Text);
            ViewState["Now"] = this.txtViewState.Text;
            Response.Redirect("FrmStateShow.aspx");
        }
    }
}