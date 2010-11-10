using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using STalk.DataModule;
using STalk.IDataProvider;

namespace STalk.MSSQLProvider
{
    public class UserProvider : IUserProvider
    {
        private string connString = SqlHelper.CONN_STRING;

        public User GetUserByUserName(string userName)
        {
            User user = new User();
            string sql = "select * from Tb_User where UserName=@UserName";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserName",SqlDbType.NVarChar,15)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userName;

            SqlDataReader dr = SqlHelper.ExecuteReader(connString, CommandType.Text, sql, parms);
             RowHelper row=new RowHelper(dr);
            if (row.Read())
            {
                user.UserID = row.GetInt32("UserID");
                user.LastLoginIP = row.GetString("LastLoginIP");
                user.LastLoginTime = row.GetDateTime("LastLoginTime");
                user.RegTime = row.GetDateTime("RegTime");
                user.Server = row.GetString("Server");
                user.Status = row.GetUInt32("Status");
                user.UserName = row.GetString("UserName");
                user.UserPwd = row.GetString("UserPwd");
            }

            return user;
        }

        public User GetUserByUserID(int userID)
        {
            User user = new User();
            string sql = "select * from Tb_User where UserID=@UserID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userID;

            SqlDataReader dr = SqlHelper.ExecuteReader(connString, CommandType.Text, sql, parms);
            RowHelper row = new RowHelper(dr);
            if (row.Read())
            {
                user.UserID = row.GetInt32("UserID");
                user.LastLoginIP = row.GetString("LastLoginIP");
                user.LastLoginTime = row.GetDateTime("LastLoginTime");
                user.RegTime = row.GetDateTime("RegTime");
                user.Server = row.GetString("Server");
                user.Status = row.GetUInt32("Status");
                user.UserName = row.GetString("UserName");
                user.UserPwd = row.GetString("UserPwd");
            }

            return user;
        }

        public void UpdateUserPwdByUserID(int userID, string userPwd)
        {
            throw new NotImplementedException();
        }

        public User CheckUserLogin(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserStatusByUserID(int userID, uint Status)
        {
            throw new NotImplementedException();
        }


        public bool IsExistsUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
