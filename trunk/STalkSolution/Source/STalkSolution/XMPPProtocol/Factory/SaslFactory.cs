

using System;
using System.Collections;

using XMPPProtocol.Sasl;
using XMPPProtocol.Sasl.Plain;
#if !SL
using XMPPProtocol.Sasl.DigestMD5;
#endif
using XMPPProtocol.Sasl.Anonymous;
using XMPPProtocol.Sasl.XGoogleToken;


namespace XMPPProtocol.Factory
{
	/// <summary>
	/// SASL factory
	/// </summary>
	public class SaslFactory
	{
		/// <summary>
		/// This Hashtable stores Mapping of mechanism <--> SASL class in XMPPProtocol
		/// </summary>
		private static Hashtable m_table = new Hashtable();

		static SaslFactory()
		{
			AddMechanism(Protocol.sasl.Mechanism.GetMechanismName(Protocol.sasl.MechanismType.PLAIN),		    typeof(PlainMechanism));
			AddMechanism(Protocol.sasl.Mechanism.GetMechanismName(Protocol.sasl.MechanismType.DIGEST_MD5),	    typeof(DigestMD5Mechanism));
            AddMechanism(Protocol.sasl.Mechanism.GetMechanismName(Protocol.sasl.MechanismType.ANONYMOUS),       typeof(AnonymousMechanism));
            AddMechanism(Protocol.sasl.Mechanism.GetMechanismName(Protocol.sasl.MechanismType.X_GOOGLE_TOKEN),  typeof(XGoogleTokenMechanism));
		}


		public static Mechanism GetMechanism(string mechanism)
		{
			Type t = (Type) m_table[mechanism];
			if (t != null)
				return (Mechanism) Activator.CreateInstance(t);
			else
				return null;			
		}
		
		/// <summary>
		/// Adds new Element Types to the Hashtable
		/// Use this function to register new SASL mechanisms
		/// </summary>
		/// <param name="mechanism"></param>
		/// <param name="t"></param>
		public static void AddMechanism(string mechanism, System.Type t)
		{
			m_table.Add( mechanism, t);
		}
	}
}
