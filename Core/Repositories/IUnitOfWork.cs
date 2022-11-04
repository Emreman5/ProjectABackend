namespace Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    int Commit();
    Task<int> CommitAsync();
}