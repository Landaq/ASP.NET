﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web.Configuration;
using DevUser.Models;

namespace DevUser.Repositories
{
    public class UserRepository
    {
        // 공통으로 사용될 커넥션 개체
        private SqlConnection con;

        public UserRepository()
        {
            con = new SqlConnection();
            con.ConnectionString = WebConfigurationManager.ConnectionStrings[
                "ConectionString"].ConnectionString;
        }

        public void AddUser(string userId, string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "WriteUsers";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public UserViewModel GetUserByUserId(string userId)
        {
            UserViewModel r = new UserViewModel();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"
                                select  *
                                From    dbo.Users
                                Where   UserID = @UserID
                                ";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@UserID", userId);

            con.Open();
            IDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                r.Id = dr.GetInt32(0);
                r.UserId = dr.GetString(1);
                r.Password = dr.GetString(2);
            }
            con.Close();

            return r;
        }
    }
}