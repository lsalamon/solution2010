 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.tls
{
	// Step 5 (alt): Server informs client that TLS negotiation has failed and closes both stream and TCP connection:

	// <failure xmlns='urn:ietf:params:xml:ns:xmpp-tls'/>
	// </stream:stream>

	/// <summary>
	/// Summary description for Failure.
	/// </summary>
	public class Failure : Element
	{
		public Failure()
		{
			this.TagName	= "failure";
			this.Namespace	= Uri.TLS;
		}
	}
}
