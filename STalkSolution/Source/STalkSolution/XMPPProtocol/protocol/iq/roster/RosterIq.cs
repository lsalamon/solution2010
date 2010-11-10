 

using System;
using XMPPProtocol.Protocol;
using XMPPProtocol.Protocol.client;

// Request Roster:
// <iq id='someid' to='myjabber.net' type='get'>
//		<query xmlns='jabber:iq:roster'/>
// </iq>
namespace XMPPProtocol.Protocol.iq.roster
{
	/// <summary>
	/// Build a new roster query, jabber:iq:roster
	/// </summary>
	public class RosterIq : IQ
	{
		private Roster m_Roster = new Roster();

		public RosterIq()
		{		
			base.Query = m_Roster;
			this.GenerateId();
		}

		public RosterIq(IqType type) : this()
		{			
			this.Type = type;		
		}	

		public new Roster Query
		{
			get { return m_Roster; }            
		}
	}
}
