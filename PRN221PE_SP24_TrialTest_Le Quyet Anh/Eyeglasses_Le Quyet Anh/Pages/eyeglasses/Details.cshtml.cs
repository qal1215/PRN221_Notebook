using Eyeglasses_Repo.Models;
using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class DetailsModel : PageModel
    {
        private readonly EyeglassesRepo _eyeglassesRepo;

        public DetailsModel(EyeglassesRepo eyeglassesRepo)
        {
            _eyeglassesRepo = eyeglassesRepo;
        }

        public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleId = Request.Cookies["Role"];
            if (roleId is null) return Redirect("/");
            if (id == null || _eyeglassesRepo.DbContext.Eyeglasses == null)
            {
                return NotFound();
            }

            var eyeglass = await _eyeglassesRepo.DbSet.FirstOrDefaultAsync(m => m.EyeglassesId == id);
            if (eyeglass == null)
            {
                return NotFound();
            }
            else
            {
                Eyeglass = eyeglass;
            }
            return Page();
        }
    }
}
