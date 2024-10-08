﻿using DevDapper.Models;
using DevDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevDapper.Views
{
    public partial class FrmMaximModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                if(!Page.IsPostBack)
                {
                    DisplayData();
                }
            }
            else
            {
                Response.Write("잘못된 요청입니다.");
                Response.End();
            }
        }

        private void DisplayData()
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);

            MaximServiceRepository repo = new MaximServiceRepository();
            Maxim maxim = repo.GetMaximById(id);

            this.lblId.Text = id.ToString();
            this.txtName.Text= maxim.Name;
            this.txtContent.Text = maxim.Content;
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Maxim maxim = new Maxim();
            // Id를 채워서 넘겨 주자
            maxim.Id= Convert.ToInt32((string)Request.QueryString["Id"]);
            maxim.Name = txtName.Text;
            maxim.Content = txtContent.Text;

            MaximServiceRepository repo = new MaximServiceRepository();
            maxim = repo.UpdateMaxim(maxim);

            lblDisplay.Text =
                maxim.Id.ToString() +"번 데이터가 수정되었습니다.";

            DisplayData();
        }
    }


}