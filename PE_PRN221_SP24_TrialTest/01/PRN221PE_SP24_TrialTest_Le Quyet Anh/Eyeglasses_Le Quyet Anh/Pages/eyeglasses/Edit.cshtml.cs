using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class EditModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepository;
        private readonly LensTypeRepository _lensTypeRepository;

        public EditModel(EyeglassesRepository eyeglassesRepository, LensTypeRepository lensTypeRepository)
        {
            _eyeglassesRepository = eyeglassesRepository;
            _lensTypeRepository = lensTypeRepository;
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyeglass = await _eyeglassesRepository.GetById(id.Value);
            if (eyeglass == null)
            {
                return NotFound();
            }
            Eyeglass = eyeglass;
            ViewData["LensTypeId"] = new SelectList(await _lensTypeRepository.GetAll(), "LensTypeId", "LensTypeId");
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

            try
            {
                await _eyeglassesRepository.UpdateWithoutTracking(Eyeglass);
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
            return _eyeglassesRepository.GetById(id) is not null;
        }
    }
}
