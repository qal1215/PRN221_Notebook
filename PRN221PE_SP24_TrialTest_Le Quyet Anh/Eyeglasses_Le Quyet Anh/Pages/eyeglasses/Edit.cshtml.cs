using Eyeglasses_Repo.Models;
using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class EditModel : PageModel
    {
        private readonly EyeglassesRepo _eyeglassesRepo;

        public EditModel(EyeglassesRepo eyeglassesRepo)
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
            Eyeglass = eyeglass;
            ViewData["LensTypeId"] = new SelectList(_eyeglassesRepo.DbContext.LensTypes, "LensTypeId", "LensTypeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _eyeglassesRepo.DbContext.Attach(Eyeglass).State = EntityState.Modified;

            try
            {
                await _eyeglassesRepo.DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeglassExists(Eyeglass.EyeglassesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EyeglassExists(int id)
        {
            return (_eyeglassesRepo.DbContext.Eyeglasses?.Any(e => e.EyeglassesId == id)).GetValueOrDefault();
        }
    }
}
