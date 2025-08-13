using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Utility.Security;
using System;

namespace ICTProfilingV3.Utility
{
    public class EncryptFile : IEncryptFile
    {
        EncryptionData IEncryptFile.EncryptFile(string filename)
        {
            var securityStamp = Guid.NewGuid().ToString();
            return new EncryptionData(Cryptography.Encrypt(filename, securityStamp), securityStamp);
        }
    }
}
