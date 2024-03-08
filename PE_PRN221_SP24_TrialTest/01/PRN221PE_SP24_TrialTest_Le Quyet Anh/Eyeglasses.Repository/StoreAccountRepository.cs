using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;

namespace Eyeglasses.Repository
{
    public class StoreAccountRepository : GenericRepository<StoreAccount, int>
    {
        public StoreAccountRepository(Eyeglasses2024DbContext context) : base(context)
        {
        }
    }
}
