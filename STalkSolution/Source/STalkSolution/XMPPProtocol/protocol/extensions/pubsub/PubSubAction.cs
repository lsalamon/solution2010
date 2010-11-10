 

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.pubsub
{
    public abstract class PubSubAction : Element
    {
        public PubSubAction()
        {
            this.Namespace = Uri.PUBSUB;
        }

        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        public Type Type
        {
            get
            {
                return (Type)GetAttributeEnum("type", typeof(Type));
            }
            set
            {
                if (value == Type.NONE)
                    RemoveAttribute("type");
                else
                    SetAttribute("type", value.ToString());
            }
        }
    }
}
