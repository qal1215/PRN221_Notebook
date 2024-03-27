using Eyeglasses_Repo.DbContext2024;
using Eyeglasses_Repo.Models;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_Repo.Repository
{
    public class EyeglassesRepo : BaseRepository<Eyeglass, int>
    {
        public EyeglassesRepo(Eyeglasses2024DBContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<Eyeglass>> GetPagination(int page, int pageSize = 4)
        {
            return await _dbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
        }
    }
}
