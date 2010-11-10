using System;
using System.Collections.Generic;
using System.Text;
using STalk.DataModule;

namespace STalk.IDataProvider
{
    public interface IUserProvider
    {
        User GetUserByUserName(string userName);

        User GetUserByUserID(int userID);

        void UpdateUserPwdByUserID(int userID, string userPwd);

        User CheckUserLogin(User user);

        void UpdateUserStatusByUserID(int userID, uint Status);

        bool IsExistsUserName(string userName);

        void InsertUser(User user);
    }
}
