using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using STalk.DataModule;
using STalk.IDataProvider;

namespace STalk.MSSQLProvider
{
    public class UserFriendProvider:IUserFriendProvider
    {
        private string connString = SqlHelper.CONN_STRING;
        public IList<UserFirend> GetFirendListByUserID(int userID)
        {
            IList<UserFirend> result = new List<UserFirend>();

            string sql = @"SELECT TOP (100) PERCENT dbo.Tb_UserFriend.*, dbo.Tb_UserInfo.NickName AS UNickName, dbo.Tb_UserGroup.GroupName, dbo.Tb_UserGroup.SortNum
FROM         dbo.Tb_UserFriend INNER JOIN
                      dbo.Tb_UserInfo ON dbo.Tb_UserFriend.FriendID = dbo.Tb_UserInfo.UserID INNER JOIN
                      dbo.Tb_UserGroup ON dbo.Tb_UserFriend.GroupID = dbo.Tb_UserGroup.ID where dbo.Tb_UserFriend.UserID=@UserID
ORDER BY dbo.Tb_UserGroup.SortNum DESC";

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
            while (row.Read())
            {
                UserFirend ufInfo = new UserFirend();
                ufInfo.ID = row.GetInt32("ID");
                ufInfo.FriendID = row.GetInt32("FriendID");
                ufInfo.GroupID = row.GetInt32("GroupID");
                ufInfo.GroupName = row.GetString("GroupName");
                ufInfo.NickName = string.IsNullOrEmpty(row.GetString("NickName")) ? row.GetString("UNickName") : row.GetString("NickName");
                ufInfo.Subscription = row.GetString("Subscription");
                ufInfo.UserID = row.GetInt32("UserID");

                result.Add(ufInfo);
            }
            dr.Close();
            return result;
        }

        public bool IsExitsFirendID(int userID,int friendID)
        {
            bool result = true;
            string sql = "select count(ID) from Tb_UserFriend where UserID=@UserID and FriendID=@FriendID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                    new SqlParameter("@FriendID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = friendID;

            int rows = Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, sql, parms));
            result = rows > 0;

            return result;
        }

        public void InsertFirend(UserFirend ufInfo)
        {
            if (IsExitsFirendID(ufInfo.UserID, ufInfo.FriendID))
            {
                return;
            }

            string sql = @"Insert into Tb_UserFriend (UserID,FriendID,GroupID,Subscription,NickName)
                            values (@UserID,@FriendID,@GroupID,@Subscription,@NickName)";

            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                     new SqlParameter("@FriendID",SqlDbType.BigInt),
                     new SqlParameter("@GroupID",SqlDbType.Int),
                     new SqlParameter("@Subscription",SqlDbType.NVarChar,10),
                     new SqlParameter("@NickName",SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = ufInfo.UserID;
            parms[1].Value = ufInfo.FriendID;
            parms[2].Value = ufInfo.GroupID;
            parms[3].Value = ufInfo.Subscription;
            parms[4].Value = ufInfo.NickName;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public void DeleteFirendByFriendID(int userID,int friendID)
        {
            string sql = "Delete from Tb_UserFriend where UserID=@UserID and FriendID=@FriendID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                     new SqlParameter("@FriendID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = friendID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }


        public void UpdateFriendNickName(int userID, int friendID, string nickName)
        {
            string sql = "Update Tb_UserFriend set NickName=@NickName where UserID=@UserID and FriendID=@FriendID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@NickName",SqlDbType.NVarChar,20),
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                     new SqlParameter("@FriendID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = nickName;
            parms[1].Value = userID;
            parms[2].Value = friendID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public void UpdateFriendSubscription(int userID, int friendID, string subscription)
        {
            string sql = "Update Tb_UserFriend set Subscription=@Subscription where UserID=@UserID and FriendID=@FriendID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@Subscription",SqlDbType.NVarChar,10),
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                     new SqlParameter("@FriendID",SqlDbType.BigInt)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = subscription;
            parms[1].Value = userID;
            parms[2].Value = friendID;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }
    }
}
