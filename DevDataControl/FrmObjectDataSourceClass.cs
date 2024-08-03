using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace DevDataControl
{
    public class FrmObjectDataSourceClass
    {
        public SqlDataReader GetMemos()
        {
            SqlConnection objCon = new SqlConnection();

            objCon.ConnectionString = ConfigurationManager.ConnectionStrings["DevADONETConnectionString"].ConnectionString;
            objCon.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objCon;
            objCmd.CommandText = "ListMemo";
            objCmd.CommandType = CommandType.StoredProcedure;

            return objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}