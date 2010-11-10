 

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.x.data
{
	/*
	<x xmlns='jabber:x:data'
		type='{form-type}'>
		<title/>
		<instructions/>
		<field var='field-name'
				type='{field-type}'
				label='description'>
			<desc/>
			<required/>
			<value>field-value</value>
			<option label='option-label'><value>option-value</value></option>
			<option label='option-label'><value>option-value</value></option>
		</field>
	</x>
	
	
	<xs:element name='option'>
    <xs:complexType>
      <xs:sequence>
        <xs:element ref='value'/>
      </xs:sequence>
      <xs:attribute name='label' type='xs:string' use='optional'/>
    </xs:complexType>
	</xs:element>
	*/

	/// <summary>
	/// Field Option.
	/// </summary>
	public class Option : Element
	{
		#region << Constructors >>
		public Option()
		{
			this.TagName	= "option";
			this.Namespace	= Uri.X_DATA;
		}

		public Option(string label, string val) : this()
		{
			Label		= label;
			SetValue(val);
		}
		#endregion

		#region << Properties >>
		/// <summary>
		/// Label of the option
		/// </summary>
		public string Label
		{
			get { return GetAttribute("label"); }
			set { SetAttribute("label", value); }
		}	
		#endregion


		/// <summary>
		/// Value of the Option
		/// </summary>
		public string GetValue()
		{	
			return GetTag(typeof(Value));			
		}

		public void SetValue(string val)
		{	
			SetTag(typeof(Value), val);
		}			
	}
}