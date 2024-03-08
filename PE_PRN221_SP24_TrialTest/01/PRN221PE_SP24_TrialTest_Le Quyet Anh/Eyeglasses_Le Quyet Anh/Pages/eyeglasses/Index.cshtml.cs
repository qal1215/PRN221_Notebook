﻿using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.DAO.Models;
using Eyeglasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_Le_Quyet_Anh.Pages.eyeglasses
{
    public class IndexModel : PageModel
    {
        private readonly EyeglassesRepository _eyeglassesRepo;

        public int PageIndex = 1;

        public int TotalPage = 0;

        public IndexModel(Eyeglasses2024DbContext context)
        {
            _eyeglassesRepo = new EyeglassesRepository(context);
        }

        public IList<Eyeglass> Eyeglass { get; set; } = default!;

        public async Task OnGetAsync([FromQuery] int page)
        {
            if (page <= 0) page = 1;
            PageIndex = page;
            var totalEyeglasses = (await _eyeglassesRepo.GetAll()).Count();
            TotalPage = totalEyeglasses % 4 < 0 ? totalEyeglasses / 4 : (totalEyeglasses / 4) + 1;
            Eyeglass = await _eyeglassesRepo.GetPaginedEyeglasses(PageIndex);
        }
    }
}
