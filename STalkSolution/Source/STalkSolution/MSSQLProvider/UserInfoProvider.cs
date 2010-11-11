using System;
using System.Data;
using System.Data.SqlClient;
using STalk.DataModule;
using STalk.IDataProvider;

namespace STalk.MSSQLProvider
{
    public class UserInfoProvider : IUserInfoProvider
    {
        private string connString = SqlHelper.CONN_STRING;

        private UserInfo FillUserInfo(RowHelper row)
        {
            UserInfo userInfo = null;

            if (row.Read())
            {
                userInfo = new UserInfo()
                {
                    Age = row.GetInt32("Age"),
                    BirthDay = row.GetDateTime("BirthDay"),
                    Email = row.GetString("Email"),
                    NickName = row.GetString("NickName"),
                    Sex = row.GetInt16("Sex"),
                    UserID = row.GetInt32("UserID")
                };
            }

            return userInfo;
        }

        public DataModule.UserInfo GetUserInfoByNickName(string nickName)
        {
            string sql = "SELECT * FROM Tb_UserInfo WHERE NickName=@NickName";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@NickName",SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = nickName;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(connString, CommandType.Text, sql, parms))
            {
                return FillUserInfo(new RowHelper(dr));
            }
        }

        public DataModule.UserInfo GetUserInfoByUserID(int userID)
        {
            string sql = "SELECT * FROM Tb_UserInfo WHERE UserID=@UserID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userID;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(connString, CommandType.Text, sql, parms))
            {
                return FillUserInfo(new RowHelper(dr));
            }
        }

        public void UpdateUserInfo(DataModule.UserInfo userInfo)
        {
            string sql = "UPDATE Tb_UserInfo set Email=@Email, NickName=@NickName, Sex=@Sex, BirthDay=@BirthDay, Age=@Age where UserID=@UserID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@Email",SqlDbType.NVarChar,50),
                    new SqlParameter("@NickName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Sex",SqlDbType.SmallInt),
                    new SqlParameter("@BirthDay",SqlDbType.Date),
                    new SqlParameter("@Age",SqlDbType.Int),
                    new SqlParameter("@UserID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userInfo.Email;
            parms[1].Value = userInfo.NickName;
            parms[2].Value = userInfo.Sex;
            parms[3].Value = userInfo.BirthDay;
            parms[4].Value = userInfo.Age;
            parms[5].Value = userInfo.UserID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public bool IsExistsNickName(string nickName)
        {
            bool result = true;
            string sql = "SELECT COUNT(UserID) FROM Tb_UserInfo WHERE NickName=@NickName";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@NickName",SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = nickName;
            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, sql, parms));
            result = rows > 0;
            return result;
        }

        public void InsertUserInfo(DataModule.UserInfo userInfo)
        {
            string sql = "INSERT INTO Tb_UserInfo (UserID, Email, NickName, Sex, BirthDay, Age) values (@UserID, @Email, @NickName, @Sex, @BirthDay, @Age)";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                    new SqlParameter("@Email",SqlDbType.NVarChar,50),
                    new SqlParameter("@NickName",SqlDbType.NVarChar,20),
                    new SqlParameter("@Sex",SqlDbType.SmallInt),
                    new SqlParameter("@BirthDay",SqlDbType.Date),
                    new SqlParameter("@Age",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }
    }
}
