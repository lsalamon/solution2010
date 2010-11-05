 

using System;

using XMPPProtocol.Xml.Dom;

// <success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>
namespace XMPPProtocol.Protocol.sasl
{
	/// <summary>
	/// Summary description for Success.
	/// </summary>
	public class Success : Element
	{
		public Success()
		{
			this.TagName	= "success";
			this.Namespace	= Uri.SASL;
		}
	}
}
