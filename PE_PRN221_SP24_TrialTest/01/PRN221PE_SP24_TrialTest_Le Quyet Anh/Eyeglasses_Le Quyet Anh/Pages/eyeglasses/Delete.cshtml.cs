using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class DeleteModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepository;

        public DeleteModel(EyeglassesRepository eyeglassesRepository)
        {
            _eyeglassesRepository = eyeglassesRepository;
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
            else
            {
                Eyeglass = eyeglass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyeglass = await _eyeglassesRepository.GetById(id.Value);
            if (eyeglass != null)
            {
                Eyeglass = eyeglass;
                _eyeglassesRepository.Delete(Eyeglass);
                await _eyeglassesRepository.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
