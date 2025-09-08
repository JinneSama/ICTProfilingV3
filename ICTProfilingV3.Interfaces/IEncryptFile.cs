namespace ICTProfilingV3.Interfaces
{
    public interface IEncryptFile
    {
        EncryptionData EncryptFile(string filename); 
    }

    public struct EncryptionData
    {
        public string filename;
        public string securityStamp;
        public EncryptionData(string _filename, string _securutyStamp)
        {
            filename = _filename;
            securityStamp = _securutyStamp; 
        }
    }
}
