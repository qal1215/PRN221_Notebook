using Core;
using Core.Entity;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class ProjectRepository : GenericRepository<Project>,
        IProjectRepository
    {
        public ProjectRepository(ProjectDBContext context, ILogger logger)
            : base(context, logger)
        {
        }
    }
}
