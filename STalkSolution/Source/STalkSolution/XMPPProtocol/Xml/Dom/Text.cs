 

using System;

namespace XMPPProtocol.Xml.Dom
{
	/// <summary>
	/// 
	/// </summary>
	public class Text : Node
	{
		public Text()
		{
			this.NodeType = NodeType.Text;
		}
		
		public Text(string text) : this()
		{
			this.Value = text;
		}
	}
}
