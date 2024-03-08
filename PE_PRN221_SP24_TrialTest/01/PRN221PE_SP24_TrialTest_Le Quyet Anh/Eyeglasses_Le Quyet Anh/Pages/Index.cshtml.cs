using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StoreAccountRepository _accountRepo;

        public bool IsError { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsAuthen { get; set; }

        public int Role { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Eyeglasses2024DbContext context = new();
            _accountRepo = new StoreAccountRepository(context);
        }

        public IActionResult OnGet()
        {
            if (IsAuthen && IsSuccess)
            {
                return Redirect("/eyeglasses");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] string email, [FromForm] string password)
        {
            var account = await _accountRepo.GetWithPredecate(x => x.EmailAddress == email && x.AccountPassword == password);
            if (account is null)
            {
                ViewData["Error"] = "Inccorect username or password";
                IsError = true;
                return Page();
            }

            int role = account.Role!.Value;
            if (role == 3 || role == 4)
            {
                ViewData["Error"] = "You do not have permission to access this page";
                IsError = true;
                return Page();
            }


            Role = account.Role!.Value;
            IsSuccess = true;
            IsAuthen = true;
            return Redirect("/eyeglasses");
        }
    }
}
