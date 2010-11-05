 

using System;

using XMPPProtocol.Xml.Dom;

//<challenge xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
//cmVhbG09InNvbWVyZWFsbSIsbm9uY2U9Ik9BNk1HOXRFUUdtMmhoIixxb3A9ImF1dGgi
//LGNoYXJzZXQ9dXRmLTgsYWxnb3JpdGhtPW1kNS1zZXNzCg==
//</challenge>
namespace XMPPProtocol.Protocol.sasl
{
	/// <summary>
	/// Summary description for Challenge.
	/// </summary>
	public class Challenge : Element
	{
		public Challenge()
		{
			this.TagName	= "challenge";
			this.Namespace	= Uri.SASL;
		}		
	}
}
