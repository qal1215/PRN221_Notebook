using Eyeglasses_Repo.Models;
using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class IndexModel : PageModel
    {
        private readonly EyeglassesRepo _eyeglassesRepo;

        public IndexModel(EyeglassesRepo eyeglassesRepo)
        {
            _eyeglassesRepo = eyeglassesRepo;
        }

        public int PageIndex = 1;

        public int TotalPage = 0;

        public int Role { get; set; }

        public IList<Eyeglass> Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync([FromQuery] int page)
        {
            var roleId = Request.Cookies["Role"];
            if (roleId is null) return Redirect("/");
            Role = int.Parse(roleId);

            if (page <= 0) page = 1;
            PageIndex = page;
            var totalEyeglasses = (await _eyeglassesRepo.GetAll()).Count();
            TotalPage = totalEyeglasses % 4 < 0 ? totalEyeglasses / 4 : (totalEyeglasses / 4) + 1;
            Eyeglass = await _eyeglassesRepo.GetPagination(PageIndex);
            return Page();
        }


    }
}
