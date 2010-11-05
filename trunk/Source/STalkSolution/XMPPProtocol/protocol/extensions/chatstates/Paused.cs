

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.chatstates
{
    /// <summary>
    /// User had been composing but now has stopped.
    /// User was composing but has not interacted with the message input interface for a short period of time (e.g., 5 seconds).
    /// </summary>
    public class Paused : Element
    {
        /// <summary>
        /// 
        /// </summary>
        public Paused()
        {
            this.TagName    = Chatstate.paused.ToString(); ;
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}
