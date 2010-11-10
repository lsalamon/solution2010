 

using System;

using XMPPProtocol.Xml.Dom;


namespace XMPPProtocol.Protocol.x.muc
{
    public class Actor : Element
    {
        public Actor()
        {
            this.TagName    = "actor";
            this.Namespace  = Uri.MUC_USER;
        }

        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }
    }
}
