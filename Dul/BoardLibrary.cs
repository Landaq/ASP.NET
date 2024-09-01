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

        #region 24시간 내에 올라온 글 new 이미지 표시하기
        /// <summary>
        /// 24시간 내에 올라온 글 new 이미지 표시하기
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string FuncNew(object strDate)
        {
            if (strDate != null)
            {
                if (!String.IsNullOrEmpty(strDate.ToString()))
                {
                    DateTime originDate = Convert.ToDateTime(strDate);

                    TimeSpan objTs = DateTime.Now - originDate;
                    string newImage = "";
                    if(objTs.TotalMinutes < 1440)
                    {
                        newImage = "<img src= \"/images/dnn/new.gif\">";
                    }
                    return newImage;
                }
            }
            return "";
        }
        #endregion

        #region 넘겨온 날짜 형식이 오늘 날짜면 시간표시
        /// <summary>
        /// 넘겨온 날짜 형식이 오늘 날짜면 시간 표시,
        /// 그렇지 않으면 날짜 표시
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FuncShowTime(Object date)
        {
            if (!string.IsNullOrEmpty(date.ToString()))
            {
                string strPostDate =
                    Convert.ToDecimal(date).ToString("yyyy-MM-dd");
                if (strPostDate == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    return Convert.ToDateTime(date).ToString("hh:mm:ss");
                }
                else
                {
                    return strPostDate;
                }
            }
            return "-";
        }
        #endregion

        #region ConvertToFileSize() 함수
        /// <summary>
        /// 파일크기를 계산해서 알맞은 단위로 변환해줌. (바이트 수)
        /// </summary>
        /// <param name="intByte"></param>
        /// <returns></returns>
        public static string ConvertToFileSize(int intByte)
        {
            int intFileSize = Convert.ToInt32(intByte);
            string strResult = "";
            if ( intFileSize >= 1048576)
            {
                strResult = string.Format("{0:F} MB", (intByte / 1048576));
            }
            else
            {
                if(intFileSize >= 1024)
                {
                    strResult = string.Format("{0} KB", (intByte / 1024));
                }
                else
                {
                    strResult = intByte + " Byte(s)";
                }
            }
            return strResult;
        }
        #endregion

        #region IsPhto() 함수
        #endregion
    }
}
