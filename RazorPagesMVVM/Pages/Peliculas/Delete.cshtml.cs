using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMVVM.Models;
using System.Threading.Tasks;

namespace RazorPagesMVVM.Pages.Peliculas
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMVVM.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesMVVM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pelicula = await _context.Peliculas.FindAsync(id);

            if (Pelicula != null)
            {
                _context.Peliculas.Remove(Pelicula);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
