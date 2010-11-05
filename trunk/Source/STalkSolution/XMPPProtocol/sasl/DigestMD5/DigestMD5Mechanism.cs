 

using System;

using XMPPProtocol;
using XMPPProtocol.Xml.Dom;
using XMPPProtocol.Protocol.sasl;

namespace XMPPProtocol.Sasl.DigestMD5
{
	/// <summary>
	/// Handels the SASL Digest MD5 authentication
	/// </summary>
	public class DigestMD5Mechanism : Mechanism
	{
        public DigestMD5Mechanism()
		{			
		}
	
		public override void Init(XmppClientConnection con)
		{
			base.XmppClientConnection = con;
            base.XmppClientConnection.Send(new Protocol.sasl.Auth(Protocol.sasl.MechanismType.DIGEST_MD5));			
		}

		public override void Parse(Node e)
		{
			if ( e.GetType() == typeof(Protocol.sasl.Challenge) )
			{
				Protocol.sasl.Challenge c = e as Protocol.sasl.Challenge;

                Sasl.DigestMD5.Step1 step1 = new Sasl.DigestMD5.Step1(c.TextBase64);
				if (step1.Rspauth == null)
				{
					//response xmlns="urn:ietf:params:xml:ns:xmpp-sasl">dXNlcm5hbWU9ImduYXVjayIscmVhbG09IiIsbm9uY2U9IjM4MDQzMjI1MSIsY25vbmNlPSIxNDE4N2MxMDUyODk3N2RiMjZjOWJhNDE2ZDgwNDI4MSIsbmM9MDAwMDAwMDEscW9wPWF1dGgsZGlnZXN0LXVyaT0ieG1wcC9qYWJiZXIucnUiLGNoYXJzZXQ9dXRmLTgscmVzcG9uc2U9NDcwMTI5NDU4Y2EwOGVjYjhhYTIxY2UzMDhhM2U5Nzc
                    Sasl.DigestMD5.Step2 s2 = new XMPPProtocol.Sasl.DigestMD5.Step2(step1, base.Username, base.Password, base.Server);
					Protocol.sasl.Response r = new XMPPProtocol.Protocol.sasl.Response(s2.ToString());
                    base.XmppClientConnection.Send(r);
				}
				else
				{
					// SEND: <response xmlns="urn:ietf:params:xml:ns:xmpp-sasl"/>
                    base.XmppClientConnection.Send(new Protocol.sasl.Response());
				}						
			}
		}
	}
}
