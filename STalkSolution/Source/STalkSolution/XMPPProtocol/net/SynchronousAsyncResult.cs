

using System;
using System.IO;
using System.Threading;

namespace XMPPProtocol.net
{
    internal class SynchronousAsyncResult : IAsyncResult
    {
        /*   
        object AsyncState { get; }      
        WaitHandle AsyncWaitHandle { get; }       
        bool CompletedSynchronously { get; }    
        bool IsCompleted { get; }         
        */

        // Fields        
        //internal Exception          _exception;
        internal bool               m_IsCompleted;
        internal bool               m_IsWrite;
        internal int                m_NumRead;
        internal object             m_StateObject;
        internal ManualResetEvent   m_WaitHandle;

        // Methods
        internal SynchronousAsyncResult(object asyncStateObject, bool isWrite)
        {
            m_StateObject = asyncStateObject;
            m_IsWrite = isWrite;
            m_WaitHandle = new ManualResetEvent(false);
        }

        // Properties
        public object AsyncState
        {
            get { return m_StateObject; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return this.m_WaitHandle; }
        }

        public bool CompletedSynchronously
        {
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return this.m_IsCompleted; }
        }

        internal bool IsWrite
        {
            get { return m_IsWrite; }
        }
    }
}
