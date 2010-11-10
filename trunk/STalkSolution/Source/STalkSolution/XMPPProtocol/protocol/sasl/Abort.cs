 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.sasl
{
	/// <summary>
	/// Summary description for Abort.
	/// </summary>
	public class Abort : Element
	{
		public Abort()
		{
			this.TagName	= "abort";
			this.Namespace	= Uri.SASL;
		}
	}
}
