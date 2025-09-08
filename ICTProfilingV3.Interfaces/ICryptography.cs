namespace ICTProfilingV3.Interfaces
{
    public interface ICryptography
    {
        string Encrypt(string encryptString, string _EncryptionKey);
        string Decrypt(string cipherText, string _EncryptionKey);
    }
}
