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

using AgsXMPP.Protocol.Client;

namespace AgsXMPP.Protocol.Extensions.SI
{
	/*
    <iq id="jcl_18" to="xxx" type="result" from="yyy">
        <si xmlns="http://jabber.org/protocol/si">
            <feature xmlns="http://jabber.org/protocol/feature-neg">
                <x xmlns="jabber:x:data" type="submit">
                    <field var="stream-method">
                        <value>http://jabber.org/protocol/bytestreams</value>
                    </field>
                </x>
            </feature>
        </si>
    </iq>
 
    */

	/// <summary>
	/// 
	/// </summary>
	public class SIIq : IQ
	{
		private SI m_SI = new SI();

		public SIIq()
		{
			this.GenerateId();
			this.AddChild(this.m_SI);
		}

		public SIIq(IQType type)
			: this()
		{
			this.Type = type;
		}

		public SIIq(IQType type, Jid to)
			: this(type)
		{
			this.To = to;
		}

		public SIIq(IQType type, Jid to, Jid from)
			: this(type, to)
		{
			this.From = from;
		}

		public SI SI
		{
			get
			{
				return this.m_SI;
			}
		}
	}
}
