using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses.Repository
{
    public class EyeglassesRepository : GenericRepository<Eyeglass, int>
    {
        public EyeglassesRepository(Eyeglasses2024DbContext context) : base(context)
        {
        }

        public async Task<IList<Eyeglass>> GetPaginedEyeglasses(int page, int pageSize = 4)
        {
            return await _context.Eyeglasses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.LensType)
                .ToListAsync();
        }
    }
}
