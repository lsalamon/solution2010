 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.iq.avatar
{
	//	<iq id='2' type='result' to='user@server/resource'>
	//		<query xmlns='jabber:iq:avatar'>
	//			<data mimetype='image/jpeg'>
	//			Base64-Encoded Data
	//			</data>
	//		</query>
	//	</iq>

	/// <summary>
	/// Summary description for Avatar.
	/// </summary>
	public class Avatar : XMPPProtocol.Protocol.Base.Avatar
	{
		public Avatar() : base()
		{
			this.Namespace	= Uri.IQ_AVATAR;			
		}	
	}
}
