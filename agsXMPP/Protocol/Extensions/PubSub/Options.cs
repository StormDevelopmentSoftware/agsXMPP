/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright (c) 2003-2019 by AG-Software, FRNathan13								 *
 * All Rights Reserved.																 *
 * Contact information for AG-Software is available at http://www.ag-software.de	 *
 *																					 *
 * Licence:																			 *
 * The agsXMPP SDK is released under a dual licence									 *
 * agsXMPP can be used under either of two licences									 *
 * 																					 *
 * A commercial licence which is probably the most appropriate for commercial 		 *
 * corporate use and closed source projects. 										 *
 *																					 *
 * The GNU Public License (GPL) is probably most appropriate for inclusion in		 *
 * other open source projects.														 *
 *																					 *
 * See README.html for details.														 *
 *																					 *
 * For general enquiries visit our website at:										 *
 * http://www.ag-software.de														 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using agsXMPP.Protocol.x.data;

using agsXMPP.Xml.Dom;

namespace agsXMPP.Protocol.extensions.pubsub
{

	/*
        <xs:element name='options'>
            <xs:complexType>
              <xs:sequence minOccurs='0'>
                <xs:any namespace='jabber:x:data'/>
              </xs:sequence>
              <xs:attribute name='jid' type='xs:string' use='required'/>
              <xs:attribute name='node' type='xs:string' use='optional'/>
              <xs:attribute name='subid' type='xs:string' use='optional'/>
            </xs:complexType>
        </xs:element>
     
        <iq type='get'
            from='francisco@denmark.lit/barracks'
            to='pubsub.shakespeare.lit'
            id='options1'>
          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
            <options node='blogs/princely_musings' jid='francisco@denmark.lit'/>
          </pubsub>
        </iq>
    */

	public class Options : Element
	{
		#region << Constructors >>
		public Options()
		{
			this.TagName = "options";
			this.Namespace = Namespaces.PUBSUB;
		}

		public Options(Jid jid) : this()
		{
			this.Jid = jid;
		}

		public Options(Jid jid, string node) : this(jid)
		{
			this.Node = node;
		}

		public Options(Jid jid, string node, string subId) : this(jid, node)
		{
			this.SubId = subId;
		}
		#endregion

		public Jid Jid
		{
			get
			{
				if (this.HasAttribute("jid"))
					return new Jid(this.GetAttribute("jid"));
				else
					return null;
			}
			set
			{
				if (value != null)
					this.SetAttribute("jid", value.ToString());
			}
		}

		public string Node
		{
			get { return this.GetAttribute("node"); }
			set { this.SetAttribute("node", value); }
		}

		public string SubId
		{
			get { return this.GetAttribute("subid"); }
			set { this.SetAttribute("subid", value); }
		}

		/// <summary>
		/// The X-Data Element/Form
		/// </summary>
		public Data Data
		{
			get
			{
				return this.SelectSingleElement(typeof(Data)) as Data;

			}
			set
			{
				if (this.HasTag(typeof(Data)))
					this.RemoveTag(typeof(Data));

				if (value != null)
					this.AddChild(value);
			}
		}
	}
}