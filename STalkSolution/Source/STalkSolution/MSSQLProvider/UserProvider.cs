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

        public void UpdateUserLoginInfo(User user)
        {
            string sql = "Update Tb_User set LastLoginIP=@LastLoginIP,LastLoginTime=getdate(),Server=@Server where UserID=@UserID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@LastLoginIP",SqlDbType.NVarChar,20),
                    new SqlParameter("@Server",SqlDbType.NVarChar,50),
                    new SqlParameter("@UserID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = user.LastLoginIP;
            parms[1].Value = user.Server;
            parms[2].Value = user.UserID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public void UpdateUserPwdByUserID(int userID, string userPwd)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserStatusByUserID(int userID, uint status)
        {
            string sql = "Update Tb_User set Status=@Status where UserID=@UserID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@UserID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = status;
            parms[1].Value = userID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExistsUserName(string userName)
        {
            bool result = true;
            string sql = "select count(UserID) from Tb_User where UserName=@UserName";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserName",SqlDbType.NVarChar,15)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userName;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, sql, parms));
            result = rows > 0;
            return result;
        }

        public void InsertUser(User user)
        {
            bool isExist = IsExistsUserName(user.UserName);
            if (isExist)
                return;

            string sql = "Insert into Tb_User (UserName,UserPwd,LastLoginIP,LastLoginTime,RegTime) values (@UserName,@UserPwd,@LastLoginIP,getdate(),getdate())";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserName",SqlDbType.NVarChar,15),
                    new SqlParameter("@UserPwd",SqlDbType.NVarChar,32),
                    new SqlParameter("@LastLoginIP",SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }
    }
}
