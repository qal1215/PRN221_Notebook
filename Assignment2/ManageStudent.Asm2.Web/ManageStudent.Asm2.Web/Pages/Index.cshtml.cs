using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageStudent.Asm2.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                @ViewData["error"] = "Password or username incorrect!";
            }
            else
            {
                Response.Redirect("/Index");
            }
        }
    }
}
