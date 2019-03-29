using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMVVM.Models;
using System.Threading.Tasks;

namespace RazorPagesMVVM.Pages.Peliculas
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesMVVM.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPagesMVVM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pelicula Pelicula { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pelicula = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == id);

            if (Pelicula == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
