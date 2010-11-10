

using System;

using XMPPProtocol.Protocol.client;

namespace XMPPProtocol.Protocol.iq.bind
{
	/// <summary>
	/// Summary description for BindIq.
	/// </summary>
	public class BindIq : IQ
	{
		private Bind m_Bind = new Bind();
		
		public BindIq()
		{
			this.GenerateId();
			this.AddChild(m_Bind);
		}

		public BindIq(IqType type) : this()
		{			
			this.Type = type;		
		}

		public BindIq(IqType type, Jid to) : this()
		{			
			this.Type = type;
			this.To = to;
		}

		public BindIq(IqType type, Jid to, string resource) : this(type, to)
		{			
			m_Bind.Resource = resource;
		}

        public new Bind Query
        {
            get
            {
                return m_Bind;
            }
        }
	}
}
