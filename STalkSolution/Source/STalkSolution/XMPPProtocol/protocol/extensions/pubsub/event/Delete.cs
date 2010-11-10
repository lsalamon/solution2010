 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.pubsub.@event
{
	public class Delete : Element
    {
        #region << Constructors >>
        public Delete()
        {
            this.TagName    = "delete";
            this.Namespace  = Uri.PUBSUB_EVENT;
        }

        public Delete(string node) : this()
        {
            this.Node = node;
        }
        #endregion

        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
	}
}
