 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.nickname
{
    // <nick xmlns='http://jabber.org/protocol/nick'>Ishmael</nick>
    public class Nickname : Element
    {
        public Nickname()
        {
            this.TagName    = "nick";
            this.Namespace  = Uri.NICK;
        }

        public Nickname(string nick) : this()
        {
            Value = nick;
        }
    }
}
