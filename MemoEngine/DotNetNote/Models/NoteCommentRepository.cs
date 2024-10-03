using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DotNetNote.Models
{
    public class NoteCommentRepository
    {
        private SqlConnection con;

        public NoteCommentRepository()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        /// <summary>
        /// 특정 게시물에 댓글 추가
        /// </summary>
        /// <param name="model"></param>
        public void AddNoteComment(NoteComment model)
        {
            //파라미터 추가
            var parameters = new DynamicParameters();
            parameters.Add("@BoardId", value: model.BoardId, dbType: DbType.Int32);
            parameters.Add("@Name", value: model.Name, dbType: DbType.String);
            parameters.Add("@Opinion", value: model.Opinion, dbType: DbType.String);
            parameters.Add("@Password", value: model.Password , dbType: DbType.String);

            string sql = @"
                Insert Into NoteComments (BoardId, Name, Opinion, Password)
                Values  (@BoardId, @Name, @Opinion, @Password);
                Update  Notes
                Set     CommentCount = CommentCount + 1
                Where   Id = @BoardId
            ";

            con.Execute(sql, parameters, commandType: CommandType.Text);
        }

        /// <summary>
        /// 특정 게시물에 해당하는 댓글 리스트
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        public List<NoteComment> GetNoteComments(int boardId)
        {
            return con.Query<NoteComment>(
                    @"
                    SELECT  *
                    From    NoteComments
                    Where   BoardId = @BoardId
                    ", new { BoardId = boardId }, commandType: CommandType.Text ).ToList();
        }


        /// <summary>
        /// 특정 게시물의 특정 Id에 해당하는 댓글 카운트
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int GetCountBy(int boardId, int id, string password) 
        {
            return con.Query<int>(
                    @"
                    SELECT  COUNT(*)
                    FROM    NoteComments
                    Where   BoardId = @BoardId
                            and Id = @Id
                            and Password = @Password
                    ",new { BoardId = boardId, Id = id, Password = password}
                    ,commandType: CommandType.Text).SingleOrDefault();
        }

        /// <summary>
        /// 댓글 삭제
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int DeleteNoteComment(int boardId, int id, string password)
        {
            return con.Execute(
                @"
                Delete  NoteComments
                Where   BoardId = @BoardId
                        and Id = @Id
                        and Password = @Password
                ", new { BoardId = boardId, Id = id, Password = password }
                 , commandType: CommandType.Text);
        }

        public List<NoteComment> GetRecentComments()
        {
            string sql = @"
                SELECT  TOP 3 Id
                        ,BoardId
                        ,Opinion
                        ,PostDate
                From    NoteComments
                Order by Id Desc
            ";
            return con.Query<NoteComment>(sql).ToList();
        }
    }
}