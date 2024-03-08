using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class DetailsModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepository;

        public DetailsModel(EyeglassesRepository eyeglassesRepository)
        {
            _eyeglassesRepository = eyeglassesRepository;
        }

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
    }
}
