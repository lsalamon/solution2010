 
using System;
using XMPPProtocol.Xml.Dom;

// JEP-0011: Jabber Browsing
//
// This JEP defines a way to describe information about Jabber entities and the relationships between entities. 
// Note: This JEP is superseded by JEP-0030: Service Discovery.

// WARNING: This JEP has been deprecated by the Jabber Software Foundation. 
// Implementation of the protocol described herein is not recommended. Developers desiring similar functionality should 
// implement the protocol that supersedes this one (if any).

// Most components and gateways still dont implement Service discovery. So we must use jabber:iq:browse for them until everything
// is replaced with JEP 30 (Service Discovery).
namespace XMPPProtocol.Protocol.iq.browse
{
	/// <summary>
	/// Summary description for Browse.
	/// </summary>
	public class Browse : Element
	{
		public Browse()
		{
			this.TagName	= "query";
			this.Namespace	= Uri.IQ_BROWSE;
		}

		public string Category
		{
			get { return GetAttribute("category"); }
			set { SetAttribute("category", value); }
		}
		
		public string Type
		{
			get { return GetAttribute("type"); }
			set { SetAttribute("type", value); }
		}
		
		public string Name
		{
			get { return GetAttribute("name"); }
			set { SetAttribute("name", value); }
		}

		public string[] GetNamespaces()
		{
            ElementList elements = SelectElements("ns");
			string[] nss = new string[elements.Count];
			
			int i=0;
			foreach (Element ns in elements)
			{
				nss[i] = ns.Value;
				i++;
			}

			return nss;
		}

		public BrowseItem[] GetItems()
		{
            ElementList nl = SelectElements(typeof(BrowseItem));
			BrowseItem[] items = new BrowseItem[nl.Count];
			int i = 0;
			foreach (Element item in nl)
			{
				items[i] = item as BrowseItem;
				i++;
			}
			return items;
		}
	}
}
