using Core;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDBContext _context;
        private readonly ILogger _logger;
        public IProjectRepository ProjectRepository { get; private set; }

        public IProjectRepository Projects => throw new NotImplementedException();

        public UnitOfWork(ProjectDBContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");
            ProjectRepository = new ProjectRepository(_context, _logger);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();

        }
    }
}
