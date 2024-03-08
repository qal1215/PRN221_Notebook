namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IProjectRepository Projects { get; }
        public Task<int> CompletedAsync();
    }
}
