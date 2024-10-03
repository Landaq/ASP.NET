using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
                case BoardWrtieFormType.Modify:
                    //[b] 수정하기 전용
                    p.Add("@ModifyIp", value: n.ModifyIp, dbType: DbType.String);
                    p.Add("@Id", value: n.Id, dbType: DbType.Int32);

                    r = con.Execute("ModifyNote", p, commandType: CommandType.StoredProcedure);
                    break;
                case BoardWrtieFormType.Reply:
                    //[b] 답변하기 전용
                    p.Add("@PostIp", value: n.PostIp, dbType: DbType.String);
                    p.Add("@ParentNum", value: n.ParentNum, dbType: DbType.Int32);

                    r = con.Execute("ReplyNote", p, commandType: CommandType.StoredProcedure);
                    break;
            }
            return r;
        }

        /// <summary>
        /// 게시판 글쓰기
        /// </summary>
        /// <param name="vm"></param>
        /// <exception cref="System.Exception"></exception>
        public void Add(Note vm)
        {
            try
            {
                SaveOrUpdate(vm, BoardWrtieFormType.Write);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message); // 로깅처리 권장 영역
            }
        }

        /// <summary>
        /// 수정하기
        /// </summary>
        /// <param name="vm"></param>
        /// <exception cref="System.Exception"></exception>
        public void UpdateNtoe(Note vm)
        {
            int r = 0;
            try
            {
                r = SaveOrUpdate(vm, BoardWrtieFormType.Modify);
            }
            catch (Exception ex)
            {
                throw new System.Exception (ex.Message);
            }
        }

        /// <summary>
        /// 답변 글쓰기
        /// </summary>
        /// <param name="vm"></param>
        /// <exception cref="System.Exception"></exception>
        public void ReplyNote(Note vm)
        {
            try
            {
                SaveOrUpdate(vm, BoardWrtieFormType.Reply);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <param name="page">페이지 번호</param>
        /// <returns></returns>
        public List<Note> GetAll(int page)
        {
            try
            {
                var parameters = new DynamicParameters(new {Page= page});
                return con.Query<Note>("ListNotes", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex) 
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// 검색카운트
        /// </summary>
        /// <param name="searchField"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public int GetCountBySearch(string searchField, string searchQuery)
        {
            try
            {
                return con.Query<int>("SearchNoteCount", new
                {
                    SearchField = searchField,
                    SearchQuery = searchQuery
                },
                    commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// Notes 테이블의 모든레코드 수
        /// </summary>
        /// <returns></returns>
        public int GetCountAll()
        {
            try
            {
                return con.Query<int>("SELECT COUNT(*) FROM NOTES").SingleOrDefault();
            }
            catch (Exception ex) 
            {
                return -1;
            }
        }

        /// <summary>
        /// Id에 해당하는 파일명 반환
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFileNameById(int id)
        {
            return
                con.Query<string>("SELECT FileName FROM NOTES WHERE Id = @Id"
                , new { Id = id }).SingleOrDefault();
        }

        /// <summary>
        /// 검색결과 리스트
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchField"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public List<Note> GetSearchAll(int page, string searchField, string searchQuery)
        {
            var paramters = new DynamicParameters(new
            {
                Page = page,
                SearchField = searchField,
                SearchQuery = searchQuery
            });

            return con.Query<Note>("SearchNotes", paramters,
                commandType: CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// 다운카운트 1 증가
        /// </summary>
        /// <param name="fileName"></param>
        public void UpdateDownCount(string fileName)
        {
            con.Execute(@"Update    NOTES
                          SET       DownCount = DownCount + 1
                          Where     FileName = @FileName",
                          new { FileName = fileName });
        }
        public void UpdateDownCountById(int id)
        {
            var p = new DynamicParameters(new { Id = id });
            con.Execute(@"Update    Notes
                          Set       DownCount = DownCount + 1
                          Where     Id = @Id", p,
                          commandType: CommandType.Text);
        }

        /// <summary>
        /// 상세보기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetNoteById(int id)
        {
            var paramters = new DynamicParameters( new { Id = id });
            return con.Query<Note>("ViewNote", paramters,
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int DeleteNote(int id, string password)
        {
            return con.Execute("DeleteNote",
                new { Id = id, Password = password },
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 최근 올라온 사진 리스트 4개 출력
        /// </summary>
        /// <returns></returns>
        public List<Note> GetNewPhotos()
        {
            string sql = @"
                            SELECT  TOP 4 Id,
                                    Title,
                                    FileName,
                                    FileSize
                            From    Notes
                            Where   FileName Like '%.png'
                                    or FileName Like '%.jpg'
                                    or FileName Like '%.jpeg'
                                    or FileName Like '%.gif'
                            Order By Id Desc";
            return con.Query<Note>(sql).ToList();
        }

        /// <summary>
        /// 최근 글 리스트
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Note> GetNoteSummaryByCategory(string category)
        {
            string sql = @"Select TOP 3 Id
                                  ,Title
                                  ,Name
                                  ,PostDate
                                  ,FileName
                                  ,FileSize
                                  ,ReadCount
                                  ,CommentCount
                                  ,Step
                            From    Notes
                            Where   Category = @Category
                            Order by Id Desc";
            return con.Query<Note>(sql, new { Category = category }).ToList();
        }

        /// <summary>
        /// 최근 글 리스트 전체 (최근 글 3개 리스트)
        /// </summary>
        /// <returns></returns>
        public List<Note> GetRecentPosts()
        {
            string sql = @"SELECT   TOP 3 Id
                                    ,Title
                                    ,Name
                                    ,PostDate
                            From    Notes
                            Order by Id Desc";
            return con.Query<Note>(sql).ToList();
        }

        /// <summary>
        /// 최근 글 리스트 n개
        /// </summary>
        /// <param name="numberOfNotes"></param>
        /// <returns></returns>
        public List<Note> GetRecentPosts(int numberOfNotes)
        {
            string sql = $@"SELECT TOP {numberOfNotes} Id
                                    ,Title
                                    ,Name
                                    ,PostDate
                            From    Notes
                            Order by Id Desc";
            return con.Query<Note>(sql).ToList();
        }
    }
}