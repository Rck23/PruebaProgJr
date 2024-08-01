using Core.Entities;

namespace Core.Interfaces
{
    public interface IFailRepository : IGenericRepository<Fail>
    {
        Task<IEnumerable<Fail>> GetFirstFails();
    }
}
