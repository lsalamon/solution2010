

using System;
using System.Collections;
using System.Threading;

using XMPPProtocol.Protocol.client;

//using XMPPProtocol.Protocol.component;

using XMPPProtocol.Xml;

namespace XMPPProtocol
{
    public delegate void IqCB(object sender, IQ iq, object data);
	
	public class IqGrabber : PacketGrabber
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="conn"></param>
		public IqGrabber(XmppClientConnection conn)
		{
			m_connection		= conn;
			conn.OnIq	+= new IqHandler(OnIq);
		}

        public IqGrabber(XmppComponentConnection conn)
        {
            m_connection = conn;
#if MONOSSL
            conn.OnIq += new XMPPProtocol.Protocol.component.IqHandler(OnIqComponent);
#else
			conn.OnIq += new XMPPProtocol.Protocol.component.IqHandler(OnIq);
#endif
        }        
        
#if !CF
        private IQ  synchronousResponse     = null;

        private int m_SynchronousTimeout    = 5000;

        /// <summary>
        /// Timeout for synchronous requests, default value is 5000 (5 seconds)
        /// </summary>
        public int SynchronousTimeout
        {
            get { return m_SynchronousTimeout; }
            set { m_SynchronousTimeout = value; }
        }
#endif 
		
#if MONOSSL
		private void OnIqComponent(object sender, XMPPProtocol.Protocol.component.IQ iq)
		{
			OnIq(sender, iq);
		}
#endif

		/// <summary>
		/// An IQ Element is received. Now check if its one we are looking for and
		/// raise the event in this case.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnIq(object sender, XMPPProtocol.Protocol.client.IQ iq)
		{			
			if (iq == null)
				return;

			string id = iq.Id;
			if(id == null)
				return;

			TrackerData td;

			lock (m_grabbing)
			{
				td = (TrackerData) m_grabbing[id];

				if (td == null)
				{
					return;
				}
				m_grabbing.Remove(id);
			}
                       
            td.cb(this, iq, td.data);           
		}

        /// <summary>
        /// Send an IQ Request and store the object with callback in the Hashtable
        /// </summary>
        /// <param name="iq">The iq to send</param>
        /// <param name="cb">the callback function which gets raised for the response</param>
        public void SendIq(IQ iq, IqCB cb)
        {
            SendIq(iq, cb, null);
        }

        /// <summary>
        /// Send an IQ Request and store the object with callback in the Hashtable
        /// </summary>
        /// <param name="iq">The iq to send</param>
        /// <param name="cb">the callback function which gets raised for the response</param>
        /// <param name="cbArg">additional object for arguments</param>
		public void SendIq(IQ iq, IqCB cb, object cbArg)
		{
            // check if the callback is null, in case of wrong usage of this class
            if (cb != null)
            {
                TrackerData td = new TrackerData();
                td.cb = cb;
                td.data = cbArg;

                m_grabbing[iq.Id] = td;
            }
			m_connection.Send(iq);
		}

#if !CF
        /// <summary>
        /// Sends an Iq synchronous and return the response or null on timeout
        /// </summary>
        /// <param name="iq">The IQ to send</param>
        /// <param name="timeout"></param>
        /// <returns>The response IQ or null on timeout</returns>
        public IQ SendIq(XMPPProtocol.Protocol.client.IQ iq, int timeout)
        {
            synchronousResponse = null;
            AutoResetEvent are = new AutoResetEvent(false);

            SendIq(iq, new IqCB(SynchronousIqResult), are);

            if (!are.WaitOne(timeout, true))
            {
                // Timed out
                lock (m_grabbing)
                {       
                    if (m_grabbing.ContainsKey(iq.Id))
                        m_grabbing.Remove(iq.Id);
                }                
                return null;
            }
            
            return synchronousResponse;
		}

        /// <summary>
        /// Sends an Iq synchronous and return the response or null on timeout.
        /// Timeout time used is <see cref="SynchronousTimeout"/>
        /// </summary>
        /// <param name="iq">The IQ to send</param>        
        /// <returns>The response IQ or null on timeout</returns>
        public IQ SendIq(IQ iq)
        {
            return SendIq(iq, m_SynchronousTimeout);
        }

        /// <summary>
        /// Callback for synchronous iq grabbing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        private void SynchronousIqResult(object sender, IQ iq, object data)
        {
            synchronousResponse = iq;
            
            AutoResetEvent are = data as AutoResetEvent;
            are.Set();
        }		
#endif
		private class TrackerData
		{
			public IqCB  cb;
			public object data;
		}		
	}
}