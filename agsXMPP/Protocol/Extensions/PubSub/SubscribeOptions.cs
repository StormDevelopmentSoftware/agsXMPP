/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright (c) 2003-2020 by AG-Software, FRNathan13								 *
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

using AgsXMPP.Xml.Dom;

namespace AgsXMPP.Protocol.Extensions.PubSub
{

	/*
        <xs:element name='subscribe-options'>
            <xs:complexType>
              <xs:sequence>
                <xs:element name='required' type='empty' minOccurs='0'/>
              </xs:sequence>
            </xs:complexType>
        </xs:element>
     
        
        Example 36. Service replies with success and indicates that subscription configuration is required

        <iq type='result'
            from='pubsub.shakespeare.lit'
            to='francisco@denmark.lit/barracks'
            id='sub1'>
          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
            <subscription 
                node='blogs/princely_musings'
                jid='francisco@denmark.lit'
                subscription='unconfigured'>
              <subscribe-options>
                <required/>
              </subscribe-options>
            </subscription>
          </pubsub>
        </iq>           
    
    */

	public class SubscribeOptions : Element
	{
		#region << Constructors >>
		public SubscribeOptions()
		{
			this.TagName = "subscribe-options";
			this.Namespace = URI.PUBSUB;
		}

		public SubscribeOptions(bool required)
		{
			this.Required = required;
		}
		#endregion

		public bool Required
		{
			get { return this.HasTag("required"); }
			set
			{
				if (value)
					this.SetTag("required");
				else
					this.RemoveTag("required");
			}
		}

	}
}
