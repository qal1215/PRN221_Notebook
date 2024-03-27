using Eyeglasses_Repo.Models;
using Eyeglasses_Repo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class CreateModel : PageModel
    {
        private readonly EyeglassesRepo _eyeglassesRepo;

        public CreateModel(EyeglassesRepo eyeglassesRepo)
        {
            _eyeglassesRepo = eyeglassesRepo;
        }

        public IActionResult OnGet()
        {
            var roleId = Request.Cookies["Role"];
            if (roleId is null) return Redirect("/");
            ViewData["LensTypeId"] = new SelectList(_eyeglassesRepo.DbContext.LensTypes, "LensTypeId", "LensTypeId");
            return Page();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _eyeglassesRepo.DbSet == null || Eyeglass == null)
            {
                return Page();
            }

            _eyeglassesRepo.DbSet.Add(Eyeglass);
            await _eyeglassesRepo.DbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
