

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.chatstates
{
    /// <summary>
    /// User is actively participating in the chat session.
    /// User accepts an initial content message, sends a content message, 
    /// gives focus to the chat interface, or is otherwise paying attention to the conversation.
    /// </summary>
    public class Active : Element
    {
        /// <summary>
        /// 
        /// </summary>
        public Active()
        {
            this.TagName    = Chatstate.active.ToString();
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}
