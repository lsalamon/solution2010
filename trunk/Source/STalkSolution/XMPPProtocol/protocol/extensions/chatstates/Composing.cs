

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.chatstates
{
    /// <summary>
    /// User is composing a message.
    /// User is interacting with a message input interface specific to this chat session 
    /// (e.g., by typing in the input area of a chat window).
    /// </summary>
    public class Composing : Element
    {
        public Composing()
        {
            this.TagName    = Chatstate.composing.ToString(); ;
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}
