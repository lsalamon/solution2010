using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using STalk.DataModule;
using STalk.IDataProvider;

namespace STalk.MSSQLProvider
{
    public class UserGroupProvider : IUserGroupProvider
    {
        private string connString = SqlHelper.CONN_STRING;

        public IList<UserGroup> GetUserGroupListByUserID(int userID)
        {
            IList<UserGroup> result = new List<UserGroup>();

            string sql = @"SELECT * FROM Tb_UserGroup WHERE UserID=@UserID ORDER BY SortNum";

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
                RowHelper row = new RowHelper(dr);
                while (row.Read())
                {
                    result.Add(new UserGroup()
                    {
                        GroupName = row.GetString("GroupName"),
                        ID = row.GetInt32("ID"),
                        SortNum = row.GetInt32("SortNum"),
                        UserID = row.GetInt32("UserID")
                    });
                }
                dr.Close();
            }
            return result;
        }

        public void InsertGroup(UserGroup userGroup)
        {
            string sql = @"INSERT INTO Tb_UserGroup (UserID, GroupName, SortNum)
                            VALUES (@UserID, @GroupName, @SortNum)";

            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@UserID",SqlDbType.BigInt),
                     new SqlParameter("@GroupName",SqlDbType.NVarChar, 20),
                     new SqlParameter("@SortNum",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userGroup.UserID;
            parms[1].Value = userGroup.GroupName;
            parms[2].Value = userGroup.SortNum;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public void UpdateGroupSortNum(int userID, int id, int sortNum)
        {
            string sql = "sp_update_group_sort";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@userid",SqlDbType.Int),
                    new SqlParameter("@id",SqlDbType.Int),
                    new SqlParameter("@sort",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = userID;
            parms[1].Value = id;
            parms[2].Value = sortNum;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, sql, parms);
        }

        public void UpdateGroupName(int id, string name)
        {
            string sql = "UPDATE Tb_UserGroup SET GroupName=@GroupName WHERE ID=@ID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@GroupName",SqlDbType.NVarChar, 20),
                    new SqlParameter("@ID",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = id;
            parms[1].Value = name;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }

        public void DeleteGroup(int id)
        {
            string sql = "DELETE FROM Tb_UserGroup WHERE ID=@ID";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(sql);
            if (parms == null)
            {
                parms = new SqlParameter[] { 
                    new SqlParameter("@ID",SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(sql, parms);
            }

            parms[0].Value = id;

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, sql, parms);
        }
    }
}
