using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class IndexModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepo;

        public int PageIndex = 1;

        public int TotalPage = 0;

        public IndexModel(Eyeglasses2024DbContext context)
        {
            _eyeglassesRepo = new EyeglassesRepository(context);
        }

        public IList<Eyeglass> Eyeglass { get; set; } = default!;

        public int Role { get; set; }

        public async Task<IActionResult> Logout()
        {

            if (Request.Cookies["Role"] != null)
            {
                Response.Cookies.Delete("Role");
            }

            return RedirectToPage("/index");

        }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page)
        {
            var roleId = Request.Cookies["Role"];
            if (roleId is null) return RedirectToPage("/");
            Role = int.Parse(roleId);

            if (page <= 0) page = 1;
            PageIndex = page;
            var totalEyeglasses = (await _eyeglassesRepo.GetAll()).Count();
            TotalPage = totalEyeglasses % 4 < 0 ? totalEyeglasses / 4 : (totalEyeglasses / 4) + 1;
            Eyeglass = await _eyeglassesRepo.GetPaginedEyeglasses(PageIndex);
            return Page();
        }
    }
}
