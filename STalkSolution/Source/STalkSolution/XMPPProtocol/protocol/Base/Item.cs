

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.Base
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class Item : Element
	{
		public Item()
		{
			this.TagName	= "item";
		}
        		

        public Jid Jid
        {
            get
            {
                if (HasAttribute("jid"))
                    return new Jid(this.GetAttribute("jid"));
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.SetAttribute("jid", value.ToString());
            }
        }


		public string Name
		{
			get	{ return GetAttribute("name"); }
			set	{ SetAttribute("name", value); }

		}
	}
}
