

using System;

namespace XMPPProtocol.exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() : base()
        {
        }

        public RegisterException(string msg) : base(msg)
        {

        }
    }
}
