 

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.x.muc.iq.owner
{
    /*
        <iq id="jcl_110" to="xxxxxx@conference.jabber.org" type="set">
            <query xmlns="http://jabber.org/protocol/muc#owner">
                <x type="submit" xmlns="jabber:x:data"/>
            </query>
        </iq>
    */

    public class Owner : Element
    {
        public Owner()
        {
            this.TagName    = "query";
            this.Namespace  = Uri.MUC_OWNER;           
        }        
    }
}