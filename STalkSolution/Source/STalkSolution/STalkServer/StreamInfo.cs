﻿using System;
using System.Collections.Generic;
using System.Text;
using XMPPProtocol.Xml.Dom;

namespace STalkServer
{
    class StreamInfo
    {
        private Node m_Node = null;
        private SocketConnection m_Client = null;

        public StreamInfo(Node node, SocketConnection client)
        {
            m_Node = node;
            m_Client = client;
        }

        public Node Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }

        public SocketConnection Client
        {
            get { return m_Client; }
            set { m_Client = value; }
        }
    }
}
