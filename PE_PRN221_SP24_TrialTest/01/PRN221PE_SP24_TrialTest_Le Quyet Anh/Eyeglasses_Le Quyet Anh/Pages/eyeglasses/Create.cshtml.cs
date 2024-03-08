using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class CreateModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepository;
        private readonly LensTypeRepository _lensTypeRepository;

        public CreateModel(EyeglassesRepository eyeglassesRepository, LensTypeRepository lensTypeRepository)
        {
            _eyeglassesRepository = eyeglassesRepository;
            _lensTypeRepository = lensTypeRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["LensTypeId"] = new SelectList(await _lensTypeRepository.GetAll(), "LensTypeId", "LensTypeId");
            return Page();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _eyeglassesRepository.Insert(Eyeglass);
            await _eyeglassesRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
