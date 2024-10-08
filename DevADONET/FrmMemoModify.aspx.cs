﻿using DevADONET.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevADONET
{
    public partial class FrmMemoModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 넘어온 스트링 값이 없다면
            if (String.IsNullOrEmpty(Request["Num"]))
            {
                Response.Write("잘못된 요청입니다.");
                Response.End();
            }
            else
            {
                // 처음 로드 시만 읽어 오고, 수정 버튼 클릭 시에는 해당 기능만 구현
                if (!IsPostBack)
                {
                    DisplayData();
                }
            }
        }
        private void DisplayData()
        {
            // [1] 커넥션
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();

            // [2] 커맨드
            SqlCommand cmd = new SqlCommand("ViewMemo",con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            // [!] 파라미터 추가
            cmd.Parameters.Add("Num", SqlDbType.Int); // 정수형
            cmd.Parameters["Num"].Value = Convert.ToInt32(Request["Num"]);

            // [3] 데이터 리더
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) 
            {
                // [!] 각각의 컨트롤에 바인딩
                this.lblNum.Text = Request["Num"];
                this.txtName.Text = dr["Name"].ToString();
                this.txtEmail.Text = dr[2].ToString();
                this.txtTitle.Text = dr.GetString(3);
            }
            else
            {
                Response.Write("없는데이터 입니다.");
                Response.End();
            }

            // [4] 마무리
            dr.Close();
            con.Close();
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            // [0] 변수 선언부
            Memo memo = new Memo();
            memo.Num = Convert.ToInt32(Request["Num"]);
            memo.Name = txtName.Text;
            memo.Email = txtEmail.Text;
            memo.Title = txtTitle.Text;
            // [1] 커넥션
            SqlConnection con = new SqlConnection(ConfigurationManager
                .ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();

            // [2] 커맨드
            SqlCommand cmd = new SqlCommand("ModifyMemo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            // [!] 파라미터 추가
            cmd.Parameters.AddWithValue("@Name", memo.Name);
            cmd.Parameters.AddWithValue("@Email", memo.Email);
            cmd.Parameters.AddWithValue("@Title", memo.Title);
            cmd.Parameters.AddWithValue("@Num",memo.Num);
            // [!]실행
            cmd.ExecuteNonQuery();
            // [3] 마무리
            con.Close();
            Response.Redirect("FrmMemoView.aspx?Num=" + Request["Num"]);
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            // 리스트 페이지로 이동
            Response.RedirectPermanent("FrmMemoList.aspx");
        }
    }
}