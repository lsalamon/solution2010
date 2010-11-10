

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.chatstates
{
    /// <summary>
    /// User has not been actively participating in the chat session.
    /// User has not interacted with the chat interface for an intermediate period of time (e.g., 30 seconds).
    /// </summary>
    public class Inactive : Element
    {
        /// <summary>
        /// 
        /// </summary>
        public Inactive()
        {
            this.TagName    = Chatstate.inactive.ToString(); ;
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}
