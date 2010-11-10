using System;
using System.Collections.Generic;
using System.Text;

namespace STalk.DataModule
{
    public class User
    {
        private int m_UserID = 0;
        public int UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private string m_UserName = string.Empty;
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        private string m_UserPwd = string.Empty;
        public string UserPwd
        {
            get { return m_UserPwd; }
            set { m_UserPwd = value; }
        }

        private DateTime m_RegTime = DateTime.Now;
        public DateTime RegTime
        {
            get { return m_RegTime; }
            set { m_RegTime = value; }
        }

        private string m_LastLoginIP = string.Empty;
        public string LastLoginIP
        {
            get { return m_LastLoginIP; }
            set { m_LastLoginIP = value; }
        }

        private uint m_Status = 0;
        public uint Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        private DateTime m_LastLoginTime = DateTime.Now;
        public DateTime LastLoginTime
        {
            get { return m_LastLoginTime; }
            set { m_LastLoginTime = value; }
        }

        private string m_Server = string.Empty;
        /// <summary>
        /// 所在服务器
        /// </summary>
        public string Server
        {
            get { return m_Server; }
            set { m_Server = value; }
        }
    }
}
