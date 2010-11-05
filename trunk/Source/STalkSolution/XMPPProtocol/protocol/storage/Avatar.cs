 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.storage
{

	//	<iq id='0' type='set' to='user@server'>
	//		<query xmlns='storage:client:avatar'>
	//			<data mimetype='image/jpeg'>
	//			Base64 Encoded Data
	//			</data>
	//		</query>
	//	</iq>

	/// <summary>
	/// Summary description for Avatar.
	/// </summary>
	public class Avatar : Base.Avatar
	{
		public Avatar() : base()
		{
			this.Namespace	= Uri.STORAGE_AVATAR;
		}
	}
}
