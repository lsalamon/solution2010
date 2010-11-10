

using System;

using XMPPProtocol.Xml;
using XMPPProtocol.Xml.Dom;
using XMPPProtocol.Protocol.client;

namespace XMPPProtocol.Protocol.Base
{
	/// <summary>
	/// Base XMPP Element
	/// This must ne used to build all other new packets
	/// </summary>
	public abstract class Stanza : DirectionalElement
	{
		public Stanza() : base()
		{
		}

		public Stanza(string tag) : base(tag)
		{			
		}

		public Stanza(string tag, string ns) : base(tag)
		{			
			this.Namespace = ns;
		}

		public Stanza(string tag, string text, string ns) : base(tag, text)
		{
			this.Namespace = ns;
		}

		public string Id
		{
			get 
			{ 
				return this.GetAttribute("id");
			}
			set
			{ 
				this.SetAttribute("id", value); 
			}
		}

		/// <summary>
		/// Generates a automatic id for the packet.
		/// !!! Overwrites existing Ids
		/// </summary>
		/// <returns></returns>
		public void GenerateId()
		{
			string sId = XMPPProtocol.Id.GetNextId();
			this.Id = sId;			
		}

        /// <summary>        
        /// XML Language attribute
        /// </summary>
        /// <remarks>
        /// The language 'xml:lang' attribute  SHOULD be included by the initiating entity on the header for the initial stream 
        /// to specify the default language of any human-readable XML character data it sends over that stream. 
        /// If the attribute is included, the receiving entity SHOULD remember that value as the default for both the 
        /// initial stream and the response stream; if the attribute is not included, the receiving entity SHOULD use 
        /// a configurable default value for both streams, which it MUST communicate in the header for the response stream. 
        /// For all stanzas sent over the initial stream, if the initiating entity does not include an 'xml:lang' attribute, 
        /// the receiving entity SHOULD apply the default value; if the initiating entity does include an 'xml:lang' attribute, 
        /// the receiving entity MUST NOT modify or delete it (see also xml:langxml:lang). 
        /// The value of the 'xml:lang' attribute MUST conform to the format defined in RFC 3066 (Tags for the Identification of Languages, January 2001.[LANGTAGS]).
        /// </remarks>
        public string Language
        {
            get { return GetAttribute("xml:lang"); }
            set { SetAttribute("xml:lang", value); }
        }
        		
        ///// <summary>
        ///// Error Child Element
        ///// </summary>
        //public XMPPProtocol.Protocol.client.Error Error
        //{
        //    get
        //    {
        //        return SelectSingleElement(typeof(XMPPProtocol.Protocol.client.Error)) as XMPPProtocol.Protocol.client.Error;

        //    }
        //    set
        //    {
        //        if (HasTag(typeof(XMPPProtocol.Protocol.client.Error)))
        //            RemoveTag(typeof(XMPPProtocol.Protocol.client.Error));
                
        //        if (value != null)
        //            this.AddChild(value);
        //    }
        //}
	}
}