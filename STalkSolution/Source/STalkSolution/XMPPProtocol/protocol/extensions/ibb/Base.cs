

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.ibb
{
    /// <summary>
    /// IBB base class
    /// </summary>
    public abstract class Base : Element
    {
        public Base()
        {
            this.Namespace = Uri.IBB;
        }

        /// <summary>
        /// Sid
        /// </summary>
        public string Sid
        {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
        }
    }
}
