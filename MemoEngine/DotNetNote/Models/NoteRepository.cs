using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNote.Models
{
    public class NoteRepository
    {
        private SqlConnection con;

        public NoteRepository()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public int SaveOrUpdate(Note n, BoardWrtieFormType formType)
        {
            int r = 0;

            //파라미터 추가
            var p = new DynamicParameters();

            //[a] 공통
            p.Add("@Name", value: n.Name, dbType: DbType.String);
            p.Add("@Email", value: n.Email, dbType: DbType.String);
            p.Add("@Title", value: n.Title, dbType: DbType.String);
            p.Add("@Content", value: n.Content, dbType: DbType.String);
            p.Add("@Password", value: n.Password, dbType: DbType.String);
            p.Add("@Encoding", value: n.Encoding, dbType: DbType.String);
            p.Add("@HomePage", value: n.HomePage, dbType: DbType.String);
            p.Add("@FileName", value: n.FileName, dbType: DbType.String);
            p.Add("@FileSize", value: n.FileSize, dbType: DbType.Int32);

            switch (formType)
            {
                case BoardWrtieFormType.Write:
                    //[b] 글쓰기 전용
                    p.Add("@PostIp", value: n.PostIp, dbType: DbType.String);

                    r = con.Execute("WriteNote", p, commandType: CommandType.StoredProcedure);
                    break;
            }

            return r;
        }
    }
}