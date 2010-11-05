

using System;

namespace XMPPProtocol.exceptions
{
    public class JidException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public JidException() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public JidException(string msg) : base(msg)
        {

        }
    }
}
