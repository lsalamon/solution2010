 

using System;

namespace XMPPProtocol.Protocol.server
{
    public class Presence : XMPPProtocol.Protocol.client.Presence
    {
        public Presence()
        {
            this.Namespace = Uri.SERVER;
        }
    }
}
