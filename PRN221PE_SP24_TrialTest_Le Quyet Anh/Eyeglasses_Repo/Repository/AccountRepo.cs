using Eyeglasses_Repo.DbContext2024;
using Eyeglasses_Repo.Models;

namespace Eyeglasses_Repo.Repository
{
    public class AccountRepo : BaseRepository<StoreAccount, int>
    {
        public AccountRepo(Eyeglasses2024DBContext context) : base(context)
        {
        }
    }
}
