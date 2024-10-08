﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevUser
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // [!] 인증 여부 확인 : 로그인했으면 참, 그렇지 않으면 거짓을 반환
            if (Page.User.Identity.IsAuthenticated)
            {
                // [!] 인증 이름 출력
                lblName.Text = Page.User.Identity.Name;
            }
            else
            {
                Response.Redirect("~/Login.aspx"); 
            }
        }
    }
}