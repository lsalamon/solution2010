using STalk.DataModule;

namespace STalk.IDataProvider
{
    public interface IUserInfoProvider
    {
        UserInfo GetUserInfoByNickName(string nickName);

        UserInfo GetUserInfoByUserID(int userID);

        void UpdateUserInfo(UserInfo userInfo);

        bool IsExistsNickName(string nickName);

        void InsertUserInfo(UserInfo userInfo);
    }
}
