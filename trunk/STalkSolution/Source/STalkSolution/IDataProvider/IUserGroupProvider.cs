using System.Collections.Generic;
using STalk.DataModule;

namespace STalk.IDataProvider
{
    public interface IUserGroupProvider
    {
        IList<UserGroup> GetUserGroupListByUserID(int userID);

        void InsertGroup(UserGroup userGroup);

        void UpdateGroupSortNum(int userID, int id, int sortNum);

        void UpdateGroupName(int id, string name);

        void DeleteGroup(int id);
    }
}
