using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class CreateModel : PageModel
    {
        private readonly Eyeglasses.DAO.DbContext2024.Eyeglasses2024DbContext _context;

        public CreateModel(Eyeglasses.DAO.DbContext2024.Eyeglasses2024DbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LensTypeId"] = new SelectList(_context.LensTypes, "LensTypeId", "LensTypeId");
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

            _context.Eyeglasses.Add(Eyeglass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
