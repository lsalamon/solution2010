

#if BCCRYPTO
using System;

using Org.BouncyCastle.Crypto.Tls;

namespace XMPPProtocol.net
{
    internal class CertificateVerifier : ICertificateVerifyer
    {
        internal event BaseSocket.CertificateValidationCallback OnVerifyCertificate;
        #region ICertificateVerifyer Members
        
        public bool IsValid(Org.BouncyCastle.Asn1.X509.X509CertificateStructure[] certs)
        {
            if (OnVerifyCertificate != null)
                return OnVerifyCertificate(certs);
            else           
                return true;
        }

        #endregion
    }
}
#endif