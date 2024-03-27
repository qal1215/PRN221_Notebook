using Eyeglasses_Repo.Models;
using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class DeleteModel : PageModel
    {
        private readonly EyeglassesRepo _eyeglassesRepo;

        public DeleteModel(EyeglassesRepo eyeglassesRepo)
        {
            _eyeglassesRepo = eyeglassesRepo;
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleId = Request.Cookies["Role"];
            if (roleId is null) return Redirect("/");
            if (id == null || _eyeglassesRepo.DbSet == null)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _eyeglassesRepo.DbSet == null)
            {
                return NotFound();
            }
            var eyeglass = await _eyeglassesRepo.DbSet.FindAsync(id);

            if (eyeglass != null)
            {
                Eyeglass = eyeglass;
                _eyeglassesRepo.DbSet.Remove(Eyeglass);
                await _eyeglassesRepo.DbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
