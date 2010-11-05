

//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

using System;

namespace XMPPProtocol.net.dns
{
	/// <summary>
	/// Thrown when the server does not respond
	/// </summary>	
	public class NoResponseException : SystemException
	{
		public NoResponseException()
		{
			// no implementation
		}

		public NoResponseException(Exception innerException) :  base(null, innerException) 
		{
			// no implementation
		}

		public NoResponseException(string message, Exception innerException) : base (message, innerException)
		{
			// no implementation
		}
        
        //protected NoResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //    // no implementation
        //}
	}
}