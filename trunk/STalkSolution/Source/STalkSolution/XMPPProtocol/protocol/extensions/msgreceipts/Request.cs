

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.msgreceipts
{
    /// <summary>
    /// 
    /// </summary>
    public class Request : Element
    {
        /*
         * <request xmlns='http://www.xmpp.org/extensions/xep-0184.html#ns'/>         
         */
        public Request()
        {
            this.TagName    = "request";
            this.Namespace  = Uri.MSG_RECEIPT;
        }
    }
}