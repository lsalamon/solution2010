

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.ibb
{
    /*
         <close xmlns='http://jabber.org/protocol/ibb' sid='mySID'/>      
    */

    /// <summary>
    /// 
    /// </summary>
    public class Close : Base
    {
        /// <summary>
        /// 
        /// </summary>
        public Close()
        {
            this.TagName = "close";           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        public Close(string sid) : this()
        {
            this.Sid = sid;            
        }       
    }
}