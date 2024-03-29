﻿using Sustainsys.Saml2.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Sustainsys.Saml2
{

    /// <summary>
    /// Crypto description for a Managed implementation of SHA256 signatures.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHA")]
	public abstract class ManagedRSASignatureDescription : SignatureDescription
	{
		public abstract string HashAlgorithm { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ManagedRSASignatureDescription()
        {
            KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            DigestAlgorithm = typeof(SHA256Managed).FullName;
        }

        /// <summary>
        /// Create a deformatter
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Deformatter</returns>
        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var df = new RSAPKCS1SignatureDeformatter(key);
			df.SetHashAlgorithm(HashAlgorithm);
            return df;
        }

        /// <summary>
        /// Create a formatter
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Formatter</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
            Justification ="No way from here find out when the created RSACRyptoServiceProvider is done. Have to leave it to GC/Finalizer.")]
        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var provider = ((RSA)key).GetSha256EnabledRSACryptoServiceProvider();

            var formatter = new RSAPKCS1SignatureFormatter(provider);
			formatter.SetHashAlgorithm(HashAlgorithm);
            return formatter;
        }
    }

	public class ManagedRSASHA256SignatureDescription : ManagedRSASignatureDescription
	{
		public override string HashAlgorithm => "sha256";
	}

	public class ManagedRSASHA384SignatureDescription : ManagedRSASignatureDescription
	{
		public override string HashAlgorithm => "sha384";
	}

	public class ManagedRSASHA512SignatureDescription : ManagedRSASignatureDescription
	{
		public override string HashAlgorithm => "sha512";
	}
}
