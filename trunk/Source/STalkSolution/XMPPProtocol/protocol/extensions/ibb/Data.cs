

using System;
using System.Text;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.ibb
{
    /*
         <data xmlns='http://jabber.org/protocol/ibb' sid='mySID' seq='0'>
            qANQR1DBwU4DX7jmYZnncmUQB/9KuKBddzQH+tZ1ZywKK0yHKnq57kWq+RFtQdCJ
            WpdWpR0uQsuJe7+vh3NWn59/gTc5MDlX8dS9p0ovStmNcyLhxVgmqS8ZKhsblVeu
            IpQ0JgavABqibJolc3BKrVtVV1igKiX/N7Pi8RtY1K18toaMDhdEfhBRzO/XB0+P
            AQhYlRjNacGcslkhXqNjK5Va4tuOAPy2n1Q8UUrHbUd0g+xJ9Bm0G0LZXyvCWyKH
            kuNEHFQiLuCY6Iv0myq6iX6tjuHehZlFSh80b5BVV9tNLwNR5Eqz1klxMhoghJOA
         </data>
      
         <xs:element name='data'>
             <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='xs:string'>
                  <xs:attribute name='sid' type='xs:string' use='required'/>
                  <xs:attribute name='seq' type='xs:string' use='required'/>
                </xs:extension>
              </xs:simpleContent>
             </xs:complexType>
           </xs:element>
    */

    /// <summary>
    /// 
    /// </summary>
    public class Data : Base
    {
        /// <summary>
        /// 
        /// </summary>
        public Data()
        {
            this.TagName    = "data";            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="seq"></param>
        public Data(string sid, int seq)  : this()
        {
            this.Sid        = sid;
            this.Sequence   = seq;
        }

        /// <summary>
        /// the sequence
        /// </summary>
        public int Sequence
        {
            get { return GetAttributeInt("seq"); }
            set { SetAttribute("seq", value); }
        }
    }
}