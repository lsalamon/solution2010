 

using System;

using XMPPProtocol.Xml.Dom;

// <auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl' mechanism='DIGEST-MD5'/>
namespace XMPPProtocol.Protocol.sasl
{
	/// <summary>
	/// Summary description for Auth.
	/// </summary>
	public class Auth : Element
	{
		public Auth()
		{
			this.TagName	= "auth";
			this.Namespace	= Uri.SASL;
		}

		public Auth(MechanismType type) : this()
		{
			MechanismType = type;
		}

		public Auth(MechanismType type, string text) : this(type)
		{			
			this.Value		= text;			
		}


		public MechanismType MechanismType
		{
			get
			{
				return Mechanism.GetMechanismType(GetAttribute("mechanism"));
			}
			set
			{
				SetAttribute("mechanism", Mechanism.GetMechanismName(value));
			}
		}
	}
}
