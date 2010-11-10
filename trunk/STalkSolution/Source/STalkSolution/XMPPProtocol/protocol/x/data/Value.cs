 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.x.data
{
	/// <summary>
	/// Summary description for Value.
	/// </summary>
	public class Value : Element
	{
		public Value()
		{
			this.TagName	= "value";
			this.Namespace	= Uri.X_DATA;
		}

		public Value(string val) : this()
		{
            Value = val;			
		}

		public Value(bool val) : this()
		{
			Value = val ? "1" : "0";
		}
		
	}
}
