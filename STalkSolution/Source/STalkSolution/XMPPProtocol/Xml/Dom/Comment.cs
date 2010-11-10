 

using System;

namespace XMPPProtocol.Xml.Dom
{
	/// <summary>
	/// Summary description for Comment.
	/// </summary>
	public class Comment : Node
	{
		public Comment()
		{
			this.NodeType = NodeType.Comment;
		}
		
		public Comment(string text) : this()
		{
			this.Value = text;
		}
	}
	
}
