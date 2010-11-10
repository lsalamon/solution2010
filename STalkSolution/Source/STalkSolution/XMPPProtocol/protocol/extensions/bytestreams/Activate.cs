

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.bytestreams
{
    public class Activate : Element
    {
        public Activate()
        {
            this.TagName    = "activate";
            this.Namespace  = Uri.BYTESTREAMS;
        }

        public Activate(Jid jid) : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// the full JID of the Target to activate
        /// </summary>
        public Jid Jid
        {
            get
            {
                if (Value == null)
                    return null;
                else
                    return new Jid(Value);
            }
            set
            {
                if (value != null)
                    Value = value.ToString();
                else
                    Value = null;
            }
        }
    }
}