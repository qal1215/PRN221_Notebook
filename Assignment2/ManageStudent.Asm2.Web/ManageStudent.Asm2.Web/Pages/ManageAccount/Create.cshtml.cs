using ManageStudent.Asm2.Repo;
using ManageStudent.Asm2.Repo.Data;
using ManageStudent.Asm2.Repo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageStudent.Asm2.Web.Pages.ManageAccount
{
    public class CreateModel : PageModel
    {
        private readonly GenericRepository<Account> _accountRepo;
        private readonly RazorPageContext _context;

        public CreateModel()
        {
            _context = new RazorPageContext();
            _accountRepo = new GenericRepository<Account>(_context);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _accountRepo.InsertAsync(Account);

            return RedirectToPage("./Index");
        }
    }
}
