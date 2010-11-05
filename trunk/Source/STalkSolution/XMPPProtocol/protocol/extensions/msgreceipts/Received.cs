

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.msgreceipts
{
    /// <summary>
    /// 
    /// </summary>
    public class Received : Element
    {
        /*         
         * <received xmlns='http://www.xmpp.org/extensions/xep-0184.html#ns'/>
         */
        public Received()
        {
            this.TagName    = "received";
            this.Namespace  = Uri.MSG_RECEIPT;
        }
    }
}
