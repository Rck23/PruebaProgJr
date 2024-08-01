namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IFailRepository fails { get; }
        Task<int> SaveAsync();
    }
}
