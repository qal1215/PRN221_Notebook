using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AccountRepo _accountRepo;

        public IndexModel(ILogger<IndexModel> logger, AccountRepo accountRepo)
        {
            _logger = logger;
            _accountRepo = accountRepo;
        }

        public void OnGet()
        {

        }

        public bool IsError { get; set; }

        [BindProperty]
        public Account? Account { get; set; }
        public int Role { get; private set; }

        public async Task<IActionResult> OnPost([FromForm] Account account)
        {
            if (account is null || !ModelState.IsValid)
            {
                ViewData["Error"] = "You do not have permission to do this function!";
                IsError = true;
                return Page();
            }

            var email = account.Email;
            var password = account.Password;
            var user = await _accountRepo.FindFirst(a => a.AccountPassword.Equals(password.Trim()) && a.EmailAddress!.ToLower().Equals(email.ToLower().Trim()));

            if (user is null)
            {
                ViewData["Error"] = "You do not have permission to do this function!";
                IsError = true;
                return Page();
            }

            if (user.Role != 1 && user.Role != 2)
            {
                ViewData["Error"] = "You do not have permission to do this function!";
                IsError = true;
                return Page();
            }

            Role = user.Role!.Value;
            Response.Cookies.Append("Role", Role.ToString());
            return Redirect("/eyeglasses");
        }
    }

    public class Account
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
