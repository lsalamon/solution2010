

using System;

using XMPPProtocol.Xml.Dom;

using XMPPProtocol.Protocol.client;
using XMPPProtocol.Protocol.iq.@private;

namespace XMPPProtocol.Protocol.extensions.bookmarks
{
    /// <summary>
    /// 
    /// </summary>
    public class StorageIq : PrivateIq
    {
        public StorageIq()
        {
            this.Query.AddChild(new Storage());
        }

        public StorageIq(IqType type) : this()
		{			
			this.Type = type;		
		}

		public StorageIq(IqType type, Jid to) : this(type)
		{
			this.To = to;
		}

        public StorageIq(IqType type, Jid to, Jid from) : this(type, to)
		{
			this.From = from;
		}

    }
}
