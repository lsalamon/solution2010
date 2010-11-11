using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using STalk.DataModule;

namespace STalk.IDataProvider
{
    public interface IUserFriendProvider
    {
        IList<UserFirend> GetFirendListByUserID(int userID);

        bool IsExitsFirendID(int userID,int friendID);

        void InsertFirend(UserFirend ufInfo);

        void DeleteFirendByFriendID(int userID, int friendID);

        void UpdateFriendNickName(int userID, int friendID, string nickName);

        void UpdateFriendSubscription(int userID, int friendID, string subscription);
    }
}
