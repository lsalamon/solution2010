using System;
using System.Collections.Generic;
using System.Text;

namespace STalk.DataModule
{
    public class UserFirend
    {
        private int m_ID = 0;
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        private int m_UserID = 0;
        public int UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private int m_FriendID=0;
        public int FriendID
        {
            get { return m_FriendID; }
            set { m_FriendID = value; }
        }

        private int m_GroupID = 0;
        /// <summary>
        /// 0为默认的未分组 -1黑名单
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }

        private string m_GroupName = string.Empty;
        public string GroupName
        {
            get { return m_GroupName; }
            set { m_GroupName = value; }
        }

        private string m_NickName = string.Empty;
        /// <summary>
        /// 对好友名字的备注
        /// </summary>
        public string NickName
        {
            get { return m_NickName; }
            set { m_NickName = value; }
        }
    }
}
