using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;

namespace Eyeglasses.Repository
{
    public class LensTypeRepository : GenericRepository<LensType, string>
    {
        public LensTypeRepository(Eyeglasses2024DbContext context) : base(context)
        {
        }
    }
}
