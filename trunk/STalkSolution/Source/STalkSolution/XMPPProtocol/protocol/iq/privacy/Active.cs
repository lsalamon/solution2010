 

using System;
using System.Text;

namespace XMPPProtocol.Protocol.iq.privacy
{
    /// <summary>
    /// The active list
    /// </summary>
    public class Active : List
    {
        public Active()
        {
            this.TagName = "active";
        }

        public Active(string name) : this()
        {
            Name = name;
        }
    }
}
