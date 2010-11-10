 

using System;
using System.IO;

namespace XMPPProtocol.Xml.Dom
{
	/// <summary>
	/// 
	/// </summary>
	public class Document : Node
	{
		public Document()
		{
			this.NodeType = NodeType.Document;
		}

		public Element RootElement
		{
			get
			{
				foreach (Node n in this.ChildNodes)
				{
					if(n.NodeType == NodeType.Element)
						return n as Element;
				}
				return null;
			}
		}

		#region << Properties and Member Variables >>
		private string	m_Encoding	= null;
		private string	m_Version	= null;

		public string Encoding
		{
			get { return m_Encoding; }
			set { m_Encoding = value; }
		}

		public string Version
		{
			get { return m_Version; }
			set { m_Version = value; }
		}
		#endregion

		/// <summary>
		/// Clears the Document
		/// </summary>
		public void Clear()
		{
			this.ChildNodes.Clear();
		}

		#region << Load functions >>		
		public void LoadXml(string xml)
		{
            if (xml != "" && xml != null)
            {
                DomLoader l = new DomLoader(xml, this);
            }
		}

		public bool LoadFile(string filename)
		{
			if (File.Exists(filename) == true)
			{
				try
				{
					StreamReader sr = new StreamReader(filename);				
					DomLoader l = new DomLoader(sr, this);
					sr.Close();					
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

        public bool LoadStream(Stream stream)
        {
            try
            {
                StreamReader sr = new StreamReader(stream);
                DomLoader l = new DomLoader(sr, this);
                sr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
		
		public void Save(string filename)
		{
			StreamWriter w = new StreamWriter(filename);

			w.Write(this.ToString(System.Text.Encoding.UTF8));
			w.Flush();
			w.Close();
		}
		#endregion
	}
}
