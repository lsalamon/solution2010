

#region Using directives

using System;

#endregion

namespace XMPPProtocol.Protocol.component
{
    /// <summary>
    /// Summary description for Iq.
    /// </summary>
    public class IQ : XMPPProtocol.Protocol.client.IQ
    {
        #region << Constructors >>
        public IQ() : base()
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(XMPPProtocol.Protocol.client.IqType type) : base(type)
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(Jid from, Jid to) : base(from, to)
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(XMPPProtocol.Protocol.client.IqType type, Jid from, Jid to) : base(type, from, to)
        {
            this.Namespace = Uri.ACCEPT;
        }
        #endregion

        /// <summary>
        /// Error Child Element
        /// </summary>
        public new XMPPProtocol.Protocol.component.Error Error
        {
            get
            {
                return SelectSingleElement(typeof(XMPPProtocol.Protocol.component.Error)) as XMPPProtocol.Protocol.component.Error;

            }
            set
            {
                if (HasTag(typeof(XMPPProtocol.Protocol.component.Error)))
                    RemoveTag(typeof(XMPPProtocol.Protocol.component.Error));

                if (value != null)
                    this.AddChild(value);
            }
        }
    }
}
