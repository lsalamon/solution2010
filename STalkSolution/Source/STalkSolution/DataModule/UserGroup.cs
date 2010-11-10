using System;
using System.Collections.Generic;
using System.Text;

namespace STalk.DataModule
{
    public class UserGroup
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

        private string m_GroupName = string.Empty;
        public string GroupName
        {
            get { return m_GroupName; }
            set { m_GroupName = value; }
        }

        private int m_SortNum = 0;
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum
        {
            get { return m_SortNum; }
            set { m_SortNum = value; }
        }
    }
}
