 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.iq.session
{
	/// <summary>
	/// Summary description for Session.
	/// </summary>
	public class Session : Element
	{
		public Session()
		{
			this.TagName	= "session";
			this.Namespace	= Uri.SESSION;
		}
	}
}
