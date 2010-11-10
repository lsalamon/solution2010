

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.Base
{
	/// <summary>
	/// Summary description for Group.
	/// </summary>
	public class Group : Element
	{
		public Group()
		{
			this.TagName = "group";
		}

		public Group(string groupname) : this()
		{
			this.Name	= groupname;
		}

		/// <summary>
		/// gets or sets the Name of the contact group
		/// </summary>
		public string Name
		{
			set	{ this.Value = value; }
			get	{ return this.Value; }
		}

	}
}
