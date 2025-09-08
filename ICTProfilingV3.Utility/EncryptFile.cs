using ICTProfilingV3.Interfaces;
using System;

namespace ICTProfilingV3.Utility
{
    public class EncryptFile : IEncryptFile
    {
        private readonly ICryptography _cryptography;
        public EncryptFile(ICryptography cryptography)
        {
            _cryptography = cryptography;
        }
        EncryptionData IEncryptFile.EncryptFile(string filename)
        {
            var securityStamp = Guid.NewGuid().ToString();
            return new EncryptionData(_cryptography.Encrypt(filename, securityStamp), securityStamp);
        }
    }
}
