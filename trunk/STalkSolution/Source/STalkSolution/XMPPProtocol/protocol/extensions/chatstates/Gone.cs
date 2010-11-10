

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.chatstates
{
    /// <summary>
    /// User has effectively ended their participation in the chat session.
    /// User has not interacted with the chat interface, system, or device for a relatively long period of time 
    /// (e.g., 2 minutes), or has terminated the chat interface (e.g., by closing the chat window).
    /// </summary>
    public class Gone : Element
    {
        /// <summary>
        ///        
        /// </summary>
        public Gone()
        {
            this.TagName    = Chatstate.gone.ToString(); ;
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}
