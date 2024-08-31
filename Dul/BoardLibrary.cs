namespace Dul
{
    public class BoardLibrary
    {
        #region 각 글의 Step별 들여쓰기 처리
        /// <summary>
        /// 각 글의 Step별 들여쓰기 처리
        /// </summary>
        /// <param name="objStep">1, 2, 3</param>
        /// <returns>Re 이미지를 포함한 Step만큼 들여쓰기</returns>
        public static string FuncStep(object objStep)
        {
            int intStep = Convert.ToInt32(objStep);
            string strTemp = String.Empty;
            if (intStep == 0)
            {
                strTemp = String.Empty;
            }
            else
            {
                for (int i = 0; i < intStep; i++)
                {
                    strTemp = String.Format(
                        $"<img src={0} height={1} width={2}>"
                        , "/images/dnn/blank.gif", "0", (intStep * 15));
                }
                strTemp += $"<img src =\"/images/dnn/re.gif\">";
            }
            return strTemp;
        }
        #endregion

        #region 댓글 개수를 표현하는 메서드
        /// <summary>
        /// 댓글개수를 표현하는 메서드
        /// </summary>
        /// <param name="objCommentCount">댓글 수</param>
        /// <returns>댓글 이미지와 함께 숫자 표시</returns>
        public static string ShowCommentCount(object objCommentCount)
        {
            string strTemp = "";
            try
            {
                if (Convert.ToInt32(objCommentCount) >0)
                {
                    strTemp = "<img src = \"/images/dnn/commentcount.gif\" />";
                    strTemp += "(" + objCommentCount.ToString() + ")";
                }
            }
            catch (Exception)
            {
                strTemp = "";
            }
            return strTemp;
        }
        #endregion
    }
}
