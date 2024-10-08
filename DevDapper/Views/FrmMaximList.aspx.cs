﻿using DevDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevDapper.Views
{
    public partial class FrmMaximList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DisplayData();
            }
        }

        private void DisplayData()
        {
            MaximServiceRepository repo = new MaximServiceRepository();
            this.lstMaxims.DataSource = repo.GetMaxims();
            this.lstMaxims.DataBind();
        }
    }
}