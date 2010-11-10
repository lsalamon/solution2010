 

using System;

using XMPPProtocol.Xml.Dom;
//<stream:stream xmlns:stream='http://etherx.jabber.org/streams/'
//xmlns='jabber:client'
//from='somedomain'
//version='1.0'>
//<stream:features>
//...
//<register xmlns='http://jabber.org/features/iq-register'/>
//...

namespace XMPPProtocol.Protocol.stream.feature
{
	/// <summary>
	/// 
	/// </summary>
	public class Register : Element
	{
		public Register()
		{
			this.TagName	= "register";
			this.Namespace	= Uri.FEATURE_IQ_REGISTER;
		}
	}
}
