 

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.x.muc.iq.admin
{
    /*
        <query xmlns='http://jabber.org/protocol/muc#admin'>
            <item nick='pistol' role='none'>
              <reason>Avaunt, you cullion!</reason>
            </item>
        </query>
    */

    /// <summary>
    /// 
    /// </summary>
    public class Admin : Element
    {
        public Admin()
        {           
			this.TagName	= "query";
			this.Namespace	= Uri.MUC_ADMIN;		
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            AddChild(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddItems(Item[] items)
        {
            foreach (Item itm in items)
            {
                AddItem(itm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Item[] GetItems()
        {
            ElementList nl = SelectElements(typeof(Item));
            Item[] items = new Item[nl.Count];
            int i = 0;
            foreach (Item itm in nl)
            {
                items[i] = itm;
                i++;
            }
            return items;
        }

    }
}
