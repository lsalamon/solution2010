

#region Using directives

using System;

#endregion

namespace XMPPProtocol.Protocol.component
{
    /// <summary>
    /// Summary description for Presence.
    /// </summary>
    public class Presence : XMPPProtocol.Protocol.client.Presence
    {
        #region << Constructors >>
        public Presence() : base()
        {
            this.Namespace = Uri.ACCEPT;
        }

        public Presence(XMPPProtocol.Protocol.client.ShowType show, string status) : this()
        {
            this.Show = show;
            this.Status = status;
        }

        public Presence(XMPPProtocol.Protocol.client.ShowType show, string status, int priority) : this(show, status)
        {
            this.Priority = priority;
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
