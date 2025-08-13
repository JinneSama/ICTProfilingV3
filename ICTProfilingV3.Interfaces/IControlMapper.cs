using System.Windows.Forms;

namespace ICTProfilingV3.Interfaces
{
    public interface IControlMapper<T> where T : class
    {
        void MapControl(T entity, params Control[] parent);
        void MapToEntity(T entity, params Control[] parent);
    }
}
