using ManageStudent.Asm2.Repo;
using ManageStudent.Asm2.Repo.Data;
using ManageStudent.Asm2.Repo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageStudent.Asm2.Web.Pages.ManageAccount
{
    public class IndexModel : PageModel
    {
        private readonly GenericRepository<Account> _accountRepo;
        private readonly RazorPageContext _context;

        public IndexModel()
        {
            _context = new RazorPageContext();
            _accountRepo = new GenericRepository<Account>(_context);
        }

        public IList<Account> Accounts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Accounts = (await _accountRepo.Get()).ToList();
        }
    }
}
