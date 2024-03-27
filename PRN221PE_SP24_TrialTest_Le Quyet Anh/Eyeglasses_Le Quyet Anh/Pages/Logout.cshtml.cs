using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages
{
    public class LogoutModel : PageModel
    {
        public LogoutModel()
        {

        }

        public void OnGet()
        {
            if (Request.Cookies["Role"] != null)
            {
                Response.Cookies.Delete("Role");
            }
            Response.Redirect("/");
        }

        //public IActionResult OnGet()
        //{

        //    if (Request.Cookies["Role"] != null)
        //    {
        //        Response.Cookies.Delete("Role");
        //    }

        //    return Redirect("/");

        //}
    }
}
