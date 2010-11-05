 

using System;

using XMPPProtocol.Protocol.x.data;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.pubsub.owner
{
    public class Configure : Element
    {
        #region << Constructor >>
        public Configure()
        {
            this.TagName    = "configure";
            this.Namespace  = Uri.PUBSUB_OWNER;
        }

        public Configure(string node)
        {
            this.Node = node;
        }
        #endregion

        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// The x-Data Element
        /// </summary>
        public Data Data
        {
            get
            {
                return SelectSingleElement(typeof(Data)) as Data;
            }
            set
            {
                if (HasTag(typeof(Data)))
                    RemoveTag(typeof(Data));

                if (value != null)
                    this.AddChild(value);
            }
        }
    }
}
