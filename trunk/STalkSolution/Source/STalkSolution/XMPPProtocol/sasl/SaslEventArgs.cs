 

using System;

using XMPPProtocol.Protocol.sasl;
using XMPPProtocol.Protocol.stream;

namespace XMPPProtocol.Sasl
{
	public delegate void SaslEventHandler	(object sender, SaslEventArgs args);

	public class SaslEventArgs
	{
		#region << Constructors >>
		public SaslEventArgs()
		{

		}
		
		public SaslEventArgs(Mechanisms mechanisms)
		{
			m_Mechanisms = mechanisms;
		}
		#endregion

		// by default the library chooses the auth method
		private bool						m_Auto			= true;
		private string						m_Mechanism;
		private Mechanisms					m_Mechanisms;

		/// <summary>
		/// Set Auto to true if the library should choose the mechanism
		/// Set it to false for choosing the authentication method yourself
		/// </summary>
		public bool Auto
		{
			get { return m_Auto; }
			set { m_Auto = value; }
		}

		/// <summary>
		/// SASL Mechanism for authentication as string
		/// </summary>
		public string Mechanism
		{
			get { return m_Mechanism; }
			set { m_Mechanism = value; }
		}

		public Mechanisms Mechanisms
		{
			get { return m_Mechanisms; }
			set { m_Mechanisms = value; }
		}
	}
}