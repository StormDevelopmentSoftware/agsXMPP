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

using System.Collections;

using AgsXMPP.Collections;
using AgsXMPP.Protocol.Client;

namespace AgsXMPP
{
	public delegate void MessageCB(object sender, Message msg, object data);

	public class MessageGrabber : PacketGrabber
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="conn"></param>
		public MessageGrabber(XmppClientConnection conn)
		{
			this.m_connection = conn;
			conn.OnMessage += new MessageHandler(this.m_connection_OnMessage);
		}

		public void Add(Jid jid, MessageCB cb, object cbArg)
		{
			lock (this.m_grabbing)
			{
				if (this.m_grabbing.ContainsKey(jid.ToString()))
					return;
			}

			var td = new TrackerData();
			td.cb = cb;
			td.data = cbArg;
			td.comparer = new BareJidComparer();

			lock (this.m_grabbing)
			{
				this.m_grabbing.Add(jid.ToString(), td);
			}
		}

		public void Add(Jid jid, IComparer comparer, MessageCB cb, object cbArg)
		{
			lock (this.m_grabbing)
			{
				if (this.m_grabbing.ContainsKey(jid.ToString()))
					return;
			}

			var td = new TrackerData();
			td.cb = cb;
			td.data = cbArg;
			td.comparer = comparer;

			lock (this.m_grabbing)
			{
				this.m_grabbing.Add(jid.ToString(), td);
			}
		}

		/// <summary>
		/// Pending request can be removed.
		/// This is useful when a ressource for the callback is destroyed and
		/// we are not interested anymore at the result.
		/// </summary>
		/// <param name="id">ID of the Iq we are not interested anymore</param>
		public void Remove(Jid jid)
		{
			lock (this.m_grabbing)
			{
				if (this.m_grabbing.ContainsKey(jid.ToString()))
					this.m_grabbing.Remove(jid.ToString());
			}
		}

		private class TrackerData
		{
			public MessageCB cb;
			public object data;
			// by default the Bare Jid is compared
			public IComparer comparer;
		}

		/// <summary>
		/// A Message is received. Now check if its from a Jid we are looking for and
		/// raise the event in this case.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="msg"></param>
		private void m_connection_OnMessage(object sender, Message msg)
		{
			if (msg == null)
				return;

			lock (this.m_grabbing)
			{
				var myEnum = this.m_grabbing.GetEnumerator();

				while (myEnum.MoveNext())
				{
					var t = myEnum.Value as TrackerData;
					if (t.comparer.Compare(new Jid((string)myEnum.Key), msg.From) == 0)
					{
						// Execute the callback
						t.cb(this, msg, t.data);
					}
				}
			}
		}
	}
}