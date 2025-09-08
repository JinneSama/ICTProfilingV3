namespace Helpers.Interfaces
{
    public interface ISingleInstance
    {
        bool IsSingleInstance();
        void ReleaseInstance();
        void ShowDuplicateInstanceWarning();
    }
}
