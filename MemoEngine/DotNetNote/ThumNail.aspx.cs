using System;
using System.Drawing;

namespace MemoEngine.DotNetNote
{
    /// <summary>
    /// ThumbNail : 축소판 이미지 생성기
    /// </summary>
    public partial class ThumNail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 변수 초기화
            int boxWidth = 100;
            int boxHeight = 100;
            double scale = 0;

            // 파일이름 설정
            string fileName = String.Empty;
            string selectedFile = String.Empty;

            if (Request["FileName"] != null)
            {
                selectedFile = Request.QueryString["FileName"];
                fileName = Server.MapPath("./MyFiles/" + selectedFile);
            }
            else
            {
                selectedFile = "/images/dnn/img.jpg"; // 기본 이미지로 초기화
                fileName = Server.MapPath("/images/dnn/img.jpg");
            }

            int tmpW = 0;
            int tmpH = 0;

            if (Request.QueryString["Width"] != null
                && Request.QueryString["Height"] != null) 
            {
                tmpW = Convert.ToInt32(Request.QueryString["Width"]);
                tmpH = Convert.ToInt32(Request.QueryString["Height"]);
            }



        }
    }
}