using System;
using System.Collections.Generic;
using System.Text;

namespace STalk.DataModule
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
    }
}
