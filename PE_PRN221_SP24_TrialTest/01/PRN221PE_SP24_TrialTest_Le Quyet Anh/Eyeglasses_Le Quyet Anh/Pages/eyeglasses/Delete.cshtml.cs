using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class DeleteModel : PageModel
    {
        private readonly Eyeglasses.DAO.DbContext2024.Eyeglasses2024DbContext _context;

        public DeleteModel(Eyeglasses.DAO.DbContext2024.Eyeglasses2024DbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyeglass = await _context.Eyeglasses.FirstOrDefaultAsync(m => m.EyeglassesId == id);

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

            var eyeglass = await _context.Eyeglasses.FindAsync(id);
            if (eyeglass != null)
            {
                Eyeglass = eyeglass;
                _context.Eyeglasses.Remove(Eyeglass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
