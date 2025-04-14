using System.Threading.Tasks;

namespace Infrastructure.Cleaner
{
    public interface ILogCleaner
    {
        Task CleanLogs();
    }
}
